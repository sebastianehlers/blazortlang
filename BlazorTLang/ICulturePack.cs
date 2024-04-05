namespace BlazorTLang;

public interface ICulturePack
{
    public string DisplayName { get; }
    public string CultureCode { get; }
}