using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using order.flow.domain.entity.Base;

namespace order.flow.domain.entity.address;

[Table("tb_address")]
public class AddressEntity : BaseEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    [ForeignKey("ResaleId")]
    [Column("resale_id")]
    public Guid ResaleId { get; set; }
    [Column("address")]
    public string Address { get; set; }
    [Column("city")]
    public string City { get; set; }
    [Column("state")]
    public string State { get; set; }
    [Column("zip_code")]
    public string ZipCode { get; set; }
    [Column("country")]
    public string Country { get; set; }
    [Column("type_address")]
    public int TypeAddress { get; set; }
}