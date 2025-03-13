using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using order.flow.domain.entity.Base;

namespace order.flow.domain.entity.order;

[Table("tb_orderItem")]
public class OrderItemEntity : BaseEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    [Column("orderid")]
    public Guid OrderId { get; set; }
    [Column("description")]
    public string Description { get; set; }
    [Column("quantity")]
    public int Quantity { get; set; }
}