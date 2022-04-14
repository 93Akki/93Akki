using AdminPanel.DataModel;
using AdminPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminPanel.Controllers
{
    public class CategoryMasterController : BaseController
    {
        //
        // GET: /CategoryMaster/

        public ActionResult Index()
        {
            try
            {
                var data = SBSEntitie.SBS_CategoryMaster.ToList();
                return View("CategoryMaster", data);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryMaster MCategoryMaster)
        {
            try
            {
                var lastMenuSideData = SBSEntitie.SBS_CategoryMaster.OrderByDescending(u => u.catID).FirstOrDefault();

                string catSection = "";
                if (lastMenuSideData != null)
                {
                    if (lastMenuSideData.CatSection == "A")
                        catSection = "B";
                    else
                        catSection = "A";
                }
                else
                    catSection = "A";

                SBS_CategoryMaster objCategoryMaster = new SBS_CategoryMaster();
                objCategoryMaster.CatName = MCategoryMaster.CatName;
                objCategoryMaster.IsActive = MCategoryMaster.IsActive == "1" ? true : false;
                objCategoryMaster.CreationDate = DateTime.Now;
                objCategoryMaster.ModifiyDate = DateTime.Now;
                objCategoryMaster.CatSection = catSection;
                SBSEntitie.SBS_CategoryMaster.Add(objCategoryMaster);
                SBSEntitie.SaveChanges();
                return RedirectToAction("Index", "CategoryMaster");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult DeleteProductCategory(int id)
        {
            try
            {
                var CategoryMaster = new SBS_CategoryMaster { catID = id };
                SBSEntitie.SBS_CategoryMaster.Attach(CategoryMaster);
                SBSEntitie.SBS_CategoryMaster.Remove(CategoryMaster);
                SBSEntitie.SaveChanges();
                return RedirectToAction("Index", "CategoryMaster");
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
