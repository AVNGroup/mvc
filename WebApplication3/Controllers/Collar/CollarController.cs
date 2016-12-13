using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.VisualBasic.ApplicationServices;
using WebApplication3.ConnectedAzure;
namespace WebApplication3.Controllers.Collar
   
{
    public class CollarController : Controller
    {
        // GET: Collar
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
        public async Task<ActionResult> Data()
        {
          
            DataTable d = await DataFil.lasttemp("Distance");
            Random rand = new Random();
            ViewData["Temp"] = d.valuetemp;
            ViewData["Coord"] = d.lat + "/" + d.lon;
            ViewData["Rand"] =  rand.Next();
            return View();
        }
        public async Task<ActionResult> Collar()
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
    }
}