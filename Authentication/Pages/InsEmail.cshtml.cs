using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetEmployee.Data;
using NetEmployee.Model;


namespace Authentication;

public class InsEmail : PageModel
{
    private readonly DataServices _data;
    

    public InsEmail(DataServices data)
    {
        _data = data;
    }
    [BindProperty]public InstitutionModel im { get; set; }
    [BindProperty]public AddressProfile address { get; set; } = default!;
    [BindProperty]public Communication communication { get; set; } = default!;
        [BindProperty]public  AuthenticationProcedure auth { get; set; }

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
        var Institution = User.FindFirst(ClaimTypes.Hash)?.Value;
        
        var code = User.FindFirst(ClaimTypes.Actor)?.Value;

        DateTime date_ = DateTime.Now;
        string date = date_.ToString("MM/dd/yyyy");
        DateTime currentTimeUtc = DateTime.UtcNow;
        var time = currentTimeUtc.ToString("HH:mm:ss");

           
       
            
                
                
                im.INSTITUTION = Institution;
                im.CODE = int.Parse(code);


                auth.Code = int.Parse(code);
                auth.Institution = Institution;
                auth.Date = date;
                auth.sha = ComputeSha256Hash(code+Institution+date+time);
                auth.State = false;
                auth.time = time;
                                            // auth.Ip = GetIpAddress().ToString();
                _data.RegisterNewAuthorization(auth);


                _data.addInstitutionalemailToCurrentUser(im);

                return RedirectToPage("/Assets");
            
            
            
             
            


    }

        public static string ComputeSha256Hash(string rawData)
    {
        // Create a SHA256 instance
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // Compute the hash as a byte array
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert the byte array to a string
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}