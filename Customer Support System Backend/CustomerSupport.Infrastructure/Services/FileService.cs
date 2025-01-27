using CustomerSupport.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSupport.Infrastructure.Services
{
    public class FileService: IFileService
    {
        private readonly string _secureFilePath;

        public FileService(IConfiguration configuration)
        {
            _secureFilePath = configuration.GetSection("FileStorage").Value ?? "SecureFiles";
            Directory.CreateDirectory(_secureFilePath);
        }

        public async Task<string> SaveFileAsync(IFormFile file, string subFolder = "")
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file");

            var folderPath = Path.Combine(_secureFilePath, subFolder);
            Directory.CreateDirectory(folderPath);

            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(folderPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return uniqueFileName;
        }

        public FileStream GetFile(string fileName, string subFolder = "")
        {
            var filePath = Path.Combine(_secureFilePath, subFolder, fileName);
            if (!File.Exists(filePath))
                throw new FileNotFoundException("File not found");

            return new FileStream(filePath, FileMode.Open, FileAccess.Read);
        }
    }
}
