using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using order.flow.application.Interface.address;
using order.flow.application.Interface.order;
using order.flow.application.Interface.phone;
using order.flow.application.Interface.resale;
using order.flow.application.service.address;
using order.flow.application.service.order;
using order.flow.application.service.phone;
using order.flow.application.service.resale;
using order.flow.bootstraper.filters;
using order.flow.crosscutting.infraestructure.Base;
using order.flow.domain.Interface.address;
using order.flow.domain.Interface.order;
using order.flow.domain.Interface.phone;
using order.flow.domain.Interface.resale;
using order.flow.domain.repository.Interface.address;
using order.flow.domain.repository.Interface.order;
using order.flow.domain.repository.Interface.phone;
using order.flow.domain.repository.Interface.resale;
using order.flow.domain.service.address;
using order.flow.domain.service.order;
using order.flow.domain.service.phone;
using order.flow.domain.service.resale;
using order.flow.persistence.configuration.uow;
using order.flow.persistence.repositories.address;
using order.flow.persistence.repositories.order;
using order.flow.persistence.repositories.phone;
using order.flow.persistence.repositories.resale;
using order.flow.utils.shared;
using order.flow.worker.service;

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
        services.AddScoped<IResaleAppService, ResaleAppService>();
        services.AddScoped<IAddressAppService, AddressAppService>();
        services.AddScoped<IOrderAppService, OrderAppService>();
        services.AddScoped<IPhoneAppService, PhoneAppService>();
        #endregion
       
        #region .::Service
        services.AddScoped<IResaleService, ResaleService>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IPhoneService, PhoneService>();
        services.AddScoped<IOrderItemService, OrderItemService>();
        #endregion
        
        #region .::Repository
        services.AddScoped<IResaleRepository, ResaleRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IPhoneRepository, PhoneRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        #endregion

        services.AddHostedService<WorkerSendItensService>();
      
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