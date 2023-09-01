using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Abstractions.Storage.Local;

namespace TaskManager.Business.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : Storage, ILocalStorage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task DeleteAsync(string path, string fileName)
         => File.Delete($"{path}\\{fileName}");
           
        public List<string> GetFiles(string path)
        {
            path = Path.Combine(_webHostEnvironment.WebRootPath, path);
            DirectoryInfo directory = new(path);
            return directory.GetFiles().Select(f => f.Name).ToList();
        }

        public bool HasFile(string path, string fileName)
           => File.Exists(($"{path}\\{fileName}"));

        private async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);

                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<string>> UploadAsync(string path, List<IFormFile>? files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);


            List<string> datas = new();
            foreach (IFormFile file in files)
            {
                //string fileNewName = await FileRenameAsync(uploadPath, file.Name, HasFile);

                //await CopyFileAsync($"{uploadPath}\\{file.Name}", file);
                //datas.Add((file.Name, $"{path}\\{file.Name}"));

                string fileNewName = await FileRenameAsync(uploadPath, file.FileName, HasFile);

                string newFilePath = Path.Combine(uploadPath, fileNewName);
                await CopyFileAsync(newFilePath, file);
                datas.Add(newFilePath);
            }
            return datas;
        }



        //public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFile? file)
        //{
        //    if (file == null || file.Length == 0)
        //    {
        //        throw new ArgumentNullException(nameof(file), "No file uploaded or file is empty.");
        //    }

        //    string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, pathOrContainerName);
        //    if (!Directory.Exists(uploadPath))
        //        Directory.CreateDirectory(uploadPath);

        //    List<(string fileName, string path)> datas = new();

        //    await CopyFileAsync($"{uploadPath}\\{file.FileName}", file);
        //    datas.Add((file.FileName, $"{pathOrContainerName}\\{file.FileName}"));

        //    return datas;
        //}








        //public Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFile? files)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
