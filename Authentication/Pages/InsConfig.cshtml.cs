using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetEmployee.Data;
using NetEmployee.Model;


namespace Authentication.Pages;

public class InsConfig : PageModel
{
    private readonly DataServices _data;
    

    public InsConfig(DataServices data)
    {
        _data = data;
    }
    [BindProperty]public InstitutionModel im { get; set; }
    [BindProperty]public Communication communication { get; set; }
    [BindProperty]public AddressProfile address { get; set; } = default!;
    [BindProperty]public string comm { get; set; } = default!;
    public async Task<IActionResult> OnGetAsync()
    {
      
        if (!User.Identity.IsAuthenticated){
            return RedirectToPage("./Error");
        }
       
        //Public acces to Unstitution string
        return Page();

    }

    public async Task<IActionResult> OnPostAsync()
    {

        
        var cookieOptions = new CookieOptions
        {
            // Domain = ".syscrafter.com", // Set the domain for the cookie
            Expires = DateTime.Now.AddHours(12), // Cookie expiration
            Path = "/", // Limit cookie to the root path
            HttpOnly = true, // Cookie not accessible via JavaScript
            Secure = true, // Send cookie only over HTTPS (if HTTPS is used)
            SameSite = SameSiteMode.Strict // Adjust based on your requirements


        };

        var Institution = User.FindFirst(ClaimTypes.Hash)?.Value;
        var code =  @User.FindFirst(ClaimTypes.Actor)?.Value;
        
        switch(communication.Command){
            case "addAddress":
                address.INSTITUTION = Institution;
                _data.SaveAdress(address);
            break;
            case "saveInstitution":
                

                im.INSTITUTIONALEMAIL = "@" + im.INSTITUTIONALEMAIL;
                im.INSTITUTION = Institution;
                im.CODE = int.Parse(code);
                _data.AddInstitutionDetails(im);


                foreach (var inst in _data.GetInstitutionNameUsingInstitutionCode(Institution))
                {    
                    if(inst.NAME.ToString() != null){Response.Cookies.Append("institutionName", inst.NAME.ToString(), cookieOptions);}
                    if(inst.Tax_ID.ToString() != null){Response.Cookies.Append("taxId", inst.Tax_ID.ToString(), cookieOptions);}   
                }


                return RedirectToPage("./InsEmail");
            break;
        }
           
       
                
                
            
            
            
               
             
            
        

        return Page();


    }
}