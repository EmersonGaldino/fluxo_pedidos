using System.ComponentModel.DataAnnotations.Schema;

namespace order.flow.domain.entity.address;

[Table("tb_typeAddress")]
public class TypeAddress
{
    public Guid Id { get; set; }
    public int Type { get; set; }
    public string Description { get; set; }
}