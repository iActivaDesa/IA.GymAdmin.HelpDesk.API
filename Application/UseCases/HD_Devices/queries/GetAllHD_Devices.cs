using IA.GymAdmin.HelpDesk.API.Application.UseCases.HD_Devices.constanst;
using IA.GymAdmin.HelpDesk.API.Domain.DTO;
using IA.GymAdmin.HelpDesk.API.Domain.Entity;
using IA.GymAdmin.HelpDesk.API.Infraestructure.Config.Reposiotires.Contrato;
using System.Data.Entity;

namespace IA.GymAdmin.HelpDesk.API.Application.UseCases.HD_Devices.queries
{
    public class GetAllHD_Devices
    {

        private readonly IGenericRepository<HDDeviceListarDTO> _hdDeviceRepository;
        private readonly constants _constants = new constants();

        public GetAllHD_Devices(IGenericRepository<HDDeviceListarDTO> hdDeviceRepository)
        {
            _hdDeviceRepository = hdDeviceRepository;
        }

        public async Task<List<HDDeviceListarDTO>> Handle()
        {
            var splistall = await _hdDeviceRepository.EjecutarSPListAsync(_constants.spGetAllHD_Devices);

            List<HDDeviceListarDTO> lista = splistall.Select(
                item => new HDDeviceListarDTO
                {
                    id = item.id,
                    nombre_dispositivo = item.nombre_dispositivo,
                    descripcion = item.descripcion,
                    nombre_empleado = item.nombre_empleado,
                    propietario = item.propietario  
                }
                ).ToList();


            return lista;
        }   

    }
}
