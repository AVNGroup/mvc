using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using WebApplication3.ConnectedAzure;

namespace WebApplication3.Controllers
{
    class DataFil
    {
        
        static public async Task<bool> IsDataNormal(string typeData)
        {
            CloudTable table = ConnectedAzureServises.tableClient.GetTableReference("DataStreamFromIOThub");
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
         public static async Task<DataTable> lasttemp(string typeData)
        {
            CloudTable table = ConnectedAzureServises.tableClient.GetTableReference("DataStreamFromHub");
            TableQuery<DataTable> query = new TableQuery<DataTable>().Where(TableQuery.GenerateFilterCondition("valueid", QueryComparisons.Equal, "RaspberryData"));
           
            TableQuerySegment<DataTable> list = await table.ExecuteQuerySegmentedAsync(query, null);
            int max = 0;
            DateTime sa = new DateTime(1,1,1);
            List<DataTable> lis = table.ExecuteQuery(query).ToList();
            lis.Sort(delegate (DataTable dat1, DataTable dat2) { return dat2.valuedatetime.CompareTo(dat1.valuedatetime); });//сортуємо список даних по даті виміру
            return lis[0];
        }
        public static async Task<List<DataTable>> InfaCount(string typeData, int count)
        {
            CloudTable table = ConnectedAzureServises.tableClient.GetTableReference("DataStreamFromHub");
            TableQuery<DataTable> query = new TableQuery<DataTable>().Where(TableQuery.GenerateFilterCondition("valueid", QueryComparisons.Equal, "RaspberryData"));

            TableQuerySegment<DataTable> list = await table.ExecuteQuerySegmentedAsync(query, null);
            List<DataTable> results = new List<DataTable>();

            list.Results.Sort((a, b) => a.valuedatetime.CompareTo(b.valuedatetime));
            for (int i = list.Results.Count - count; i < list.Results.Count; i++)
            {
                results.Add(list.Results[i]);
            }
            return results;
        }
    }
}
