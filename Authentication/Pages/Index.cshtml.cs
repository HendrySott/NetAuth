using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetEmployee.Data;
using System.Net.Sockets;
using System.Net;

namespace Authentication.Pages;

public class IndexModel : PageModel
{
    private readonly DataServices _data; 
    public IndexModel(DataServices data)
    {
       _data = data;
    }

    [BindProperty]public Autentication Input { get; set; }
    [BindProperty]public string ReturnUrl { get; set; }
    [BindProperty]public AuthenticationModel ModelInput { get; set; }
    [BindProperty]public string LoginTo{get;set;}
    [TempData] public string ErrorMessage { get; set; }
    [BindProperty]public  AuthenticationProcedure auth { get; set; }

    

        
        // public async Task<IActionResult> OnGetAsync()
        // {

        //     var cookie = _ctr.GetCookieUsingKey("CODE");
        //     if (cookie == null)
        //     {
        //         return Page();
        //     }
        //     else
        //     {
        //         return Redirect("~/Dashboard");
        //     }
        // }


    public async Task<IActionResult> OnGetAsync(string login)
    {
        LoginTo = login;
        ReturnUrl = login;
        if (User.Identity.IsAuthenticated){
            // foreach (var cookie in Request.Cookies.Keys)
            //     {
            //         Response.Cookies.Delete(cookie);
            //     }

            return RedirectToPage("Assets");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string login)
    {
        DateTime date_ = DateTime.Now;
        string date = date_.ToString("MM/dd/yyyy");
        DateTime currentTimeUtc = DateTime.UtcNow;
        var time = currentTimeUtc.ToString("HH:mm:ss");
        try{
            var cookieOptions = new CookieOptions{
                // Domain = ".syscrafter.com",
                Expires = DateTime.Now.AddHours(12), // Cookie expiration
                Path = "/", // Limit cookie to the root path
                HttpOnly = true, // Cookie not accessible via JavaScript
                Secure = true, // Send cookie only over HTTPS (if HTTPS is used)
                SameSite = SameSiteMode.Strict // Adjust based on your requirements
            };

            var user = _data.userLogin(ModelInput);
            int code =0;
            string ins = "";
            string role = "";
            string insemail = "";
            string pswx = "";

                if(user.Count()>0){
                    
                    foreach (var i in user)
                    {
                        
                        if(i.EMAIL.ToString() != null){ Response.Cookies.Append("email", ModelInput.EMAIL.ToString(), cookieOptions);}
                        if(i.ROLE.ToString() != null){ role =  i.ROLE.ToString();}
                        // if(i.PASSWORD.ToString() != null){ pswx =  i.PASSWORD.ToString();}
                        
                        var psw = _data.LoadInstitutionDetails(i.INSTITUTION);
                        if (psw.Count() > 0)
                        {
                            foreach (var name in psw)
                            {
                                if (name.DEF_PASS == ModelInput.PASSWORD)
                                {
                                    return RedirectToPage("/Account/NewPswd");
                                }
                            }

                                
                                        foreach (var usr in _data.ILoadAllUserByUID(i.INSTITUTION, i.CODE))
                                        {
                                            // SetCookies("CODE", usr.CODE.ToString(), 12);
                                            // SetCookies("INSTITUTION", usr.INSTITUTION, 12);
                                            code = usr.CODE;
                                            ins = usr.INSTITUTION;
                                            
                                            if(usr.F_NAME.ToString() != null){ Response.Cookies.Append("fname", usr.F_NAME.ToString(), cookieOptions);}
                                            // if(usr.S_NAME.ToString() != null){ Response.Cookies.Append("sname", usr.S_NAME.ToString(), cookieOptions);}
                                            if(usr.F_LASTN.ToString() != null){ Response.Cookies.Append("flastname", usr.F_LASTN.ToString(), cookieOptions);}
                                            // if(usr.S_LASTN.ToString() != null){ Response.Cookies.Append("slastname", usr.S_LASTN.ToString(), cookieOptions);}
                                            // if(usr.PHONE.ToString() != null){ Response.Cookies.Append("phone", usr.PHONE.ToString(), cookieOptions);}
                                            if(usr.ADMITION_DATE.ToString() != null){ Response.Cookies.Append("admitionDate", usr.ADMITION_DATE.ToString(), cookieOptions);}
                                            if(usr.TEAM_ID.ToString() != null){ Response.Cookies.Append("team", usr.TEAM_ID.ToString(), cookieOptions);}
                                            if(usr.POSITION_KEY.ToString() != null){ 
                                                foreach(var item in _data.GetPositionById(i.INSTITUTION, usr.POSITION_KEY)){
                                                    Response.Cookies.Append("positionName", item.POSITION_NAME.ToString(), cookieOptions);
                                                    // ModelState.AddModelError(string.Empty, item.POSITION_NAME.ToString());

                                                }
                                                Response.Cookies.Append("positionId", usr.POSITION_KEY.ToString(), cookieOptions);
                                                
                                            }
                                            if(usr.PERSONAL_EMAIL.ToString() != null){Response.Cookies.Append("personalEmail", usr.PERSONAL_EMAIL.ToString(), cookieOptions);}


                                            
                                            
                                        }
                        
                                        ///intitution name cookies,
                                        foreach (var inst in _data.GetInstitutionNameUsingInstitutionCode(i.INSTITUTION))
                                        {
                                            
                                            if(inst.NAME.ToString() != null){Response.Cookies.Append("institutionName", inst.NAME.ToString(), cookieOptions);}
                                            // if(inst.Tax_ID.ToString() != null){Response.Cookies.Append("taxId", inst.Tax_ID.ToString(), cookieOptions);}  
                                        }
                                        
                                        var claims = new List<Claim>
                                        {
                                            new Claim(ClaimTypes.Actor, code.ToString()),
                                            new Claim(ClaimTypes.Hash, ins.ToString()),
                                            new Claim(ClaimTypes.Role, role.ToString()),
                                            // new Claim(ClaimTypes.UserData, pswx.ToString())

                                        };

                                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                                        var authProperties = new AuthenticationProperties
                                        {
                                            IsPersistent = ModelInput.REMEMBERME, // Set as needed
                                            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(12) // Set expiration time as needed
                                        };

                                        await HttpContext.SignInAsync(
                                            CookieAuthenticationDefaults.AuthenticationScheme,
                                            new ClaimsPrincipal(claimsIdentity),
                                            authProperties);


                                        auth.Code = code;
                                            auth.Institution = ins;
                                            auth.Date = date;
                                            auth.sha = ComputeSha256Hash(code+ins+date+time);
                                            auth.State = false;
                                            auth.time = time;
                                            // auth.Ip = GetIpAddress().ToString();
                                            _data.RegisterNewAuthorization(auth);

                                        if(login != null){
                                            // Thread.Sleep(2000);
                                            
                                            
                                           
                                            return RedirectPermanent("https://"+login+".syscrafter.com/account/login?token="+auth.sha+"&id="+login);
                                            
                                        }else{
                                            
                                            return RedirectToPage("/Assets");

                                        }
                                      
                                    
                                    // return RedirectToPage("./Login");
                                
                            
                        }
                    }


                }else{
                    ModelState.AddModelError(string.Empty, "Forgot my password? Please communicate with your organization's IT team");
                    
                }


            return Page();
        }catch(Exception e){
                                ModelState.AddModelError(string.Empty, $"{e}");

        }
        return Page();
    
       
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

    public static string GetIpAddress(){
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4");
    }
}
