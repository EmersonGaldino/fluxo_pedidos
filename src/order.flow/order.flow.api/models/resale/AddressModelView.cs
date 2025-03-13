using System.ComponentModel.DataAnnotations.Schema;

namespace order.flow.api.models.resale;

public class AddressModelView
{
    [NotMapped]
    public Guid Id { get; set; }
    public Guid ResaleId { get; set; }
    public string Value { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    public int TypeAddress { get; set; }
}