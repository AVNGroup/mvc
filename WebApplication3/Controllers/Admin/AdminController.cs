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


namespace WebApplication3.Controllers.Admin
{
    public class AdminController : Controller
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
        // GET: Admin
        public ActionResult Index()
        {
            //ViewData["type"] = HttpContext.Request.Cookies["id"].Value;
            return View();
        }
        public async Task<ActionResult> Delete()
        {
            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
            CloudTable table = tableClient.GetTableReference("Users");

            CustomerEntity customer = new CustomerEntity("collar", "Test1");
            TableOperation insertOPeration = TableOperation.Insert(customer);
            table.Execute(insertOPeration);

            return Redirect("/Admin/Index");
        }
        public async Task<ActionResult> Login(string Login, string Password)
        {
            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
            CloudTable table = tableClient.GetTableReference("Users");

            CustomerEntity customer = new CustomerEntity(Login, Password);
            TableOperation insertOPeration = TableOperation.Insert(customer);
            table.Execute(insertOPeration);
            return Redirect("/Admin/Index");
        }
        public  ActionResult Сheck(string ID)
        {

            ViewData["type"] = ID;
            return View();

        }
    }
}