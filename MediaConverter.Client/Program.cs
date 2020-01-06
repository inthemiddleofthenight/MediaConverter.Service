
using System;
using System.IO;
using System.Text;

namespace MediaConverter.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press anu key to continue...");
            Console.ReadLine();

            MediaServiceClient mediaServiceClient = new MediaServiceClient("https://localhost:44314/");
            var t = mediaServiceClient.ConvertAsync("demo.wav", "mp3").Result;

            File.WriteAllBytes($"demo.{t.OutFormat}", t.Data);
            Console.ReadLine();
        }
    }
}
