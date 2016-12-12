using System;
using Microsoft.WindowsAzure.Storage.Table;
public class CustomerEntity : TableEntity
{
    public string ID { get; set; }
    public string Password { get; set; }

    public CustomerEntity(string PartKey, string RoKey) {
        this.PartitionKey = PartKey;
        this.RowKey = RoKey;
    }

    public CustomerEntity() { }

}
