namespace IA.GymAdmin.HelpDesk.API.Domain.Entity
{
    public partial class HD_Device
    {
        public int? Id { get; set; }
        public string? id_employee { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public string? owner { get; set; }

        public virtual Empleado? idDeviceNavigation { get; set; }

        public virtual ICollection<HD_Access_Remote> Access_Remotes { get; set; } = new List<HD_Access_Remote>();

        public virtual ICollection<HD_Licensing> HD_Licensings { get; set; } = new List<HD_Licensing>();
       
    }
}
