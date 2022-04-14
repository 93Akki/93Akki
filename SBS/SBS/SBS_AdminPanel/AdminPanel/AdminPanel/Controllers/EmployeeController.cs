using AdminPanel.DataModel;
using AdminPanel.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace AdminPanel.Controllers
{
    public class EmployeeController : BaseController
    {
        //
        // GET: /Employee/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SBSEmployeeDetail()
        {
            try
            {
                var Parameter = new SqlParameter("@Criteria", "Detail");
                var result = SBSEntitie.Database.SqlQuery<Employee>("SBS_GetEmployeeDetails @Criteria", Parameter).ToList();
                return View("EmployeeMaster", result);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult CreateNewEmployee()
        {
            try
            {
                ViewBag.CityList = new SelectList(SBSEntitie.CityMasters.ToList(), "CityID", "CityName");
                ViewBag.StateList = new SelectList(SBSEntitie.StateMasters.ToList(), "StateID", "StateName");
                ViewBag.CountryList = new SelectList(SBSEntitie.CountryMasters.ToList(), "CountryID", "CountryName");
                var listItems = new List<ListItem> {
                    new ListItem { Text = "Active", Value = "Active" },
                    new ListItem { Text = "Resigned", Value = "Resigned" },
                    new ListItem { Text = "Terminated", Value = "Terminated" }
                };
                ViewBag.StatusList = new SelectList(listItems);

                var listItems1 = new List<ListItem> {
                    new ListItem { Text = "Admin", Value = "Admin" },
                    new ListItem { Text = "Back Office", Value = "Back Office" },
                    new ListItem { Text = "Sales", Value = "Sales" }
                };
                ViewBag.DepartmentList = new SelectList(listItems1);
                return View("NewEmployee");

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult SaveNewEmployee(EmployeeSave EmployeeSaveModel, HttpPostedFileBase PassportPhoto, List<HttpPostedFileBase> EducationCertificates, List<HttpPostedFileBase> IDProofs)
        {
            try
            {
                long EmployeeID = 0;
                if (!SBSEntitie.SBS_EmployeeMaster.Any())
                    EmployeeID = 1;
                else
                    EmployeeID = SBSEntitie.SBS_EmployeeMaster.Max(item => item.EmployeeID);

                string dirPath = Server.MapPath("~/SBSEmployee_Documents/EmpID_" + EmployeeID + "_Documents");
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                //string path = "http://login.sbsjodhpur.com/SBSEmployee_Documents/EmpID_";
                string path = "~/SBSEmployee_Documents/EmpID_";
                #region[Passport Size Image]
                string PassportSize_path = path + EmployeeID + "_Documents/" + Path.GetFileName(PassportPhoto.FileName);
                PassportPhoto.SaveAs(Server.MapPath(PassportSize_path));
                #endregion

                SBS_EmployeeMaster sbstbl = new SBS_EmployeeMaster();
                sbstbl.FirstName = EmployeeSaveModel.FirstName;
                sbstbl.LastName = EmployeeSaveModel.LastName;
                sbstbl.Mobile = EmployeeSaveModel.Mobile;
                sbstbl.Email = EmployeeSaveModel.Email;
                sbstbl.Gender = EmployeeSaveModel.Gender;
                sbstbl.DOB = EmployeeSaveModel.DOB;
                sbstbl.City = EmployeeSaveModel.City;
                sbstbl.State = EmployeeSaveModel.State;
                sbstbl.Country = EmployeeSaveModel.Country;
                sbstbl.Status = EmployeeSaveModel.Status;
                sbstbl.CreationDate = DateTime.Now;
                sbstbl.ModifiyDate = DateTime.Now;
                sbstbl.Department = EmployeeSaveModel.Department;
                sbstbl.PossportPhoto = PassportSize_path;
                SBSEntitie.SBS_EmployeeMaster.Add(sbstbl);
                SBSEntitie.SaveChanges();




                SBS_Employee_EducationCertificates SBS_ECtbl = new SBS_Employee_EducationCertificates();
                foreach (HttpPostedFileBase postedFile in EducationCertificates)
                {
                    if (postedFile != null)
                    {
                        string fileName = Path.GetFileName(postedFile.FileName);
                        string EC_path = path + EmployeeID + "_Documents/" + fileName;
                        postedFile.SaveAs(Server.MapPath(EC_path));

                        SBS_ECtbl.EmployeeID = EmployeeID;
                        SBS_ECtbl.EducationCertificates = EC_path;
                        SBS_ECtbl.CreationDate = DateTime.Now;
                        SBS_ECtbl.ModifiyDate = DateTime.Now;
                        SBSEntitie.SBS_Employee_EducationCertificates.Add(SBS_ECtbl);
                        SBSEntitie.SaveChanges();
                    }
                }

                SBS_Employee_IDProofs IDProofstbl = new SBS_Employee_IDProofs();
                foreach (HttpPostedFileBase postedFile in IDProofs)
                {
                    if (postedFile != null)
                    {
                        string fileName = Path.GetFileName(postedFile.FileName);
                        string IDPath = path + EmployeeID + "_Documents/" + fileName;
                        postedFile.SaveAs(Server.MapPath(IDPath));

                        IDProofstbl.EmployeeID = EmployeeID;
                        IDProofstbl.IDProofs = IDPath;
                        IDProofstbl.CreationDate = DateTime.Now;
                        IDProofstbl.ModifiyDate = DateTime.Now;
                        SBSEntitie.SBS_Employee_IDProofs.Add(IDProofstbl);
                        SBSEntitie.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return RedirectToAction("SBSEmployeeDetail", "Employee");
        }

        //public ActionResult FillSBSEmployeeDetil(RouteValueDictionary routeValues)
        //{
        //    try
        //    {
        //        var EmployeeDetail = SBSEntitie.SBS_EmployeeMaster.Select(m => m.EmployeeID == Convert.ToInt64(routeValues["id"])).ToList();
        //        var Employee_ECData = SBSEntitie.SBS_Employee_EducationCertificates.Select(m => m.EmployeeID == Convert.ToInt64(routeValues["id"])).ToList();
        //        var Employee_IDProof = SBSEntitie.SBS_Employee_IDProofs.Select(m => m.EmployeeID == Convert.ToInt64(routeValues["id"])).ToList();
        //        return View("EmployeeMaster", EmployeeDetail, Employee_ECData, Employee_IDProof);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

    }
}
