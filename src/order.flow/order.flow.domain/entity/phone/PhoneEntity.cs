using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using order.flow.domain.entity.Base;

namespace order.flow.domain.entity.phone;

[Table("tb_phone")]
public class PhoneEntity : BaseEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    [Column("phone")]
    public string Phone { get; set; }
    [Column("typephone")]
    public string TypePhone { get; set; }
}