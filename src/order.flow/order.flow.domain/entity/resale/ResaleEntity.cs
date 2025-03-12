using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using order.flow.domain.entity.address;
using order.flow.domain.entity.Base;
using order.flow.domain.entity.phone;

namespace order.flow.domain.entity.resale;

[Table("tb_resale")]
public class ResaleEntity : BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    public string Document { get; set; }
    public string SocialReason { get; set; }
    public string FantasyName { get; set; }
    public string Email { get; set; }
    public IList<PhoneEntity> Phones { get; set; }
    public IList<AddressEntity> AddressDelevery { get; set; } 
}