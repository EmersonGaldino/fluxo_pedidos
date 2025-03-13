using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using order.flow.domain.entity.Base;

namespace order.flow.domain.entity.order;

[Table("tb_order")]
public class OrderEntity : BaseEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    [Column("status")]
    public char Status { get; set; }
    [Column("resale_id")]
    public Guid ResaleId { get; set; }
    [Column("address_id")]
    public Guid AddressId { get; set; }
    [Column("phone_id")]
    public Guid PhoneId { get; set; }
    public List<OrderItemEntity> Items { get; set; }
}