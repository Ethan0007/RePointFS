using Azure.Storage.Blobs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RePoint.FileSystem.Manager.RePoint
{
    public class RepointDB
    {

        private string _localRootPath;
        private BlobServiceClient _blobServiceClient;
        private string _azureContainerName;
        private string _connectionString;

        public RepointDB(string localRootPath, 
            string connectionString, 
            string azureContainerName)
        {
            _localRootPath = localRootPath;
            _blobServiceClient = new BlobServiceClient(connectionString);
            _azureContainerName = azureContainerName;
            _connectionString = connectionString;
        }

        public async Task DownloadAllFilesAsync(string tableName, string columnName, 
            string? destinationDirectory,
            bool isSaveToLacal = false)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_azureContainerName);
            await foreach (var blobItem in containerClient.GetBlobsAsync())
            {
                if(isSaveToLacal)
                {
                    if (string.IsNullOrEmpty(destinationDirectory)) 
                        throw new ArgumentNullException($"RePoint Error: Destination directory is null or empty!");

                    var blobClient = containerClient.GetBlobClient(blobItem.Name);
                    var destinationPath = Path.Combine(destinationDirectory, blobItem.Name);
                    await blobClient.DownloadToAsync(destinationPath);
                }

                await SaveFilePathToDatabaseAsync(blobItem.Name, tableName, columnName);
            }
        }

        private async Task SaveFilePathToDatabaseAsync(string filePath, string tableName, string columnName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"INSERT INTO {tableName} ({columnName}) VALUES (@FilePath)";
                    command.Parameters.AddWithValue("@FilePath", filePath);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
