using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using order.flow.bootstraper.filters;
using order.flow.crosscutting.infraestructure.Base;
using order.flow.persistence.configuration.uow;
using order.flow.utils.shared;

namespace order.flow.bootstraper.configurations.dependencyInjection;

public static class DependencyInjectionsExtension
{
  
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();
        #region .::Include injection configurations estructure
        var infraConfig = new InfrastructureBaseConfig();
        new ConfigureFromConfigurationOptions<InfrastructureBaseConfig>(
                configuration.GetSection("Infrastructure"))
            .Configure(infraConfig);
        services.AddSingleton(infraConfig);
        #endregion
        
        #region .:: Configuration filter performace
    
        services.AddTransient<PerformaceFilters>();
        services.AddMvc(options => options.Filters.AddService<PerformaceFilters>())
            .AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true)
            .SetCompatibilityVersion(CompatibilityVersion.Latest);
        #endregion

        services.AddScoped<IConnectionPostgres, UnitOfWorkPostgres>(x
            => new UnitOfWorkPostgres(infraConfig.DataBase.ConnectionString));
        
        #region .::AppService
        // services.AddScoped<IRouteAppService, RouteAppService>();
        
        #endregion
        
        #region .::Service

        #endregion
        
        #region .::Repository
        
        #endregion
        
        
        return services;

    }
    private static void RegistrarInterfaces(IServiceCollection services, Type typeBase, string containsInNamespace,
        string sulfix)
    {
        var types = typeBase
            .Assembly
            .GetTypes()
            .Where(type => !string.IsNullOrEmpty(type.Namespace) &&
                           type.Namespace.Contains(containsInNamespace) &&
                           type.Name.EndsWith(sulfix) &&
                           !type.IsGenericType &&
                           type.IsClass &&
                           type.GetInterfaces().Any());

        foreach (var type in types)
        {
            var interfaceType = type
                .GetInterfaces()?
                .FirstOrDefault(t => t.Name == $"I{type.Name}");
            if (interfaceType == null) continue;
            services.AddScoped(interfaceType, type);
        }
    }
}