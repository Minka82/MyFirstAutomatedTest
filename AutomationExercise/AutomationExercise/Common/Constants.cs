namespace AutomationExercise.Common;

public static class Constants
{
    public const string AutomationExerciseWebsiteUrl = "https://automationexercise.com/";

    public static TimeSpan ExplicitWaitInSeconds = TimeSpan.FromSeconds(10);

    public static TimeSpan ImplicitWaitInSeconds = TimeSpan.FromSeconds(10);

    public static string ConsentXPath = "//p[text()='Consent'] | //button[contains(text(),'Consent')]";

    public const int ConsentOverlayWaitTimeInMs = 1000;

    public const string MaximizedWindow = "--start-maximized";
}
