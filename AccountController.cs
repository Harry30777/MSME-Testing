using Microsoft.AspNetCore.Mvc;
using MsmePortal.Models;
using MsmePortal.Services;

namespace MsmePortal.Controllers
{
    public class AccountController : Controller
    {
        private readonly PortalDataStore _dataStore;

        public AccountController(PortalDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        [HttpGet]
        [Route("")]
        [Route("auth/login")]
        [Route("Account/Login")]
        public IActionResult Login(string? next = null)
        {
            ViewBag.NextUrl = next;
            return View("~/Views/Account/Login.cshtml", new LoginViewModel());
        }

        [HttpPost]
        [Route("")]
        [Route("auth/login")]
        [Route("Account/Login")]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Account/Login.cshtml", model);
            }

            var auth = _dataStore.Authenticate(model.LoginId.Trim(), model.Password.Trim());

            if (auth.IsValid)
            {
                // Store user session info
                HttpContext.Session.SetString("UserRole", auth.Role.ToString());
                HttpContext.Session.SetString("UserName", auth.DisplayName);
                HttpContext.Session.SetString("AssignedOffice", auth.AssignedOffice);
                HttpContext.Session.SetString("LoginId", model.LoginId);

                TempData["SuccessMessage"] = $"स्वागत है, {auth.DisplayName}! [भूमिका: {auth.Role} | कार्यालय: {auth.AssignedOffice}]";

                switch (auth.Role)
                {
                    case UserRole.HO_User:
                        return RedirectToAction("Index", "Ho");
                    case UserRole.RO_User:
                        return RedirectToAction("Index", "Ro");
                    case UserRole.DTIC_User:
                        return RedirectToAction("Index", "Dtic");
                    default:
                        return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "अमान्य लॉगिन आईडी या पासवर्ड। (Invalid Login ID or Password)");
            return View("~/Views/Account/Login.cshtml", model);
        }

        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["InfoMessage"] = "आप सफलतापूर्वक लॉग आउट हो गए हैं।";
            return RedirectToAction("Login");
        }

        [HttpGet]
        [Route("health")]
        [Route("ping")]
        public IActionResult Health()
        {
            return Ok("Healthy");
        }
    }
}
