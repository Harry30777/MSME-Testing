using Microsoft.AspNetCore.Mvc;
using MsmePortal.Services;

namespace MsmePortal.Controllers
{
    public class DticController : Controller
    {
        private readonly PortalDataStore _dataStore;

        public DticController(PortalDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("UserRole");
            var assignedOffice = HttpContext.Session.GetString("AssignedOffice") ?? "भोपाल DTIC कार्यालय";

            if (role != "DTIC_User" && role != "RO_User" && role != "HO_User")
            {
                TempData["InfoMessage"] = "केवल DTIC जिला अधिकारियों को इस पृष्ठ तक पहुंच की अनुमति है।";
                return RedirectToAction("Login", "Account");
            }

            ViewBag.AssignedOffice = assignedOffice;
            ViewBag.Users = _dataStore.GetUsersForDtic(assignedOffice);
            ViewBag.Applications = _dataStore.GetDticApplications(assignedOffice);

            return View();
        }
    }
}
