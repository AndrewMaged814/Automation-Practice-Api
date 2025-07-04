using Common.Interfaces;
using Common.RestMethods;
using Common.UIDriver;

namespace BaladGateway.Utils;

public record Client(
    string Name,
    string Adapter,
    string PartnerId,
    string ClientId,
    string ClientSecret,
    ApiCredentials ApiCredentials,
    DashboardCredentials DashboardCredentials,
    ClientEncryption ClientEncryption,
    string[] Modules
);

public record ClientEncryption(
    bool Enabled,
    string EncryptionKey
);
public record ApiCredentials(
    string ClientId,
    string ClientSecret,
    string Endpoint
)
{

    private string? _token;
    public string GetToken()
    {
        if (_token is not null)
            return _token;

        Dictionary<string, object> clientCredentialsParams = new()
        {
            { "grant_type", "client_credentials" },
            { "client_id", ClientId },
            { "client_secret", ClientSecret }
        };
        var httpClient = PlainRestMethods.GetInstance();
        var apiResponse = httpClient.PostWithParams<IResponse>(Endpoint, formParams: clientCredentialsParams);
        var clientCredentialsResponse = apiResponse.GetBody<ClientCredentialsAuthResponse>();

        return _token = clientCredentialsResponse.AccessToken;
    }
}

public record DashboardCredentials(
    string Email,
    string Password,
    DashboardAuthType AuthType
)
{
    private string? _token;
    public string? Token => _token;
    public string InitToken(
        string url,
        string? scope = null,
        string? clientId = null,
        string? clientSecret = null)
    {
        if (_token is not null)
        {
            return _token;
        }

        if (AuthType == DashboardAuthType.Driver)
        {
            _token = new DriverAuthToken(
                    Email,
                    Password,
                    url
                ).GetToken();
        }
        else
        {
            if (string.IsNullOrEmpty(scope))
                throw new ArgumentNullException(nameof(scope), "Scope cannot be null for ROPC authentication.");
            if (string.IsNullOrEmpty(clientId))
                throw new ArgumentNullException(nameof(clientId), "ClientId cannot be null for ROPC authentication.");
            if (string.IsNullOrEmpty(clientSecret))
                throw new ArgumentNullException(nameof(clientSecret), "ClientSecret cannot be null for ROPC authentication.");

            _token = new ROPCAuthToken(
                    Email,
                    Password,
                    clientId,
                    clientSecret,
                    url,
                    scope
                ).GetToken();
        }

        return _token;
    }
}

public record ROPCAuthToken(string Email,
    string Password,
    string ClientId,
    string ClientSecret,
    string TokenEndpoint,
    string Scopes)
{
    private string? _token;
    public string GetToken()
    {
        if (_token is not null)
        {
            return _token;
        }

        Dictionary<string, object> clientCredentialsParams = new()
        {
            { "grant_type", "password" },
            { "client_id", ClientId },
            { "client_secret", ClientSecret },
            { "scope", Scopes },
            { "username", Email },
            { "password", Password },
        };
        var httpClient = PlainRestMethods.GetInstance();
        var apiResponse = httpClient.PostWithParams<IResponse>(TokenEndpoint, formParams: clientCredentialsParams);
        var clientCredentialsResponse = apiResponse.GetBody<ClientCredentialsAuthResponse>();

        _token = clientCredentialsResponse.AccessToken;

        return _token;
    }
}

public record DriverAuthToken(
    string Email,
    string Password,
    string DashboardUrl)
{
    private string? _token;
    public string GetToken()
    {
        if (_token is not null)
        {
            return _token;
        }

        var driver = new Driver();
        driver.GetUrl(DashboardUrl);
        var MicrosoftLoginPage = new MicrosoftLoginPage(driver);
        var token = string.Empty;
        while (string.IsNullOrEmpty(token))
        {
            try
            {
                MicrosoftLoginPage.TypeInEmail(Email);
                MicrosoftLoginPage.ClickToProceed();
                MicrosoftLoginPage.TypeInPassword(Password);
                MicrosoftLoginPage.ClickToProceed();
                MicrosoftLoginPage.ClickOnAccept();
                token = driver.GetTokenFromLocalStorage();
            }
            finally
            {
                driver.TearDown();
            }
        }
        return _token = token;
    }
}

public enum DashboardAuthType
{
    ROPC = 1,
    Driver = 2
}

internal record ClientCredentialsAuthResponse(
    string AccessToken,
    int? ExpiresIn,
    int? ExtExpiresIn,
    string TokenType
    );