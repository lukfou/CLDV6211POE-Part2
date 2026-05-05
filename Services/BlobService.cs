using Azure.Storage.Blobs;

namespace CLDV6211POE.Services
{
    public class BlobService(IConfiguration config)
    {
        private readonly string _containerName = "venue-images";

        public string ConnectionString => ConnectionString1;

        public string ConnectionString1 => ConnectionString2;

        public string ConnectionString2 => ConnectionString3;

        public string ConnectionString3 { get; } = config.GetConnectionString("AzureBlobStorage");

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var container = new BlobContainerClient(ConnectionString, _containerName);
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
