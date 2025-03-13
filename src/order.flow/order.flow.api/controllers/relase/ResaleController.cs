using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using order.flow.api.controllers.Base;
using order.flow.api.models.Base;
using order.flow.api.models.resale;
using order.flow.application.Interface.resale;
using order.flow.domain.entity.resale;
using Swashbuckle.AspNetCore.Annotations;

namespace order.flow.api.controllers.relase;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class ResaleController : ApiBaseController
{
    private IResaleAppService resaleAppService => GetService<IResaleAppService>();
    private IMapper mapper => GetService<IMapper>();

    [HttpGet]
    [SwaggerOperation(Summary = "Busca todos so representantes",
        Description = "Aqui traremos todos os representantes que temos em nossa base.")]
    [SwaggerResponse(200, "Sucesso.", typeof(SuccessResponse<>))]
    [SwaggerResponse(400, "Não localiamos dados na nossa base", typeof(BadResponse))]
    [SwaggerResponse(500, "Erro interno no servidor.", typeof(BadResponse))]
    public async Task<IActionResult> Get()=> await EventResult(async () =>
        new BaseModelView<object>
        {
            Message = "Loading all resale success", 
            Data = mapper.Map<List<ResaleModelView>>(await resaleAppService.Get())
        });
    
    [HttpPost]
    [SwaggerOperation(Summary = "Cria um representantes",
        Description = "Aqui criamosu um representante em nossa base.")]
    [SwaggerResponse(200, "Sucesso.", typeof(SuccessResponse<>))]
    [SwaggerResponse(400, "Dados salvo com sucesso", typeof(BadResponse))]
    [SwaggerResponse(500, "Erro interno no servidor.", typeof(BadResponse))]
    public async Task<IActionResult> Post([FromBody] ResaleModelView model)=> await EventResult(async () =>
        new BaseModelView<object>
        {
            Message = "Resale save with success", 
            Data = mapper.Map<ResaleModelView>(await resaleAppService.Post(mapper.Map<ResaleEntity>(model)))
        });
    
}