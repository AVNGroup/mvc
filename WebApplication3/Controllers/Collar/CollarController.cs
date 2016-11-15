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
            ApplicationBase ApplicationVariable = new ApplicationBase();
            string s = ApplicationVariable.GetEnvironmentVariable("TEST");
            string accountName = "avngroupf";
            string accountKey = "sQe3fgEb8Vrn6OWXs1ZvM/zhIlQmwrGLw2RSsO98htfwjiCD0cENbE9xCCBrH+qCi2T29WmNCOVyiu9AncbYNg==";
            StorageCredentials creds = new StorageCredentials(accountName, accountKey);
            CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
            CloudTableClient tableClient = account.CreateCloudTableClient();
            DataTable d = await DataFil.lasttemp(tableClient, "Distance");
            Random rand = new Random();
            ViewData["Temp"] = d.valuetemp;
            ViewData["Coord"] = d.lat + "/" + d.lon;
            ViewData["Rand"] = s;
            return View();
        }
        public ViewResult Collar()
        {
            return View();
        }
    }
}