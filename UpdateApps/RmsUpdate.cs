﻿using System;
using System.Diagnostics;
using System.Linq;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace UpdateApps
{
    internal class RmsUpdate
    {
        public void DoUpdate(ListObjectsResponse resp, AmazonS3Client s3,
            ListObjectsRequest req, string bucket, TransferUtility trans)
        {
            var rmsToFolder =
                @"C:\Program Files (x86)\ProHealth Pharmacy Solutions\RMSv2";
            //@"C:\Program Files (x86)\ProHealth Pharmacy Solutions\RMSv2";

            Console.WriteLine(
                "Closing RMS if it's open. We cannot update RMS if it's running...");
            foreach (var process in Process.GetProcessesByName(
                "PRM")) process.Kill();
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
                    if (x.First() == "RMSv2" && x.Last() != "")
                    {
                        Console.WriteLine(
                            "Downloading Item: {0} to your your local RMS Directory",
                            obj.Key);
                        trans.Download(
                            rmsToFolder + "\\" + x.Last(),
                            bucket, obj.Key);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message,
                        ex.InnerException);
                }

            Console.WriteLine(
                "All items are downloaded locally to your RMS Directory. RMS Update Complete");
            Console.WriteLine("");
            Console.WriteLine(
                "Press any key to end your session...");
            Console.ReadKey();
        }
    }
}