using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class ContactController : BaseController
    {
        //
        // GET: /Contact/

        public ActionResult Index()
        {
            return View("Contact");
        }


        [HttpPost]
        public ActionResult SendMail(string Name, string Email, string Mobile, string Message)
        {
            string res = "";
            try
            {

                string html = "";
                html = "<table style='width:100%;' border='1'>" +
                              "<tr>" +
                                "<td>Full Name</td>" +
                                "<td>" + Name + "</td>" +
                              "</tr>" +
                              "<tr>" +
                                "<td>E-Mail</td>" +
                                "<td>" + Email + "</td>" +
                              "</tr>" +
                              "<tr>" +
                                "<td>Mobile Number</td>" +
                                "<td>" + Mobile + "</td>" +
                              "</tr>" +
                              "<tr>" +
                                "<td>Message</td>" +
                                "<td>" + Message + "</td>" +
                              "</tr>" +
                        "</table>";

                //Create the msg object to be sent
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                //Add your email address to the recipients
                msg.To.Add("helpdesk@sbsjodhpur.com");
                //msg.To.Add("joshiakshayjoshi421@gmail.com");
                //Configure the address we are sending the mail from
                MailAddress address = new MailAddress("info@sbsjodhpur.com");
                msg.From = address;
                msg.Subject = "Get In Touch Mail";
                msg.Body = html;
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

                res = "S|Mail sent successfully!!!";
            }
            catch (Exception ex)
            {
                res = "E|" + ex.Message.ToString();
            }

            return Json(new { success = true, data = res }, JsonRequestBehavior.AllowGet);
        }
    }
}
