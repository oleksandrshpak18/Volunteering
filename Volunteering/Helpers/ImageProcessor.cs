using Volunteering.Data.Interfaces;

namespace Volunteering.Helpers
{
    public static class ImageProcessor
    {
        public static byte[]? ImageToByte(IFormFile? image)
        {
            byte[]? res = null;
            if (image != null && image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    image.CopyTo(memoryStream);
                    var imageData = memoryStream.ToArray();
                    res = imageData;
                }
            }

            return res;
        }

        public static string? ByteToBase64(byte[]? image)
        {
            return image != null ? Convert.ToBase64String(image) : null;
        }
    }
}
