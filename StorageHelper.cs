using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Storage.Blobs;

namespace WebStorageSample
{
    public class StorageHelper
    {
        static public async Task UploadBlob(string containerEndpoint, string containerName, string blobName, string blobContents)
        {
            var blobContainerUri = new Uri(new Uri(containerEndpoint), containerName);
            BlobContainerClient containerClient = new BlobContainerClient(blobContainerUri, new DefaultAzureCredential());

            try
            {
                // Create the container if it does not exist.
                await containerClient.CreateIfNotExistsAsync();

                BlobClient blobClient = containerClient.GetBlobClient(blobName);

                // Upload text to a new block blob.
                byte[] byteArray = Encoding.ASCII.GetBytes(blobContents);

                using (MemoryStream stream = new MemoryStream(byteArray))
                {
                    await blobClient.UploadAsync(stream, overwrite: true);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        static public async Task<string> GetBlob(string containerEndpoint, string containerName, string blobName)
        {
            var blobContainerUri = new Uri(new Uri(containerEndpoint), containerName);
            BlobContainerClient containerClient = new BlobContainerClient(blobContainerUri, new DefaultAzureCredential());

            try
            {
                // Create the container if it does not exist.
                await containerClient.CreateIfNotExistsAsync();

                BlobClient blobClient = containerClient.GetBlobClient(blobName);
                if (await blobClient.ExistsAsync())
                {
                    var response = await blobClient.DownloadAsync();
                    using (var streamReader = new StreamReader(response.Value.Content))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            var line = await streamReader.ReadLineAsync();
                            Console.WriteLine(line);
                            return line;
                        }
                    }
                }
                return "";
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
