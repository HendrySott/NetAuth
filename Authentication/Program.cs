
using Microsoft.AspNetCore.Authentication.Cookies;
using NetEmployee.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
builder.Services.AddServerSideBlazor();


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5);
    
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Always use secure cookies
    
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // options.Cookie.Domain = ".syscrafter.com";
        options.Cookie.Name = "alhfsldkfnuin893h4niunjgAuth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Use Always in production
        options.LoginPath = "/Account/Login"; // Customize as needed
        options.LogoutPath = "/Account/Logout"; // Customize as needed
        
        // Configure cookie encryption
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.IsEssential = true;

        // Configure encryption and decryption
        options.CookieManager = new ChunkingCookieManager { ChunkSize = 3000 };

    });
    

builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AuthenticatedUser", policy =>
                policy.RequireAuthenticatedUser());
        });



builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var connectionString = builder.Configuration.GetConnectionString("DatabaceConectionString");
// builder.Services.AddDbContext<DataServices>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
// builder.Services.Add(new ServiceDescriptor(typeof(UserAuthenticationServices), new UserAuthenticationServices(connectionString)));
builder.Services.Add(new ServiceDescriptor(typeof(DataServices), new DataServices("server=127.0.0.1;uid=userOfRolesx;pwd=Unlockthis-01101;database=admbasic")));




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



var cookiePolicyOptions = new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
        HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
        Secure = CookieSecurePolicy.SameAsRequest,
};
app.UseCookiePolicy(cookiePolicyOptions);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization(); // Add this line if you need authorization

app.MapRazorPages();

app.Run();

