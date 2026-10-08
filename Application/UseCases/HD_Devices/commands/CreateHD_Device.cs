using AutoMapper;
using IA.GymAdmin.HelpDesk.API.Domain.DTO;
using IA.GymAdmin.HelpDesk.API.Domain.Entity;
using IA.GymAdmin.HelpDesk.API.Infraestructure.Config.Reposiotires.Contrato;



namespace IA.GymAdmin.HelpDesk.API.Application.UseCases.HD_Devices.commands
{
    public class CreateHD_Device
    {

        private readonly IGenericRepository<HD_Device> _hdDeviceRepository;
        private readonly IMapper _mapper;

        public CreateHD_Device(IGenericRepository<HD_Device> hdDeviceRepository, IMapper mapper)
        {
            _hdDeviceRepository = hdDeviceRepository;
            _mapper = mapper;
        }

        public async Task<HD_Device> Handle(HD_DeviceDTO modelo, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new UnauthorizedAccessException("No se pudo obtener el Id del usuario desde el token JWT.");
            }

            var  hd_device_modelo = _mapper.Map<HD_Device>(modelo);
            // El dueño del dispositivo siempre es el usuario autenticado (Id del JWT).
            hd_device_modelo.id_employee = userId.Trim();
            var createdDevice = await _hdDeviceRepository.Crear(hd_device_modelo);
            if (createdDevice == null)
            {
                throw new Exception("Error al crear el dispositivo");
            }
            return _mapper.Map<HD_Device>(createdDevice);

        }






    }
}
