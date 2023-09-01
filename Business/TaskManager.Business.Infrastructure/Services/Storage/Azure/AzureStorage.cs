using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Abstractions.Storage.Azure;

namespace TaskManager.Business.Infrastructure.Services.Storage.Azure
{
    public class AzureStorage : Storage, IAzureStorage
    {
         BlobServiceClient _blobServiceClient;
        BlobContainerClient _blobContainerClient;
        public static string azureConStr { get; set; }
        public AzureStorage()
        {
            var configurationBuilder = new ConfigurationBuilder();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            azureConStr = configurationBuilder.Build().GetSection("Storage:Azure").Value ?? "";

            //_blobServiceClient. = azureConStr ?? "";
            _blobServiceClient = new BlobServiceClient(azureConStr);
        }


        public async Task DeleteAsync(string containerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
            await blobClient.DeleteAsync();
        }

        public List<string> GetFiles(string containerName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            return _blobContainerClient.GetBlobs().Select(x => x.Name).ToList();
        }

        public bool HasFile(string containerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            return _blobContainerClient.GetBlobs().Any(x => x.Name == fileName);
        }

        public async Task<List<string>> UploadAsync(string containerName, List<IFormFile>? files)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await _blobContainerClient.CreateIfNotExistsAsync();
            await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

            List<string> datas = new();
            foreach (IFormFile file in files)
            {
                string fileNewName = await FileRenameAsync(containerName, file.FileName, HasFile);

                BlobClient blobClient = _blobContainerClient.GetBlobClient(fileNewName);
               await blobClient.UploadAsync(file.OpenReadStream());

                datas.Add(blobClient.Uri.ToString());
                 //Alttakine dikkat
                //datas.Add((fileNewName, $"{containerName}/{fileNewName}"));
            }
            return datas;
        }

    }
}
