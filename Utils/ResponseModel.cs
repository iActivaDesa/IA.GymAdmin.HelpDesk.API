namespace IA.GymAdmin.HelpDesk.API.Utils
{
    /// <summary>
    /// Modelo de respuesta API simplificado.
    /// Formato: { status: int, message: string, data: object }
    /// </summary>
    public class ResponseModel<T>
    {
        public int status { get; set; }
        public string message { get; set; } = string.Empty;
        public T? data { get; set; }

        // 200 OK
        public static ResponseModel<T> Ok(T? data = default, string message = "Operación exitosa")
        {
            return new ResponseModel<T>
            {
                status = StatusCodes.Status200OK,
                message = message,
                data = data
            };
        }

        // 201 Created
        public static ResponseModel<T> Created(T? data = default, string message = "Recurso creado exitosamente")
        {
            return new ResponseModel<T>
            {
                status = StatusCodes.Status201Created,
                message = message,
                data = data
            };
        }

        // 400 Bad Request
        public static ResponseModel<T> BadRequest(string message = "Solicitud inválida")
        {
            return new ResponseModel<T>
            {
                status = StatusCodes.Status400BadRequest,
                message = message,
                data = default
            };
        }

        // 401 Unauthorized
        public static ResponseModel<T> Unauthorized(string message = "Autenticación requerida")
        {
            return new ResponseModel<T>
            {
                status = StatusCodes.Status401Unauthorized,
                message = message,
                data = default
            };
        }

        // 403 Forbidden
        public static ResponseModel<T> Forbidden(string message = "Acceso prohibido")
        {
            return new ResponseModel<T>
            {
                status = StatusCodes.Status403Forbidden,
                message = message,
                data = default
            };
        }

        // 404 Not Found
        public static ResponseModel<T> NotFound(string message = "Recurso no encontrado")
        {
            return new ResponseModel<T>
            {
                status = StatusCodes.Status404NotFound,
                message = message,
                data = default
            };
        }

        // 405 Method Not Allowed
        public static ResponseModel<T> MethodNotAllowed(string message = "Método HTTP no permitido")
        {
            return new ResponseModel<T>
            {
                status = StatusCodes.Status405MethodNotAllowed,
                message = message,
                data = default
            };
        }

        // 409 Conflict
        public static ResponseModel<T> Conflict(string message = "Conflicto: El recurso ya existe")
        {
            return new ResponseModel<T>
            {
                status = StatusCodes.Status409Conflict,
                message = message,
                data = default
            };
        }

        // 500 Internal Server Error
        public static ResponseModel<T> InternalServerError(string message = "Error interno del servidor")
        {
            return new ResponseModel<T>
            {
                status = StatusCodes.Status500InternalServerError,
                message = message,
                data = default
            };
        }

        // Método genérico para códigos personalizados
        public static ResponseModel<T> Custom(int statusCode, string message, T? data = default)
        {
            return new ResponseModel<T>
            {
                status = statusCode,
                message = message,
                data = data
            };
        }
    }

    /// <summary>
    /// Versión no genérica para operaciones sin retorno de datos.
    /// </summary>
    public class ResponseModel
    {
        public int status { get; set; }
        public string message { get; set; } = string.Empty;
        public object? data { get; set; }

        public static ResponseModel Ok(object? data = null, string message = "Operación exitosa")
        {
            return new ResponseModel
            {
                status = StatusCodes.Status200OK,
                message = message,
                data = data
            };
        }

        public static ResponseModel Created(object? data = null, string message = "Recurso creado exitosamente")
        {
            return new ResponseModel
            {
                status = StatusCodes.Status201Created,
                message = message,
                data = data
            };
        }

        public static ResponseModel BadRequest(string message = "Solicitud inválida")
        {
            return new ResponseModel
            {
                status = StatusCodes.Status400BadRequest,
                message = message,
                data = null
            };
        }

        public static ResponseModel Unauthorized(string message = "Autenticación requerida")
        {
            return new ResponseModel
            {
                status = StatusCodes.Status401Unauthorized,
                message = message,
                data = null
            };
        }

        public static ResponseModel Forbidden(string message = "Acceso prohibido")
        {
            return new ResponseModel
            {
                status = StatusCodes.Status403Forbidden,
                message = message,
                data = null
            };
        }

        public static ResponseModel NotFound(string message = "Recurso no encontrado")
        {
            return new ResponseModel
            {
                status = StatusCodes.Status404NotFound,
                message = message,
                data = null
            };
        }

        public static ResponseModel MethodNotAllowed(string message = "Método HTTP no permitido")
        {
            return new ResponseModel
            {
                status = StatusCodes.Status405MethodNotAllowed,
                message = message,
                data = null
            };
        }

        public static ResponseModel Conflict(string message = "Conflicto: El recurso ya existe")
        {
            return new ResponseModel
            {
                status = StatusCodes.Status409Conflict,
                message = message,
                data = null
            };
        }

        public static ResponseModel InternalServerError(string message = "Error interno del servidor")
        {
            return new ResponseModel
            {
                status = StatusCodes.Status500InternalServerError,
                message = message,
                data = null
            };
        }

        public static ResponseModel Custom(int statusCode, string message, object? data = null)
        {
            return new ResponseModel
            {
                status = statusCode,
                message = message,
                data = data
            };
        }
    }
}
