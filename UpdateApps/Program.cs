using System;

namespace UpdateApps
{
    /// <inheritdoc />
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class Program : AppInterface
    {
        private static void Main()
        {
            Console.WriteLine("Enter the downloadable content key:");

            // Create a string variable and get user input from the keyboard and store it in the variable
            var contentKey = Console.ReadLine();

            if (contentKey == null)
            {
                Console.WriteLine(
                    "A null or empty value is not permitted. Please try again.");
                Console.ReadKey();
            }
            else
            {
                switch (contentKey.ToLower())
                {
                    // Print the value of the variable (input from user), which will display the input value
                    case "rms":
                    {
                        var rms = new RmsUpdate();
                        rms.DoUpdate(Response, S3Client, Request, BucketName,
                            FileTransferUtility);
                        break;
                    }
                    case "test":
                        Console.WriteLine(
                            "This is a test of the emergency broadcast system... This is only....... OH MY GOSH, WE'RE ALL GONNA DIE!!!...");
                        Console.ReadLine();
                        break;
                    case "au":
                        var au = new AppUpdater();
                        au.DoUpdate(Response, S3Client, Request, BucketName,
                            FileTransferUtility);
                        break;
                    default:
                        Console.WriteLine(
                            "Your downloadable content key was not found. Please try again.");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}