
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.Azure.Devices;
namespace WebApplication3.ConnectedAzure
{
    public  class ConnectedAzureServises
    {
        public static CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=avngroupf;AccountKey=sQe3fgEb8Vrn6OWXs1ZvM/zhIlQmwrGLw2RSsO98htfwjiCD0cENbE9xCCBrH+qCi2T29WmNCOVyiu9AncbYNg==");
        public static CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
        public static ServiceClient serviceClient = ServiceClient.CreateFromConnectionString("HostName=AVN-group.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=jtRCksTr0b+5qWiPsSwVMQwO91+UiATq7JUJ/oqfsBY=");
    }
}