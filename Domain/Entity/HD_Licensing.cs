namespace IA.GymAdmin.HelpDesk.API.Domain.Entity
{
    public partial class HD_Licensing
    {
        public int? id { get; set; }
        public int? id_device { get; set; }
        public DateTime date_expired { get; set; }
        public string? type { get; set; }

        public virtual HD_Device? IdDeviceNavigation { get; set; }
    }
}
