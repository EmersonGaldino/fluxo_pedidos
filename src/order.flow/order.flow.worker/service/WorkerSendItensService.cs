using Cronos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using order.flow.domain.Interface.order;

namespace order.flow.worker.service;

public class WorkerSendItensService: IHostedService
{
    private readonly ILogger<WorkerSendItensService> _logger;
    private readonly CronExpression _cronExpression;
    private DateTimeOffset? _nextRun;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public WorkerSendItensService(
        ILogger<WorkerSendItensService> logger,
        IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _cronExpression = CronExpression.Parse("*/5 * * * *");

    }

    public async Task StartAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _nextRun = _cronExpression.GetNextOccurrence(DateTimeOffset.Now, TimeZoneInfo.Local);

            if (_nextRun.HasValue)
            {
                var delay = _nextRun.Value - DateTimeOffset.Now;
                using var scope = _serviceScopeFactory.CreateScope();
                var orderItemService = scope.ServiceProvider.GetRequiredService<IOrderItemService>();
                var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
                
                var result = await orderItemService.GetAllAsyncCount();
                if (result.Select(x => x.TotalQuantity).Sum() == 120)
                {
                    _logger.LogInformation("Próxima chamaria o servico: {time}", _nextRun.Value);

                    foreach (var order in result)
                    {
                        await orderService.UpdateSatatusAsync(order.OrderId);

                    }
                }

                _logger.LogInformation("Próxima execução do Worker em: {time}", _nextRun.Value);

                if (delay > TimeSpan.Zero)
                {
                    await Task.Delay(delay, stoppingToken);
                }
            }

            _logger.LogInformation("Executando job em: {time}", DateTimeOffset.Now);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}