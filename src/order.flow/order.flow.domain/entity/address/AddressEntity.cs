using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using order.flow.domain.entity.Base;

namespace order.flow.domain.entity.address;

[Table("tb_address")]
public class AddressEntity : BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    [ForeignKey("id")]
    public Guid ResaleId { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    public int TypeAddress { get; set; }
}