namespace ApiFoto.Domain.Settings.Rols
{
    public class RolsSettings
    {
        public List<Rol> Rols { get; set; }
    }

    public class Rol
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
