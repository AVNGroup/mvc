using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;
namespace WebApplication3.Models
{
    /// <summary>
    /// Represents how data of the module is stored in DB.
    /// </summary>
    public class Devices : TableEntity
    {
        /// <value>Id <c>X</c> represents the module Id</value>
        /// <value>type <c>X</c> represents the module type</value>
        public Devices (string Id, string type)
        {
            this.PartitionKey = Id;
            this.RowKey = type;
        }
        public Devices() { }
    }
}