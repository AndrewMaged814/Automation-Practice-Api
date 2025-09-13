using System;
using System.IO;
using Common.Utils;

namespace AutomationPractice_ui_api.Utils;

public static class ConfigurationsHelper
{
    public static Configurations GetConfigurations()
    {
        var env = Environment.GetEnvironmentVariable("ENVIRONMENT");
        if (string.IsNullOrEmpty(env)) env = "testing"; 

        var configurationsFile = Path.Combine(AppContext.BaseDirectory, $"configurations-{env}.json");

        Console.WriteLine($"Loading config from: {configurationsFile}");

        return new JsonConverter().FromJson<Configurations>(File.ReadAllText(configurationsFile));
    }
}

public record Configurations(
    string BaseUrl
);


