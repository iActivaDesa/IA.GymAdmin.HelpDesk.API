
using IA.GymAdmin.HelpDesk.API.Domain.DTO;
using IA.GymAdmin.HelpDesk.API.Domain.Entity;
using IA.GymAdmin.HelpDesk.API.Domain.Exceptions;
using IA.GymAdmin.HelpDesk.API.Domain.Interfaces;
using IA.GymAdmin.HelpDesk.API.Infraestructure.Config.Reposiotires.Contrato;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using IA.GymAdmin.HelpDesk.API.Utils;

namespace IA.GymAdmin.HelpDesk.API.Application
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<Empleado> _empleadoRepository;
        private readonly IConfiguration _configuration;
        private readonly Desencriptar _desencriptar = new Desencriptar();
        public AuthService(IGenericRepository<Empleado> empleadoRepository,  IConfiguration configuration ) 
        {
            _empleadoRepository = empleadoRepository;
            _configuration = configuration;
        }


        public async Task<string> GenerarToken(Empleado empleado)
        {
            return await Task.Run(() =>
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, empleado.Id.ToString())
                };

                var jwtKey = _configuration["Jwt:Key"]
                    ?? throw new InvalidOperationException("La clave jwt no esta disponible.");
                var issuer = _configuration["Jwt:Issuer"];
                var audience = _configuration["Jwt:Audience"];
                var expirationMinutes = int.TryParse(_configuration["Jwt:ExpirationMinutes"], out var minutes) ? minutes : 60;

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                    signingCredentials: creds

                );


                return new JwtSecurityTokenHandler().WriteToken(token);

            });
        }

        public async Task<Empleado> ValidarUsuario(AuthDTO authDTO)
        {
            if (authDTO == null || string.IsNullOrWhiteSpace(authDTO.Username) || string.IsNullOrWhiteSpace(authDTO.Password))
            {
                throw new UnauthorizedDomainException("Credenciales Incorrectas");
            }

            var empleado = await _empleadoRepository.Obtener(
                e => e.Id == authDTO.Username.Trim()
            );

            if (empleado == null)
            {
                throw new UnauthorizedDomainException("Credenciales Incorrectas");
            }

            if (!empleado.Habilitado.HasValue || !empleado.Habilitado.Value)
            {
                throw new UnauthorizedDomainException("Usuario no habilitado");
            }

            if (string.IsNullOrEmpty(empleado.clave))
            {
                throw new UnauthorizedDomainException("Credenciales Incorrectas");
            }

            var passwordValidate = await _desencriptar.DesencriptarPassword(empleado.clave);

            if (passwordValidate != authDTO.Password)
            {
                throw new UnauthorizedDomainException("Credenciales Incorrectas");
            }

            return empleado;
        }

        
    }
}
