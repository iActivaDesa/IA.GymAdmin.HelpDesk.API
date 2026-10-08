namespace IA.GymAdmin.HelpDesk.API.Infraestructure.Response
{
    public class Response<T>
    {
        public bool status { get; set; }

        public T? Value { get; set; }

        public string msg { get; set; } = string.Empty;

        public IReadOnlyCollection<string> errors { get; set; } = Array.Empty<string>();

        public static Response<T> Success(T value, string message = "")
        {
            return new Response<T>
            {
                status = true,
                Value = value,
                msg = message
            };
        }

        public static Response<T> Failure(string message, IEnumerable<string>? errors = null)
        {
            return new Response<T>
            {
                status = false,
                msg = message,
                errors = errors?.ToArray() ?? Array.Empty<string>()
            };
        }

    }
}
