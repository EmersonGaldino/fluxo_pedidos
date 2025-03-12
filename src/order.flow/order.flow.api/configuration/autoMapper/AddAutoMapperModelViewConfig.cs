namespace order.flow.api.configuration.autoMapper;

public static class AddAutoMapperModelViewConfig
{
    public static void AddAutoMapperModelViewConfiguration(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));
        services.AddAutoMapper(typeof(MappingProfilesModelView), typeof(MappingProfilesModelView));
    }
}