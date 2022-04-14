using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class CareerController : BaseController
    {
        //
        // GET: /Career/

        public ActionResult Index()
        {
            return View("Career");
        }

        [HttpPost]
        public ActionResult SBSCareer(MvcApplication1.Models.CareerModel model, HttpPostedFileBase FResume)
        {
            string res = "";
            try
            {

                string html = "";
                html = "<table style='width:100%;' border='1'>" +
                              "<tr>" +
                                "<td>Full Name</td>" +
                                "<td>" + (model._FirstName + ' ' + model._LastName) + "</td>" +
                              "</tr>" +
                              "<tr>" +
                                "<td>E-Mail</td>" +
                                "<td>" + model._Email + "</td>" +
                              "</tr>" +
                              "<tr>" +
                                "<td>Mobile Number</td>" +
                                "<td>" + model._Mobile + "</td>" +
                              "</tr>" +
                        "</table>";

                //Create the msg object to be sent
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                //Add your email address to the recipients
                msg.To.Add("Career@sbsjodhpur.com");
                //msg.To.Add("joshiakshayjoshi421@gmail.com");
                //Configure the address we are sending the mail from
                MailAddress address = new MailAddress("info@sbsjodhpur.com");
                msg.From = address;
                msg.Subject = "Career with SBS";
                msg.Body = html;
                msg.IsBodyHtml = true;

                if (FResume != null)
                {
                    string fileName = Path.GetFileName(FResume.FileName);
                    msg.Attachments.Add(new Attachment(FResume.InputStream, fileName));
                }

                // Define content type.
                ContentType contentType = new ContentType();
                contentType.MediaType = MediaTypeNames.Text.Plain; // or whatever your attachment is

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

                res = "Mail sent successfully!!!";
                ViewBag.Message = res;
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                res = "E|" + ex.Message.ToString();
                ViewBag.Message = res;
            }

            return PartialView("Career");
        }

    }
}
