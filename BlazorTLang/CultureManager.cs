using System.Globalization;

namespace BlazorTLang;

public sealed class CultureManager<T> where T : class, ICulturePack
{
    public IEnumerable<T> CulturePacks => _culturePacks.Values;
    
    private readonly Dictionary<string, T> _culturePacks = new(StringComparer.InvariantCultureIgnoreCase);
    private readonly CultureProxy _cultureProxy;
    
    internal CultureManager(IEnumerable<Type> culturePackTypes, IServiceProvider provider)
    {
        foreach (var culturePackType in culturePackTypes)
        {
            if (provider.GetService(culturePackType) is not T culturePack)
                continue;

            _culturePacks[culturePack.CultureCode] = culturePack;
        }

        _cultureProxy = (CultureProxy)provider.GetService(typeof(T))!;
    }
    
    private CultureInfo? _culture;
    public CultureInfo? Culture
    {
        get => _culture;
        set
        {
            if (Equals(_culture, value)) 
                return;

            if (!_culturePacks.TryGetValue(value?.Name ?? "en-US", out var culturePack))
                return; //TODO Log that no culture was found?
            
            _culture = value;
            CultureInfo.DefaultThreadCurrentCulture = _culture;
            CultureInfo.DefaultThreadCurrentUICulture = _culture;
            _cultureProxy.SetImplementation(culturePack);
            OnCultureChanged();
        }
    }
    
    public event EventHandler? CultureChanged; 
    private void OnCultureChanged() => CultureChanged?.Invoke(this, EventArgs.Empty);
}