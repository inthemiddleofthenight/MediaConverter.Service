using log4net;
using MediaConverter.FFMPEGProvider.Interfaces;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace MediaConverter.FFMPEGProvider
{
    public class MediaProvider : IMediaProvider
    {
        private readonly ILog _log;
        private readonly string _workFolder;
        private readonly string _ffmpegPath;
        public MediaProvider(string workFolder, ILog log)
        {
            _workFolder = workFolder;
            _ffmpegPath = "ffmpeg.exe";
            _log = log;
        }

        public async Task<byte[]> ConvertAsync(byte[] source, string inFormat, string outFormat)
        {
            try
            {
                _log?.Info($"In {source}, {inFormat}, {outFormat}");

                string sourceFile = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, _workFolder, $"{Guid.NewGuid()}.{inFormat.Trim('.')}");
                string targetFile = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, _workFolder, $"{Guid.NewGuid()}.{outFormat.Trim('.')}");

                File.WriteAllBytes(sourceFile, source);
                await Task.Run(() =>
                {
                    ProcessStartInfo processStartInfo = new ProcessStartInfo();
                    processStartInfo.FileName = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, _ffmpegPath);
                    processStartInfo.Arguments = $"-i {sourceFile} {targetFile}";
                    processStartInfo.UseShellExecute = false;
                    Process process = Process.Start(processStartInfo);
                    _log?.Info($"{nameof(MediaProvider)} {nameof(Convert)} sourceLength: {source.Length}, WorkingSet64: {process.WorkingSet64}, UserProcessorTime: {process.UserProcessorTime}");
                    process.WaitForExit();
                });

                byte[] conversionData = File.ReadAllBytes(targetFile);
                if (File.Exists(sourceFile)) File.Delete(sourceFile);
                if (File.Exists(targetFile)) File.Delete(targetFile);

                _log?.Info($"Out {source}, {inFormat}, {outFormat}");

                return conversionData;
            }
            catch (Exception ex)
            {
                _log?.Error($"{nameof(MediaProvider)} {nameof(Convert)} ({inFormat}, {outFormat}) error: {ex.Message} {ex.InnerException?.Message}");
                return Array.Empty<byte>();
            }
        }
    }
}
