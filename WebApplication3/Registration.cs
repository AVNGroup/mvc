using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;

public class RegistrationLogin {
    // private const string HOST = "AVN-group.azure-devices.net";
    // private const int PORT = 5671;
    // private const string SHARED_ACCESS_KEY_NAME = "iothubowner";
    // private const string SHARED_ACCESS_KEY = "jtRCksTr0b+5qWiPsSwVMQwO91+UiATq7JUJ/oqfsBY=";
    // private static string connectionString = "HostName=AVN-group.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=jtRCksTr0b+5qWiPsSwVMQwO91+UiATq7JUJ/oqfsBY=";

    private static string accountName = "avngroupf";
    private static string accountKey = "sQe3fgEb8Vrn6OWXs1ZvM/zhIlQmwrGLw2RSsO98htfwjiCD0cENbE9xCCBrH+qCi2T29WmNCOVyiu9AncbYNg==";
    private static StorageCredentials creds = new StorageCredentials(accountName, accountKey);

    private static string registrationTableName = "IdentityTable";

    public RegistrationLogin() {
        check = true;
    }
    static private bool check;
    public async Task<TableQuerySegment<CustomerEntity>> ActionAsync(CloudTable table, TableQuery<CustomerEntity> query2) {
        var data = await table.ExecuteQuerySegmentedAsync(query2, null);

        return data;
    }
    static public async Task Registration(CloudTableClient tableClient, string id, string pasword) {
        CloudTable table = tableClient.GetTableReference(registrationTableName);
        TableQuery<CustomerEntity> query = new TableQuery<CustomerEntity>().Where(TableQuery.GenerateFilterCondition("ID", QueryComparisons.Equal, id));
        TableQuerySegment<CustomerEntity> list = await table.ExecuteQuerySegmentedAsync(query, null);
        if (list.Results.Count == 0) {
            check = false;
        } else {
            TableQuery<CustomerEntity> query2 = new TableQuery<CustomerEntity>().Where(TableQuery.GenerateFilterCondition("Password", QueryComparisons.Equal, pasword));
            TableQuerySegment<CustomerEntity> list2 = await table.ExecuteQuerySegmentedAsync(query2, null);
            if (list2.Results.Count == 0 || list.Results[0].ID != list2.Results[0].ID) {
                check = false;
            }
            else
                check = true;
        }
    }
    static public async Task<bool> IsLoginAndPasswordCorrect(CloudTableClient tableClient,string tablename, string id, string pasword) {
        CloudTable table = tableClient.GetTableReference(tablename);
        TableQuery<CustomerEntity> query = new TableQuery<CustomerEntity>().Where(TableQuery.GenerateFilterCondition("ID", QueryComparisons.Equal, id));
        TableQuerySegment<CustomerEntity> list = await table.ExecuteQuerySegmentedAsync(query, null);
        if (list.Results.Count == 0) {
            check = false;
        }
        else {
            if (list.Results[0].Password != pasword){
                check = false;
            } else {
                check = true;
            }
        }
        return check;
    }
    static public async Task<bool> IsLoginAndPasswordCorrect(string id, string pasword) {
        CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
        CloudTableClient tableClient = account.CreateCloudTableClient();
        CloudTable table = tableClient.GetTableReference(registrationTableName);

        TableQuery<CustomerEntity> query = new TableQuery<CustomerEntity>().Where(TableQuery.GenerateFilterCondition("ID", QueryComparisons.Equal, id));
        TableQuerySegment<CustomerEntity> list = await table.ExecuteQuerySegmentedAsync(query, null);
        if (list.Results.Count == 0)
        {
            return false;
        }
        else {
            if (list.Results[0].Password != pasword)
                return false;
            else
                return true;
        }

    }
    static public async Task<bool> IsIDDeviceCorrect(CloudTableClient tableClient, string tablename, string id) {
        CloudTable table = tableClient.GetTableReference(tablename);

        TableQuery<CustomerEntity> query = new TableQuery<CustomerEntity>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id));

        TableQuerySegment<CustomerEntity> list = await table.ExecuteQuerySegmentedAsync(query, null);

        if (list.Results.Count == 0) 
            return false;
        else
            return true;

        }
    static public async Task<bool> IsLoginNew(CloudTableClient tableClient, string tablename, string login, string pasword) {
        CloudTable table = tableClient.GetTableReference(tablename);

        TableQuery<CustomerEntity> query = new TableQuery<CustomerEntity>().Where(TableQuery.GenerateFilterCondition("ID", QueryComparisons.Equal, login));

        TableQuerySegment<CustomerEntity> list = await table.ExecuteQuerySegmentedAsync(query, null);

        if (list.Results.Count == 0) 
            return true;
        else
            return false;

    }

}
