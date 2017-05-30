using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    public class TabelController : Controller
    {
        private readonly CloudTableClient tableClient;
        public TabelController(IConfigurationRoot config)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(config.GetConnectionString("StorageConnection"));

            // Create the table client.
            tableClient = storageAccount.CreateCloudTableClient();
        }
        // GET api/values
        [HttpGet]
        public async Task<TableQuerySegment<CustomerEntity>> Get()
        {
            CloudTable table = tableClient.GetTableReference("Anunturi"   );

            // Construct the query operation for all customer entities where PartitionKey="Smith".
            TableQuery<CustomerEntity> query = new TableQuery<CustomerEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Anunturi"));

            // Print the fields for each customer.
            var result = await table.ExecuteQuerySegmentedAsync(query, default(TableContinuationToken));
            return result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async void Post([FromBody]CustomerEntity value)  
        {

            
            CloudTable table = tableClient.GetTableReference("Anunturi");
            await table.CreateIfNotExistsAsync();
           

            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.Insert(value);

            // Execute the insert operation.
           var x = await table.ExecuteAsync(insertOperation);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
