using RePoint.FileSystem.Manager.RePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RePoint.FileSystem.Manager.FSSample
{
    internal class FSMain
    {
        static async Task FS(string[] args)
        {
            // Example usage
            var localRootPath = @"C:\JHStorage";
            var azureConnectionString = "your-azure-storage-connection-string";
            var azureContainerName = "your-container-name";

            var repointFS = new RePointFS(localRootPath, azureConnectionString, azureContainerName);

            // Upload a file
            await repointFS.UploadFileAsync(@"C:\example.txt", "example.txt", useAzureStorage: true);

            // Check if a file exists
            var fileExists = await repointFS.FileExistsAsync("example.txt", useAzureStorage: true);
            Console.WriteLine($"File exists: {fileExists}");

            // Download a file
            await repointFS.DownloadFileAsync("example.txt", @"C:\Downloads\example.txt", useAzureStorage: true);
        }

        static async Task DB(string[] args)
        {
            // Azure Blob Storage settings
            var azureConnectionString = "your-azure-storage-connection-string";
            var azureContainerName = "your-container-name";
            var localRootPath = @"C:\JHStorage";

            // SQL Server database settings
            var sqlConnectionString = "your-sql-server-connection-string"; // Replace with your actual SQL Server connection string

            // Create an instance of FileSystemManager
            var fileSystemManager = new RepointDB(sqlConnectionString, azureConnectionString, azureContainerName);


            // Download all files from Azure Blob Storage and Save to Database
            await fileSystemManager.DownloadAllFilesAsync("SampleTable", "ColumnName", localRootPath, true);

        }
    }
}
