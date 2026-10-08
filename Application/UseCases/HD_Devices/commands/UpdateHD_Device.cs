using AutoMapper;
using IA.GymAdmin.HelpDesk.API.Domain.DTO;
using IA.GymAdmin.HelpDesk.API.Domain.Entity;
using IA.GymAdmin.HelpDesk.API.Domain.Exceptions;
using IA.GymAdmin.HelpDesk.API.Infraestructure.Config.Reposiotires.Contrato;

namespace IA.GymAdmin.HelpDesk.API.Application.UseCases.HD_Devices.commands
{
    public class UpdateHD_Device 
    {

        private readonly IGenericRepository<HD_Device> _hdDeviceRepository;
        private readonly IMapper _mapper;


        public UpdateHD_Device(IGenericRepository<HD_Device> hdDeviceRepository, IMapper mapper)
        {
            _hdDeviceRepository = hdDeviceRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(HD_DeviceDTO modelo)
        {
            var deviceToUpdate = await _hdDeviceRepository.Obtener(x => x.Id == modelo.Id);
            if (deviceToUpdate == null)
                throw new NotFoundException("Dispositivo no encontrado");
            deviceToUpdate.name = modelo.name;
            deviceToUpdate.description = modelo.description;
            deviceToUpdate.owner = modelo.owner;
            var updatedDevice = await _hdDeviceRepository.Editar(deviceToUpdate);
            if (!updatedDevice )
                throw new OperationFailedException("Error al actualizar el dispositivo");
            return updatedDevice;
        }
    }
}
