using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp.net_mvc
{
    public class NewRazor_Page : PageModel
    {
        private readonly ILogger<NewRazor_Page> _logger;

        public NewRazor_Page(ILogger<NewRazor_Page> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}