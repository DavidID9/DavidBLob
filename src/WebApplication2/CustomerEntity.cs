using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2
{
    public class CustomerEntity : TableEntity
    {
        public CustomerEntity(string title, DateTime Data, string body)
        {
            this.PartitionKey = "Anunturi";
            this.RowKey = title;
            this.Body = body;
            this.Data = Data;

        }

        public CustomerEntity() { }

        public string Body { get; set; }

        public DateTime Data { get;set; }
        public string title { get;set; }


    }
}
