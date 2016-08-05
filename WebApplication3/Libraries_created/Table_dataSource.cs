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


namespace WebApplication3.Libraries_created
{
    public class Table_dataSource
    {
        //static ServiceClient serviceClient;
        //static string connectionString = "HostName=AVN-group.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=jtRCksTr0b+5qWiPsSwVMQwO91+UiATq7JUJ/oqfsBY=";
        public static string accountName = "avngroupf";
        public static string accountKey = "sQe3fgEb8Vrn6OWXs1ZvM/zhIlQmwrGLw2RSsO98htfwjiCD0cENbE9xCCBrH+qCi2T29WmNCOVyiu9AncbYNg==";
        public static StorageCredentials creds = new StorageCredentials(accountName, accountKey);
        public static CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
        static CloudTableClient tableClient = account.CreateCloudTableClient();

        static private readonly long UtcReference = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).Ticks;
        // GET: Registration

        public static void createTableInAzure()
        {

            CloudTable tableTest = tableClient.GetTableReference("testTestIvan");
            tableTest.CreateIfNotExists();
        }

    }
}