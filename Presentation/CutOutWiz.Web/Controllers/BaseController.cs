using CutOutWiz.Core.Utilities;
using CutOutWiz.Data.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CutOutWiz.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public LoginUserInfo CurrentLoggedInUser
        {
            get
            {
                return GetCurrentLoggedInUser();
            }
        }

        // <summary>
        /// Gets or sets the current logged in user.
        /// </summary>
        /// <value>
        /// The current logged in user.
        /// </value>
        public LoginUserInfo GetCurrentLoggedInUser()
        {
            var loginUserInfo = new LoginUserInfo();

            loginUserInfo.UserGuid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            loginUserInfo.UserId = Convert.ToInt32(User.FindFirstValue(ClaimTypesConstants.UserId));
            loginUserInfo.ContactId = Convert.ToInt32(User.FindFirstValue(ClaimTypesConstants.ContactId));
            loginUserInfo.RoleName = User.FindFirstValue(ClaimTypes.Role);
            loginUserInfo.Username = User.FindFirstValue(ClaimTypes.Name);
            loginUserInfo.FirstName = User.FindFirstValue(ClaimTypes.GivenName);
            loginUserInfo.CompanyId = Convert.ToInt32(User.FindFirstValue(ClaimTypesConstants.CompanyId));
            loginUserInfo.ClientRootFolderPath = User.FindFirstValue(ClaimTypesConstants.ClientRootFolderPath);

            return loginUserInfo;
        }
    }
}
