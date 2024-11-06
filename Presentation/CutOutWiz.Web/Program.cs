using CutOutWiz.Services.ApprovalTool;
//using CutOutWiz.Services.DataService;
using CutOutWiz.Services.DbAccess;
using CutOutWiz.Services.StorageService;
using CutOutWiz.Services.MessageService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CutOutWiz.Services.ReportServices;
using CutOutWiz.Services.LogServices;
using CutOutWiz.Services.Security;
using CutOutWiz.Services.HR;
using CutOutWiz.Services.Managers.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
//https://www.youtube.com/watch?v=5tYSO5mAjXs
builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
//builder.Services.AddSingleton<CutOutWiz.Services.DataService.IUserService, CutOutWiz.Services.DataService.UserService>();
//builder.Services.AddSingleton<IFileTrackingService, FileTrackingService>();
//builder.Services.AddSingleton<CutOutWiz.Services.DataService.ICompanyService, CutOutWiz.Services.DataService.CompanyService>();
builder.Services.AddSingleton<IAwsService, AwsService>();
builder.Services.AddSingleton<IEmailSenderService, EmailSenderService>();
builder.Services.AddSingleton<IApprovalToolCommonService, ApprovalToolCommonService>();
builder.Services.AddSingleton<IReportService, ReportService>();
builder.Services.AddSingleton<ILogService, LogService>();
builder.Services.AddSingleton<IModuleService, ModuleService>();
builder.Services.AddSingleton<IMenuService,MenuService>();
builder.Services.AddSingleton<IPermissionService,PermissionService>();
builder.Services.AddSingleton<IDesignationService, DesignationService>();
builder.Services.AddSingleton<IRoleManager,RoleManager>();
builder.Services.AddSingleton<IContactManager, ContactManager>();
builder.Services.AddSingleton<ICountryManager, CountryManager>();

builder.Services.AddMemoryCache();

//Authentication
builder.Services.Configure<CookiePolicyOptions>(options =>
                {
                    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                })
                .Configure<CookieTempDataProviderOptions>(options => { options.Cookie.IsEssential = true; });

//builder.Services.Configure<SecurityStampValidatorOptions>(options => { options.ValidationInterval = TimeSpan.Zero; });
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

//new added code
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.Cookie.Name = "mycow20765577";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(300);
    options.LoginPath = "/Account/Login";
    // ReturnUrlParameter requires 
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.SlidingExpiration = true;
});

//builder.Services.AddAuthorization(config =>
//{
//    var userAuthPolicyBuilder = new AuthorizationPolicyBuilder();
//    config.DefaultPolicy = userAuthPolicyBuilder
//                        .RequireAuthenticatedUser()
//                        .RequireClaim(ClaimTypes.Role)
//                        .Build();
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
