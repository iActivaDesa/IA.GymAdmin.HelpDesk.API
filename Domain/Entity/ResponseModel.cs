namespace IA.GymAdmin.HelpDesk.API.Domain.Entity
{
    public class ResponseModel
    {
        public int statusCode { get; set; }
        public string? message { get; set; }
        public object? data { get; set; }

    }
}
