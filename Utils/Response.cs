namespace IA.GymAdmin.HelpDesk.API.Utils
{
    /// <summary>
    /// Clase heredada - Usar ResponseModel en su lugar
    /// Esta clase se mantiene por compatibilidad con código existente
    /// </summary>
    public class Response<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static Response<T> CreateSuccess(T data, string message = "Operación exitosa")
        {
            return new Response<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }
    }
}
