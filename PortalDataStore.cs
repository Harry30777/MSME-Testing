using MsmePortal.Models;

namespace MsmePortal.Services
{
    public class PortalDataStore
    {
        private static readonly List<RoViewModel> _ros = new();
        private static readonly List<DticViewModel> _dtics = new();
        private static readonly List<SectionViewModel> _sections = new();
        private static readonly List<UserViewModel> _users = new();

        // Domain Sub-Module datasets
        private static readonly List<SectionFileModel> _sectionFiles = new();
        private static readonly List<RegionalInspectionModel> _roInspections = new();
        private static readonly List<DistrictApplicationModel> _dticApplications = new();

        static PortalDataStore()
        {
            // 1. Seed ROs (Parent: Head Office)
            _ros.Add(new RoViewModel
            {
                Id = 1,
                Name = "भोपाल क्षेत्रीय कार्यालय",
                Address = "एम.पी. नगर जोन-1, भोपाल",
                Contact = "0755-2551234",
                Email = "ro.bhopal@mpmsme.gov.in",
                GstNo = "23AAAAA0000A1Z5",
                Password = "ro123",
                Code = "RO-101",
                ParentOfficeName = "भोपाल मुख्य कार्यालय (Head Office)"
            });

            _ros.Add(new RoViewModel
            {
                Id = 2,
                Name = "इंदौर क्षेत्रीय कार्यालय",
                Address = "प्रेस कॉम्प्लेक्स, इंदौर",
                Contact = "0731-2445678",
                Email = "ro.indore@mpmsme.gov.in",
                GstNo = "23BBBBB0000B1Z6",
                Password = "ro123",
                Code = "RO-102",
                ParentOfficeName = "भोपाल मुख्य कार्यालय (Head Office)"
            });

            // 2. Seed Sections (Parent: Head Office)
            _sections.Add(new SectionViewModel
            {
                Id = 1,
                Name = "भोपाल बजट अनुभाग",
                Address = "उद्योग भवन, भोपाल",
                Contact = "0755-2771122",
                Email = "sec.budget@mpmsme.gov.in",
                GstNo = "23DDDDD0000D1Z8",
                Password = "sec123",
                Code = "SEC-301",
                ParentOfficeName = "भोपाल मुख्य कार्यालय (Head Office)"
            });

            // 3. Seed DTICs (Parent: Regional Office)
            _dtics.Add(new DticViewModel
            {
                Id = 1,
                Name = "भोपाल DTIC कार्यालय",
                Address = "गोविंदपुरा इंडस्ट्रियल एरिया, भोपाल",
                Contact = "0755-2667890",
                Email = "dtic.bhopal@mpmsme.gov.in",
                GstNo = "23CCCCC0000C1Z7",
                Password = "dtic123",
                Code = "DTIC-201",
                ParentOfficeName = "भोपाल क्षेत्रीय कार्यालय"
            });

            _dtics.Add(new DticViewModel
            {
                Id = 2,
                Name = "इंदौर DTIC कार्यालय",
                Address = "सांवेर रोड इंडस्ट्रियल एरिया, इंदौर",
                Contact = "0731-2559988",
                Email = "dtic.indore@mpmsme.gov.in",
                GstNo = "23HHHHH0000H1Z2",
                Password = "dtic123",
                Code = "DTIC-202",
                ParentOfficeName = "इंदौर क्षेत्रीय कार्यालय"
            });

            // 4. Seed Users (Mapped to Assigned Office Locations)
            _users.Add(new UserViewModel
            {
                Id = 1,
                Name = "अनिल शर्मा (Section Babu)",
                Address = "न्यू मार्केट, भोपाल",
                Contact = "9876543210",
                Email = "anil@mpmsme.gov.in",
                GstNo = "23EEEEE0000E1Z9",
                Password = "ho123",
                Code = "USR-401",
                Role = UserRole.HO_User,
                AssignedOfficeName = "भोपाल बजट अनुभाग"
            });

            _users.Add(new UserViewModel
            {
                Id = 2,
                Name = "राजेश वर्मा (Regional Manager)",
                Address = "एम.पी. नगर, भोपाल",
                Contact = "9876543211",
                Email = "rajesh@mpmsme.gov.in",
                GstNo = "23FFFFF0000F1Z0",
                Password = "ro123",
                Code = "USR-402",
                Role = UserRole.RO_User,
                AssignedOfficeName = "भोपाल क्षेत्रीय कार्यालय"
            });

            _users.Add(new UserViewModel
            {
                Id = 3,
                Name = "सुरेश गुप्ता (District Officer - Bhopal)",
                Address = "गोविंदपुरा, भोपाल",
                Contact = "9876543212",
                Email = "suresh@mpmsme.gov.in",
                GstNo = "23GGGGG0000G1Z1",
                Password = "dtic123",
                Code = "USR-403",
                Role = UserRole.DTIC_User,
                AssignedOfficeName = "भोपाल DTIC कार्यालय"
            });

            _users.Add(new UserViewModel
            {
                Id = 4,
                Name = "विकास पटेल (District Officer - Indore)",
                Address = "विजयनगर, इंदौर",
                Contact = "9876543213",
                Email = "vikas@mpmsme.gov.in",
                GstNo = "23IIIII0000I1Z3",
                Password = "dtic123",
                Code = "USR-404",
                Role = UserRole.DTIC_User,
                AssignedOfficeName = "इंदौर DTIC कार्यालय"
            });

            _users.Add(new UserViewModel
            {
                Id = 5,
                Name = "सामान्य आवेदक (General User)",
                Address = "एम.पी. नगर, भोपाल",
                Contact = "9876500000",
                Email = "user@gmail.com",
                GstNo = "23JJJJJ0000J1Z4",
                Password = "user123",
                Code = "USR-405",
                Role = UserRole.User,
                AssignedOfficeName = "भोपाल DTIC कार्यालय"
            });

            // 5. Seed Section Files
            _sectionFiles.Add(new SectionFileModel
            {
                Id = 1,
                FileNo = "MSME/SEC/2026/081",
                Subject = "वर्ष 2026-27 हेतु MSME क्लस्टर विकास बजट आवंटन स्वीकृत",
                SectionName = "भोपाल बजट अनुभाग",
                Status = "स्वीकृत (Approved)",
                BudgetAmount = 25000000,
                AssignedOfficer = "अनिल शर्मा (Section Babu)",
                CreatedDate = DateTime.Now.AddDays(-5)
            });

            // 6. Seed RO Industrial Inspections
            _roInspections.Add(new RegionalInspectionModel
            {
                Id = 1,
                InspectionCode = "RO-INSP-501",
                UnitName = "महाकाल पॉलिमर्स लिमिटेड (गोविंदपुरा इंडस्ट्रियल एरिया)",
                DticName = "भोपाल DTIC कार्यालय",
                RegionalOffice = "भोपाल क्षेत्रीय कार्यालय",
                InspectorOfficer = "राजेश वर्मा (Regional Manager)",
                Status = "सत्यापन पूर्ण (Passed)",
                VerifiedGrantAmount = 1500000,
                InspectionDate = DateTime.Now.AddDays(-10)
            });

            // 7. Seed District DTIC Loan Applications
            _dticApplications.Add(new DistrictApplicationModel
            {
                Id = 1,
                ApplicationNo = "MP-UDHYAM-2026-108",
                ApplicantName = "सामान्य आवेदक (General User)",
                SchemeName = "मुख्यमंत्री उद्यम क्रांति योजना",
                DistrictName = "भोपाल DTIC कार्यालय",
                LoanAmount = 2500000,
                Status = "संवितरित (Disbursed)",
                AppliedDate = DateTime.Now.AddDays(-12)
            });
            _dticApplications.Add(new DistrictApplicationModel
            {
                Id = 2,
                ApplicationNo = "MP-PMEGP-2026-442",
                ApplicantName = "सामान्य आवेदक (General User)",
                SchemeName = "PMEGP ऋण सब्सिडी योजना",
                DistrictName = "भोपाल DTIC कार्यालय",
                LoanAmount = 1000000,
                Status = "सत्यापन हेतु लंबित (Pending Verification)",
                AppliedDate = DateTime.Now.AddDays(-4)
            });
        }

