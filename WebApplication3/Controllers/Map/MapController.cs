﻿using System;
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
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetData()
        {
            // создадим список данных
            string accountName = "avngroupf";
            string accountKey = "sQe3fgEb8Vrn6OWXs1ZvM/zhIlQmwrGLw2RSsO98htfwjiCD0cENbE9xCCBrH+qCi2T29WmNCOVyiu9AncbYNg==";
            StorageCredentials creds = new StorageCredentials(accountName, accountKey);
            CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
            CloudTableClient tableClient = account.CreateCloudTableClient();
            DataTable d = await DataFil.lasttemp(tableClient, "Distance");
            string l;
            string t;
            if (d.lat[0] == '0')
            {
                t = "43.22440151";
                l = "27.9371419";
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