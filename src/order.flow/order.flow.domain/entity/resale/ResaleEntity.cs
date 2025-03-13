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
    [Column("id")]
    public Guid Id { get; set; }
    [Column("document")]
    public string Document { get; set; }
    [Column("social_reason")]
    public string SocialReason { get; set; }
    [Column("fantasy_name")]
    public string FantasyName { get; set; }
    [Column("email")]
    public string Email { get; set; }
    [Column("contact_name")]
    public string ContactName { get; set; }
    public IList<PhoneEntity> Phones { get; set; }
    public IList<AddressEntity> AddressDelevery { get; set; } 
}