        public List<RoViewModel> GetRos() => _ros;
        public void AddRo(RoViewModel ro)
        {
            ro.Id = _ros.Count > 0 ? _ros.Max(r => r.Id) + 1 : 1;
            _ros.Add(ro);
        }

        public List<DticViewModel> GetDtics() => _dtics;
        public void AddDtic(DticViewModel dtic)
        {
            dtic.Id = _dtics.Count > 0 ? _dtics.Max(d => d.Id) + 1 : 1;
            _dtics.Add(dtic);
        }

        public List<SectionViewModel> GetSections() => _sections;
        public void AddSection(SectionViewModel sec)
        {
            sec.Id = _sections.Count > 0 ? _sections.Max(s => s.Id) + 1 : 1;
            _sections.Add(sec);
        }

        public List<UserViewModel> GetUsers() => _users;
        public void AddUser(UserViewModel user)
        {
            user.Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1;
            _users.Add(user);
        }

        // Domain Module Queries
        public List<SectionFileModel> GetSectionFiles() => _sectionFiles;

        public List<RegionalInspectionModel> GetRoInspections(string roName)
        {
            return _roInspections.Where(i => i.RegionalOffice.Contains(roName, StringComparison.OrdinalIgnoreCase) || roName.Contains(i.RegionalOffice, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<DistrictApplicationModel> GetDticApplications(string dticName)
        {
            return _dticApplications.Where(a => a.DistrictName.Contains(dticName, StringComparison.OrdinalIgnoreCase) || dticName.Contains(a.DistrictName, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<DistrictApplicationModel> GetApplicationsForUser(string userName)
        {
            return _dticApplications.Where(a => a.ApplicantName.Contains(userName, StringComparison.OrdinalIgnoreCase) || userName.Contains(a.ApplicantName, StringComparison.OrdinalIgnoreCase) || userName.Equals("सामान्य आवेदक (General User)", StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void AddDistrictApplication(DistrictApplicationModel app)
        {
            app.Id = _dticApplications.Count > 0 ? _dticApplications.Max(a => a.Id) + 1 : 1;
            app.ApplicationNo = "MP-APP-2026-" + Random.Shared.Next(500, 999);
            _dticApplications.Add(app);
        }

        // Scope-restricted queries for RO
        public List<DticViewModel> GetDticsForRo(string roName)
        {
            return _dtics.Where(d => d.ParentOfficeName.Contains(roName, StringComparison.OrdinalIgnoreCase) || roName.Contains(d.ParentOfficeName, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<UserViewModel> GetUsersForRo(string roName)
        {
            var dticNames = GetDticsForRo(roName).Select(d => d.Name).ToList();
            return _users.Where(u => u.AssignedOfficeName.Contains(roName, StringComparison.OrdinalIgnoreCase) || dticNames.Any(dn => u.AssignedOfficeName.Equals(dn, StringComparison.OrdinalIgnoreCase))).ToList();
        }

        // Scope-restricted query for DTIC
        public List<UserViewModel> GetUsersForDtic(string dticName)
        {
            return _users.Where(u => u.AssignedOfficeName.Contains(dticName, StringComparison.OrdinalIgnoreCase) || dticName.Contains(u.AssignedOfficeName, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<string> GetAllOfficeLocations()
        {
            var offices = new List<string>
            {
                "भोपाल मुख्य कार्यालय (Head Office)"
            };

            offices.AddRange(_sections.Select(s => s.Name));
            offices.AddRange(_ros.Select(r => r.Name));
            offices.AddRange(_dtics.Select(d => d.Name));

            return offices;
        }

        public (bool IsValid, UserRole Role, string DisplayName, string AssignedOffice) Authenticate(string loginId, string password)
        {
            // HO Admin default login
            if ((loginId.Equals("admin", StringComparison.OrdinalIgnoreCase) || loginId.Equals("ho", StringComparison.OrdinalIgnoreCase)) && password == "admin123")
            {
                return (true, UserRole.HO_User, "भोपाल मुख्य कार्यालय प्रशासक (HO Admin)", "भोपाल मुख्य कार्यालय (Head Office)");
            }

            // RO default login
            if (loginId.Equals("ro", StringComparison.OrdinalIgnoreCase) && password == "ro123")
            {
                return (true, UserRole.RO_User, "भोपाल क्षेत्रीय प्रबंधक (RO Officer)", "भोपाल क्षेत्रीय कार्यालय");
            }

            // DTIC default login
            if (loginId.Equals("dtic", StringComparison.OrdinalIgnoreCase) && password == "dtic123")
            {
                return (true, UserRole.DTIC_User, "भोपाल जिला अधिकारी (District Officer)", "भोपाल DTIC कार्यालय");
            }

            // User default login
            if (loginId.Equals("user", StringComparison.OrdinalIgnoreCase) && password == "user123")
            {
                return (true, UserRole.User, "सामान्य आवेदक (General User)", "भोपाल DTIC कार्यालय");
            }

            // Check dynamically registered user accounts
            var userMatch = _users.FirstOrDefault(u => (u.Email.Equals(loginId, StringComparison.OrdinalIgnoreCase) || u.Contact.Equals(loginId) || u.Code.Equals(loginId, StringComparison.OrdinalIgnoreCase)) && u.Password == password);
            if (userMatch != null)
            {
                return (true, userMatch.Role, userMatch.Name, userMatch.AssignedOfficeName);
            }

            return (false, UserRole.User, string.Empty, string.Empty);
        }
    }
}
