using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AMCA_IT_SOLUTIONS.Models
{
    public class Validation
    {
        
        [Required(ErrorMessage = "Enter Company Name")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Full Name")]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$")]
        [Required(ErrorMessage = "Enter Your Name")]
        public string ConcernPerson { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Enter Email Address")]
        [EmailAddress(ErrorMessage = "Please Enter Valid Email Id")]
        public string EmailId { get; set; }

        [Display(Name = "Trade License Authority")]
        [Required(ErrorMessage = "Please select any")]
        public string TradeLicenseAuthority { get; set; }

        [Display(Name = "Services")]
        [Required(ErrorMessage = "Select Service")]
        public string ServiceList { get; set; }

        [Display(Name = "Where did you here about Tamca?")]
        [Required(ErrorMessage = "Please Select Any one")]
        public string AboutTamca { get; set; }

        [Required(ErrorMessage = "Select Country Code")]
        [Display(Name = "Country Code")]
        public string CountryCode { get; set; }

        [Display(Name = "Contact Number")]
        [Required(ErrorMessage = "Contact Number is required.")]
        [MaxLength(10, ErrorMessage = "Cannot be longer than 10 characters")]
        [MinLength(5, ErrorMessage = "Cannot be less than 5 characters")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Invalid Mobile Number")]
        public string ContactNumber { get; set; }

        [Display(Name = "Message")]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$")]
        public string MessageContact { get; set; }

        [Display(Name = "Comment")]
        [Required(ErrorMessage = "Please give your Suitable Comments")]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$")]
        public string Usercomment { get; set; }

        public int AutoId { get; set; }
        public int OpCode { get; set; }
        public string JobTitle { get; set; }
        public string CandidateId { get; set; }

        [Required(ErrorMessage = "Please select Job")]
        public string reSelectJob { get; set; }

        [Required(ErrorMessage = "Please select one")]
        public string AboutJob { get; set; }

        [Required(ErrorMessage = "Please Enter your Name")]
        public string CandidateName { get; set; }

        [Required(ErrorMessage = "Choose Your DOB")]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DoB { get; set; }


        [Required(ErrorMessage = "Please select one")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "Please select one")]
        public string Experience { get; set; }

        [Required(ErrorMessage = "Please select one")]
        public string UAEExperience { get; set; }

        public string LastDesignation { get; set; }

        [Required(ErrorMessage = "Please Enter your Location")]
        public string CurrentLocation { get; set; }

        [Required(ErrorMessage = "Please Select your Criteria")]
        public string SalaryExpectation { get; set; }

        public string LastSalaryDrawn { get; set; }

        [Required(ErrorMessage = "Please Enter Your Mobile No")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Select Your Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Select Your Notice Period")]
        public string NoticePeriod { get; set; }

        [Required(ErrorMessage = "Select your VISA type")]
        public string VisaType { get; set; }

        [Required(ErrorMessage = "Select Marital Status")]
        public string MaritalStatus { get; set; }
        [Required(ErrorMessage = "Select Marital Status")]
        public string HighestQualification { get; set; }

        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only Image files allowed.")]
        [Required(ErrorMessage = "Please Upload CV")]
        public string CV { get; set; }

        public string file { get; set; }

        [Required(ErrorMessage = "Please select file.")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.pdf)$", ErrorMessage = "Only PDF files Allowed")]
        public HttpPostedFileBase PostedFile { get; set; }


    }
}