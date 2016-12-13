using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
namespace WebApplication3.Controllers.Map
{
    public class MapController : Controller
    {
        // GET: Map
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
        public async Task<JsonResult> GetData()
        {
            DataTable d = await DataFil.lasttemp("Distance");
            string l;
            string t;
            if (d.lat[0] == '0')
            {
                t = "50.43921";
                l = "30.49865";
            }
            else
            {
                l = d.lat;
                t = d.lon;
            }
            List<PuppyMark> stations = new List<PuppyMark>();
            stations.Add(new PuppyMark()
            {
                Id = 1,
                GeoLat = l,
                GeoLong = t,
                Name = "Bob"
            });
            return Json(stations, JsonRequestBehavior.AllowGet);
        }
    }

}