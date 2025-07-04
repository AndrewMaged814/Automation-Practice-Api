namespace BaladGateway.Utils;

public static class ConfigurationsHelper
{
    public static Configurations GetConfigurations()
    {
        var env = Environment.GetEnvironmentVariable("ENVIRONMENT");
        if (string.IsNullOrEmpty(env)) env = "testing"; 

        var configurationsFile = Path.Combine(Directory.GetCurrentDirectory(), $"configurations-{env}.json");
        return new JsonConverter().FromJson<Configurations>(File.ReadAllText(configurationsFile));
    }
}
public record Configurations(
    string GatewayBaseUrl,
    string DashboardApiPrefix,
    string OperationsApiPrefix,
    string OperationsDashboardUrl,
    string DashboardUrl,
    string TenantTokenEndpoint,
    string OperationsApiScope,
    string DashboardApiScope,
    List<Client> Clients,
    Client OperationsClient
);


