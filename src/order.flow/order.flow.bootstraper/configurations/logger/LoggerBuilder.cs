using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace order.flow.bootstraper.configurations.logger;

public class LoggerBuilder
{
    public static void ConfigureLogging()
    {
        var environment =  "appsettings.json";

        var configuration = new ConfigurationBuilder()
            .AddJsonFile(environment, optional: false, reloadOnChange: true)
            .AddJsonFile(
                environment,
                optional: true)
            .Build();

        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithMachineName()
            .WriteTo.Debug()
            .WriteTo.Console()
            // .WriteTo.Elasticsearch(ConfigureElasticSink(configuration))
            .Enrich.WithProperty("Environment", environment)
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

    }
    private static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration) =>
        new(new Uri(configuration["KibanaConfiguration:Uri"]))
        {
            AutoRegisterTemplate = true,
            IndexFormat = configuration["KibanaConfiguration:ApplicationName"]
        };
}