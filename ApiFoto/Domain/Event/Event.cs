using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiFoto.Domain.Event
{
    [Table("Events")]
    public class Event : Audit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        [Description("ignore")]
        public string? BannerUrl { get; set; }
        public bool Active { get; set; }
        public decimal PhotoPrice { get; set; }
        public decimal PhotoPricePackage { get; set; }
        public int PackageQuantity { get; set; }
    }
}
