using Microsoft.AspNetCore.Builder;

namespace order.flow.bootstraper.configurations.swagger;

public static class SwaggerConfig
{
    public static void UseSwaggerConfig(this IApplicationBuilder app)
    {
        
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Flow ");
            c.RoutePrefix = string.Empty;
        });
    }
}