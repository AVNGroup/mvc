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

            string accountName = "avngroupf";
            string accountKey = "sQe3fgEb8Vrn6OWXs1ZvM/zhIlQmwrGLw2RSsO98htfwjiCD0cENbE9xCCBrH+qCi2T29WmNCOVyiu9AncbYNg==";
            StorageCredentials creds = new StorageCredentials(accountName, accountKey);
            CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
            CloudTableClient tableClient = account.CreateCloudTableClient();
            List<DataTable> infa = await DataFil.InfaCount(tableClient, "asd",Convert.ToInt32(count));
            string[] test2 = new string[Convert.ToInt32(count)];
            string[] test = new string[Convert.ToInt32(count)];
            for (int i = 0; i < Convert.ToInt32(count); i++)
            {
                test[i] = infa[i].valuetemp;
                test2[i] = infa[i].valuedatetime;
            }

            ViewBag.intArray = test;
            ViewBag.sringArray = test2;
            return View();
        }
    }
}