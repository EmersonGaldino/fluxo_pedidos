using System.ComponentModel.DataAnnotations;
using order.flow.api.models.Base;

namespace order.flow.api.models.order;

public class OrderViewModel : BaseViewModel
{
    [Required(ErrorMessage = "É obrigatório vincuolar a um cliente.")]
    public Guid ResaleId { get; set; }
    [Required(ErrorMessage = "É obrigatório inserir ao menos um endereco.")]
    public Guid AddressId { get; set; }
    public List<OrderItemViewModel> Items { get; set; }
}