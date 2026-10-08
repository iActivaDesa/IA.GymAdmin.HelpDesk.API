using IA.GymAdmin.HelpDesk.API.Domain.DTO;
using IA.GymAdmin.HelpDesk.API.Domain.Entity;
using System.Linq.Expressions;

namespace IA.GymAdmin.HelpDesk.API.Domain.Interfaces
{
    public interface IHD_Device
    {
        Task<HD_Device> Crear(HD_DeviceDTO modelo, string userId);
        Task<List<HDDeviceListarDTO>> Listar();
        Task<HD_Device> BuscarPorId(int id);
        Task<HD_Device> BuscarPorNombre(string filtro);
        Task<bool> Editar(HD_DeviceDTO modelo);
        Task<bool> Eliminart(int id);


    }
}
