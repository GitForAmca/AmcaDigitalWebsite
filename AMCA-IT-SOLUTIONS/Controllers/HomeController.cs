using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using AMCA_IT_SOLUTIONS.Models;
using System.Configuration;
using System.Net.Mail;
using CaptchaMvc.HtmlHelpers;

namespace AMCA_IT_SOLUTIONS.Controllers
{
    public class HomeController : Controller
    {
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

                ///tempsmtp.Send(MailMsg);
                msg = "Successful";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
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
        public void BindDropDown()
        {
            //Select Authority
            ValidationModel PL = new ValidationModel();

            //Select Country Code
            PL.OpCode = 3;
            ReturnDataModel.returnTable(PL);
            ViewBag.CountryList = ToSelectList(PL.dt, "CountryISDCode", "CountryName");

            PL.OpCode = 4;
            ReturnDataModel.returnTable(PL);
            ViewBag.ServiceList = ToSelectList(PL.dt, "ServiceName", "ServiceName");

            PL.OpCode = 5;
            ReturnDataModel.returnTable(PL);
            ViewBag.AboutTamca = ToSelectList(PL.dt, "AboutAMCA", "AboutAMCA");

            PL.OpCode = 7;
            ReturnDataModel.returnTable(PL);
            ViewBag.TradeLicenseAuthority = ToSelectList(PL.dt, "TradeLicenseAuthority", "TradeLicenseAuthority");

        }
        [HttpPost]
        public ActionResult QueryForm(string CompanyName, string ConcernPerson, string EmailId, string ServiceList, string AboutTamca, string CountryCode, string ContactNumber, string txtPageName, string TradeLicenseAuthority, Validation model)
        {
            var reqPageName = Session["txtPageName"].ToString();
            if (!this.IsCaptchaValid(errorText: ""))
            {
                BindDropDown();
                ViewBag.ErrorMessage = "Captacha is not valid";
                return View(reqPageName);

            }
            else
            {

                ValidationModel PL = new ValidationModel();
                PL.OpCode = 1;
                //PL.AutoId = Convert.ToInt32(AutoId);
                PL.CompanyName = CompanyName;
                PL.ConcernPerson = ConcernPerson;
                PL.EmailId = EmailId;
                PL.ServiceList = ServiceList;
                PL.AboutTamca = AboutTamca;
                PL.CountryCode = CountryCode;
                PL.ContactNumber = ContactNumber;
                PL.txtPageName = reqPageName;
                PL.TradeLicenseAuthority = TradeLicenseAuthority;

                ReturnDataModel.returnTable(PL);

                TempData["ConcernPerson"] = PL.ConcernPerson;
                return RedirectToAction("Success", "Home");

                // return Json(new { isok = true, message = "Thank you! " + PL.ConcernPerson + "We will back soon." });
            }


        }
        // GET: QueryForm
        public ActionResult QueryForm()
        {
            BindDropDown();
            return View();
        }
        public ActionResult Index()
        {
            BindDropDown();
            Session["txtPageName"] = "Index";
            return QueryForm();
        }
        [Route("about-us")]
        public ActionResult AboutUs()
        {

            BindDropDown();
            Session["txtPageName"] = "AboutUs";
            return View();
        }
        [HttpPost]
        public ActionResult ContactUsForm(string ConcernPerson, string EmailId, string CountryCode, string ContactNumber, string MessageContact, Validation model)
        {
            if (this.IsCaptchaValid(errorText: ""))
            {
                ValidationModel PL = new ValidationModel();
                PL.OpCode = 2;
                //PL.AutoId = Convert.ToInt32(AutoId);
                PL.ConcernPerson = ConcernPerson;
                PL.EmailId = EmailId;
                PL.CountryCode = CountryCode;
                PL.ContactNumber = ContactNumber;
                PL.MessageContact = MessageContact;
                TempData["ConcernPerson"] = PL.ConcernPerson;
                ReturnDataModel.returnTable(PL);

                //Sending Mail
                return RedirectToAction("Success", "Home");
                //var body = "";
                //body += "<p style='margin-top: 0px'>Dear AMCA Team,</p>";
                //body += "<p style='margin-top: 2px'>Please Contact the following details</p>";
                //body += " <table width='300' border='1' cellpadding='5' cellspacing='0'> <tr> <td> <strong> Name </strong></td> <td>" + ConcernPerson + " </td></tr> <tr> <td> <strong> Email Id </strong></td> <td>" + EmailId + " </td></tr> " +
                //    "<tr> <td> <strong> Contact Details </strong></td> <td>" + CountryCode + " " + ContactNumber + " </td></tr>" +
                //    "<tr> <td> <strong> Message </strong></td> <td>" + MessageContact + " </td></tr>" +
                //    "</table>";

                //var msg = SendMail("dev@amcaauditing.com", "", "crm@amcaauditing.com", "", "", "Assign Lead to BD", body, "", 465, true);
                //if (msg == "Successfull")
                //{
                //    return RedirectToAction("Success", "Home");
                //}
                //else
                //{
                //    BindDropDown();
                //    ViewBag.ErrorMessage = "Mail not sent";
                //    return View("ContactUs");
                //}
            }
            else
            {
                BindDropDown();
                ViewBag.ErrorMessage = "Error: captcha is not valid.";
                return View("ContactUs");
            }

        }
        [HttpGet]
        public ActionResult ContactUsForm()
        {
            BindDropDown();
            Session["txtPageName"] = "ContactUs";
            return View();
        }
        [Route("contact-us")]
        public ActionResult ContactUs()
        {
            BindDropDown();
            return ContactUsForm();
        }
        [Route("pricing")]
        public ActionResult Pricing()
        {
            Session["txtPageName"] = "pricing";
            return View();
        }
        [Route("careers")]
        public ActionResult Careers()
        {
            Session["txtPageName"] = "Careers";
            return View();
        }
        [Route("thankyou-career")]
        public ActionResult SuccessCareer()
        {
            return View();
        }
        [Route("industries")]
        public ActionResult Industries()
        {
            Session["txtPageName"] = "Industries";
            return View();
        }

        [Route("Success")]
        public ActionResult Success()
        {
            return View();
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