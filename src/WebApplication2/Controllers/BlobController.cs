using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    public class BlobController : Controller
    {

       

        private readonly CloudBlobClient blobClient;
        public BlobController (IConfigurationRoot config)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(config.GetConnectionString("StorageConnection"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

        }
        // GET api/values
        [HttpGet]
        public async static Task ListBlobsSegmentedInFlatListing(CloudBlobContainer container)
        {
            // Retrieve reference to a previously created container.
            //List blobs to the console window, with paging.
            Console.WriteLine("List blobs in pages:");

            int i = 0;
            BlobContinuationToken continuationToken = null;
            BlobResultSegment resultSegment = null;

            //Call ListBlobsSegmentedAsync and enumerate the result segment returned, while the continuation token is non-null.
            //When the continuation token is null, the last page has been returned and execution can exit the loop.
            do
            {
                //This overload allows control of the page size. You can return all remaining results by passing null for the maxResults parameter,
                //or by calling a different overload.
                resultSegment = await container.ListBlobsSegmentedAsync("", true, BlobListingDetails.All, 10, continuationToken, null, null);
                return resultSegment;
                if (resultSegment.Results.Count<IListBlobItem>() > 0) { Console.WriteLine("Page {0}:", ++i); }
                foreach (var blobItem in resultSegment.Results)
                {
                    Console.WriteLine("\t{0}", blobItem.StorageUri.PrimaryUri);
                }
                Console.WriteLine();

                //Get the continuation token.
                continuationToken = resultSegment.ContinuationToken;
            }
            while (continuationToken != null);
        }

    }

    }
