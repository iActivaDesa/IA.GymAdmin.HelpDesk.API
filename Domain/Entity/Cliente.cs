namespace IA.GymAdmin.HelpDesk.API.Domain.Entity
{
    public partial class Cliente
    {
        public String Id { get; set; }
        public String? NombreCompleto { get; set; }
        public String? NroIdentificacion { get; set; }
        public String? Direccion { get; set; }
        public String? Telefono { get; set; }

    }
}
