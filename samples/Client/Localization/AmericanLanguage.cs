namespace Client.Localization;

public class AmericanLanguage : ILanguage
{
    public string DisplayName => "English (US)";
    public string CultureCode => "en-US";
    public string Hello => "Hello";
    public string WelcomeToYourApp => "Welcome to your app";
    public string Ok => "Ok";
    public string About => "About";
    public string Home => "Home";
    public string Counter => "Counter";
    public string Weather => "Weather";
}