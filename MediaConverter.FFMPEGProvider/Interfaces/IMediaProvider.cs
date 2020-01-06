using System.Threading.Tasks;

namespace MediaConverter.FFMPEGProvider.Interfaces
{
    public interface IMediaProvider
    {
        Task<byte[]> ConvertAsync(byte[] source, string inFormat, string outFormat);
    }
}
