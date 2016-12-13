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
using WebApplication3.ConnectedAzure;

namespace WebApplication3.Controllers.Feeder
{
    public class FeederController : Controller
    {

        public async Task<ActionResult> Index()
        {
            try
            {
                if (await RegistrationLogin.IsLoginAndPasswordCorrect(HttpContext.Request.Cookies["Login"].Value, HttpContext.Request.Cookies["Password"].Value))
                {
                    return View();
                }
                else
                {
                    return Redirect("/Home/Login");
                }
            }
            catch (System.NullReferenceException)
            {
                return Redirect("/Home/Login");
            }
        }
        public async Task<ActionResult> OnWather()
        {
            await SendCloudToDeviceMessageAsync("1");
            try
            {
                if (await RegistrationLogin.IsLoginAndPasswordCorrect(HttpContext.Request.Cookies["Login"].Value, HttpContext.Request.Cookies["Password"].Value))
                {
                    return Redirect("/Feeder/Index");
                }
                else
                {
                    return Redirect("/Home/Login");
                }
            }
            catch (System.NullReferenceException)
            {
                return Redirect("/Home/Login");
            }
           

        }
        public async Task<ActionResult> OffWather()
        {
            await SendCloudToDeviceMessageAsync("-1");
            try
            {
                if (await RegistrationLogin.IsLoginAndPasswordCorrect(HttpContext.Request.Cookies["Login"].Value, HttpContext.Request.Cookies["Password"].Value))
                {
                    return Redirect("/Feeder/Index");
                }
                else
                {
                    return Redirect("/Home/Login");
                }
            }
            catch (System.NullReferenceException)
            {
                return Redirect("/Home/Login");
            }


        }
        private async static Task SendCloudToDeviceMessageAsync(string comand)
        {
            var commandMessage = new Message(Encoding.ASCII.GetBytes(comand));
            await ConnectedAzureServises.serviceClient.SendAsync("RaspberyData", commandMessage);
        }
    }
}