using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using app;
using app.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace app.api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("Account")]
    public class AccountController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return new ChallengeResult("Auth0", new AuthenticationProperties() { RedirectUri = "/" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("Logout")]
        [HttpGet]
        public async Task Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Auth0", new AuthenticationProperties
            {
            });
            await HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("AccessDenied")]
        [HttpGet]
        public string AccessDenied()
        {
            return "Access Denied";
        }
    }
}

