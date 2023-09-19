using Amazon.S3.Model;
using Amazon.S3;
using NewTestProject.Interface;
using NewTestProject.Models;
using Amazon;
using Microsoft.Extensions.Options;
using System.IO.Compression;


namespace NewTestProject.Repositories
{

    public class DocumentHandler: IDocumentHandler
    {
        private readonly string _awsAccessKey;
        private readonly string _awsSecretKey;
        private readonly string _awsRegionEndPoint;
        private readonly string _awsBucketName;

        public DocumentHandler(IOptions<AWSConfig> awsConfig)
        {
            _awsAccessKey = awsConfig.Value.AWSKey;
            _awsSecretKey = awsConfig.Value.SecretKey;
            _awsRegionEndPoint = awsConfig.Value.RegionEndPoint;
            _awsBucketName = awsConfig.Value.BucketName;
        }

        //UploadDocument to S3 Bucket//        

        public async Task<string> UploadDocumentAsync(Document doc)
        {
            string awsRequestID = string.Empty;
            string fileExtension = Path.GetExtension(doc.File.FileName);
            var stream = doc.File.OpenReadStream();
            var fileNameInS3Bucket = doc.Name + fileExtension;

            IAmazonS3 client;
            using (client = new AmazonS3Client(_awsAccessKey, _awsSecretKey, RegionEndpoint.APSouth1))  //ap-south-1
            {
                var request = new PutObjectRequest()
                {
                    //BucketName = "document2023",
                    BucketName = _awsBucketName,
                    Key = fileNameInS3Bucket,
                    InputStream = stream
                };
                try
                {
                    var response = await client.PutObjectAsync(request);
                    awsRequestID = response.ResponseMetadata.RequestId.ToString();
                }
                catch (AmazonS3Exception amazonS3Exception)
                {
                    var error = amazonS3Exception.Message;
                    throw new Exception(error);
                }
            }
            return awsRequestID;
        }

        //DownloadDocument from S3 Bucket//  
        public async Task<string> DownloadDocumentAsync(string fileName)
        {
            var awsRequestID = string.Empty;

            IAmazonS3 client;
            using (client = new AmazonS3Client(_awsAccessKey, _awsSecretKey, RegionEndpoint.APSouth1))  
            {
                var request = new GetObjectRequest()
                {
                    BucketName = _awsBucketName,
                    Key = fileName,
                };
                try
                {
                    var response = await client.GetObjectAsync(request);
                    awsRequestID = response.ResponseMetadata.RequestId.ToString();
                    string storagelocation = "C:\\Temp\\" + fileName;

                    await response.WriteResponseStreamToFileAsync(storagelocation, false, new CancellationTokenSource().Token);
                }
                catch (AmazonS3Exception amazonS3Exception)
                {
                    var error = amazonS3Exception.Message;
                    throw new Exception(error);
                }
            }
            return awsRequestID;
        }

        //DeleteDocument from S3 Bucket//  
        public async Task<string> DeleteDocumentAsync(string fileName)
        {
            var awsRequestID = string.Empty;

            IAmazonS3 client;
            using (client = new AmazonS3Client(_awsAccessKey, _awsSecretKey, RegionEndpoint.APSouth1))
            {
                var request = new DeleteObjectRequest()
                {
                    BucketName = _awsBucketName,
                    Key = fileName,
                };
                try
                {
                    var response = await client.DeleteObjectAsync(request);
                    awsRequestID = response.ResponseMetadata.RequestId.ToString();

                }
                catch (AmazonS3Exception amazonS3Exception)
                {
                    var error = amazonS3Exception.Message;
                    throw new Exception(error);
                }
            }
            return awsRequestID;

        }
    }
}






