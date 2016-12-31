using System.Web.Mvc;
using Microsoft.Azure.Devices;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using WebApplication3.Models;
using WebApplication3.Libraries_created;
using Microsoft.VisualBasic.ApplicationServices;
using WebApplication3.ConnectedAzure;

namespace WebApplication3.Controllers.Registration
{
    /// <summary>
    /// Class RegistrationController is responsible for
    /// verifying entered module ID and adding a new user with
    /// entered password and login
    /// </summary>
    public class RegistrationController : Controller
    {
        private string CLIENT_TABLE_NAME = "IdentityTable";

        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Check if the entered moduleID is entered right
        /// </summary>
        /// <param name="ID">module id</param>
        public async Task<ActionResult> Сheck(string ID) {
            if (await RegistrationLogin.IsIDDeviceCorrect("Modules", ID)) {
                CloudTable table = ConnectedAzureServises.tableClient.GetTableReference("Modules");
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

        /// <summary>
        /// Add a new User with the entered login and password.
        /// With a Hash function the password is converted to a hash 
        /// that will be stored in DB.
        /// </summary>
        /// <param name="login">the user login</param>
        /// /// <param name="password">the user password</param>
        public async Task<ActionResult> AddUsers(string login, string password) {
            string hashedPassword = SecurePasswordHasher.Hash(password);

            if (await RegistrationLogin.IsLoginNew(CLIENT_TABLE_NAME, login, hashedPassword)) {

                CloudTable table = ConnectedAzureServises.tableClient.GetTableReference(CLIENT_TABLE_NAME);

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