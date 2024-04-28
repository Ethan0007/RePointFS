# RePointFS Utilities Library
<p style="text-align: center;" align="center">
 <img width="200" src="https://github.com/Ethan0007/RePointFS/blob/development/Images/RePointFS.png" alt="RePoint">
</p>

The RePointFS Utilities Library provides a set of tools to facilitate common file system operations, including file downloads from Azure Blob Storage and saving file paths to a SQL Server database.

## Features
  - Download files from Azure Blob Storage to a local directory.
  - Save file paths to a SQL Server database table.

## Installation
You can install the library via NuGet Package Manager:
```
 Install-Package RePointFS
```

## High Level Architecture
<p style="text-align: center;" align="center">
 <img width="700" src="https://github.com/Ethan0007/RePointFS/blob/development/Images/RepointFS_Ark.png" alt="RePoint">
</p>

## Usage
## Downloading Files from Azure Blob Storage
```
using FileSystemUtilities;

// Create an instance of RePointFS Manager
var repointFS = new RePointFS(localRootPath, azureConnectionString, azureContainerName);

// Download all files from Azure Blob Storage to a local directory
await repointFS.DownloadAllFilesAsync(destinationDirectory);
```

## Saving File Paths to SQL Server Database
```
using FileSystemUtilities;

// Create an instance of RePointDB Manager
var repointDB = new RepointDB(localRootPath, connectionString, azureContainerName);

// Save file paths to SQL Server database table
await repointDB.DownloadAllFilesAsync(tableName, columnName, destinationDirectory, true);
```

## Configuration
  - localRootPath: The local directory where files will be downloaded.
  - azureConnectionString: The connection string for Azure Blob Storage.
  - azureContainerName: The name of the Azure Blob Storage container.
  - connectionString: The connection string for the SQL Server database.
  - tableName: The name of the table in the SQL Server database.
  - columnName: The name of the column in the SQL Server database table where file paths will be saved.
  - destinationDirectory: The destination directory for downloaded files.

# License 
  [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)  
  Copyright (c) 2024 [Joever Monceda](https://github.com/Ethan0007)

Linkedin: [Joever Monceda](https://www.linkedin.com/in/joever-monceda-55242779/)  
  Medium: [Joever Monceda](https://medium.com/@joever.monceda/new-net-core-vuejs-vuex-router-webpack-starter-kit-e94b6fdb7481)  
  Twitter [@_EthanHunt07](https://twitter.com/_EthanHunt07)  
