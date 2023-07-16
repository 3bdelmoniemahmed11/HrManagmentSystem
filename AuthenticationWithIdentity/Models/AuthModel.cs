namespace AuthenticationWithIdentity.Models
{
    public class AuthModel
    {
        public string Token { get; set; }
        public List<object> Permissions { get; set; }
    }
}
