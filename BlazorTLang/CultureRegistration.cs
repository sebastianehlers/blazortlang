using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorTLang;

public static class CultureRegistration
{
    public static IServiceCollection AddBlazorTLang<T>(this IServiceCollection serviceCollection) where T : class, ICulturePack
    {
        var languagePackType = typeof(T);

        serviceCollection.AddSingleton(languagePackType, provider => CultureProxy.Create<T>(null));

        var assembly = Assembly.GetAssembly(languagePackType);
        if (assembly == null)
            throw new Exception($"Unable to get assembly for type {typeof(T)}");

        var culturePackTypes = assembly.GetTypes()
            .Where(type => typeof(T).IsAssignableFrom(type) && type is { IsInterface: false, IsAbstract: false })
            .ToArray();

        foreach (var culturePackType in culturePackTypes)
            serviceCollection.AddSingleton(culturePackType);
        
        return serviceCollection.AddSingleton(provider => new CultureManager<T>(culturePackTypes, provider));
    }
}