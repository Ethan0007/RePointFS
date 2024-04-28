/*
 * AUTHOR: JOEVER E. MONCEDA
 * LINKEDIN: https://www.linkedin.com/in/joever-monceda-55242779/
 * GITHUB: https://github.com/Ethan0007
 * EMAIL: JOEVER.MONCEDA@GMAIL.COM
 * PHONE: 09762624018
 */
using Azure.Storage.Blobs;

namespace RePoint.FileSystem.Manager.RePoint
{
    public class RePointFS
    {
        private string _localRootPath;
        private BlobServiceClient _blobServiceClient;
        private string _azureContainerName;

        public RePointFS(string localRootPath, string connectionString, string azureContainerName)
        {
            _localRootPath = localRootPath;
            _blobServiceClient = new BlobServiceClient(connectionString);
            _azureContainerName = azureContainerName;
        }

        public async Task UploadFileAsync(string filePath, string fileName, bool useAzureStorage = false)
        {
            if (useAzureStorage)
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient(_azureContainerName);
                var blobClient = containerClient.GetBlobClient(fileName);
                await blobClient.UploadAsync(filePath, true);
            }
            else
            {
                var destinationPath = Path.Combine(_localRootPath, fileName);
                File.Copy(filePath, destinationPath, true);
            }
        }

        public async Task DownloadFileAsync(string fileName, string destinationPath, bool useAzureStorage = false)
        {
            if (useAzureStorage)
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient(_azureContainerName);
                var blobClient = containerClient.GetBlobClient(fileName);
                await blobClient.DownloadToAsync(destinationPath);
            }
            else
            {
                var sourcePath = Path.Combine(_localRootPath, fileName);
                File.Copy(sourcePath, destinationPath, true);
            }
        }

        public async Task DownloadAllFilesAsync(string destinationDirectory)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_azureContainerName);
            await foreach (var blobItem in containerClient.GetBlobsAsync())
            {
                var blobClient = containerClient.GetBlobClient(blobItem.Name);
                var destinationPath = Path.Combine(destinationDirectory, blobItem.Name);
                await blobClient.DownloadToAsync(destinationPath);
            }
        }

        public async Task<bool> FileExistsAsync(string fileName, bool useAzureStorage = false)
        {
            if (useAzureStorage)
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient(_azureContainerName);
                var blobClient = containerClient.GetBlobClient(fileName);
                return await blobClient.ExistsAsync();
            }
            else
            {
                var filePath = Path.Combine(_localRootPath, fileName);
                return File.Exists(filePath);
            }
        }
    }
}