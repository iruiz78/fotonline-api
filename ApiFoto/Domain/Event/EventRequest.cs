using System.Text.Json.Serialization;

namespace ApiFoto.Domain.Event
{
    public class EventRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? BannerUrl { get; set; }
        public bool Active { get; set; }
        public decimal PhotoPrice { get; set; }
        public decimal PhotoPricePackage { get; set; }
        public int PackageQuantity { get; set; }
    }
}
