namespace AutomationPractice_ui_api.Utils;

public static class ScenarioContextExtensions
{
    public static Client GetContextClient(this ScenarioContext context)
    {
        return context.Get<Client>(ScenarioContextKeys.ContextClientKey);
    }

    public static void SetContextClient(this ScenarioContext context, Client value)
    {
        context[ScenarioContextKeys.ContextClientKey] = value;
    }

    public static void SetValue<T>(this ScenarioContext context, string key, T value)
    {
        context[key] = value;
    }

    public static T GetValue<T>(this ScenarioContext context, string key)
    {
        return context.Get<T>(key);
    }
}