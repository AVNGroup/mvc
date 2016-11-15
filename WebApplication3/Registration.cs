using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using WebApplication3.Models;
using Chart;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using System.IO;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Azure.Devices;
public class RegistrationLogin
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
    public static CloudTableClient tableClient = account.CreateCloudTableClient();
    public RegistrationLogin()
    {
        proverka = true;
    }
    static private bool proverka;
    public async Task<TableQuerySegment<CustomerEntity>> ActionAsync(CloudTable table, TableQuery<CustomerEntity> query2)
    {
        // этот метод использует async/await и возвращает Task
        var data = await table.ExecuteQuerySegmentedAsync(query2, null);

        return data;
    }
    static public async Task Registration(CloudTableClient tableClient, string id, string pasword)
    {

        CloudTable table = tableClient.GetTableReference("IdentityTable");
        TableQuery<CustomerEntity> query = new TableQuery<CustomerEntity>().Where(TableQuery.GenerateFilterCondition("ID", QueryComparisons.Equal, id));
        TableQuerySegment<CustomerEntity> list = await table.ExecuteQuerySegmentedAsync(query, null);
        if (list.Results.Count == 0)
        {
            proverka = false;
        }
        else
        {
            TableQuery<CustomerEntity> query2 = new TableQuery<CustomerEntity>().Where(TableQuery.GenerateFilterCondition("Password", QueryComparisons.Equal, pasword));
            TableQuerySegment<CustomerEntity> list2 = await table.ExecuteQuerySegmentedAsync(query2, null);
            if (list2.Results.Count == 0 || list.Results[0].ID != list2.Results[0].ID)
            {
                proverka = false;
            }
            else
                proverka = true;
        }

    }
    static public async Task<bool> IsLoginAndPasswordCorrect(CloudTableClient tableClient,string tablename, string id, string pasword)
    {
        CloudTable table = tableClient.GetTableReference(tablename);
        TableQuery<CustomerEntity> query = new TableQuery<CustomerEntity>().Where(TableQuery.GenerateFilterCondition("ID", QueryComparisons.Equal, id));
        TableQuerySegment<CustomerEntity> list = await table.ExecuteQuerySegmentedAsync(query, null);
        if (list.Results.Count == 0)
        {
            proverka = false;
        }
        else
        {
            if (list.Results[0].Password != pasword)
            {
                proverka = false;
            }
            else
                proverka = true;
        }
        return proverka;
    }
    static public async Task<bool> IsLoginAndPasswordCorrect(string id, string pasword)
    {

        CloudTable table = tableClient.GetTableReference("IdentityTable");
        TableQuery<CustomerEntity> query = new TableQuery<CustomerEntity>().Where(TableQuery.GenerateFilterCondition("ID", QueryComparisons.Equal, id));
        TableQuerySegment<CustomerEntity> list = await table.ExecuteQuerySegmentedAsync(query, null);
        if (list.Results.Count == 0)
        {
            proverka = false;
        }
        else
        {
            if (list.Results[0].Password != pasword)
            {
                proverka = false;
            }
            else
                proverka = true;
        }
        return proverka;
    }
    static public async Task<bool> IsIDDeviceCorrect(CloudTableClient tableClient, string tablename, string id)
    {
        CloudTable table = tableClient.GetTableReference(tablename);
        TableQuery<CustomerEntity> query = new TableQuery<CustomerEntity>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id));
        TableQuerySegment<CustomerEntity> list = await table.ExecuteQuerySegmentedAsync(query, null);
        if (list.Results.Count == 0)
        {
            proverka = false;
        }
        else
        {
            proverka = true;
        }
        return proverka;
    }
    static public async Task<bool> IsLoginCorrect(CloudTableClient tableClient, string tablename, string id, string pasword)
    {
        CloudTable table = tableClient.GetTableReference(tablename);
        TableQuery<CustomerEntity> query = new TableQuery<CustomerEntity>().Where(TableQuery.GenerateFilterCondition("ID", QueryComparisons.Equal, id));
        TableQuerySegment<CustomerEntity> list = await table.ExecuteQuerySegmentedAsync(query, null);
        if (list.Results.Count == 0)
        {
            proverka = true;
        }
        else
        {
           proverka = false;
        }
        return proverka;
    }

}
