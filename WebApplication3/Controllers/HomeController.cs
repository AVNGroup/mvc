using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Threading;
using Microsoft.Azure.Devices;
using System.Threading.Tasks;
using IoTHubAmqp;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using WebApplication3.Models;
using Chart;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using System.IO;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private const string HOST = "AVN-group.azure-devices.net";
        private const int PORT = 5671;
        private const string SHARED_ACCESS_KEY_NAME = "iothubowner";
        private const string SHARED_ACCESS_KEY = "jtRCksTr0b+5qWiPsSwVMQwO91+UiATq7JUJ/oqfsBY=";
        static ServiceClient serviceClient;
        public static string connectionString = "HostName=AVN-group.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=jtRCksTr0b+5qWiPsSwVMQwO91+UiATq7JUJ/oqfsBY=";
        public static string accountName = "avngroupf";
        public static string accountKey = "sQe3fgEb8Vrn6OWXs1ZvM/zhIlQmwrGLw2RSsO98htfwjiCD0cENbE9xCCBrH+qCi2T29WmNCOVyiu9AncbYNg==";
        public static StorageCredentials creds = new StorageCredentials(accountName, accountKey);
        public static CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
        CloudTableClient tableClient = account.CreateCloudTableClient();
        static private readonly long UtcReference = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).Ticks;

        // Parse the connection string and return a reference to the storage account
       // public static CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting(connectionString));
      //  CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string Login, string Password) {


            //HttpContext.Response.Cookies["id"].Value = Login;
            if (await RegistrationLogin.IsLoginAndPasswordCorrect(tableClient, "IdentityTable", Login, Password))
            {
                return Redirect("/MyPage/Index");
            }
            else
            {
                return Redirect("/Home/NotSuccessEnter");
            }
        }



        [HttpGet]
        public ActionResult SuccessEnter () {
            return View();
        }



        public ActionResult NotSuccessEnter() {
            return View();
        }



        [HttpPost]
        public ActionResult SuccessEnter(string action) {
            if (action == "a") {
                return Redirect("/Home/ON_viev");
            }
            else {
                return Redirect("/Home/SuccessEnter");
            }
        }
        public ViewResult Index_ru()
        {
            return View();
        }
        public ViewResult Index_ua()
        {
            return View();
        }
        public ViewResult personal_account()
        {
            return View();
        }
        public ViewResult personal_account_ru()
        {
            return View();
        }
        public ViewResult personal_account_ua()
        {
            return View();
        }

        //The DownloadToStream method was used to download the contents of a blob as a text string
        public ActionResult downloadBlobs()
        {

            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting(connectionString));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");

            // Retrieve reference to a blob named "myblob.txt"
            CloudBlockBlob blockBlob2 = container.GetBlockBlobReference("myblob.txt");

            string text;
            using (var memoryStream = new MemoryStream())
            {
                blockBlob2.DownloadToStream(memoryStream);
                text = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
            }

            return View();
        }

    }
}
