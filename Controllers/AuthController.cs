using IA.GymAdmin.HelpDesk.API.Domain.DTO;
using IA.GymAdmin.HelpDesk.API.Domain.Exceptions;
using IA.GymAdmin.HelpDesk.API.Domain.Interfaces;
using IA.GymAdmin.HelpDesk.API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IA.GymAdmin.HelpDesk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authservice;

        public AuthController(IAuthService authservice  )
        {
            _authservice = authservice;
        }

        /// <summary>
        /// Autentica un empleado y genera un token JWT.
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] AuthDTO authDTO)
        {
            try
            {
                if (authDTO == null || string.IsNullOrWhiteSpace(authDTO.Username) || string.IsNullOrWhiteSpace(authDTO.Password))
                {
                    var response = ResponseModel<object>.BadRequest("Usuario y contraseña son requeridos");
                    return StatusCode(response.status, response);
                }

                var empleado = await _authservice.ValidarUsuario(authDTO);
                if (empleado == null)
                {
                    var response = ResponseModel<object>.Unauthorized("Credenciales incorrectas");
                    return StatusCode(response.status, response);
                }

                var token = await _authservice.GenerarToken(empleado);
                var successResponse = ResponseModel<object>.Ok(token, "Autenticación exitosa");
                return StatusCode(successResponse.status, successResponse);
            }
            catch (UnauthorizedDomainException ex)
            {
                var response = ResponseModel<object>.Unauthorized(ex.Message);
                return StatusCode(response.status, response);
            }
            catch (ValidationException ex)
            {
                var response = ResponseModel<object>.BadRequest(ex.Message);
                return StatusCode(response.status, response);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Login error: {ex.Message}\n{ex.StackTrace}");
                var response = ResponseModel<object>.InternalServerError($"Error en la autenticación: {ex.Message}");
                return StatusCode(response.status, response);
            }
        }
    }
}
