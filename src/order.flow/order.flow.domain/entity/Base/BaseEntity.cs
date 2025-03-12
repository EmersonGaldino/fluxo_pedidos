using System.Runtime.InteropServices.JavaScript;

namespace order.flow.domain.entity.Base;

public class BaseEntity
{
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
    public bool Active { get; set; }
}