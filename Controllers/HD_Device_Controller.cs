using IA.GymAdmin.HelpDesk.API.Domain.DTO;
using IA.GymAdmin.HelpDesk.API.Domain.Entity;
using IA.GymAdmin.HelpDesk.API.Domain.Exceptions;
using IA.GymAdmin.HelpDesk.API.Domain.Interfaces;
using IA.GymAdmin.HelpDesk.API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IA.GymAdmin.HelpDesk.API.Controllers
{
    [Route("api/hddevice")]
    [ApiController]
    [Authorize]
    public class HD_Device_Controller : ControllerBase
    {
        private readonly IHD_Device _hdDeviceService;

        public HD_Device_Controller(IHD_Device hdDeviceService)
        {
            _hdDeviceService = hdDeviceService;
        }

        /// <summary>
        /// Obtiene todos los dispositivos.
        /// </summary>
        [HttpGet("AllDevices")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDevices()
        {
            try
            {
                var devices = await _hdDeviceService.Listar();
                var response = ResponseModel<List<HDDeviceListarDTO>>.Ok(devices, "Dispositivos obtenidos exitosamente");
                return StatusCode(response.status, response);
            }
            catch (Exception ex)
            {
                var response = ResponseModel<object>.InternalServerError($"Error al obtener los dispositivos: {ex.Message}");
                return StatusCode(response.status, response);
            }
        }

        /// <summary>
        /// Obtiene un dispositivo por su Id.
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDeviceById(int id)
        {
            try
            {
                var device = await _hdDeviceService.BuscarPorId(id);
                var response = ResponseModel<HD_Device>.Ok(device, "Dispositivo obtenido exitosamente");
                return StatusCode(response.status, response);
            }
            catch (NotFoundException ex)
            {
                var response = ResponseModel<object>.NotFound(ex.Message);
                return StatusCode(response.status, response);
            }
            catch (Exception ex)
            {
                var response = ResponseModel<object>.InternalServerError($"Error al obtener el dispositivo: {ex.Message}");
                return StatusCode(response.status, response);
            }
        }

        /// <summary>
        /// Obtiene un dispositivo por su nombre.
        /// </summary>
        [HttpGet("nombre/{nombre}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDeviceByName(string nombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    var badReq = ResponseModel<object>.BadRequest("El nombre del dispositivo es requerido");
                    return StatusCode(badReq.status, badReq);
                }

                var device = await _hdDeviceService.BuscarPorNombre(nombre);
                var response = ResponseModel<HD_Device>.Ok(device, "Dispositivo obtenido exitosamente");
                return StatusCode(response.status, response);
            }
            catch (NotFoundException ex)
            {
                var response = ResponseModel<object>.NotFound(ex.Message);
                return StatusCode(response.status, response);
            }
            catch (ValidationException ex)
            {
                var response = ResponseModel<object>.BadRequest(ex.Message);
                return StatusCode(response.status, response);
            }
            catch (Exception ex)
            {
                var response = ResponseModel<object>.InternalServerError($"Error al obtener el dispositivo: {ex.Message}");
                return StatusCode(response.status, response);
            }
        }

        /// <summary>
        /// Crea un nuevo dispositivo.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateDevice([FromBody] HD_DeviceDTO modelo)
        {
            try
            {
                // Decodifica el JWT (ya validado por [Authorize]) y obtiene el Id del usuario.
                var userId = JwtDecoder.GetUserIdFromRequest(Request, User);
                if (string.IsNullOrWhiteSpace(userId))
                {
                    var unauthorized = ResponseModel<object>.Unauthorized("No se pudo obtener el Id del usuario desde el token.");
                    return StatusCode(unauthorized.status, unauthorized);
                }

                if (modelo == null)
                {
                    var badReq = ResponseModel<object>.BadRequest("Los datos del dispositivo son requeridos");
                    return StatusCode(badReq.status, badReq);
                }

                if (string.IsNullOrWhiteSpace(modelo.name))
                {
                    var badReq = ResponseModel<object>.BadRequest("El nombre del dispositivo es requerido");
                    return StatusCode(badReq.status, badReq);
                }

                var created = await _hdDeviceService.Crear(modelo, userId);
                var response = ResponseModel<HD_Device>.Created(created, "Dispositivo creado exitosamente");
                return StatusCode(response.status, response);
            }
            catch (ValidationException ex)
            {
                var response = ResponseModel<object>.BadRequest(ex.Message);
                return StatusCode(response.status, response);
            }
            catch (UnauthorizedAccessException ex)
            {
                var response = ResponseModel<object>.Unauthorized(ex.Message);
                return StatusCode(response.status, response);
            }
            catch (Exception ex)
            {
                var response = ResponseModel<object>.InternalServerError($"Error al crear el dispositivo: {ex.Message}");
                return StatusCode(response.status, response);
            }
        }

        /// <summary>
        /// Actualiza un dispositivo existente.
        /// </summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateDevice([FromBody] HD_DeviceDTO modelo)
        {
            try
            {
                if (modelo == null)
                {
                    var badReq = ResponseModel<object>.BadRequest("Los datos del dispositivo son requeridos");
                    return StatusCode(badReq.status, badReq);
                }

                if (modelo.Id == null || modelo.Id <= 0)
                {
                    var badReq = ResponseModel<object>.BadRequest("El Id del dispositivo es requerido para actualizar");
                    return StatusCode(badReq.status, badReq);
                }

                var updated = await _hdDeviceService.Editar(modelo);
                var response = ResponseModel<bool>.Ok(updated, "Dispositivo actualizado exitosamente");
                return StatusCode(response.status, response);
            }
            catch (NotFoundException ex)
            {
                var response = ResponseModel<object>.NotFound(ex.Message);
                return StatusCode(response.status, response);
            }
            catch (ValidationException ex)
            {
                var response = ResponseModel<object>.BadRequest(ex.Message);
                return StatusCode(response.status, response);
            }
            catch (Exception ex)
            {
                var response = ResponseModel<object>.InternalServerError($"Error al actualizar el dispositivo: {ex.Message}");
                return StatusCode(response.status, response);
            }
        }

        /// <summary>
        /// Elimina un dispositivo por su Id.
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDevice(int id)
        {
            try
            {
                var deleted = await _hdDeviceService.Eliminart(id);
                var response = ResponseModel<bool>.Ok(deleted, "Dispositivo eliminado exitosamente");
                return StatusCode(response.status, response);
            }
            catch (NotFoundException ex)
            {
                var response = ResponseModel<object>.NotFound(ex.Message);
                return StatusCode(response.status, response);
            }
            catch (Exception ex)
            {
                var response = ResponseModel<object>.InternalServerError($"Error al eliminar el dispositivo: {ex.Message}");
                return StatusCode(response.status, response);
            }
        }
    }
}
