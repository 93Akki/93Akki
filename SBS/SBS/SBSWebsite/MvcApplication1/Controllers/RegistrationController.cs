using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;
using ClassLibrary;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Mail;

namespace MvcApplication1.Controllers
{
    public class RegistrationController : BaseController
    {
        //
        // GET: /Registration/

        public ActionResult Index()
        {
            return View("Registration");
        }

        [HttpPost]
        public ActionResult MemberRegistration(string FirstName, string LastName, string Mobile, string Email, string Gender, string City, string Country, string ReferralCode)
        {
            string res = "";
            try
            {
                DataTable DT = new DataTable();
                clsRegistration objRegistration = new clsRegistration();
                DT = objRegistration.MemberRegistration(FirstName, LastName, Mobile, Email, Gender, City, Country, ReferralCode);
                res = JsonConvert.SerializeObject(DT);

                string result = DT.Rows[0]["Msg"].ToString();
                string[] arr = result.Split('|');
                if (arr[0] == "S" && arr.Length > 2)
                {

                    //Create the msg object to be sent
                    System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                    //Add your email address to the recipients
                    msg.To.Add(Email);
                    //msg.To.Add("joshiakshayjoshi421@gmail.com");
                    //Configure the address we are sending the mail from
                    MailAddress address = new MailAddress("info@sbsjodhpur.com");
                    msg.From = address;
                    msg.Subject = "Welcome to SBS";
                    msg.Body = "Thank you for register in SBS. <br/>Your membership id is " + arr[2] + "." +
                               "To activate your membership id please click on link and complete your KYC details : http://sbsjodhpur.com/KYC?kycparam=" + arr[2] + "";
                    msg.IsBodyHtml = true;
                    //Configure an SmtpClient to send the mail.
                    SmtpClient client = new SmtpClient();
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.EnableSsl = false;
                    client.Host = "sbsjodhpur.com";
                    client.Port = 25;//587

                    //Setup credentials to login to our sender email address ("UserName", "Password")
                    NetworkCredential credentials = new NetworkCredential("info@sbsjodhpur.com", "SBS@2020#");
                    client.UseDefaultCredentials = false;
                    client.Credentials = credentials;

                    //Send the msg
                    client.Send(msg);
                }

            }
            catch (Exception ex)
            {
                res = "E|" + ex.Message.ToString();
            }

            return Json(new { success = true, data = res }, JsonRequestBehavior.AllowGet);
        }

        public static string GetResponse(string sURL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sURL);
            request.MaximumAutomaticRedirections = 4;
            request.Credentials = CredentialCache.DefaultCredentials;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string sResponse = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
                return sResponse;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

    }
}
