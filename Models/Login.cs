namespace asp.net_mvc.Models
{
    public class Login
    {
        public required string Email { get; set; }
        public required string Password { get; set; }

        public bool KeepLoggedIn { get; set; }

    }
}