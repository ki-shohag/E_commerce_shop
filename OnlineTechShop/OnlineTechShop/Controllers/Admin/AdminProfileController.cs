using OnlineTechShop.Models.AdminModel.AdminDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechShop.Controllers.Admin
{
    public class AdminProfileController : AdminBaseController
    {
        AdDataModel data = new AdDataModel();
        public ActionResult Index()
        {
            var admin = data.GetAdminByEmail(Session["email"].ToString());
            return View(admin);
        }
    }
}