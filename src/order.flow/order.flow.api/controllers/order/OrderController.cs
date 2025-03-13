using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using order.flow.api.controllers.Base;
using order.flow.api.models.Base;
using order.flow.api.models.order;
using order.flow.api.models.resale;
using order.flow.application.Interface.order;
using order.flow.domain.entity.order;
using order.flow.domain.entity.resale;
using Swashbuckle.AspNetCore.Annotations;

namespace order.flow.api.controllers.order;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class OrderController : ApiBaseController
{
    private IOrderAppService orderAppService => GetService<IOrderAppService>();
    private IMapper mapper => GetService<IMapper>();
    
    
    [HttpPost]
    [SwaggerOperation(Summary = "Cria um pedido",
        Description = "Aqui criamos o pedido em nossa base.")]
    [SwaggerResponse(200, "Sucesso.", typeof(SuccessResponse<>))]
    [SwaggerResponse(400, "Dados salvo com sucesso", typeof(BadResponse))]
    [SwaggerResponse(500, "Erro interno no servidor.", typeof(BadResponse))]
    public async Task<IActionResult> Post([FromBody] OrderViewModel model)=> await EventResult(async () =>
        new BaseModelView<object>
        {
            Message = "Order created success", 
            Data = await orderAppService.Post(mapper.Map<OrderEntity>(model))
        });
}