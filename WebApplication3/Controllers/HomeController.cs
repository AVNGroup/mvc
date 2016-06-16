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
namespace WebApplication3.Controllers
{
    public class HomeController : Controller
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


    

        private async static Task SendCloudToDeviceMessageAsync(string comand)
        {
            var commandMessage = new Message(Encoding.ASCII.GetBytes(comand));
            await serviceClient.SendAsync("RaspberyData", commandMessage);
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public string About(string sd)
        {

            ViewBag.s = sd;
            
            return sd;
        }
        
        public async Task<ActionResult>  ON_viev()
        {
            return View();

        }
        public async Task<ActionResult> ON()
        {
            ViewBag.inta = "sdfsdf";
            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
            await SendCloudToDeviceMessageAsync("1");
            return Redirect("/Home/ON_viev");

        }


        public async Task<ActionResult> OFF()
        {
            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
            await SendCloudToDeviceMessageAsync("-1");
            return Redirect("/Home/Index");        
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string Login, string Password) {
           
            //await RegistrationLogin.Registration(tableClient, Login_textBox.Text,Password_textBox1.Text);
            // bool d = await RegistrationLogin.IsLoginAndPasswordCorrect(tableClient, Login_textBox.Text, Login_textBox.Text);
            if (await RegistrationLogin.IsLoginAndPasswordCorrect(tableClient, Login, Password))
            {
                return Redirect("/Home/SuccessEnter");
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
    }
}
