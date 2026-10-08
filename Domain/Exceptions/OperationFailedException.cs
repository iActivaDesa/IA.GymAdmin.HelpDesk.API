namespace IA.GymAdmin.HelpDesk.API.Domain.Exceptions
{
    public sealed class OperationFailedException : DomainException
    {
        public OperationFailedException(string message)
            : base(message, StatusCodes.Status400BadRequest)
        {
        }
    }
}
