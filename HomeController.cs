using Microsoft.AspNetCore.Mvc;
using MsmePortal.Models;
using MsmePortal.Services;
using System.Diagnostics;

namespace MsmePortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly PortalDataStore _dataStore;

        public HomeController(PortalDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public IActionResult Index()
        {
            var userName = HttpContext.Session.GetString("UserName") ?? "सामान्य आवेदक (General User)";
            ViewBag.UserName = userName;
            ViewBag.Applications = _dataStore.GetApplicationsForUser(userName);
            ViewBag.Dtics = _dataStore.GetDtics();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ApplyScheme(string SchemeName, string DistrictName, decimal LoanAmount)
        {
            var userName = HttpContext.Session.GetString("UserName") ?? "सामान्य आवेदक (General User)";

            if (LoanAmount > 0 && !string.IsNullOrEmpty(SchemeName))
            {
                var newApp = new DistrictApplicationModel
                {
                    ApplicantName = userName,
                    SchemeName = SchemeName,
                    DistrictName = DistrictName ?? "भोपाल DTIC कार्यालय",
                    LoanAmount = LoanAmount,
                    Status = "सत्यापन हेतु लंबित (Pending Verification)",
                    AppliedDate = DateTime.Now
                };

                _dataStore.AddDistrictApplication(newApp);
                TempData["SuccessMessage"] = $"आपका '{SchemeName}' हेतु ₹{LoanAmount:N0} का आवेदन (सं: {newApp.ApplicationNo}) सफलतापूर्वक जमा कर दिया गया है।";
            }

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewBag.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
