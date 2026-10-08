using IA.GymAdmin.HelpDesk.API.Domain.Entity;
using IA.GymAdmin.HelpDesk.API.Domain.Exceptions;
using IA.GymAdmin.HelpDesk.API.Infraestructure.Config.Reposiotires.Contrato;

namespace IA.GymAdmin.HelpDesk.API.Application.UseCases.HD_Devices.queries
{
    public class GetByNameHD_Device
    {


        private readonly IGenericRepository<HD_Device> _hdDeviceRepository;

        public GetByNameHD_Device(IGenericRepository<HD_Device> hdDeviceRepository)
        {
            _hdDeviceRepository = hdDeviceRepository;
        }

        public async Task<HD_Device> Handle(string name)
        {
            var device = await _hdDeviceRepository.Obtener(x => x.name == name);
            if (device == null)
                throw new NotFoundException("Dispositivo no encontrado");
            return device;
        }
    }
}
