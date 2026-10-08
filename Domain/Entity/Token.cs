namespace IA.GymAdmin.HelpDesk.API.Domain.Entity
{
    public class TokenModel
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration {  get; set; }
    }
}
