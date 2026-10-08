using IA.GymAdmin.HelpDesk.API.Domain.DTO;
using IA.GymAdmin.HelpDesk.API.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace IA.GymAdmin.HelpDesk.API.Infraestructure.Config.DBContext
{
    public partial class DBContextConfiguration : DbContext
    {
        public DBContextConfiguration() { }
        public DBContextConfiguration(DbContextOptions<DBContextConfiguration> options)
                 : base(options) { }

        public virtual DbSet<Empleado> Empleados { get; set; }
        public virtual DbSet<HD_Device> Devices { get; set; }
        public virtual DbSet<HD_Access_Remote> Access_Remotes { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<HD_Desk> HD_Desks { get; set; }
        public virtual DbSet<HD_Licensing> HD_Licensings { get; set; }
        public virtual DbSet<HDDeviceListarDTO> HDDeviceListarDTOs { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.ToTable("EMPLEADO");
                entity.HasKey(e => e.Id).HasName("PK");

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(e => e.Nombre)
                    .HasColumnName("Nombre")
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Direccion)
                    .HasColumnName("Direccion")
                    .HasMaxLength(200);

                entity.Property(e => e.Telefono)
                    .HasColumnName("Telefono")
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.clave)
                    .HasColumnName("Clave")
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Habilitado)
                    .HasColumnName("Habilitado");

                entity.Property(e => e.HoraEntrada)
                    .HasColumnName("HoraEntrada");

                entity.Property(e => e.HoraSalida)
                    .HasColumnName("HoraSalida");

                entity.Property(e => e.Rol)
                    .HasColumnName("Rol")
                    .HasMaxLength(10);

                entity.Property(e => e.HoraEntrada2)
                    .HasColumnName("HoraEntrada2");

                entity.Property(e => e.HoraSalida2)
                    .HasColumnName("HoraSalida2");

                entity.Property(e => e.AlmacenDefault)
                    .HasColumnName("AlmacenDefault")
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.RolReportes)
                    .HasColumnName("RolReportes")
                    .HasMaxLength(10);

                entity.Property(e => e.DebeCambiarPwd)
                    .HasColumnName("DebeCambiarPwd");

                // Especificamos "datetime" explícitamente para evitar que EF lo mapee por defecto a "datetime2"
                entity.Property(e => e.LastLogin)
                    .HasColumnName("LastLogin")
                    .HasColumnType("datetime");

                entity.Property(e => e.LastLogout)
                    .HasColumnName("LastLogout")
                    .HasColumnType("datetime");

                entity.Property(e => e.ExtId)
                    .HasColumnName("ExtId")
                    .HasMaxLength(50);

                entity.Property(e => e.ExtCentroCostosId)
                    .HasColumnName("ExtCentroCostosId");

                entity.Property(e => e.ExtAlmacenId)
                    .HasColumnName("ExtAlmacenId");

                entity.Property(e => e.ExtCajaId)
                    .HasColumnName("ExtCajaId");

                entity.Property(e => e.ExtTarifaId)
                    .HasColumnName("ExtTarifaId");

                entity.Property(e => e.EsVendedor)
                    .HasColumnName("EsVendedor");

                entity.Property(e => e.AccederDesde)
                    .HasColumnName("AccederDesde")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<HD_Device>(entity =>
            {
                entity.ToTable("HD_DEVICE");
                entity.HasKey(e => e.Id).HasName("Id");
                entity.Property(e=> e.name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(50);
                entity.Property(e => e.description)
                .HasColumnName("description")
                .IsRequired()
                .HasMaxLength(50);
                entity.Property(e => e.owner)
                .HasColumnName("owner")
                .IsRequired()
                .HasMaxLength(50);

                entity.HasOne(d => d.idDeviceNavigation)
                .WithMany(p => p.Devices)
                .HasForeignKey(d => d.id_employee);
           
                

            }
            );

            modelBuilder.Entity<HD_Access_Remote>(entity =>
            {
                entity.ToTable("HD_ACCESS_REMOTE");
                entity.HasKey(e => e.Id)
                .HasName("Id");
                entity.Property(e => e.name_vendor)
                .HasColumnName("name")
                .HasMaxLength(50);
                entity.Property(e => e.ip)
                .HasColumnName("ip")
                .HasMaxLength(50);
                entity.Property(e => e.password)
                .HasColumnName("password")
                .HasMaxLength(50);
                entity.Property(e => e.device_name_vendor)
                .HasColumnName("device")
                .HasMaxLength(50);
                entity.Property(e => e.route_vendor_executable)
                .HasColumnName("route_executable")
                .HasMaxLength(50);

                entity.HasOne(d => d.IdDeviceNavigation)
                .WithMany(p => p.Access_Remotes)
                .HasForeignKey(d => d.IdDevice);




            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("CLIENTE");
                entity.HasKey(e => e.Id).HasName("Id");
                entity.Property(e => e.NombreCompleto)
                .HasColumnName("NombreCompleto")
                .IsRequired()
                .HasMaxLength(151);
                entity.Property(e => e.Telefono)
                .HasColumnName("TelefonoContacto")
                .IsRequired()
                .HasMaxLength(10);
                entity.Property(e => e.NroIdentificacion)
                .HasColumnName("NroIdentificacion")
                .IsRequired()
                .HasMaxLength(15);
                entity.Property(e => e.Direccion)
                .HasColumnName("Direccion")
                .HasMaxLength(200);


            });

            modelBuilder.Entity<HD_Desk>(entity =>
            {
                entity.ToTable("HD_DESK");
                entity.HasKey(e => e.id).HasName("Id");
                entity.Property(e => e.id_contact_client)
                .HasColumnName("id_client")
                .HasColumnType("integer");
                entity.Property(e => e.id_employee)
                .HasColumnName("id_employee")
                .HasMaxLength(10);
                entity.Property(e => e.name_service)
                .HasColumnName("name_service")
                .HasMaxLength(50);
                entity.Property(e => e.description)
                .HasColumnName("description")
                .HasMaxLength(50);
                entity.Property(e => e.credit)
                .HasColumnName("credit")
                .HasMaxLength(50);

                entity.HasOne(d => d.idDeviceNavigation)
                .WithMany(p => p.HD_Desks)
                .HasForeignKey(d => d.id_employee);

            });

            modelBuilder.Entity<HD_Licensing>(entity =>
            {
                entity.ToTable("HD_LICENSING");
                entity.HasKey(e => e.id)
                .HasName("Id");
                entity.Property(e => e.id_device)
                .HasColumnName("id_device")
                .HasColumnType("integer");
                entity.Property(e => e.date_expired)
                .HasColumnName("date_expired")
                .HasColumnType("timestamp");
                entity.Property(e => e.type)
                .HasColumnName("type")
                .HasMaxLength(50);

                entity.HasOne(d => d.IdDeviceNavigation)
                .WithMany(p => p.HD_Licensings)
                .HasForeignKey(d => d.id_device);

            });


            OnModelCreatingPartial(modelBuilder);
        }




        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
