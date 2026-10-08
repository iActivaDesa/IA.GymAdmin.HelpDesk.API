using IA.GymAdmin.HelpDesk.API.Domain.DTO;
using IA.GymAdmin.HelpDesk.API.Domain.Entity;

namespace IA.GymAdmin.HelpDesk.API.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerarToken(Empleado empleado);
        Task<Empleado> ValidarUsuario(AuthDTO authDTO);
    }
}
