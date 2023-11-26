namespace ApiFoto.Domain.User
{
    public class UserRequestUpdate
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int RolId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int UserModifiedId { get; set; }
    }
}
