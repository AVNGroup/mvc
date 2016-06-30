using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

public class RegistrationLogin
{
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

}
