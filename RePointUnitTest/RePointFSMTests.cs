using RePoint.FileSystem.Manager.RePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RePoint.FileSystem.Manager.FSUnitTest
{
    public class RePointFSMTests
    {
        private const string _localRootPath = @"C:\Temp";
        private const string _azureConnectionString = "your-azure-storage-connection-string";
        private const string _azureContainerName = "your-container-name";

        [Fact]
        public async Task UploadFile_LocalStorage_Success()
        {
            // Arrange
            var fileSystemManager = new RePointFS(_localRootPath, _azureConnectionString, _azureContainerName);
            var filePath = Path.Combine(_localRootPath, "example.txt");
            var fileName = "example.txt";
            File.Create(filePath).Dispose();

            // Act
            await fileSystemManager.UploadFileAsync(filePath, fileName);

            // Assert
            Assert.True(File.Exists(Path.Combine(_localRootPath, fileName)));


            File.Delete(filePath);
        }

        [Fact]
        public async Task DownloadAllFiles_Success()
        {
            // Arrange
            var fileSystemManager = new RePointFS(_localRootPath, _azureConnectionString, _azureContainerName);
            var filePaths = new[] { "example1.txt", "example2.txt", "example3.txt" };

            // Act
            foreach (var filePath in filePaths)
            {
                var destinationPath = Path.Combine(_localRootPath, filePath);
                await fileSystemManager.DownloadFileAsync(filePath, destinationPath, useAzureStorage: true);
            }

            // Assert
            foreach (var filePath in filePaths)
            {
                var destinationPath = Path.Combine(_localRootPath, filePath);
                Assert.True(File.Exists(destinationPath));
                File.Delete(destinationPath); // Clean up
            }
        }
    }
}
