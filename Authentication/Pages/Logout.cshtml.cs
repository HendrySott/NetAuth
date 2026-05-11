
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NetEmployee.Pages.Account
{
	public class Logout : PageModel
    {

        private readonly IAuthenticationService _authenticationService;

        public Logout(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        public async Task<IActionResult> OnGetAsync(string logout)
        {
            if (logout != null)
            {
                string param = "?login="+logout;
                if (User.Identity.IsAuthenticated)
                {
                    foreach (var cookie in Request.Cookies.Keys)
                    {
                        Response.Cookies.Delete(cookie);
                    }

                    
                    return RedirectToPage("/Index");
                }else{
                    return RedirectToPage("/Index");
                }
            }
            if (User.Identity.IsAuthenticated)
            {
                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }

                return RedirectToPage("/Index");
            }else{
                return RedirectToPage("/Index");
            }

            
        }
    }
}
