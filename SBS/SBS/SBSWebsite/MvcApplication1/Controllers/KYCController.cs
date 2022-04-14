using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class KYCController : BaseController
    {
        //
        // GET: /KYC/

        public ActionResult Index()
        {
            Models.KYCModel objKycModel = new Models.KYCModel();
            try
            {
                string MemberID = Request.QueryString["kycparam"];

                clsKYC ObjKYC = new clsKYC();
                DataTable DT = ObjKYC.GetMemberKYCDetail(MemberID);

                objKycModel.MemberID = DT.Rows[0]["SBSMemberID"].ToString();
                objKycModel.FirstName = DT.Rows[0]["FirstName"].ToString();
                objKycModel.LastName = DT.Rows[0]["LastName"].ToString();
                objKycModel.Mobile = DT.Rows[0]["Mobile"].ToString();
                objKycModel.Email = DT.Rows[0]["Email"].ToString();
                objKycModel.City = DT.Rows[0]["City"].ToString();
                objKycModel.Country = DT.Rows[0]["Country"].ToString();
                objKycModel.Status = (DT.Rows[0]["IsActive"].ToString() == "true" ? "Active" : "InActive");
            }
            catch (Exception ex)
            {

            }
            return View("KYC", objKycModel);
        }

        [HttpPost]
        public ActionResult SBSKYC(Models.KYCModel model, HttpPostedFileBase ImgAadharCard, HttpPostedFileBase ImgPanCard, HttpPostedFileBase ImgPassportPhoto, HttpPostedFileBase ImgAadharCardBack, HttpPostedFileBase ImgPanCardBack)
        {
            try
            {
                string dirUrl = model.MemberID;
                string dirPath = Server.MapPath("~/KYCDocument/" + dirUrl);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                #region[Passport Image]
                string fileUrl2 = "~/KYCDocument/" + dirUrl + "/" + Path.GetFileName(ImgPassportPhoto.FileName);
                ImgPassportPhoto.SaveAs(Server.MapPath(fileUrl2));
                #endregion

                #region[Save Aadhar Image]
                string fileUrl = "~/KYCDocument/" + dirUrl + "/" + Path.GetFileName(ImgAadharCard.FileName);
                ImgAadharCard.SaveAs(Server.MapPath(fileUrl));

                string fileUrl3 = "~/KYCDocument/" + dirUrl + "/" + Path.GetFileName(ImgAadharCardBack.FileName);
                ImgAadharCardBack.SaveAs(Server.MapPath(fileUrl3));
                #endregion

                #region[Save PanCard Image]
                string fileUrl1 = "~/KYCDocument/" + dirUrl + "/" + Path.GetFileName(ImgPanCard.FileName);
                ImgPanCard.SaveAs(Server.MapPath(fileUrl1));

                string fileUrl4 = "~/KYCDocument/" + dirUrl + "/" + Path.GetFileName(ImgPanCardBack.FileName);
                ImgPanCardBack.SaveAs(Server.MapPath(fileUrl4));
                #endregion


                clsKYC ObjKYC = new clsKYC();
                DataTable DT = ObjKYC.saveMemberKYCDetails(model.MemberID, fileUrl, fileUrl1, fileUrl2, fileUrl3, fileUrl4);

                Response.Write("<script>alert('Thank you for submitting your document. Our SBS mitra contact you soon.')</script>"); //works great
            }
            catch (Exception)
            {

                throw;
            }
            return Redirect("http://sbsjodhpur.com/");
        }

    }
}
