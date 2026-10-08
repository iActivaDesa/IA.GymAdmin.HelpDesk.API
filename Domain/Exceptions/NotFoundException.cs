namespace IA.GymAdmin.HelpDesk.API.Domain.Exceptions
{
    public sealed class NotFoundException : DomainException
    {
        public NotFoundException(string message)
            : base(message, StatusCodes.Status404NotFound)
        {
        }
    }
}
