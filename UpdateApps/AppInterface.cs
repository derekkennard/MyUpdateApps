using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace UpdateApps
{
    internal abstract class AppInterface
    {
        // add your AWS Key
        private static readonly string accessKey = "************";
        // add your AWS Secret Key
        private static readonly string secretKey = "************";

        protected static readonly string BucketName = "bucket-name";

        protected static readonly TransferUtility FileTransferUtility =
            new(
                new AmazonS3Client(accessKey, secretKey,
                    RegionEndpoint.USEast1));

        protected static readonly AmazonS3Client S3Client = new(
            new BasicAWSCredentials(accessKey, secretKey),
            RegionEndpoint.USEast1);

        protected static readonly ListObjectsRequest Request =
            new();

        protected static readonly ListObjectsResponse
            Response = new();
    }
}
