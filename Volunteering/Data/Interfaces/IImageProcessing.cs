namespace Volunteering.Data.Interfaces
{
    public interface IImageProcessing
    {
        public byte[] ?ImageToByte(IFormFile? image);
        public string? ByteToBase64(byte []? image);
    }
}
