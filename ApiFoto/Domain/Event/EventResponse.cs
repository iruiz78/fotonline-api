namespace ApiFoto.Domain.Event
{
    public class EventResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string BannerUrl { get; set; }
        public bool Active { get; set; }
        public decimal PhotoPrice { get; set; }
        public decimal PhotoPricePackage { get; set; }
        public int PackageQuantity { get; set; }
        public int PhotosCount { get; set; }
    }
}
