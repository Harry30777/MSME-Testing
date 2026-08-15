using Microsoft.AspNetCore.Mvc;
using MsmePortal.Models;
using MsmePortal.Services;

namespace MsmePortal.Controllers
{
    public class HoController : Controller
    {
        private readonly PortalDataStore _dataStore;

        public HoController(PortalDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        private bool IsHoAdmin()
        {
            var role = HttpContext.Session.GetString("UserRole");
            return role == "HO_User";
        }

        // HO Dashboard
        public IActionResult Index()
        {
            if (!IsHoAdmin())
            {
                var role = HttpContext.Session.GetString("UserRole");
                if (role == "RO_User") return RedirectToAction("Index", "Ro");
                if (role == "DTIC_User") return RedirectToAction("Index", "Dtic");
                return RedirectToAction("Login", "Account");
            }

            ViewBag.RoCount = _dataStore.GetRos().Count;
            ViewBag.DticCount = _dataStore.GetDtics().Count;
            ViewBag.SectionCount = _dataStore.GetSections().Count;
            ViewBag.UserCount = _dataStore.GetUsers().Count;
            ViewBag.SectionFiles = _dataStore.GetSectionFiles();

            return View();
        }

        // 1. Add RO
        [HttpGet]
        public IActionResult AddRo()
        {
            if (!IsHoAdmin()) return RedirectToAction("Index");

            ViewBag.RoList = _dataStore.GetRos();
            ViewBag.ParentHeadOffices = new List<string> { "भोपाल मुख्य कार्यालय (Head Office)" };
            return View(new RoViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddRo(RoViewModel model)
        {
            if (!IsHoAdmin()) return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                _dataStore.AddRo(model);
                TempData["SuccessMessage"] = $"क्षेत्रीय कार्यालय (RO) '{model.Name}' (पैरेंट: {model.ParentOfficeName}) सफलतापूर्वक सहेजा गया।";
                return RedirectToAction("AddRo");
            }

            ViewBag.RoList = _dataStore.GetRos();
            ViewBag.ParentHeadOffices = new List<string> { "भोपाल मुख्य कार्यालय (Head Office)" };
            return View(model);
        }

        // 2. Add DTIC
        [HttpGet]
        public IActionResult AddDtic()
        {
            if (!IsHoAdmin()) return RedirectToAction("Index");

            ViewBag.DticList = _dataStore.GetDtics();
            ViewBag.ParentRos = _dataStore.GetRos().Select(r => r.Name).ToList();
            return View(new DticViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDtic(DticViewModel model)
        {
            if (!IsHoAdmin()) return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                _dataStore.AddDtic(model);
                TempData["SuccessMessage"] = $"जिला केंद्र (DTIC) '{model.Name}' (पैरेंट RO: {model.ParentOfficeName}) सफलतापूर्वक सहेजा गया।";
                return RedirectToAction("AddDtic");
            }

            ViewBag.DticList = _dataStore.GetDtics();
            ViewBag.ParentRos = _dataStore.GetRos().Select(r => r.Name).ToList();
            return View(model);
        }

        // 3. Add Section
        [HttpGet]
        public IActionResult AddSection()
        {
            if (!IsHoAdmin()) return RedirectToAction("Index");

            ViewBag.SectionList = _dataStore.GetSections();
            ViewBag.SectionFiles = _dataStore.GetSectionFiles();
            ViewBag.ParentHeadOffices = new List<string> { "भोपाल मुख्य कार्यालय (Head Office)" };
            return View(new SectionViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSection(SectionViewModel model)
        {
            if (!IsHoAdmin()) return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                _dataStore.AddSection(model);
                TempData["SuccessMessage"] = $"अनुभाग (Section) '{model.Name}' (पैरेंट: {model.ParentOfficeName}) सफलतापूर्वक सहेजा गया।";
                return RedirectToAction("AddSection");
            }

            ViewBag.SectionList = _dataStore.GetSections();
            ViewBag.SectionFiles = _dataStore.GetSectionFiles();
            ViewBag.ParentHeadOffices = new List<string> { "भोपाल मुख्य कार्यालय (Head Office)" };
            return View(model);
        }

        // 4. Add User
        [HttpGet]
        public IActionResult AddUser()
        {
            if (!IsHoAdmin()) return RedirectToAction("Index");

            ViewBag.UserList = _dataStore.GetUsers();
            ViewBag.OfficeLocations = _dataStore.GetAllOfficeLocations();
            return View(new UserViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUser(UserViewModel model)
        {
            if (!IsHoAdmin()) return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                _dataStore.AddUser(model);
                TempData["SuccessMessage"] = $"उपयोगकर्ता '{model.Name}' [{model.Role}] पदस्थापित: {model.AssignedOfficeName} सफलतापूर्वक सहेजा गया।";
                return RedirectToAction("AddUser");
            }

            ViewBag.UserList = _dataStore.GetUsers();
            ViewBag.OfficeLocations = _dataStore.GetAllOfficeLocations();
            return View(model);
        }
    }
}
