using Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetEmployee.Data;

namespace NetEmployee.Pages.Account;

public class NewPswd : PageModel
{
    
    private readonly DataServices _auth;

    public NewPswd(DataServices auth)
    {
        
        _auth = auth;
    }
    
    [BindProperty]
    public AuthenticationModel newdata { get; set; }
    [BindProperty]
    public string confirm { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        if (!User.Identity.IsAuthenticated){
            return Page();
        }
        return Redirect("/Index");
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        var user = _auth.ValidateEmail(newdata);
        
        if (user.Count() == 0)
        {
            ModelState.AddModelError(string.Empty, "Institutional email does not exist.");
            return Page();
        }
        if (newdata.PASSWORD == confirm)
        {
            foreach (var item in user)
            {
                _auth.IUpdatePassword(newdata.PASSWORD, item.CODE, item.INSTITUTION);
                return RedirectToPage("./Index");
            }
            
        }
        else
        {
            ModelState.AddModelError(string.Empty, "The password is not the same.");
        }
        return Page();
    }
}