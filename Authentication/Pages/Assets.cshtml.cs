using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Authentication.Pages;
public class AssetsModel :PageModel{
    public AssetsModel(){

    }

    public async Task<IActionResult> OnGetAsync(){
        if (!User.Identity.IsAuthenticated){
            return RedirectToPage("/Index");
        }
        return Page();
    }

        public async Task<IActionResult> OnPostAsync(){
        if (!User.Identity.IsAuthenticated){
            return RedirectToPage("/Index");
        }
        return Page();
    }


}