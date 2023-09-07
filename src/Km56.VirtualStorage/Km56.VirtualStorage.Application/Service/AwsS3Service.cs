using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Azure.Core;
using km56.VirtualStorage.Application.Common;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net;

namespace km56.VirtualStorage.Application.Service
{
    /// <summary>
    /// Virtual Storage Service for AWS S3
    /// </summary>
    public class AwsS3Service : IVirtualStorageService
    {
        private readonly IAmazonS3 _amazonS3Client;

        public AwsS3Service(IConfiguration configuration) 
        {
            string accessKey = configuration.GetSection("Aws:AccessKey").Value;
            string secretAccessKey = configuration.GetSection("Aws:SecretAccessKey").Value;

            var awsCredentials = new Amazon.Runtime.BasicAWSCredentials(accessKey, secretAccessKey);
            _amazonS3Client = new AmazonS3Client(awsCredentials, RegionEndpoint.USWest1);
        }

        public async Task<CreateFileResult> CreateFile(string filePath, string fileName, byte[] fileContent)
        {
            using (var stream = new MemoryStream(fileContent))
            {
                var request = new PutObjectRequest
                {
                    BucketName = filePath,
                    Key = fileName,
                    InputStream = stream 
                };

                await _amazonS3Client.PutObjectAsync(request);

                CreateFileResult result = new CreateFileResult
                {
                    Successful = true
                };

                return result;
            }
        }

        public async Task<byte[]> DownloadFile(string filePath, string fileName)
        {
            var request = new GetObjectRequest
            {
                BucketName = filePath,
                Key = fileName
            };

            using (var response = await _amazonS3Client.GetObjectAsync(request))
            using (var stream = response.ResponseStream)
            {
                var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                return memoryStream.ToArray();
            }
        }

        public async Task<bool> DeleteFile(string filePath, string fileName)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = filePath,
                Key = fileName
            };

            var response = await _amazonS3Client.DeleteObjectAsync(request); 

            return response.HttpStatusCode == HttpStatusCode.NoContent;
        }
    }
}
