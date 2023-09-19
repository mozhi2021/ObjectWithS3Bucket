using Amazon.S3.Model;
using Amazon.S3;
using Amazon;

namespace NewTestProject.Common
{
    public class Util    {        

        public async static Task<bool> UploadDocumentToS3Bucket(string bucket, string filePathToSend, string fileStream, string fileNameInS3Bucket)
                    //public async static Task<string> SendMail(string To, string subject, string body)
        {
            string awsRequestID = string.Empty;

            using (FileStream stream = File.Open(fileStream, FileMode.Open))
           // using (FileStream stream = File.Open(filePathToSend, FileMode.Open))
            {
                IAmazonS3 client;
                using (client = new AmazonS3Client("AKIAR2RDF5VPIGNKP4PV", "7OsuGkiAY2UvqrfebcYDbvDHvBHOH9hMhSqQHUIQ", RegionEndpoint.APSouth1))
                {
                    var request = new PutObjectRequest()
                    {
                        BucketName = bucket,
                        Key = fileNameInS3Bucket,
                        InputStream = stream
                        
                       //TimeSpan.FromMinutes(10)
                    //PutObjectRequest.Timeout = TimeSpan.FromMinutes(10)

                    };
                    //var response = new PutObjectReponse();
                    try
                    {
                        //var res = client.PutObjectAsync(request);
                        var response = await client.PutObjectAsync(request);
                        awsRequestID = response.ResponseMetadata.RequestId.ToString();

                       // PutObjectResponse response = await client.PutObjectAsync(putRequest);
                    }
                    catch (AmazonS3Exception amazonS3Exception)
                    {
                        var error = amazonS3Exception.Message;
                        throw new Exception(error);
                    }
                }
            }
            return (awsRequestID.Trim().Length > 0) ? true : false;
        }

    }
}


