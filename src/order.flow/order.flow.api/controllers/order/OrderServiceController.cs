using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using order.flow.api.controllers.Base;
using order.flow.api.models.Base;
using order.flow.api.models.order;
using order.flow.domain.entity.order;
using Swashbuckle.AspNetCore.Annotations;

namespace order.flow.api.controllers.order;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class OrderServiceController: ApiBaseController
{
    [HttpPost]
    [SwaggerOperation(Summary = "recebe pedidos",
        Description = "Aqui receberemos todos os pedidos e devolveremos um pedidoId.")]
    [SwaggerResponse(200, "Sucesso.", typeof(SuccessResponse<>))]
    [SwaggerResponse(400, "Dados salvo com sucesso", typeof(BadResponse))]
    [SwaggerResponse(500, "Erro interno no servidor.", typeof(BadResponse))]
    public async Task<IActionResult> Post([FromBody] List<OrderCountEntity> model)=> await EventResult(async () =>
        new BaseModelView<object>
        {
            Message = "Pedido recebido com sucesso.", 
            Data =new
            {
                Order = RandonNumberOrder()
            }
        });

    private int RandonNumberOrder()
    {
        Random random = new Random();
        int randomNumber = random.Next(100, 1000); 
        return randomNumber;
    }
}