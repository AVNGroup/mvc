using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using Amqp;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
namespace WebApplication3.Controllers
{
    public class AzureController : Controller
    {
        private const string HOST = "AVN-group.azure-devices.net";
        private const int PORT = 5671;
        private const string SHARED_ACCESS_KEY_NAME = "iothubowner";
        private const string SHARED_ACCESS_KEY = "jtRCksTr0b+5qWiPsSwVMQwO91+UiATq7JUJ/oqfsBY=";

        static Address address = new Address(HOST, PORT, null, null);
        static public Connection connection = new Connection(address);
        public static Session session = new Session(connection);
        public static string accountName = "avngroupf";
        public static string accountKey = "sQe3fgEb8Vrn6OWXs1ZvM/zhIlQmwrGLw2RSsO98htfwjiCD0cENbE9xCCBrH+qCi2T29WmNCOVyiu9AncbYNg==";
        public static StorageCredentials creds = new StorageCredentials(accountName, accountKey);
        public static CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
        public static bool IsTransmision = false;

        CloudTableClient tableClient = account.CreateCloudTableClient();
        public static string temp;
        public static string coord;
        // GET: Azure
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async void ReadTemp()
        {
            DataTable d = await DataFil.lasttemp(tableClient, "Distance");
            Double l = Convert.ToDouble(d.lat);
            Double t = Convert.ToDouble(d.lon);
            if (Convert.ToDouble(d.lat) < 2.0)
            {
                l = 43.22440151;
                t = 27.9371419;
            }
            coord = l + "/" + t;
            temp = d.valuetemp;
            ReadTemp();
        }  
        public ActionResult BookSearch(string name)
        {
            Thread readtable = new Thread(ReadTemp);
            readtable.Start();
            var allbooks = temp + coord;
            return PartialView(allbooks);
        }
    }
}