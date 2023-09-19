using Amazon.S3.Transfer;
using Amazon.S3;
using Microsoft.Extensions.Options;
using NewTestProject.Interface;
using NewTestProject.Models;
using Amazon;


namespace NewTestProject.Repositories
{
    public class TransferUtilHandler: ITransferUtilHandler
    {
        private readonly string _awsAccessKey;
        private readonly string _awsSecretKey;
        private readonly string _awsRegionEndPoint;
        private readonly string _awsBucketName;
        private IAmazonS3 s3Client;
        private Stream stream;
        private string fileName;

        public string BucketName { get; private set; }

        public TransferUtilHandler(IOptions<AWSConfig> awsConfig)
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
            string awsBucketName = string.Empty;

            var stream = new System.IO.MemoryStream();
            var fileNameInS3Bucket = doc.Name;

            // Create the S3 client
            IAmazonS3 client;

            // Adding AWS Credentials
            using (client = new AmazonS3Client(_awsAccessKey, _awsSecretKey, RegionEndpoint.APSouth1))
            {
                // Create the upload Request
                var request = new TransferUtilityUploadRequest()
                {
                    BucketName = _awsBucketName,
                     Key = fileNameInS3Bucket,
                   // Key = fileName,
                    InputStream = stream,
                };
                try
                {
                    //upload utility to S3

                    var TransferUtility = new TransferUtility(s3Client);
                    await TransferUtility.UploadAsync(request);
                    //fileTransferUtility.Upload(request);

                    //TransferUtility.Upload(filePath, BucketName, Key);
                    //object value = Path.Combine();
                    //object value = Path.Combine(BucketName, key);

                    //TransferUtility TransferUtilHandler = new TransferUtility(s3Client);
                    //TransferUtilHandler.Upload(request);

                    //await TransferUtility.UploadAsync(request);


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
                var request = new TransferUtilityDownloadRequest()
                {
                    BucketName = _awsBucketName,
                    Key = fileName,
                };
                try
                {

                    var TransferUtility = new TransferUtility(s3Client);
                   // var response = await client.DownloadAsync(request);
                    //awsRequestID = response.ResponseMetadata.RequestId.ToString();


                    TransferUtility fileTransferUtility = new TransferUtility(s3Client);
                   fileTransferUtility.Download(request);

                    await TransferUtility.DownloadAsync(request);

                    //awsRequestID = response.ResponseMetadata.RequestId.ToString();
                    string storagelocation = "C:\\Temp\\" + fileName;

                   //await response.WriteResponseStreamToFileAsync(storagelocation, false, new CancellationTokenSource().Token);
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









