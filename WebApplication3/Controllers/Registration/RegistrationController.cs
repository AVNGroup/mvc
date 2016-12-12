using System.Web.Mvc;
using Microsoft.Azure.Devices;
using System.Threading.Tasks;
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
        private static string accountName = "avngroupf";
        private static string accountKey = "sQe3fgEb8Vrn6OWXs1ZvM/zhIlQmwrGLw2RSsO98htfwjiCD0cENbE9xCCBrH+qCi2T29WmNCOVyiu9AncbYNg==";
        private static StorageCredentials creds = new StorageCredentials(accountName, accountKey);
        private static CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
        private CloudTableClient tableClient = account.CreateCloudTableClient();

        private string CLIENT_TABLE_NAME = "IdentityTable";

        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Сheck(string ID) {
            if (await RegistrationLogin.IsIDDeviceCorrect(tableClient, "Modules", ID)) {
                CloudTable table = tableClient.GetTableReference("Modules");
                TableQuery<Devices> query = new TableQuery<Devices>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, ID));

                TableQuerySegment<Devices> list = await table.ExecuteQuerySegmentedAsync(query, null);
                
                ViewData["type"] = list.Results[0].PartitionKey;

                Table_dataSource.createTableInAzure();
                
                return View();
            }
            else {
                return Redirect("/Home/NotSuccessEnter"); 
            }
        }

        public async Task<ActionResult> AddUsers(string login, string password) {
            //Hash
            string hashedPassword = SecurePasswordHasher.Hash(password);

            if (await RegistrationLogin.IsLoginNew(tableClient, CLIENT_TABLE_NAME, login, hashedPassword)) {
                ApplicationBase ApplicationVariable = new ApplicationBase();
                string connectionString = ApplicationVariable.GetEnvironmentVariable("connectionString"); //"HostName=AVN-group.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=jtRCksTr0b+5qWiPsSwVMQwO91+UiATq7JUJ/oqfsBY=";
      
                ServiceClient serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
                CloudTable table = tableClient.GetTableReference(CLIENT_TABLE_NAME);

                CustomerEntity customer = new CustomerEntity(login, hashedPassword);
                customer.ID = login;
                customer.Password = hashedPassword;

                TableOperation insertOPeration = TableOperation.Insert(customer);

                table.Execute(insertOPeration);

                return View();
            }
            else {
                return Redirect("/Home/NotSuccessEnter");
            }
        }
        
    }
}