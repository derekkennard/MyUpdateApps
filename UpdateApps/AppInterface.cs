using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace UpdateApps
{
    internal abstract class AppInterface
    {
        private static readonly string accessKey = "AKIAQYWJGQAKOG22ISDD";

        private static readonly string secretKey =
            "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";

        protected static readonly string BucketName = "phm-deployment";

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