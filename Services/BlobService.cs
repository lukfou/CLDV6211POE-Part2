using Azure.Storage.Blobs;

namespace CLDV6211POE.Services
{
    public class BlobService
    {
        private readonly string _connectionString;
        private readonly string _containerName = "venue-images";

        public BlobService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("AzureBlobStorage");
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var container = new BlobContainerClient(_connectionString, _containerName);
            await container.CreateIfNotExistsAsync();

            var blob = container.GetBlobClient(file.FileName);

            using var stream = file.OpenReadStream();
            await blob.UploadAsync(stream, overwrite: true);

            return blob.Uri.ToString();
        }

        internal async Task<string> UploadFileAsync(object imageFile)
        {
            throw new NotImplementedException();
        }
    }
}
