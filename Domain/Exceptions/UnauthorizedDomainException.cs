namespace IA.GymAdmin.HelpDesk.API.Domain.Exceptions
{
    public sealed class UnauthorizedDomainException : DomainException
    {
        public UnauthorizedDomainException(string message)
            : base(message, StatusCodes.Status401Unauthorized)
        {
        }
    }
}
