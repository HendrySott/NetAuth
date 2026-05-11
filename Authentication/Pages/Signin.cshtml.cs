using System.Security.Claims;
using Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetEmployee.Data;


namespace Authentication.Pages;
public class SigninModel :PageModel{
    //private readonly ILogger<LoginModel> _logger;
        
       
        [BindProperty] public PersonProfile person { get; set; }
        [BindProperty] public AuthenticationModel auth { get; set; }
        // [BindProperty] public SetupModel setup { get; set; }
        [BindProperty] public string psw { get; set; }
        public string ReturnUrl { get; private set; }
        
        private readonly DataServices _data;



        public SigninModel(DataServices auth)
        {
           
            _data = auth;
            
        }
        private static Random random = new Random();

        //todo get in this form just the email and password, the sistem send an email to the email registred



        public async Task<IActionResult> OnGetAsync()
        {
            
            if (User.Identity.IsAuthenticated)
            {
                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }

                
            }

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
                SameSite = SameSiteMode.Strict// Adjust based on your requirements


            };



            try
            {
                var user = _data.LoadUserFromLogin(auth);

                if (user.Count() > 0)
                {
                    ModelState.AddModelError(string.Empty, "This email address is already in use by another user");
                    return Page();
                }
                else
                {
                   
                    if (string.IsNullOrEmpty(person.S_NAME))
                    {
                        person.S_NAME = " ";
                    }
                    
                  
                    if (string.IsNullOrEmpty(person.S_LASTN))
                    {
                        person.S_LASTN = " ";
                    }
                    
                    person.GRADE = "-";
                    person.ADDRESS = "-";
                    person.PHONE = "0";
                    person.IDENTIFICATION = "n/a";
                    person.PERSONAL_EMAIL = auth.EMAIL;
                    person.TEAM_ID = 0;
                    person.POSITION_KEY = 0;
                    person.STREET = "n/a";
                    person.HOME = 0;
                    person.COUNTY = "n/a";
                    person.COUNTRY = "n/a";
                    person.TEAM_ID = 0;

                    
                    
                    person.DATA = auth.PASSWORD;
                    
                    
                    
                    
                    
                    _data.registerNewPerson(person);
                    
                    
                    foreach (var gdata in _data.LoadUserFromLogin(auth)){

                    
                    
                    
                                
                                        foreach (var usr in _data.ILoadAllUserByUID(gdata.INSTITUTION, gdata.CODE))
                                        {
                                            // SetCookies("CODE", usr.CODE.ToString(), 12);
                                            // SetCookies("INSTITUTION", usr.INSTITUTION, 12);
                                           
                                            
                                            if(usr.F_NAME.ToString() != null){ Response.Cookies.Append("fname", usr.F_NAME.ToString(), cookieOptions);}
                                            // if(usr.S_NAME.ToString() != null){ Response.Cookies.Append("sname", usr.S_NAME.ToString(), cookieOptions);}
                                            if(usr.F_LASTN.ToString() != null){ Response.Cookies.Append("flastname", usr.F_LASTN.ToString(), cookieOptions);}
                                            // if(usr.S_LASTN.ToString() != null){ Response.Cookies.Append("slastname", usr.S_LASTN.ToString(), cookieOptions);}
                                            // if(usr.PHONE.ToString() != null){ Response.Cookies.Append("phone", usr.PHONE.ToString(), cookieOptions);}
                                            // if(usr.ADMITION_DATE.ToString() != null){ Response.Cookies.Append("admitionDate", usr.ADMITION_DATE.ToString(), cookieOptions);}
                                            if(usr.TEAM_ID.ToString() != null){ Response.Cookies.Append("team", usr.TEAM_ID.ToString(), cookieOptions);}
                                            // if(usr.POSITION_KEY.ToString() != null){ 
                                            //     foreach(var item in _data.GetPositionById(usr.INSTITUTION, usr.POSITION_KEY)){
                                            //         Response.Cookies.Append("positionName", item.POSITION_NAME.ToString(), cookieOptions);
                                            //         ModelState.AddModelError(string.Empty, item.POSITION_NAME.ToString());

                                            //     }
                                                Response.Cookies.Append("positionId", usr.POSITION_KEY.ToString(), cookieOptions);
                                                
                                            // }
                                            // if(usr.PERSONAL_EMAIL.ToString() != null){Response.Cookies.Append("personalEmail", usr.PERSONAL_EMAIL.ToString(), cookieOptions);}


                                            
                                            
                                        }
                        
                                        ///intitution name cookies,
                                        // foreach (var inst in _data.GetInstitutionNameUsingInstitutionCode(gdata.INSTITUTION))
                                        // {
                                            
                                        //     if(inst.NAME.ToString() != null){Response.Cookies.Append("institutionName", inst.NAME.ToString(), cookieOptions);}
                                        //     if(inst.RNC.ToString() != null){Response.Cookies.Append("rnc", inst.RNC.ToString(), cookieOptions);}
                                        // }


                                        var claims = new List<Claim>

                                        {
                                            new Claim(ClaimTypes.Actor, gdata.CODE.ToString()),
                                            new Claim(ClaimTypes.Hash, gdata.INSTITUTION.ToString()),
                                            new Claim(ClaimTypes.Role, gdata.ROLE.ToString()),
                                            // new Claim(ClaimTypes.UserData, gdata.PASSWORD.ToString())


                                        };


                                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                                        var authProperties = new AuthenticationProperties
                                        {
                                            IsPersistent = auth.REMEMBERME, // Set as needed
                                            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(12) // Set expiration time as needed
                                        };

                                        await HttpContext.SignInAsync(
                                            CookieAuthenticationDefaults.AuthenticationScheme,
                                            new ClaimsPrincipal(claimsIdentity),
                                            authProperties);




                    }


                    return RedirectToPage("/InsConfig");
                  


                }
                
                
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return Page();
        }
}


