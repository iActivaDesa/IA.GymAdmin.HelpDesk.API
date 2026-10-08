namespace IA.GymAdmin.HelpDesk.API.Domain.Exceptions
{
    public abstract class DomainException : Exception
    {
        protected DomainException(string message, int statusCode, IEnumerable<string>? errors = null)
            : base(message)
        {
            StatusCode = statusCode;
            Errors = errors?.ToArray() ?? Array.Empty<string>();
        }

        public int StatusCode { get; }

        public IReadOnlyCollection<string> Errors { get; }
    }
}
