using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
namespace WebApplication3.Controllers
{
    class DataFil
    {
        
        static public async Task<bool> IsDataNormal(CloudTableClient tableClient, string typeData)
        {
            CloudTable table = tableClient.GetTableReference("DataStreamFromIOThub");
            TableQuery<DataTable> query = new TableQuery<DataTable>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, typeData));
            TableQuerySegment<DataTable> list = await table.ExecuteQuerySegmentedAsync(query, null);
            if (list.Results.Count == 0)
            {
                
            }
            else
            {   for(int i =0; i < list.Results.Count; i++)
                {
                    if(Convert.ToInt32( list.Results[i].value) > 350)
                    {
                        return false;
                    }
                }
           
            }
            return true;
        }
         public static async Task<DataTable> lasttemp(CloudTableClient tableClient, string typeData)
        {
            CloudTable table = tableClient.GetTableReference("DataStreamFromHub");
            TableQuery<DataTable> query = new TableQuery<DataTable>().Where(TableQuery.GenerateFilterCondition("valueid", QueryComparisons.Equal, "RaspberryData"));
           
            TableQuerySegment<DataTable> list = await table.ExecuteQuerySegmentedAsync(query, null);
            int max = 0;
            DateTimeOffset sa = new DateTimeOffset();
            for(int i = 0; i < list.Results.Count; i++)
            {
                if(Convert.ToDateTime(list.Results[i].valuedatetime) > sa)
                {
                    max = i;
                    sa = Convert.ToDateTime(list.Results[i].valuedatetime);
                }
            }
            return list.Results[max];
        }
    }
}
