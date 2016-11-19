using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;

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

    }
}