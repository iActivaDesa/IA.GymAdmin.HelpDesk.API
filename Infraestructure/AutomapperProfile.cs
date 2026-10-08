using AutoMapper;
using IA.GymAdmin.HelpDesk.API.Domain.DTO;
using IA.GymAdmin.HelpDesk.API.Domain.Entity;
using System.Globalization;

namespace IA.GymAdmin.HelpDesk.API.Infraestructure

{
    public class AutomapperProfile : Profile
    {
     
        public AutomapperProfile()
        {
            #region Auth
            CreateMap<AuthDTO, Empleado>().ReverseMap();
            #endregion

            #region
            CreateMap<TokenDTO, Empleado>().ReverseMap();
            #endregion

            #region
            CreateMap<HD_Device, HD_DeviceDTO>().ReverseMap();
            #endregion


        }

    }
}
