using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using order.flow.bootstraper.configurations.constants;
using order.flow.bootstraper.configurations.security;
using order.flow.crosscutting.infraestructure.tokenConfig;

namespace order.flow.bootstraper.configurations.auth;

public static class AuthConfiguration
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        var signConfiguration = new SignConfigurationToken();
        services.AddSingleton(signConfiguration);

        var tokenConfigure = new TokenConfiguration();

        new ConfigureFromConfigurationOptions<TokenConfiguration>(
                configuration.GetSection(nameof(TokenConfiguration)))
            .Configure(tokenConfigure);

        services.AddSingleton(tokenConfigure);

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(bearrerOptions =>
            {
                var paramsValidation = bearrerOptions.TokenValidationParameters;
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ValidateActor = true;
                paramsValidation.ValidateAudience = true;
                paramsValidation.ValidAudience = tokenConfigure.Audience;
                paramsValidation.ValidIssuer = tokenConfigure.Issuer;
                paramsValidation.ClockSkew = TimeSpan.Zero;

                paramsValidation.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigure.SigningKey));
            });

        services.AddAuthorization(auth =>
        {
            auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build());

            auth.AddPolicy("OrderFlow", policy =>
            {
                policy.RequireAssertion(context =>
                    context.User.HasClaim(c => c.Type == Constant.ID));
            });
        });
        services.AddMemoryCache();
    }
}