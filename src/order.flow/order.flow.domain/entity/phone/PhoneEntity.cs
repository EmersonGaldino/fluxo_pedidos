using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using order.flow.domain.entity.Base;

namespace order.flow.domain.entity.phone;

[Table("tb_phone")]
public class PhoneEntity : BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Phone { get; set; }
    public string TypePhone { get; set; }
}