using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
// Далее ваши библиотеки
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApplication3.ConnectedAzure;
namespace WebApplication3.Controllers.Admin
{
    public class AdminController : Controller
    {
        public async Task<ActionResult> Index()

        {
            
            DataTable d = await DataFil.lasttemp("Distance");
            Random rand = new Random();
            ViewData["Temp"] = d.valuetemp;
            ViewData["Coord"] = d.lat + "/" + d.lon;
            ViewData["Rand"] = rand;
            ViewData["ListRoute"] = Route.GetRoute(ConnectedAzureServises.tableClient, "Bob", "Шарик");
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
            List<List<string>> ListLanAndLot = Route.GetLanAndLonList(name);
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