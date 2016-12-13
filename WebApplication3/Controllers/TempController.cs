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