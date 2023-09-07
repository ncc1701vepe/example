using km56.VirtualStorage.Api.Test.Http;
using km56.VirtualStorage.Application.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.Versioning;

namespace km56.VirtualStorage.Api.Test
{
    [TestClass]
    public class AzureCssTest
    {
        private readonly IConfiguration _configuration; 

        public AzureCssTest() 
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();
        }

        [SupportedOSPlatform("windows")]
        [TestMethod]
        public async Task CreateDownloadAndDeleteImageSuccessfully()
        {
            string cloudmersiveApi = _configuration.GetSection("CloudmersiveApi").Value;
            IWebApiProxy webApiProxy = new WebApiProxy(cloudmersiveApi);

            string connectionName = _configuration.GetSection("AzureConnectionName").Value;

            string filePath = _configuration.GetSection("AzureFilePath").Value;

            // Create File
            Image img = Image.FromFile(@"Samples\desktop-wallpaper.jpg");
            byte[] fileContent;
            using (var ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                fileContent = ms.ToArray();
            }

            CreateFileRequestDto createFileRequestDto = new CreateFileRequestDto
            {
                ConnectionName = connectionName,
                FilePath = filePath,
                FileName = "desktop-wallpaper.jpg",
                FileContent = fileContent
            };

            string url = _configuration.GetSection("CreateFileUrl").Value;
            var createFileResultDto = await webApiProxy.PostAsync<CreateFileRequestDto, CreateFileResultDto>(url, createFileRequestDto);

            Assert.IsTrue(createFileResultDto != null);
            Assert.IsTrue(createFileResultDto.Successful);

            // Download File
            DownloadFileRequestDto downloadFileRequestDto = new DownloadFileRequestDto
            {
                ConnectionName = connectionName,
                FilePath = filePath,
                FileName = "desktop-wallpaper.jpg"
            };

            url = _configuration.GetSection("DownloadFileUrl").Value;
            var downloadFileResultDto = await webApiProxy.PostAsync<DownloadFileRequestDto, DownloadFileResultDto>(url, downloadFileRequestDto);

            Assert.IsTrue(downloadFileResultDto != null);
            Assert.IsTrue(fileContent.Length == downloadFileResultDto?.FileContent?.Length);

            // Delete File
            DeleteFileRequestDto deleteFileRequestDto = new DeleteFileRequestDto
            {
                ConnectionName = connectionName,
                FilePath = filePath,
                FileName = "desktop-wallpaper.jpg"
            };

            url = _configuration.GetSection("DeleteFileUrl").Value;
            var deleteFileResultDto = await webApiProxy.PostAsync<DeleteFileRequestDto, DeleteFileResultDto>(url, deleteFileRequestDto);

            Assert.IsTrue(deleteFileResultDto != null);
            Assert.IsTrue(deleteFileResultDto.Successful);
        }
    }
}