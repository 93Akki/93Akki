using AdminPanel.DataModel;
using AdminPanel.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace AdminPanel.Controllers
{
    public class ProductMasterController : BaseController
    {
        //
        // GET: /ProductMaster/

        public ActionResult Index()
        {
            try
            {
                var Parameter = new SqlParameter("@Criteria", "ProductList");
                var result = SBSEntitie.Database.SqlQuery<ProductMaster>("SBS_GetProductDetails @Criteria", Parameter).ToList();
                return View("ProductMaster", result);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult AddProduct()
        {
            try
            {
                ViewBag.CategoryList = new SelectList(SBSEntitie.SBS_CategoryMaster.ToList(), "CatID", "CatName");
                var listItems = new List<ListItem> {
                    new ListItem { Text = "L", Value = "L" },
                    new ListItem { Text = "ml", Value = "ml" },
                    new ListItem { Text = "kg", Value = "kg" },
                    new ListItem { Text = "g", Value = "g" },
                    new ListItem { Text = "Pack", Value = "Pack" },
                    new ListItem { Text = "Pice", Value = "Pice" }
                };
                ViewBag.QtyTypeList = new SelectList(listItems);
                return View("AddProduct");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult SaveNewProduct(AddProduct clsAddProduct, HttpPostedFileBase ProductImg, List<HttpPostedFileBase> ProductLinkedImg, HttpPostedFileBase ProductDocument)
        {
            try
            {
                long ProductID = 0;
                //string path = "D:/Akshay/MyData/SBS/SBS/SBSWebsite/MvcApplication1";
                string path = "~/Content/images/products/";
                string savePath = "http://login.sbsjodhpur.com/Content/images/products/";
                string dirPath = Server.MapPath(path + "Category_" + clsAddProduct.catID + "_img");

                if (!SBSEntitie.SBS_ProductMaster.Any())
                    ProductID = 1;
                else
                    ProductID = SBSEntitie.SBS_ProductMaster.Max(item => item.PID);


                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                //string path = "http://login.sbsjodhpur.com/SBSEmployee_Documents/EmpID_";

                #region[Product Image]
                string ProductImg_path = path + "/Category_" + clsAddProduct.catID + "_img/" + Path.GetFileName(ProductImg.FileName);
                ProductImg.SaveAs(Server.MapPath(ProductImg_path));
                #endregion

                string docPath = "";
                #region[File Upload]
                if (ProductDocument != null)
                {
                    string DocfileName = Path.GetFileName(ProductDocument.FileName);
                    string fileUploadPth = Path.Combine(path + "productDocument", DocfileName);
                    ProductDocument.SaveAs(Server.MapPath(fileUploadPth));
                    docPath = savePath + "productDocument/" + DocfileName;
                }
                #endregion



                SBS_ProductMaster tblProductMaster = new SBS_ProductMaster();
                tblProductMaster.PName = clsAddProduct.PName;
                tblProductMaster.marketRate = clsAddProduct.marketRate;
                tblProductMaster.sbsRate = clsAddProduct.sbsRate;
                tblProductMaster.Qty = clsAddProduct.Qty;
                tblProductMaster.QtyType = clsAddProduct.QtyType;
                tblProductMaster.imgPath = savePath + "Category_" + clsAddProduct.catID + "_img/" + Path.GetFileName(ProductImg.FileName);
                tblProductMaster.catID = clsAddProduct.catID;
                tblProductMaster.IsActive = clsAddProduct.IsActive;
                tblProductMaster.IsAddToCart = clsAddProduct.IsAddToCart;
                tblProductMaster.CreationDate = DateTime.Now;
                tblProductMaster.ModifiyDate = DateTime.Now;
                tblProductMaster.ProductDescription = clsAddProduct.ProductDescription;
                tblProductMaster.DocPath = docPath;
                SBSEntitie.SBS_ProductMaster.Add(tblProductMaster);
                SBSEntitie.SaveChanges();

                ProductID = SBSEntitie.SBS_ProductMaster.Max(item => item.PID);

                SBS_ProductDetail tbl_productdetail = new SBS_ProductDetail();
                #region[Product Linked Image]
                foreach (HttpPostedFileBase postedFile in ProductLinkedImg)
                {
                    if (postedFile != null)
                    {
                        string fileName1 = Path.GetFileName(postedFile.FileName);
                        string EC_path = path + "Category_" + clsAddProduct.catID + "_img/" + fileName1;
                        postedFile.SaveAs(Server.MapPath(EC_path));

                        tbl_productdetail.PID = ProductID;
                        tbl_productdetail.SubimgPath = savePath + "Category_" + clsAddProduct.catID + "_img/" + fileName1;
                        tbl_productdetail.CreationDate = DateTime.Now;
                        tbl_productdetail.MoifiyDate = DateTime.Now;
                        SBSEntitie.SBS_ProductDetail.Add(tbl_productdetail);
                        SBSEntitie.SaveChanges();
                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
                throw;
            }

            return RedirectToAction("Index", "ProductMaster");
        }


    }
}
