using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using WebApplication3.Controllers.Registration;
using WebApplication3.ConnectedAzure;

/// <summary>
/// Class RegistrationLogin is responsible for 
/// the proccess of the user registration 
/// </summary>
public class RegistrationLogin {
    /// <value>Variable <c>registrationTableName</c> represents table in DB</value>
    private static string registrationTableName = "IdentityTable";

    public RegistrationLogin() {
        check = true;
    }
    static private bool check;
    public async Task<TableQuerySegment<CustomerEntity>> ActionAsync(CloudTable table, TableQuery<CustomerEntity> query2) {
        var data = await table.ExecuteQuerySegmentedAsync(query2, null);

        return data;
    }

    /// <summary>
    /// Register of the User
    /// </summary>
    /// <param name="tableClient">table Client</param>
    /// <param name="id">module Id</param>
    /// <param name="pasword">User pasword</param>
    /// <returns>boolean (is registered or no)</returns>
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

    /// <summary>
    /// Is the login and password entered correct
    /// </summary>
    /// <param name="tableClient">table Client</param>
    /// /// <param name="tablename">table name</param>
    /// <param name="id">module Id</param>
    /// <param name="pasword">User pasword</param>
    /// <returns>boolean (Is the login and password entered correct)</returns>
    static public async Task<bool> IsLoginAndPasswordCorrect(CloudTableClient tableClient,string tablename, string id, string password) {
        CloudTable table = tableClient.GetTableReference(tablename);

        TableQuery<CustomerEntity> query = new TableQuery<CustomerEntity>().Where(TableQuery.GenerateFilterCondition("ID", QueryComparisons.Equal, id));

        TableQuerySegment<CustomerEntity> list = await table.ExecuteQuerySegmentedAsync(query, null);

        if (list.Results.Count == 0) {
            return false;
        }
        else {
            for (int i = 0; i < list.Results.Count; i++) {
                if (SecurePasswordHasher.Verify(password, list.Results[i].Password)) {
                    return true;
                }
            }
            return false;
        }
    }

    /// <summary>
    /// Is the login and password entered correct
    /// </summary>
    /// <param name="password">the password of the User</param>
    /// <param name="login">the login of the User</param>
    /// <returns>boolean (Is the login and password entered correct)</returns>
    static public async Task<bool> IsLoginAndPasswordCorrect(string login, string password) {

        CloudTable table = ConnectedAzureServises.tableClient.GetTableReference(registrationTableName);

        TableQuery<CustomerEntity> query = new TableQuery<CustomerEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, login));

        TableQuerySegment<CustomerEntity> list = await table.ExecuteQuerySegmentedAsync(query, null);

        if (list.Results.Count == 0) {
            return false;
        }
        else {
            for (int i = 0; i < list.Results.Count; i++) {
                if (SecurePasswordHasher.Verify(password, list.Results[i].Password)) {
                    return true;
                }
            }
            return false;
        }

    }

    /// <summary>
    /// Is Device ID entered correct
    /// </summary>
    /// <param name="tablename">the table name</param>
    /// <param name="id">the module id</param>
    /// <returns>boolean (Is Device ID entered correct)</returns>
    static public async Task<bool> IsIDDeviceCorrect(string tablename, string id) {
        CloudTable table = ConnectedAzureServises.tableClient.GetTableReference(tablename);

        TableQuery<CustomerEntity> query = new TableQuery<CustomerEntity>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id));

        TableQuerySegment<CustomerEntity> list = await table.ExecuteQuerySegmentedAsync(query, null);

        if (list.Results.Count == 0) 
            return false;
        else
            return true;

        }

    /// <summary>
    /// Check if User is a new
    /// </summary>
    /// <param name="tablename">the table name</param>
    /// <param name="login">the login of the user</param>
    /// <param name="hashedPassword">the hashed password of the user</param>
    /// <returns>boolean (if user is a new)</returns>
    static public async Task<bool> IsLoginNew(string tablename, string login, string hashedPassword) {
        CloudTable table = ConnectedAzureServises.tableClient.GetTableReference(tablename);

        TableQuery<CustomerEntity> query = new TableQuery<CustomerEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, login));

        TableQuerySegment<CustomerEntity> list = await table.ExecuteQuerySegmentedAsync(query, null);

        if (list.Results.Count == 0) 
            return true;
        else
            return false;

    }

}
