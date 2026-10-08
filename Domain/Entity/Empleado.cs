namespace IA.GymAdmin.HelpDesk.API.Domain.Entity
{
    public partial class Empleado
    {
        public string Id { get; set; } = string.Empty;
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? clave { get; set; } // Mantenido en minúscula según tu Fluent API
        public bool? Habilitado { get; set; }
        public TimeSpan? HoraEntrada { get; set; }
        public TimeSpan? HoraSalida { get; set; }
        public string? Rol { get; set; }
        public TimeSpan? HoraEntrada2 { get; set; }
        public TimeSpan? HoraSalida2 { get; set; }
        public string? AlmacenDefault { get; set; }
        public string? RolReportes { get; set; }
        public bool? DebeCambiarPwd { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? LastLogout { get; set; }
        public string? ExtId { get; set; }
        public int? ExtCentroCostosId { get; set; }
        public int? ExtAlmacenId { get; set; }
        public int? ExtCajaId { get; set; }
        public int? ExtTarifaId { get; set; }
        public bool? EsVendedor { get; set; }
        public string? AccederDesde { get; set; }

        public virtual ICollection<HD_Device> Devices{ get; set; } = new List<HD_Device>();

        public virtual ICollection<HD_Desk> HD_Desks { get; set; } = new List<HD_Desk>();
    }
}
