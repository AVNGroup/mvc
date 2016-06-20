using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Controllers.MyPage
{
    public class MyPageController : Controller
    {
        // GET: MyPage
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OnClickMenu(string action)
        {
            if (action == "Feeder")
            {
                return Redirect("/Feeder/Index");
            }
            else if (action == "Collar")
            {
                return Redirect("/Collar/Index");
            }if(action == "Map")
            {
                return Redirect("/Map/Index");
            }
            else
            {
                return Redirect("/Home/SuccessEnter");
            }
        }
    }
}