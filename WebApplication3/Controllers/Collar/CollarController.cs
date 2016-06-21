using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
namespace WebApplication3.Controllers.Collar
{
    public class CollarController : Controller
    {
        // GET: Collar
        public ActionResult Index()
        {
            
            return View();
        }
        public async Task<ActionResult> Data()
        {
            string accountName = "avngroupf";
            string accountKey = "sQe3fgEb8Vrn6OWXs1ZvM/zhIlQmwrGLw2RSsO98htfwjiCD0cENbE9xCCBrH+qCi2T29WmNCOVyiu9AncbYNg==";
            StorageCredentials creds = new StorageCredentials(accountName, accountKey);
            CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
            CloudTableClient tableClient = account.CreateCloudTableClient();
            DataTable d = await DataFil.lasttemp(tableClient, "Distance");
            Random rand = new Random();
            ViewData["Temp"] = d.valuetemp;
            ViewData["Coord"] = d.lat + "/" + d.lon;
            ViewData["Rand"] = rand.Next(10,50).ToString();
            return View();
        }
    }
}