using Microsoft.AspNetCore.Mvc;
using MsmePortal.Services;

namespace MsmePortal.Controllers
{
    public class RoController : Controller
    {
        private readonly PortalDataStore _dataStore;

        public RoController(PortalDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("UserRole");
            var assignedOffice = HttpContext.Session.GetString("AssignedOffice") ?? "भोपाल क्षेत्रीय कार्यालय";

            if (role != "RO_User" && role != "HO_User")
            {
                TempData["InfoMessage"] = "केवल RO क्षेत्रीय अधिकारियों को इस पृष्ठ तक पहुंच की अनुमति है।";
                return RedirectToAction("Login", "Account");
            }

            ViewBag.AssignedOffice = assignedOffice;
            ViewBag.Dtics = _dataStore.GetDticsForRo(assignedOffice);
            ViewBag.Users = _dataStore.GetUsersForRo(assignedOffice);
            ViewBag.Inspections = _dataStore.GetRoInspections(assignedOffice);

            return View();
        }
    }
}
