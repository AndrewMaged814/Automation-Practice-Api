using System.Collections.Frozen;
using System.Net;
using System.Text.Json;
using BaladGateway.Models;
using BaladGateway.StepDefinitions.ClientAPI.Link.Lookups;
using BaladGateway.StepDefinitions.ClientAPI.Money.Lookups;
using Common.Encryption;
using Common.Extensions;
using Common.Logger;
using Serilog;
using Xunit.Abstractions;

namespace BaladGateway.Utils;

[Binding]
public sealed class Hooks(ScenarioContext scenarioContext, ITestOutputHelper outputHelper)
{
    private readonly ILogger _logger = LoggerFactory.GetLogger(outputHelper);
    private static Client? _defaultClient;

    public static FrozenDictionary<string, Client> MultipleClients { get; private set; }
        = new List<(string, Client)>().ToFrozenDictionary(c => c.Item1, c => c.Item2);

    [BeforeTestRun]
    public static void BeforeAll()
    {
        var configurations = ConfigurationsHelper.GetConfigurations();
        Constants.BASE_URL = configurations!.GatewayBaseUrl;

        InitializeClients(configurations);
        InitializeAdminUser(configurations);
        InitializeEncryptionIfEnabled(_defaultClient!);

        BaladLinkLookupsTest.LoadLookups(_defaultClient!);
        BaladMoneyLookupsTest.LoadLookups(_defaultClient!);
    }

    [BeforeTestRun]
    public static async Task StartWebhookListener()
    {
        var server = new NgrokWebhookServer();
        await server.StartAsync();
        Console.WriteLine($"Server running at: {server.PublicUrl}");
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        Skip.IfNot(ClientHasAccessToModule(scenarioContext), "Client doesn't have access to module");
        Skip.IfNot(ClientIsAuthorized(scenarioContext),
            $"Client is not authorized for module: {GetModuleFromTags(scenarioContext)}");
        Skip.If(ScenarioBDCAndAdapterIsNotBDC(scenarioContext), "Adapter is not BDC");
        Skip.If(ScenarioIsProduction(scenarioContext), "Production scenario skipped");

        if (ScenarioHasMultipleClients(scenarioContext)) return;
        scenarioContext.SetContextClient(_defaultClient!);
        LogClientDetails(_defaultClient!);
        TokenDebugger.DebugClient(MultipleClients, outputHelper);
    }

    [AfterTestRun]
    public static void StopWebhookListener()
    {
        var server = new NgrokWebhookServer();
        if (server != null)
        {
            server.Stop();
            Console.WriteLine("[Webhook Listener] Stopped successfully.");
        }
    }

    [AfterScenario]
    public void AfterScenario()
    {
        if (scenarioContext.TestError is not { } ex) return;

        LogFailureDetails(ex);
        LogStatusCode();
        LogResponseBody();
    }

    private void LogClientDetails(Client client)
    {
        _logger.LogDebug("""
                         Assigned client:
                         - Name       : {Name}
                         - Partner ID : {PartnerId}
                         - Credentials:
                             - ClientId : {ClientId}
                             - Secret   : {ClientSecret}
                         """,
            client.Name, client.PartnerId, client.ApiCredentials.ClientId, client.ApiCredentials.ClientSecret
        );
    }

    private void LogFailureDetails(Exception ex)
    {
        _logger.LogError("""
                         🚨 Scenario FAILED: {Scenario}
                         ❌ Exception: {ExceptionType}
                         ❌ Message  : {Message}
                         """,
            scenarioContext.ScenarioInfo.Title,
            ex.GetType().Name,
            ex.Message
        );
    }

    private void LogStatusCode()
    {
        if (!scenarioContext.ContainsKey(ScenarioContextKeys.ResponseStatusCodeKey)) return;

        var statusCode = scenarioContext.Get<HttpStatusCode>(ScenarioContextKeys.ResponseStatusCodeKey);
        _logger.LogError("📦 Status Code: {StatusName} ({StatusCode})", statusCode, (int)statusCode);
    }

    private void LogResponseBody()
    {
        if (!scenarioContext.ContainsKey(ScenarioContextKeys.ResponseKey)) return;

        var response = scenarioContext.Get<object>(ScenarioContextKeys.ResponseKey);
        try
        {
            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
            _logger.LogError("📦 Response (JSON):\n{Json}", json);

            var wrapper = JsonSerializer.Deserialize<ErrorWrapper>(json);
            var firstError = wrapper?.Errors?.FirstOrDefault();
            if (firstError != null)
                _logger.LogError("🧨 Error Detail -> {@Error}", firstError);
        }
        catch (Exception ex)
        {
            _logger.LogWarning("⚠️ Could not format response JSON: {Error}", ex.Message);
            _logger.LogError("📦 Last Raw Response:\n{@Raw}", response);
        }
    }

