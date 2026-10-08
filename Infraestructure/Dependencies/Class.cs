using IA.GymAdmin.HelpDesk.API.Application;
using IA.GymAdmin.HelpDesk.API.Application.UseCases.HD_Devices.commands;
using IA.GymAdmin.HelpDesk.API.Application.UseCases.HD_Devices.queries;
using IA.GymAdmin.HelpDesk.API.Domain.Interfaces;
using IA.GymAdmin.HelpDesk.API.Infraestructure.Config.DBContext;
using IA.GymAdmin.HelpDesk.API.Infraestructure.Config.Reposiotires;
using IA.GymAdmin.HelpDesk.API.Infraestructure.Config.Reposiotires.Contrato;
using Microsoft.EntityFrameworkCore;

namespace IA.GymAdmin.HelpDesk.API.Infraestructure.Dependencies
{
    public static class Dependencies
    {
        public static void InjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DBContextConfiguration>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAuthService, AuthService>();
            services.AddAutoMapper(cfg => { }, typeof(AutomapperProfile));


            services.AddScoped<GetAllHD_Devices>();
            services.AddScoped<GetByIdHD_Device>();
            services.AddScoped<GetByNameHD_Device>();
            services.AddScoped<CreateHD_Device>();
            services.AddScoped<DeleteHD_Device>();
            services.AddScoped<UpdateHD_Device>();

            services.AddScoped<IHD_Device, HD_DeviceService>();
        }
            

    }
}
