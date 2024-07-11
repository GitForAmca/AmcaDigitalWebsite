using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Data;

namespace AMCA_IT_SOLUTIONS.Models
{
    public class ValidationModel
    {
        public int AutoId { get; set; }
        public int OpCode { get; set; }
        public string ServiceType { get; set; }
        public string CompanyName { get; set; }
        public string ConcernPerson { get; set; }
        public string EmailId { get; set; }
        public string TradeLicenseAuthority { get; set; }
        public string ServiceList { get; set; }
        public string AboutTamca { get; set; }
        public string CountryCode { get; set; }
        public string ContactNumber { get; set; }
        public DataTable dt { get; set; }
        public string MessageContact { get; set; }
        public int ServiceID { get; set; }
        public string txtPageName { get; set; }
        public string ServiceName { get; set; }
        public object AboutJob { get; set; }
        public object JobTitle { get; set; }
        public object CandidateName { get; set; }
        public object MobileNo { get; set; }
        public object Nationality { get; set; }
        public object DoB { get; set; }
        public object Gender { get; set; }
        public object MaritalStatus { get; set; }
        public object Experience { get; set; }
        public object UAEExperience { get; set; }
        public object LastDesignation { get; set; }
        public object CurrentLocation { get; set; }
        
        public object SalaryExpectation { get; set; }
        public object LastSalaryDrawn { get; set; }

        public object NoticePeriod { get; set; }
        
        public object VisaType { get; set; }
        public object HighestQualification { get; set; }
        public string CV { get; set; }
        public object exceptionMessage { get; set; }
        public bool isException { get; set; }

        public string Usercomment { get; set; }
        
        public string BlogTitle { get; set; }

        public string file { get; set; }
        public HttpPostedFileBase PostedFile { get; set; }


    }
}