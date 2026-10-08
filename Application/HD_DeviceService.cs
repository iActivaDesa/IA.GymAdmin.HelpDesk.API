using IA.GymAdmin.HelpDesk.API.Application.UseCases.HD_Devices.commands;
using IA.GymAdmin.HelpDesk.API.Application.UseCases.HD_Devices.queries;
using IA.GymAdmin.HelpDesk.API.Domain.DTO;
using IA.GymAdmin.HelpDesk.API.Domain.Entity;
using IA.GymAdmin.HelpDesk.API.Domain.Interfaces;

namespace IA.GymAdmin.HelpDesk.API.Application
{
    public class HD_DeviceService : IHD_Device
    {

        private readonly CreateHD_Device _createHD_Device;
        private readonly DeleteHD_Device _deleteHD_Device;
        private readonly UpdateHD_Device _updateHD_Device;
        private readonly GetAllHD_Devices _getAllHD_Devices;
        private readonly GetByIdHD_Device _getByIdHD_Device;
        private readonly GetByNameHD_Device _getByNameHD_Device; 


        public HD_DeviceService(
            CreateHD_Device createHD_Device, 
            DeleteHD_Device deleteHD_Device, 
            UpdateHD_Device updateHD_Device, 
            GetAllHD_Devices getAllHD_Devices, 
            GetByIdHD_Device getByIdHD_Device, 
            GetByNameHD_Device getByNameHD_Device)
        {
            _createHD_Device = createHD_Device;
            _deleteHD_Device = deleteHD_Device;
            _updateHD_Device = updateHD_Device;
            _getAllHD_Devices = getAllHD_Devices;
            _getByIdHD_Device = getByIdHD_Device;
            _getByNameHD_Device = getByNameHD_Device;
        }


        public Task<HD_Device> BuscarPorId(int id) => _getByIdHD_Device.Handle(id);

        public Task<HD_Device> BuscarPorNombre(string filtro) => _getByNameHD_Device.Handle(filtro);

        public Task<HD_Device> Crear(HD_DeviceDTO modelo, string userId) => _createHD_Device.Handle(modelo, userId);

        public Task<bool> Editar(HD_DeviceDTO modelo) => _updateHD_Device.Handle(modelo);

        public Task<bool> Eliminart(int id) => _deleteHD_Device.Handle(id);

        public Task<List<HDDeviceListarDTO>> Listar() => _getAllHD_Devices.Handle();

      
    }
}
