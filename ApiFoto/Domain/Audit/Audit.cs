namespace ApiFoto.Domain
{
    public class Audit
    {
        public DateTime CreatedDate { get; set; }
        public int UserCreatedId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int UserModifiedId { get; set; }
    }
}
