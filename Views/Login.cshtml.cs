using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp.net_mvc.Pages
{
    public class Login_Page : PageModel
    {
        private readonly ILogger<Login_Page> _logger;

        public Login_Page(ILogger<Login_Page> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}