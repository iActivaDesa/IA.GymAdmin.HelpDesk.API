using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace IA.GymAdmin.HelpDesk.API.Domain.Entity
{
    public partial class HD_Desk
    {

        public int? id { get; set; }
        public int? id_contact_client { get; set; }
        public String? id_employee { get; set; }
        public string? name_service { get; set; }
        public string? description { get; set; }
        public string? credit { get; set; }
        public virtual Empleado? idDeviceNavigation { get; set; }

        public virtual Cliente? idContactNavigation { get; set; }

    }
}
