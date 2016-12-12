using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.Azure.Devices;
using System.Text;
using WebApplication3.Models;
using System.Web.Caching;
using System.Web.UI;
using Microsoft.Azure;
using WebApplication3.Controllers;
// Далее ваши библиотеки
using Newtonsoft.Json;
namespace WebApplication3.Controllers.Admin
{
    public class AdminController : Controller
    {


        public static CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=avngroupf;AccountKey=sQe3fgEb8Vrn6OWXs1ZvM/zhIlQmwrGLw2RSsO98htfwjiCD0cENbE9xCCBrH+qCi2T29WmNCOVyiu9AncbYNg==");
        public static CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
        public  ActionResult Index()

        {
            ViewData["ListRoute"] = Route.GetRoute(tableClient, "Bob", "Шарик");
            return View();
        }
        //[OutputCache(Duration = 30, Location = OutputCacheLocation.Downstream)]
        public ActionResult StartRoute()
        {
            ViewData["Rand"] = "Тест";
            return View();
        }
        public ActionResult AddRoute()
        {
            return View();
        }
        public ActionResult SelectRoute(string name)
        {
            List<List<string>> ListLanAndLot = Route.GetLanAndLonList(tableClient ,name);
            List<string> listresult = new List<string>();
            for(int i =0; i < ListLanAndLot[0].Count; i++)
            {
                listresult.Add(ListLanAndLot[0][i]);
                listresult.Add(ListLanAndLot[1][i]);
            }
            ViewData["SelectRoute"] = JsonConvert.SerializeObject(listresult);
            return View();
        }
    }
}