    private static bool ScenarioIsProduction(ScenarioContext ctx)
    {
        var env = Environment.GetEnvironmentVariable("ENVIRONMENT") ?? "testing";
        return ctx.ScenarioInfo.CombinedTags.Contains("NOT_ON_PRODUCTION")
               && env.Equals("production", StringComparison.OrdinalIgnoreCase);
    }

    private static bool ClientIsAuthorized(ScenarioContext ctx)
    {
        var module = GetModuleFromTags(ctx);
        if (module == null) return true;
        return module switch
        {
            "ClientAPI" => _defaultClient?.ApiCredentials.GetToken() != null,
            "Dashboard" => _defaultClient?.DashboardCredentials.Token != null,
            "Operations" => Constants.ADMIN_TOKEN != null,
            "Cross" => _defaultClient?.ApiCredentials.GetToken() != null
                       && _defaultClient?.DashboardCredentials.Token != null
                       && Constants.ADMIN_TOKEN != null,
            _ => false
        };
    }

    private static string? GetModuleFromTags(ScenarioContext ctx)
    {
        var tags = ctx.ScenarioInfo.CombinedTags;
        return tags.FirstOrDefault(t =>
            t.Equals("clientapi", StringComparison.OrdinalIgnoreCase) ||
            t.Equals("dashboard", StringComparison.OrdinalIgnoreCase) ||
            t.Equals("operations", StringComparison.OrdinalIgnoreCase) ||
            t.Equals("Cross", StringComparison.OrdinalIgnoreCase)
        );
    }

    private static void InitializeClients(Configurations cfg)
    {
        if (cfg?.Clients is null || cfg.Clients.Count == 0)
            throw new Exception("Clients configurations do not exist");

        var results = new List<(string, Client)>();
        for (var i = 0; i < cfg.Clients.Count; i++)
        {
            var client = cfg.Clients[i];
            var result = ((i + 1).ToString(), InitializeClientsToken(client, cfg));
            results.Add(result);
            Thread.Sleep(1000);
        }

        MultipleClients = results.ToFrozenDictionary(t => t.Item1, t => t.Item2);

        _defaultClient = MultipleClients.First().Value;
    }

    private static void InitializeAdminUser(Configurations cfg)
    {
        if (cfg?.OperationsClient is null)
            throw new Exception("Operations client missing");
        try
        {
            DashboardCredentials adminCredentials = new(cfg.OperationsClient.DashboardCredentials.Email,
                cfg.OperationsClient.DashboardCredentials.Password,
                cfg.OperationsClient.DashboardCredentials.AuthType);

            Constants.ADMIN_TOKEN = adminCredentials.InitToken(
                cfg.TenantTokenEndpoint,
                cfg.OperationsApiScope,
                cfg.OperationsClient.ClientId,
                cfg.OperationsClient.ClientSecret
            );
        }
        catch
        {
        }
    }

    private static Client InitializeClientsToken(Client client, Configurations cfg)
    {
        var nc = client with
        {
            ApiCredentials = new ApiCredentials(
                client.ClientId,
                client.ClientSecret,
                Models.ClientAPI.Constants.IdentityEndpoint
            )
        };

        try
        {
            nc.ApiCredentials.GetToken();
            nc.DashboardCredentials.InitToken(
                cfg.TenantTokenEndpoint,
                cfg.DashboardApiScope,
                client.ClientId,
                client.ClientSecret
            );
        }
        catch
        {
        }

        return nc;
    }

    private static void InitializeEncryptionIfEnabled(Client client)
    {
        if (client.ClientEncryption.Enabled)
            new EncryptionSecrets().SetAESKeys(client.ClientEncryption.EncryptionKey);
    }

    private static bool ScenarioHasMultipleClients(ScenarioContext ctx)
    {
        return ctx.ScenarioInfo.Tags.Contains("MultipleClients");
    }

    private static bool ClientHasAccessToModule(ScenarioContext ctx)
    {
        return ctx.ScenarioInfo.CombinedTags
            .Select(t => t.ToLowerInvariant())
            .Intersect(_defaultClient!.Modules.Select(m => m.ToLowerInvariant()))
            .Any();
    }

    private static bool ScenarioBDCAndAdapterIsNotBDC(ScenarioContext ctx)
    {
        var adapter = Environment.GetEnvironmentVariable("ADAPTER") ?? _defaultClient?.Adapter;
        var isBDCScenario = ctx.ScenarioInfo.CombinedTags
            .Contains(Models.ClientAPI.Constants.BDCBankCode);
        var isBDCAdapter = adapter != null
                           && adapter.Equals(Models.ClientAPI.Constants.BDCBankCode,
                               StringComparison.OrdinalIgnoreCase);
        return isBDCScenario && !isBDCAdapter;
    }
}