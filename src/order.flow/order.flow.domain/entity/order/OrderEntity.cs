using System.ComponentModel.DataAnnotations.Schema;
using order.flow.domain.entity.Base;

namespace order.flow.domain.entity.order;

[Table("tb_order")]
public class OrderEntity : BaseEntity
{
    public Guid Id { get; set; }
    public char Status { get; set; }
    public Guid ResaleId { get; set; }
    public Guid AddressId { get; set; }
    public Guid PhoneId { get; set; }
}