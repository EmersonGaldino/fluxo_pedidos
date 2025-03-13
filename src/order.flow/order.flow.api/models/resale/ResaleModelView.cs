using System.ComponentModel.DataAnnotations;
using order.flow.utils.validation;

namespace order.flow.api.models.resale;

public class ResaleModelView
{
    public string Id { get; set; }
    [CpfCnpjValidation]
    public string Document { get; set; }
    [Required(ErrorMessage = "O razão social é obrigatório.")]
    public string SocialReason { get; set; }
    public string FantasyName { get; set; }
    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "O nome de contato é obrigatório.")]
    public string Contactname { get; set; }
    public IList<PhoneModelView> Phones { get; set; }
    [Required(ErrorMessage = "É obrigatório inserir ao menos um endereco.")]
    public IList<AddressModelView> AddressDelevery { get; set; } 
}