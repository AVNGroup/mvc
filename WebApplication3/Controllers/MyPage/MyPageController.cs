using Microsoft.Azure.Devices;
using Microsoft.WindowsAzure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;

namespace WebApplication3.Controllers.MyPage
{
    public class MyPageController : Controller
    {
        // GET: MyPage
        public async Task<ActionResult> Index() 
        {
            try
            {
                if (await RegistrationLogin.IsLoginAndPasswordCorrect(HttpContext.Request.Cookies["Login"].Value, HttpContext.Request.Cookies["Password"].Value))
                {
                    return View();
                }
                else
                {
                    return Redirect("/Home/Login");
                }
            }
            catch (System.NullReferenceException)
            {
                return Redirect("/Home/Login");
            }
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
            }else if (action == "Chart")
            {
                return Redirect("/Chart/Index");
            }
            else
            {
                return Redirect("/Home/SuccessEnter");
            }
        }
        
    }
}