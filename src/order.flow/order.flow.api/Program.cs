using order.flow.api.configuration.autoMapper;
using order.flow.bootstraper.configurations.auth;
using order.flow.bootstraper.configurations.cors;
using order.flow.bootstraper.configurations.dependencyInjection;
using order.flow.bootstraper.configurations.logger;
using order.flow.bootstraper.configurations.security;
using order.flow.bootstraper.configurations.swagger;
using order.flow.utils.autoMapper;
using order.flow.worker.service;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var provider = services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog();
builder.Services.AddControllers();

//My injections
services.AddAutoMapperConfiguration();
services.AddAutoMapperModelViewConfiguration();

AuthConfiguration.Register(services, configuration);
LoggerBuilder.ConfigureLogging();

services.AddProtectedControllers();
services.AddCors();
services.AddSwaggerService();
services.AddServices(configuration); 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCorsConfig();
app.UseAuthorization();
app.UseAuthentication();
app.UseRouting();
app.UseSwaggerConfig();
app.UseEndpointsConfig();
app.UseHttpsRedirection();

app.Run();

