namespace IA.GymAdmin.HelpDesk.API.Domain.Exceptions
{
    public sealed class ValidationException : DomainException
    {
        public ValidationException(string message, IEnumerable<string>? errors = null)
            : base(message, StatusCodes.Status400BadRequest, errors)
        {
        }
    }

}
