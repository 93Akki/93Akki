using AdminPanel.Models;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Routing;

namespace AdminPanel.Controllers
{
    public class MembersController : BaseController
    {
        //
        // GET: /Members/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewMembers()
        {
            try
            {
                var Parameter = new SqlParameter("@Criteria", "NewMembers");
                var result = SBSEntitie.Database.SqlQuery<Member>("SBS_GetMemberDetails @Criteria", Parameter).ToList();
                return View("NewMembers", result);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult SBSMembers()
        {
            try
            {
                var Parameter = new SqlParameter("@Criteria", "SBSMembers");
                var result = SBSEntitie.Database.SqlQuery<Member>("SBS_GetMemberDetails @Criteria", Parameter).ToList();
                return View("SBSMembers", result);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult MembersDetail(RouteValueDictionary routeValues)
        {
            try
            {
                var Parameter = new SqlParameter("@Criteria", routeValues["id"].ToString());
                var result = SBSEntitie.Database.SqlQuery<Member>("SBS_GetMemberDetails @Criteria", Parameter).ToList();
                Member objModel = new Member();
                objModel.ID = result[0].ID;
                objModel.FirstName = result[0].FirstName;
                objModel.LastName = result[0].LastName;
                objModel.Mobile = result[0].Mobile;
                objModel.Email = result[0].Email;
                objModel.Gender = result[0].Gender;
                objModel.City = result[0].City;
                objModel.Country = result[0].Country;
                objModel.SBSMemberID = result[0].SBSMemberID;
                objModel.SBSPassword = result[0].SBSPassword;
                objModel.IsActive = result[0].IsActive;
                objModel.CreationDate = result[0].CreationDate;
                objModel.ModifiyDate = result[0].ModifiyDate;
                objModel.referralCode = result[0].referralCode;
                objModel.referralCount = result[0].referralCount;

                objModel.KYC_ID = result[0].KYC_ID;
                objModel.KYC_MemberID = result[0].KYC_MemberID;
                objModel.KYC_AadharCard = result[0].KYC_AadharCard;
                objModel.KYC_AadharCard_back = result[0].KYC_AadharCard_back;
                objModel.KYC_PanCard = result[0].KYC_PanCard;
                objModel.KYC_PanCard_back = result[0].KYC_PanCard_back;
                objModel.PassportPhoto = result[0].PassportPhoto;
                objModel.Verified = result[0].Verified;
                objModel.VerifiedBy = result[0].VerifiedBy;
                objModel.KYC_CreationDate = result[0].KYC_CreationDate;
                objModel.KYC_ModifiyDate = result[0].KYC_ModifiyDate;

                return View("MembersDetail", objModel);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult UpdateMembersDetail(RouteValueDictionary routeValues)
        {
            try
            {
                var Parameter = new SqlParameter("@MemberID", routeValues["id"].ToString());
                var result = SBSEntitie.Database.SqlQuery<Member>("SBS_Verifiy_KYC_Documents @MemberID", Parameter).ToList();

                //Create the msg object to be sent
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                //Add your email address to the recipients
                msg.To.Add("");
                //msg.To.Add("joshiakshayjoshi421@gmail.com");
                //Configure the address we are sending the mail from
                MailAddress address = new MailAddress("info@sbsjodhpur.com");
                msg.From = address;
                msg.Subject = "SBS membership id activated";
                msg.Body = "Dear Akshay Joshi<br/>Your SBS membership id : <b>00000001</b> is active now. For more details please visit our website : http://sbsjodhpur.com/";
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
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction("NewMembers", "Members");
        }

        public ActionResult MemberReferralDetail(string id)
        {
            try
            {
                var Parameter = new SqlParameter("@Criteria", " AND referralBY =" + id);
                var result = SBSEntitie.Database.SqlQuery<Member>("SBS_GetMemberReferralDetails @Criteria", Parameter).ToList();
                return View("MemberReferralDetail", result);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
