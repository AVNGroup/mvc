using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Azure.Devices;
using System.Threading.Tasks;
using IoTHubAmqp;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using WebApplication3.Models;

namespace WebApplication3.Controllers.Registration
{
    public class RegistrationController : Controller
    {

        private const string HOST = "AVN-group.azure-devices.net";
        private const int PORT = 5671;
        private const string SHARED_ACCESS_KEY_NAME = "iothubowner";
        private const string SHARED_ACCESS_KEY = "jtRCksTr0b+5qWiPsSwVMQwO91+UiATq7JUJ/oqfsBY=";
        static ServiceClient serviceClient;
        static string connectionString = "HostName=AVN-group.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=jtRCksTr0b+5qWiPsSwVMQwO91+UiATq7JUJ/oqfsBY=";
        public static string accountName = "avngroupf";
        public static string accountKey = "sQe3fgEb8Vrn6OWXs1ZvM/zhIlQmwrGLw2RSsO98htfwjiCD0cENbE9xCCBrH+qCi2T29WmNCOVyiu9AncbYNg==";
        public static StorageCredentials creds = new StorageCredentials(accountName, accountKey);
        public static CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
        CloudTableClient tableClient = account.CreateCloudTableClient();
        bool red = true;
        static private readonly long UtcReference = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).Ticks;
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Сheck(string ID)
        {
            if (await RegistrationLogin.IsIDDeviceCorrect(tableClient, "Users", ID))
            {
                CloudTable table = tableClient.GetTableReference("Users");
                TableQuery<Devices> query = new TableQuery<Devices>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, ID));

                TableQuerySegment<Devices> list = await table.ExecuteQuerySegmentedAsync(query, null);
                
                ViewData["type"] = list.Results[0].PartitionKey;
                return View();
            }
            else
            {
                return Redirect("/Home/NotSuccessEnter");
            }
        }
    }
}