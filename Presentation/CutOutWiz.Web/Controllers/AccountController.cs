using CutOutWiz.Core.Utilities;
using CutOutWiz.Data;
using CutOutWiz.Services.DataService;
using CutOutWiz.Services.LogService;
using CutOutWiz.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CutOutWiz.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogService _logger;
        private readonly IUserService _userService;
        private readonly ICompanyService _companyService;
        public AccountController(ILogService logger, IUserService userService,ICompanyService companyService)
        {
            _userService = userService;
            _companyService = companyService;
            _logger = logger;
        }

        //https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-6.0
        public IActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Password))
            {   
                if (model is null)
                {
                    model = new LoginViewModel();
                }

                model.ErrorMessage = "Invalid Credentials. Please Try Again.";
                return View(model);
            }

            var user = await _userService.GetUserByUsername(model.UserName);

            if (user == null )
            {
                model.ErrorMessage = "Invalid Credentials. Please Try Again.";
                return View(model);
            }

            if (user.PasswordHash != _userService.GetHashPassword(model.Password,user.PasswordSalt))
            {
                model.ErrorMessage = "Invalid Credentials. Please Try Again.";
                return View(model);
            }

            _logger.Log($"User {model.UserName} logged in");

            await SetCookie(user, model.RememberMe);

            return RedirectToAction("Index", "Home");          
        }

        public async Task<IActionResult> Logout()
        {
            _logger.Log($"User {HttpContext.User.Identity.Name} logged out");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        private async Task<bool> SetCookie(UserModel user, bool isPersistent)
        {
            var loginUserInfo = await _userService.GetLoginUserInfo(user.UserGuid, false);

            if (loginUserInfo is null)
            {
                return false;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserGuid),
                new Claim(ClaimTypesConstants.UserId, user.Id.ToString()),
                new Claim(ClaimTypesConstants.ContactId, user.ContactId.ToString()),
                new Claim(ClaimTypes.Role, loginUserInfo.RoleName),
                new Claim(ClaimTypes.Name, loginUserInfo.Username),
                new Claim(ClaimTypes.GivenName, loginUserInfo.FirstName),
                new Claim(ClaimTypesConstants.CompanyId, Convert.ToString(loginUserInfo.CompanyId ?? 0)),
                new Claim(ClaimTypesConstants.ClientRootFolderPath, loginUserInfo.ClientRootFolderPath)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
         
            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(300),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                IsPersistent = isPersistent,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return true;
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            return View(await _userService.GetAllUserDetails());
        }

        public async Task<IActionResult> Edit(string username)
        {
            var user = _userService.GetUserByUsernameForUpdate(username);
            ViewBag.companies = await _userService.GetAllCompany();
            ViewBag.roles = await _userService.GetAllRoles();
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return View(user.Result);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            var updateUser = _userService.UpdateUser(model);
            if (updateUser == null)
            {
                ViewBag.Message = "Update Failed ..!";
                return View(updateUser);
            }
            return RedirectToAction(nameof(UserList));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.companies = await _userService.GetAllCompany();
            ViewBag.roles =await _userService.GetAllRoles();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(IFormCollection collection)
        {
            try
            {
                var userName = collection["username"].ToString();
                var userFromDb = await _userService.GetUserByUsername(userName);

                if(userFromDb != null)
                {
                    ViewBag.companies = await _userService.GetAllCompany();
                    ViewBag.roles = await _userService.GetAllRoles();
                    ViewBag.Message = "User already exist! Please try with another";
                    return View();
                }
                if (collection["password"].ToString() != collection["confirm-password"].ToString())
                {
                    ViewBag.companies = await _userService.GetAllCompany();
                    ViewBag.roles = await _userService.GetAllRoles();
                    ViewBag.Message = "Confirm Password not Matched ..!";
                    return View();
                }
                var contact = new Contact
                {
                    FirstName = collection["firstname"].ToString(),
                    LastName = collection["lastname"].ToString(),
                    Phone = collection["phone"].ToString(),
                    Email = collection["email"].ToString(),
                    CompanyId =Convert.ToInt32(collection["company"]),
                    ContactGuid = Guid.NewGuid().ToString(),
                };
                int contactId = await _userService.InsertContact(contact);
                string salt = "";
                while (true)
                {
                    salt = _userService.CreateRandomSalt(); // its ok to use this to regenerate token
                    if (salt.Contains('+') || salt.Contains('/'))
                        continue;
                    else
                        break;
                }

                var PasswordSalt = salt;
                var PasswordHash = _userService.GetHashPassword(collection["password"].ToString(), salt);

                var user = new UserModel
                {
                    ContactId = contactId,
                    RoleId = Convert.ToInt32(collection["role"]),
                    Username = collection["username"].ToString(),
                    UserGuid = Guid.NewGuid().ToString(),
                    PasswordHash = PasswordHash,
                    PasswordSalt = PasswordSalt,
                    Status = StatusConstants.Active

                };
                await _userService.InsertUser(user);
                return RedirectToAction(nameof(Create));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> AccessDenied(string ReturnUrl)
        {
            return View();
        }
       
    }
}
