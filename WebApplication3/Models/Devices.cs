using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;
namespace WebApplication3.Models
{
    public class Devices : TableEntity
    {
        public Devices (string Id, string type)
        {
            this.PartitionKey = Id;
            this.RowKey = type;
        }
        public Devices() { }
    }
}