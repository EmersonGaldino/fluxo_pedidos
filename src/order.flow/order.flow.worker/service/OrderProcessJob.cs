using System.Net;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using order.flow.domain.entity.order;
using order.flow.domain.Interface.order;
using order.flow.utils.cache.memory;
using order.flow.utils.enums;
using order.flow.utils.service.htpp;
using Quartz;

namespace order.flow.worker.service;

public class OrderProcessJob(
    ILogger<OrderProcessJob> logger,
    IServiceScopeFactory serviceScopeFactory)
    : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var orderItemService = scope.ServiceProvider.GetRequiredService<IOrderItemService>();
        var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
        var memoryCache = scope.ServiceProvider.GetRequiredService<LocalCacheService>();

        var result = await orderItemService.GetAllAsyncCount();
        if (result.Select(x => x.TotalQuantity).Sum() >= 180)
        {
            logger.LogInformation(">>> Enviando dados para o servico <<<<");

            try
            {
                await CallServiceSendOrder(result);

            }
            catch (Exception e)
            {
                memoryCache.Set("ReprocessOrder", result, 5);
            }

            foreach (var order in result)
            {
                await orderService.UpdateStatusAsync(order.OrderId);
            }
        }

        logger.LogInformation(">>> Processo de envio finalizado <<<");

        logger.LogInformation("Executando job em: {time}", DateTimeOffset.Now);
    }

    private async Task<HttpStatusCode> CallServiceSendOrder(List<OrderCountEntity> orders)
    {
        logger.LogInformation(">>> Enviando dados para o servico <<<< {order}", orders);
        using var scope = serviceScopeFactory.CreateScope();
        var webRequestService = scope.ServiceProvider.GetRequiredService<IWebRequest>();
        var response = await webRequestService.RequestJsonSerialize<object>(
            "https://localhost:7011/api/OrderService",
            orders,
            ETypeMethods.POST);
        logger.LogInformation("Devolucao da api -> {response}", response);
        return HttpStatusCode.OK;
    }
}