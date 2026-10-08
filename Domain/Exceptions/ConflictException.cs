namespace IA.GymAdmin.HelpDesk.API.Domain.Exceptions
{
    public sealed class ConflictException : DomainException
    {
        public ConflictException(string message)
            : base(message, StatusCodes.Status409Conflict)
        {
        }
    }
}
