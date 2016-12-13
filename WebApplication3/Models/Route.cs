using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;
using WebApplication3.Controllers;
using WebApplication3.ConnectedAzure;
namespace WebApplication3.Models
{
    public class Route : TableEntity
    {
        public Route()
        {
            this.PartitionKey = Guid.NewGuid().ToString();
            this.RowKey = "";
        }

        public string Login { get; set; }
        public string NameAnimals { get; set; }      
        public string NameRoute { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public Route(string login, string nameadnimals, string nameroute, string starttime, string endtime)
        {
            this.Login = login;
            this.NameAnimals = nameadnimals;
            this.NameRoute = nameroute;
            this.StartTime = starttime;
            this.EndTime = endtime;
        }
        public static List<string> GetRoute(CloudTableClient tableClient, string Login, string NameAnimal)
        {
            List<string> ListRoute = new List<string>();
            CloudTable table = tableClient.GetTableReference("Route");
            TableQuery<Route> query = new TableQuery<Route>().Where(TableQuery.GenerateFilterCondition("Login", QueryComparisons.Equal, "Tom"));
            foreach (Route route in table.ExecuteQuery(query))
            {
                if (route.NameAnimals == NameAnimal)
                {
                    ListRoute.Add(route.NameRoute);
                }
            }
            return ListRoute;
        }

        public static List<List<string>> GetLanAndLonList(string nameroute)
        {
            List<List<string>> ListLatAndLon = new List<List<string>>();
            CloudTable table = ConnectedAzureServises.tableClient.GetTableReference("Route");
            TableQuery<Route> query = new TableQuery<Route>().Where(TableQuery.GenerateFilterCondition("NameRoute", QueryComparisons.Equal, nameroute));
            foreach (Route route in table.ExecuteQuery(query))
            {
                List<string> ListLat = new List<string>();
                List<string> ListLon = new List<string>();
                CloudTable table2 = ConnectedAzureServises.tableClient.GetTableReference("DataStreamFromHub");
                TableQuery<DataTable> query2 = new TableQuery<DataTable>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, "Tom"));
                List<DataTable> lis = table2.ExecuteQuery(query2).ToList();
                lis.Sort(delegate (DataTable dat1, DataTable dat2) { return dat1.valuedatetime.CompareTo(dat2.valuedatetime); });//сортуємо список даних по даті виміру
                foreach (DataTable dat in lis)
                {
                    if (Convert.ToDateTime(dat.valuedatetime) >= Convert.ToDateTime(route.StartTime) && Convert.ToDateTime(dat.valuedatetime) <= Convert.ToDateTime(route.EndTime))
                    {
                        ListLat.Add(dat.lat);
                        ListLon.Add(dat.lon);
                    }
                }
                ListLatAndLon.Add(ListLat);
                ListLatAndLon.Add(ListLon);
            }
            return ListLatAndLon; 
        }
    }
}