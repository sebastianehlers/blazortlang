using BlazorTLang;

namespace Client.Localization;

public interface ILanguage: ICulturePack
{
    string Hello { get; }
    string WelcomeToYourApp { get; }
    string Ok { get; }
    string About { get; }
    string Home { get; }
    string Counter { get; }
    string Weather { get; }
}