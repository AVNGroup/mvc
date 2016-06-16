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
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
namespace WebApplication3.Controllers
{
    public class TempController : Controller
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
        public static double temp = 25.5;
        static private readonly long UtcReference = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).Ticks;
        // GET: Temp
        public static string FirstDevice = "HostName=AVN-group.azure-devices.net;DeviceId=RaspberyData;SharedAccessKey=en9r0KNg4+zG15msYX/aFjtut4n0R+2fmi3iVU9GC+E=";

        public async Task Sending(CancellationToken token)
        {
            DeviceClient deviceClient = DeviceClient.CreateFromConnectionString(FirstDevice, Microsoft.Azure.Devices.Client.TransportType.Http1);

            var msg = new Microsoft.Azure.Devices.Client.Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject("Data:[{\"valueTemp\":" + temp + ",\"valueDateTime\":\"25.05.2016 12:25:48\",\"valueID\":\"RaspberryData\"}]")));
            await deviceClient.SendEventAsync(msg);
        }
        public ActionResult Index()
        {
            return View();
        }
      
        public async Task<ActionResult> ON()
        {
            temp += 0.5;

            DeviceClient deviceClient = DeviceClient.CreateFromConnectionString(FirstDevice, Microsoft.Azure.Devices.Client.TransportType.Http1);
            string d = "Data:[{\"valueTemp\":" + temp + ",\"valueDateTime\":\"" + DateTime.Now.ToString() + "\",\"valueID\":\"RaspberryData\"}]";
            var msg = new Microsoft.Azure.Devices.Client.Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(d)));
            await deviceClient.SendEventAsync(msg);
            return Redirect("/Temp/Index");
            string s = DateTime.Now.ToString();
        }


        public async Task<ActionResult> OFF()
        {
            temp -= 0.5;
            DeviceClient deviceClient = DeviceClient.CreateFromConnectionString(FirstDevice, Microsoft.Azure.Devices.Client.TransportType.Http1);

            var msg = new Microsoft.Azure.Devices.Client.Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject("Data:[{\"valueTemp\":" + temp + ",\"valueDateTime\":\"25.05.2016 12:25:48\",\"valueID\":\"RaspberryData\"}]")));
            await deviceClient.SendEventAsync(msg);
            return Redirect("/Temp/Index");
        }
    }
}