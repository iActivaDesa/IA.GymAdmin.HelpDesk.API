namespace IA.GymAdmin.HelpDesk.API.Domain.DTO
{
    public class HD_Access_RemoteDTO
    {
        public string? name_vendor { get; set; }
        public string? ip { get; set; }
        public string? password { get; set; }

        public string? device_name_vendor { get; set; }
        public string? route_vendor_executable { get; set; }
    }
}
