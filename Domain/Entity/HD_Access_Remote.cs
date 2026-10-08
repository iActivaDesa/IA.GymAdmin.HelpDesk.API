namespace IA.GymAdmin.HelpDesk.API.Domain.Entity
{
    public partial class HD_Access_Remote
    {
        public int? Id { get; set; }

        public int? IdDevice { get; set; }
        public string? name_vendor { get; set; }
        public string? ip { get; set; }
        public string? password { get; set; }

        public string? device_name_vendor { get; set; }
        public string? route_vendor_executable { get; set; }

        public virtual HD_Device? IdDeviceNavigation { get; set; }
    }
}
