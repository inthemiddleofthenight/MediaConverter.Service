
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MediaConverter.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
            Console.WriteLine("Enter MediaServiceClient base url");
            string baseUrl = Console.ReadLine();
            MediaServiceClient mediaServiceClient = new MediaServiceClient(baseUrl);
            Stopwatch stopwatch = new Stopwatch();
            Task.Run(() => {
                for (int i = 0; i < 100; i++)
                {
                    try
                    {
                        stopwatch.Restart();
                        var result = mediaServiceClient.ConvertAsync("demo.wav", "ogg").Result;
                        stopwatch.Stop();
                        File.WriteAllBytes($"demo_{Guid.NewGuid()}.{result.OutFormat}", result.Data);
                        
                        Console.WriteLine($"{i} - {stopwatch.Elapsed} - ok");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                  
                }

            });


            Console.ReadLine();
        }
    }
}
