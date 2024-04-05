using System.Reflection;

namespace BlazorTLang;

internal class CultureProxy : DispatchProxy
{
    private ICulturePack? _currentImplementation;

    internal void SetImplementation(ICulturePack? implementation)
    {
        _currentImplementation = implementation;
    }
    
    protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
    {
        if (_currentImplementation == null)
            throw new InvalidOperationException("No implementation is set.");
            
        return targetMethod?.Invoke(_currentImplementation, args);
    }

    internal static T Create<T>(T? initialImplementation) where T : class, ICulturePack
    {
        object proxy = Create<T, CultureProxy>();
        ((CultureProxy)proxy).SetImplementation(initialImplementation);
        return (T)proxy;
    }
}