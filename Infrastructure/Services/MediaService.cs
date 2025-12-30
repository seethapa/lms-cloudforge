using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace Infrastructure.Services
{
    public class MediaService
    {
        private readonly BlobServiceClient _blobClient;

        public MediaService(BlobServiceClient blobClient)
        {
            _blobClient = blobClient;
        }

        public async Task<string> UploadThumbnailAsync(
            Stream fileStream, string fileName)
        {
            var container =
                _blobClient.GetBlobContainerClient("course-thumbnails");

            await container.CreateIfNotExistsAsync();

            var blob = container.GetBlobClient(fileName);
            await blob.UploadAsync(fileStream, overwrite: true);

            return blob.Uri.ToString();
        }
    }
}
