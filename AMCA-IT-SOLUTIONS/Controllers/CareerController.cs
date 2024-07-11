using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Data;
using AMCA_IT_SOLUTIONS.Models;
using System.Net.Mail;
using CaptchaMvc.HtmlHelpers;

namespace AMCA_IT_SOLUTIONS.Controllers
{
    public class CareerController : Controller
    {
        [NonAction]
        public SelectList ToSelectList(DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row[textField].ToString(),
                    Value = row[valueField].ToString()
                });
            }
            return new SelectList(list, "Value", "Text");
        }
        [NonAction]
        public string SendMail(string FromMailID, string fromEmailPassword, string ToMailID, string CC, string BCC, string subject, string body, string servername, int PortNo, bool ssl)
        {
            servername = "smtp-relay.sendinblue.com"; PortNo = 587; ssl = false; FromMailID = "notification@amca.ae"; fromEmailPassword = "4J7UwO5p2VDzG8Nq";

            string msg = string.Empty;
            MailMessage MailMsg = new MailMessage();
            try
            {
                if (ToMailID.EndsWith(","))
                {
                    ToMailID = ToMailID.Substring(0, ToMailID.Length - 1);
                }
                if (CC.EndsWith(","))
                {
                    CC = CC.Substring(0, CC.Length - 1);

                }
                if (BCC.EndsWith(","))
                {
                    BCC = BCC.Substring(0, BCC.Length - 1);

                }

                MailMsg.To.Add(ToMailID);
                if (CC != "")
                {
                    MailMsg.CC.Add(CC);
                }
                if (BCC != "")
                {
                    MailMsg.Bcc.Add(BCC);
                }

                //=================
                MailMsg.From = new MailAddress(FromMailID);
                MailMsg.Subject = subject;
                MailMsg.IsBodyHtml = true;
                MailMsg.Body = body;

                SmtpClient tempsmtp = new SmtpClient();
                tempsmtp.Host = servername;
                tempsmtp.Port = PortNo;
                tempsmtp.Credentials = new System.Net.NetworkCredential(FromMailID, fromEmailPassword);
                tempsmtp.EnableSsl = ssl;

                tempsmtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                tempsmtp.Send(MailMsg);
                msg = "Successful";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
        [HttpPost]
        public ActionResult SubmitJob(Validation job)
        {
            TempData["Job"] = job;
            return RedirectToAction("ApplyJobs", "Career");
        }
        // GET: Career
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("apply-job")]
        public ActionResult ApplyJobs()
        {
            ValidationModel PL = new ValidationModel();
            Validation job = TempData["Job"] as Validation;

            //Select Country Code
            PL.OpCode = 3;
            ReturnDataModel.returnTable(PL);
            ViewBag.CountryList = ToSelectList(PL.dt, "CountryISDCode", "CountryName");

            PL.OpCode = 8;
            ReturnDataModel.returnTable(PL);
            ViewBag.Nationality = ToSelectList(PL.dt, "CountryID", "CountryName");

            PL.OpCode = 8;
            ReturnDataModel.returnTable(PL);
            ViewBag.CurrentLocation = ToSelectList(PL.dt, "CountryID", "CountryName");

            PL.OpCode = 9;
            ReturnDataModel.returnTable(PL);
            ViewBag.AboutJob = ToSelectList(PL.dt, "AutoId", "AboutJob");

            PL.OpCode = 9;
            ReturnDataModel.returnTable(PL);
            ViewBag.AboutJob = ToSelectList(PL.dt, "AutoId", "AboutJob");

            return View(job);
        }
        [HttpPost]
        public ActionResult RequestJob(string AboutJob, string JobTitle, string CandidateName, string EmailId, string MobileNo, string Nationality, DateTime DoB, string Gender, string MaritalStatus, string Experience, string UAEExperience, string LastDesignation, string CurrentLocation, string SalaryExpectation, string LastSalaryDrawn, string NoticePeriod, string VisaType, HttpPostedFileBase PostedFile, string CountryCode, string HighestQualification, Validation model)
        {
            
            if (!this.IsCaptchaValid(errorText: ""))
            {
                ViewBag.ErrorMessage = "Captacha is not valid";
                TempData["Job"] = JobTitle;
                ApplyJobs();
                return View("ApplyJobs");
            }
            else
            {
                try
                {
                    if (PostedFile.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileNameWithoutExtension(PostedFile.FileName);
                        string extension = Path.GetExtension(PostedFile.FileName);
                        _FileName = _FileName + DateTime.Now.ToString("yymmddssfff") + extension;
                        string relativeFileName = "CandidateCV/" + _FileName;
                        _FileName = Path.Combine(Server.MapPath("~/CandidateCV/"), _FileName);
                        PostedFile.SaveAs(_FileName);

                        ValidationModel PL = new ValidationModel();

                        PL.OpCode = 1;
                        PL.AboutJob = AboutJob;
                        PL.JobTitle = JobTitle;
                        PL.CandidateName = CandidateName;
                        PL.EmailId = EmailId;
                        PL.MobileNo = MobileNo;
                        PL.Nationality = Nationality;
                        PL.DoB = DoB;
                        PL.Gender = Gender;
                        PL.MaritalStatus = MaritalStatus;
                        PL.Experience = Experience;
                        PL.UAEExperience = UAEExperience;
                        PL.LastDesignation = LastDesignation;
                        PL.CurrentLocation = CurrentLocation;
                        PL.SalaryExpectation = SalaryExpectation;
                        PL.LastSalaryDrawn = LastSalaryDrawn;
                        PL.NoticePeriod = NoticePeriod;
                        PL.CountryCode = CountryCode;
                        PL.HighestQualification = HighestQualification;
                        PL.VisaType = VisaType;
                        PL.CV = relativeFileName;
                        CareerReturnDataModel.returnTable(PL);

                        if (PL.dt.Rows.Count > 0)
                        {
                            string Autoid = PL.dt.Rows[0]["Autoid"].ToString();
                            PL.OpCode = 2;
                            PL.AutoId = Convert.ToInt32(Autoid);
                            CareerReturnDataModel.returnTable(PL);

                            /// Select Candidate Id
                            PL.OpCode = 3;
                            PL.AutoId = Convert.ToInt32(Autoid);
                            CareerReturnDataModel.returnTable(PL);
                            DataTable dt = PL.dt;
                            TempData["Id"] = PL.dt.Rows[0]["CandidateId"].ToString();
                            TempData["Name"] = PL.dt.Rows[0]["CandidateName"].ToString();
                            TempData["ApplyJob"] = PL.dt.Rows[0]["JobTitle"].ToString();

                            //Sending Mail
                            //var mail = "<p>Dear HR,</p>";
                            //mail += "<p>Please go thru the link: <a href='https://portal.amca.ae'>AMCA Portal</a> and find the below candidate details.</p>";

                            //mail += "Candidate Id: " + PL.dt.Rows[0]["CandidateId"].ToString();
                            //mail += "<br>Candidate Name: " + PL.dt.Rows[0]["CandidateName"].ToString();
                            //mail += "<br>Job Position: " + PL.dt.Rows[0]["JobTitle"].ToString();
                            //mail += "<p>Thank you!</p>";
                            //mail += "<p>Regards,<br>AMCA</p>";

                            //var msg = SendMail("careers@amca.ae", "Careers@1244.ae", "hr@amcaauditing.com", "info@amcaauditing.com", "", "Job Application", mail, "win1.server.ae", 465, true);

                            return RedirectToAction("SuccessCareer", "Home");
                        }
                    }
                    return RedirectToAction("SuccessCareer", "Home");
                }
                catch
                {
                    ViewBag.Message = "File upload failed!!";
                    return View();
                }

            }

        }
        [HttpPost]
        public ActionResult SubscribForm(string EmailId, Validation model)
        {

            ValidationModel PL = new ValidationModel();
            PL.OpCode = 13;
            PL.EmailId = EmailId;
            ReturnDataModel.returnTable(PL);
            DataTable dt = PL.dt;
            ModelState.Clear();
            if (PL.dt.Rows.Count == 0)
            {
                PL.OpCode = 12;
                PL.EmailId = EmailId;
                ReturnDataModel.returnTable(PL);

                ////Sending Mail
                //var body = "";
                //body += "<p style='margin-top: 0px'>Dear TAMCA Team,</p>";
                //body += "<p style='margin-top: 2px'>He wants our daily Newsletter, please save the below Email id in your Newsletter list.</p>";
                //body += " <table width='600' border='1' cellpadding='5' cellspacing='0'> " +
                //    "<tr> <td> <strong> Email Id </strong></td> <td>" + EmailId + " </td></tr> " +
                //    "</table>";
                ////var msg = "";
                // var msg = new clsgeneral().SendMail("notification@amca.ae", "4J7UwO5p2VDzG8Nq", "crm@amcaauditing.com", "editor@amcaauditing.com", "", "New Subscriber", body, "smtp-relay.sendinblue.com", 587, true);

                return Json(new { isok = true, message = "Thank you! We will back soon." });
            }
            else
            {
                return Json(new { isok = false, message = EmailId + " :Already exist." });
            }
        }
    }
}