using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace UpdateApps
{
    internal class AppUpdater
    {
        public void DoUpdate(ListObjectsResponse resp, AmazonS3Client s3,
            ListObjectsRequest req, string bucket, TransferUtility trans)
        {
            var bucketName = "phm-deployment";
            var auToFolder =
                @"C:\Program Files (x86)\ProHealth Pharmacy Solutions\AppUpdater";

            if (Directory.Exists(auToFolder))
            {
                var auToFolderNewFile =
                    @"C:\Program Files (x86)\ProHealth Pharmacy Solutions\AppUpdaterPend";

                Console.WriteLine(
                    "Starting AWS Connection for Transfer Utility");
                Console.WriteLine(
                    "Building AWS S3 Client using AWS Credentials");
                Console.WriteLine(
                    "Generate a list object to put downloaded files from AWS S3");
                Console.WriteLine(
                    "Generate a \"Response\" object for download items from AWS S3 bucket");
                resp =
                    s3.ListObjects(req.BucketName =
                        bucket);
                Console.WriteLine(
                    "Loop through the AWS S3 bucket and put all items into the \"Response\" object");

                foreach (var obj in resp.S3Objects)
                    try
                    {
                        var x = obj.Key.Split('/');
                        if (x.First() == "AppUpdater" &&
                            x.Last() != "")
                        {
                            Console.WriteLine(
                                "Downloading Item: {0} to your your local Application Update Directory",
                                obj.Key);
                            trans.Download(
                                auToFolderNewFile + "\\" + x.Last(),
                                bucketName, obj.Key);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message,
                            ex.InnerException);
                    }

                Console.WriteLine(
                    "All items are downloaded locally to your Application Update Directory.");
                Console.WriteLine("");

                Console.WriteLine(
                    "Start XCOPY to move new files...");
                Process.Start(
                    "C:\\Program Files (x86)\\ProHealth Pharmacy Solutions\\AppUpdaterPend\\RunCopy.bat");
                Console.WriteLine("XCOPY Complete!");
                Console.WriteLine("");
                Console.WriteLine(
                    "Closing Application Updater if it's open. We cannot update if it's running...");
                foreach (var process in Process.GetProcessesByName(
                    "UpdateApps")) process.Kill();
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(auToFolder);
                    Console.WriteLine("Directory created.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        "Directory can't be created. " +
                        ex.Message);
                }


                Console.WriteLine(
                    "Starting AWS Connection for Transfer Utility");
                Console.WriteLine(
                    "Building AWS S3 Client using AWS Credentials");
                Console.WriteLine(
                    "Generate a list object to put downloaded files from AWS S3");
                Console.WriteLine(
                    "Generate a \"Response\" object for download items from AWS S3 bucket");
                resp =
                    s3.ListObjects(req.BucketName =
                        bucket);
                Console.WriteLine(
                    "Loop through the AWS S3 bucket and put all items into the \"Response\" object");

                foreach (var obj in resp.S3Objects)
                    try
                    {
                        var x = obj.Key.Split('/');
                        if (x.First() == "AppUpdater" &&
                            x.Last() != "")
                        {
                            Console.WriteLine(
                                "Downloading Item: {0} to your your local Application Update Directory",
                                obj.Key);
                            trans.Download(
                                auToFolder + "\\" + x.Last(),
                                bucketName, obj.Key);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message,
                            ex.InnerException);
                    }

                Console.WriteLine(
                    "All items are downloaded locally to your Application Update Directory. Application Update Complete");
                Console.WriteLine("");
                Console.WriteLine(
                    "Press any key to end your session...");
                Console.ReadKey();
            }
        }
    }
}