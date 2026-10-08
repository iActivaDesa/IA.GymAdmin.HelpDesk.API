using IA.GymAdmin.HelpDesk.API.Domain.Entity;
using IA.GymAdmin.HelpDesk.API.Domain.Exceptions;
using IA.GymAdmin.HelpDesk.API.Infraestructure.Config.Reposiotires.Contrato;

namespace IA.GymAdmin.HelpDesk.API.Application.UseCases.HD_Devices.queries
{
    public class GetByIdHD_Device
    {

        private readonly IGenericRepository<HD_Device> _hdDeviceRepository;

        public GetByIdHD_Device(IGenericRepository<HD_Device> hdDeviceRepository)
        {
            _hdDeviceRepository = hdDeviceRepository;
        }

        public async Task<HD_Device> Handle(int id)
        {
            var device = await _hdDeviceRepository.Obtener(x => x.Id == id);
            if (device == null)
                throw new NotFoundException("Dispositivo no encontrado");
            return device;
        }
    }
}
