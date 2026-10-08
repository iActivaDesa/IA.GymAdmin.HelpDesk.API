using IA.GymAdmin.HelpDesk.API.Domain.Entity;
using IA.GymAdmin.HelpDesk.API.Domain.Exceptions;
using IA.GymAdmin.HelpDesk.API.Infraestructure.Config.Reposiotires.Contrato;

namespace IA.GymAdmin.HelpDesk.API.Application.UseCases.HD_Devices.commands
{
    public class DeleteHD_Device
    {

        private readonly IGenericRepository<HD_Device> _hdDeviceRepository;

        public DeleteHD_Device(IGenericRepository<HD_Device> hdDeviceRepository)
        {
            _hdDeviceRepository = hdDeviceRepository;
        }   

        public async Task<bool> Handle(int id)
        {
            var deviceToDelete = await _hdDeviceRepository.Obtener(x => x.Id == id);
            if (deviceToDelete == null)
                throw new NotFoundException("Dispositivo no encontrado");
            
            var result = await _hdDeviceRepository.Eliminar(deviceToDelete);
            if (!result)
                throw new Exception("Error al eliminar el dispositivo");
            return result;
        }

    }
}
