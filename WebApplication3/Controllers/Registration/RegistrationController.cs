using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Azure.Devices;
using System.Threading.Tasks;
using IoTHubAmqp;
using Microsoft.Azure; // Namespace for CloudConfigurationManager 
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using WebApplication3.Models;
using WebApplication3.Libraries_created;
using Microsoft.VisualBasic.ApplicationServices;

namespace WebApplication3.Controllers.Registration
{
    public class RegistrationController : Controller
    {
        static ServiceClient serviceClient;
        static ApplicationBase ApplicationVariable = new ApplicationBase();
        static string connectionString = ApplicationVariable.GetEnvironmentVariable("connectionString"); //"HostName=AVN-group.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=jtRCksTr0b+5qWiPsSwVMQwO91+UiATq7JUJ/oqfsBY=";
        public static string accountName = "avngroupf";
        public static string accountKey = "sQe3fgEb8Vrn6OWXs1ZvM/zhIlQmwrGLw2RSsO98htfwjiCD0cENbE9xCCBrH+qCi2T29WmNCOVyiu9AncbYNg==";
        public static StorageCredentials creds = new StorageCredentials(accountName, accountKey);
        public static CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
        CloudTableClient tableClient = account.CreateCloudTableClient();

        // Parse the connection string and return a reference to the storage account.
        //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
        //CloudConfigurationManager.GetSetting("DefaultEndpointsProtocol=https;AccountName=avngroupf;AccountKey=sQe3fgEb8Vrn6OWXs1ZvM/zhIlQmwrGLw2RSsO98htfwjiCD0cENbE9xCCBrH+qCi2T29WmNCOVyiu9AncbYNg=="));

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

                Table_dataSource.createTableInAzure();
                
                return View();
            }
            else
            {
                return Redirect("/Home/NotSuccessEnter"); 
            }
        }

        public async Task<ActionResult> AddUsers(string login, string password)
        {
            if (await RegistrationLogin.IsLoginCorrect(tableClient, "IdentityTable", login, password))
            {
                serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
                CloudTable table = tableClient.GetTableReference("IdentityTable");

                CustomerEntity customer = new CustomerEntity(login, login);
                customer.ID = login;
                customer.Password = password;
                TableOperation insertOPeration = TableOperation.Insert(customer);
                table.Execute(insertOPeration);
                return View();
            }
            else
            {
                return Redirect("/Home/NotSuccessEnter");
            }
        }
        
    }
}