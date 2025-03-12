using System.ComponentModel.DataAnnotations.Schema;
using order.flow.domain.entity.Base;

namespace order.flow.domain.entity.order;

[Table("tb_statusOrder")]
public class StatusOrder : BaseEntity
{
    public Guid Id { get; set; }
    public char Status { get; set; }
    public string Description { get; set; }
}