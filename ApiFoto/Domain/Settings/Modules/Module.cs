namespace ApiFoto.Domain.Settings.Modules
{
    public class ModulesSettings
    {
        public List<Module> Modules { get; set; }
    }

    public class Module : Audit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
    }
}
