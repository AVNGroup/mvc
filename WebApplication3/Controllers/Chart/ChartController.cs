using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chart.Mvc.ComplexChart;
using Chart.Mvc.Extensions;
using Chart.Mvc.SimpleChart;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using WebApplication3.ConnectedAzure;

namespace WebApplication3.Controllers.Chart
{
    public class ChartController : Controller
    {
        // GET: Chart
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
        public async Task<ActionResult> PaintChar(string count)
        {

            List<DataTable> infa = await DataFil.InfaCount("asd",Convert.ToInt32(count));
            string[] test2 = new string[Convert.ToInt32(count)];
            string[] test = new string[Convert.ToInt32(count)];
            for (int i = 0; i < Convert.ToInt32(count); i++)
            {
                test[i] = infa[i].valuetemp;
                test2[i] = infa[i].valuedatetime;
            }

            ViewBag.intArray = test;
            ViewBag.sringArray = test2;
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