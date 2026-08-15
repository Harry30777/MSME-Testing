using System.ComponentModel.DataAnnotations;

namespace MsmePortal.Models
{
    public enum UserRole
    {
        [Display(Name = "HO User (मुख्यालय/अनुभाग कर्मचारी)")]
        HO_User,

        [Display(Name = "RO User (क्षेत्रीय प्रबंधक/अधिकारी)")]
        RO_User,

        [Display(Name = "DTIC User (जिला अधिकारी/कर्मचारी)")]
        DTIC_User,

        [Display(Name = "General User (आवेदक)")]
        User
    }

    public enum OfficeLevel
    {
        HO,
        RO,
        DTIC,
        Section
    }

    public class EntityBaseViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "नाम आवश्यक है (Name is required)")]
        [Display(Name = "नाम (Name)")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "पता आवश्यक है (Address is required)")]
        [Display(Name = "पता (Address)")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "संपर्क नंबर आवश्यक है (Contact No is required)")]
        [Phone(ErrorMessage = "अमान्य फोन नंबर (Invalid Phone Number)")]
        [Display(Name = "संपर्क (Contact)")]
        public string Contact { get; set; } = string.Empty;

        [Required(ErrorMessage = "ईमेल आईडी आवश्यक है (Email ID is required)")]
        [EmailAddress(ErrorMessage = "अमान्य ईमेल आईडी (Invalid Email ID)")]
        [Display(Name = "ईमेल आईडी (Email ID)")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "GST नंबर आवश्यक है (GST No is required)")]
        [Display(Name = "GST नंबर (GST No)")]
        public string GstNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "पासवर्ड आवश्यक है (Password is required)")]
        [DataType(DataType.Password)]
        [Display(Name = "पासवर्ड (Password)")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "पैरेंट कार्यालय (Parent Office)")]
        public string ParentOfficeName { get; set; } = "भोपाल मुख्य कार्यालय (HO)";
        public int ParentOfficeId { get; set; } = 1;

        public OfficeLevel Level { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class RoViewModel : EntityBaseViewModel
    {
        public string Code { get; set; } = "RO-" + Random.Shared.Next(100, 999);
        public RoViewModel()
        {
            Level = OfficeLevel.RO;
            ParentOfficeName = "भोपाल मुख्य कार्यालय (Head Office)";
        }
    }

    public class DticViewModel : EntityBaseViewModel
    {
        public string Code { get; set; } = "DTIC-" + Random.Shared.Next(100, 999);
        public DticViewModel()
        {
            Level = OfficeLevel.DTIC;
        }
    }

    public class SectionViewModel : EntityBaseViewModel
    {
        public string Code { get; set; } = "SEC-" + Random.Shared.Next(100, 999);
        public SectionViewModel()
        {
            Level = OfficeLevel.Section;
            ParentOfficeName = "भोपाल मुख्य कार्यालय (Head Office)";
        }
    }

    public class UserViewModel : EntityBaseViewModel
    {
        public string Code { get; set; } = "USR-" + Random.Shared.Next(1000, 9999);

        [Required(ErrorMessage = "भूमिका चुनना अनिवार्य है (Role is required)")]
        [Display(Name = "भूमिका (Role)")]
        public UserRole Role { get; set; } = UserRole.HO_User;

        [Required(ErrorMessage = "कार्यालय स्थान का चयन अनिवार्य है (Office Location Mapping is required)")]
        [Display(Name = "पदस्थापित कार्यालय (Assigned Office Location)")]
        public string AssignedOfficeName { get; set; } = "भोपाल बजट अनुभाग (Section)";
        public int AssignedOfficeId { get; set; } = 1;
    }

    // --- Domain Sub-Modules inside Section, RO, and DTIC ---

    // 1. Section Internal File Processing
    public class SectionFileModel
    {
        public int Id { get; set; }
        public string FileNo { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string SectionName { get; set; } = string.Empty;
        public string Status { get; set; } = "स्वीकृत / Approved"; // Approved, Pending, Under Review
        public decimal BudgetAmount { get; set; }
        public string AssignedOfficer { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }

    // 2. Regional Office Industrial Inspection & Grant Model
    public class RegionalInspectionModel
    {
        public int Id { get; set; }
        public string InspectionCode { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;
        public string DticName { get; set; } = string.Empty;
        public string RegionalOffice { get; set; } = string.Empty;
        public string InspectorOfficer { get; set; } = string.Empty;
        public string Status { get; set; } = "सत्यापन पूर्ण (Passed)"; // Passed, Pending
        public decimal VerifiedGrantAmount { get; set; }
        public DateTime InspectionDate { get; set; } = DateTime.Now;
    }

    // 3. District Office Entrepreneur Loan & Subsidy Application
    public class DistrictApplicationModel
    {
        public int Id { get; set; }
        public string ApplicationNo { get; set; } = string.Empty;
        public string ApplicantName { get; set; } = string.Empty;
        public string SchemeName { get; set; } = string.Empty; // PMEGP, Mukhyamantri Udhyam Kranti
        public string DistrictName { get; set; } = string.Empty;
        public decimal LoanAmount { get; set; }
        public string Status { get; set; } = "संवितरित / Disbursed"; // Disbursed, Pending Verification
        public DateTime AppliedDate { get; set; } = DateTime.Now;
    }
}
