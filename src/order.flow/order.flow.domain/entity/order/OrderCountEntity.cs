using System.ComponentModel.DataAnnotations.Schema;

namespace order.flow.domain.entity.order;

public class OrderCountEntity
{
    [Column("orderid")]
    public Guid OrderId { get; set; }
    [Column("total_quantity")]
    public int TotalQuantity { get; set; }
}