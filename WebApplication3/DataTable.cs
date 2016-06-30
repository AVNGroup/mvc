using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
namespace WebApplication3.Controllers
{
    public class DataTable : TableEntity
    {
        public DataTable(string PartKey, string RoKey)
        {
            this.PartitionKey = PartitionKey;
            this.RowKey = RoKey;
          
        }

        public DataTable() { }
        public string name { get; set; }

        public string value { get; set; }

        public string valuedist { get; set; }

        public string valuetemp { get; set; }

        public string valueid { get; set; }

        public string lat { get; set; }

        public string lon { get; set; }

        public string waterlevel { get; set; }

        public string valuedatetime { get; set; }
    }
}
