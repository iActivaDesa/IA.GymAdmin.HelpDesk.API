## Guía de Uso: ResponseModel para Respuestas de API

### Ubicación
`Utils/ResponseModel.cs`

### Importación
```csharp
using IA.GymAdmin.HelpDesk.API.Utils;
```

### Formato de Respuesta
Todos los endpoints retornan exactamente 3 atributos:
```json
{
  "status": 200,
  "message": "Descripción de la operación",
  "data": { /* objeto o null */ }
}
```

---

## Códigos de Estado Soportados

| Código | Método | Caso de Uso | Data |
|--------|--------|-----------|------|
| **200** | `Ok()` | Solicitud exitosa | Datos retornados |
| **201** | `Created()` | Recurso creado | Recurso creado |
| **400** | `BadRequest()` | Validación fallida | null |
| **401** | `Unauthorized()` | Autenticación fallida | null |
| **403** | `Forbidden()` | Acceso prohibido | null |
| **404** | `NotFound()` | Recurso no encontrado | null |
| **405** | `MethodNotAllowed()` | Método no permitido | null |
| **409** | `Conflict()` | Recurso duplicado | null |
| **500** | `InternalServerError()` | Error interno | null |

---

## Ejemplos de Uso

### 1. Éxito con datos (200 OK)
```csharp
[HttpGet("{id}")]
public async Task<IActionResult> ObtenerUsuario(int id)
{
    var usuario = await _usuarioService.ObtenerUsuario(id);
    if (usuario == null)
        return StatusCode(404, ResponseModel<object>.NotFound("Usuario no encontrado"));
    
    var response = ResponseModel<UsuarioDTO>.Ok(usuario, "Usuario obtenido");
    return StatusCode(response.status, response);
}
```

**Respuesta:**
```json
{
  "status": 200,
  "message": "Usuario obtenido",
  "data": {
    "id": 1,
    "nombre": "Juan",
    "email": "juan@example.com"
  }
}
```

---

### 2. Crear recurso (201 Created)
```csharp
[HttpPost]
public async Task<IActionResult> CrearUsuario([FromBody] CreateUsuarioDTO dto)
{
    var nuevoUsuario = await _usuarioService.CrearUsuario(dto);
    var response = ResponseModel<UsuarioDTO>.Created(nuevoUsuario, "Usuario creado exitosamente");
    return StatusCode(response.status, response);
}
```

**Respuesta:**
```json
{
  "status": 201,
  "message": "Usuario creado exitosamente",
  "data": {
    "id": 5,
    "nombre": "Carlos",
    "email": "carlos@example.com"
  }
}
```

---

### 3. Validación fallida (400 Bad Request)
```csharp
[HttpPost("login")]
public async Task<IActionResult> Login([FromBody] AuthDTO authDTO)
{
    if (authDTO == null || string.IsNullOrWhiteSpace(authDTO.Username))
    {
        var response = ResponseModel<object>.BadRequest("Usuario y contraseña son requeridos");
        return StatusCode(response.status, response);
    }
    
    // ... resto del código
}
```

**Respuesta:**
```json
{
  "status": 400,
  "message": "Usuario y contraseña son requeridos",
  "data": null
}
```

---

### 4. Autenticación fallida (401 Unauthorized)
```csharp
var response = ResponseModel<object>.Unauthorized("Credenciales incorrectas");
return StatusCode(response.status, response);
```

**Respuesta:**
```json
{
  "status": 401,
  "message": "Credenciales incorrectas",
  "data": null
}
```

---

### 5. Recurso no encontrado (404 Not Found)
```csharp
if (usuario == null)
{
    var response = ResponseModel<object>.NotFound($"Usuario con ID {id} no encontrado");
    return StatusCode(response.status, response);
}
```

**Respuesta:**
```json
{
  "status": 404,
  "message": "Usuario con ID 99 no encontrado",
  "data": null
}
```

---

### 6. Conflicto / Duplicado (409 Conflict)
```csharp
var usuarioExistente = await _usuarioService.ObtenerPorEmail(email);
if (usuarioExistente != null)
{
    var response = ResponseModel<object>.Conflict("El email ya está registrado");
    return StatusCode(response.status, response);
}
```

**Respuesta:**
```json
{
  "status": 409,
  "message": "El email ya está registrado",
  "data": null
}
```

---

### 7. Error interno (500 Internal Server Error)
```csharp
catch (Exception ex)
{
    var response = ResponseModel<object>.InternalServerError($"Error: {ex.Message}");
    return StatusCode(response.status, response);
}
```

**Respuesta:**
```json
{
  "status": 500,
  "message": "Error: Database connection failed",
  "data": null
}
```

---

### 8. Código personalizado
```csharp
var response = ResponseModel<object>.Custom(418, "Soy una tetera", null);
return StatusCode(response.status, response);
```

**Respuesta:**
```json
{
  "status": 418,
  "message": "Soy una tetera",
  "data": null
}
```

---

## Patrón Recomendado en Controladores

```csharp
[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuariosController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerUsuario(int id)
    {
        var usuario = await _usuarioService.ObtenerUsuario(id);
        if (usuario == null)
        {
            var notFoundResponse = ResponseModel<object>.NotFound($"Usuario {id} no encontrado");
            return StatusCode(notFoundResponse.status, notFoundResponse);
        }

        var okResponse = ResponseModel<UsuarioDTO>.Ok(usuario);
        return StatusCode(okResponse.status, okResponse);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CrearUsuario([FromBody] CreateUsuarioDTO dto)
    {
        try
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Email))
            {
                var badReq = ResponseModel<object>.BadRequest("Email es requerido");
                return StatusCode(badReq.status, badReq);
            }

            var nuevoUsuario = await _usuarioService.CrearUsuario(dto);
            var response = ResponseModel<UsuarioDTO>.Created(nuevoUsuario, "Usuario creado");
            return StatusCode(response.status, response);
        }
        catch (Exception ex)
        {
            var error = ResponseModel<object>.InternalServerError(ex.Message);
            return StatusCode(error.status, error);
        }
    }
}
```

---

## Integración con Middleware

El `ExceptionHandlingMiddleware` captura automáticamente las excepciones de dominio:

```csharp
// En un servicio lanzas la excepción
throw new ValidationException("Email inválido");

// El middleware lo convierte automáticamente a:
// { "status": 400, "message": "Email inválido", "data": null }
```

---

## Características

✅ Formato consistente: 3 atributos siempre  
✅ `data` es `null` en errores  
✅ Mensajes descriptivos personalizables  
✅ Compatible con Swagger/OpenAPI  
✅ Serialización automática JSON  

---

## Versión No Genérica

Para operaciones sin retorno de datos específico:

```csharp
// Retorna ResponseModel (sin genérico)
var response = ResponseModel.Ok(null, "Operación completada");
return StatusCode(response.status, response);

// O simplemente
var response = ResponseModel.Ok(message: "Deleted successfully");
return StatusCode(response.status, response);
```
