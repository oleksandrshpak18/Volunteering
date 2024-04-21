using System;
using System.IO;
using System.Net.Mime;
using Volunteering.Data.Interfaces;
using Volunteering.Data.Models;
using Volunteering.Data.ViewModels;

namespace Volunteering.Data.DomainServices
{
    public class NewsDomainService : IDomainService<NewsVM, News>, IImageProcessing
    {
        private AppDbContext _context;
        public NewsDomainService(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<News> GetAll()
        {
            return _context.News.ToList();
        }

        public NewsVM ConvertToVm(News obj)
        {
            NewsVM? res = new NewsVM()
            {
                NewsTitle = obj.NewsTitle,
                NewsText = obj.NewsText,
                NewsPhotoBase64 = ByteToBase64(obj?.NewsPhoto)
            };
            return res;
        }

        public News Add(NewsVM obj)
        {
            News res = new News()
            {
                NewsTitle = obj.NewsTitle,
                NewsText = obj.NewsText,
                NewsPhoto = ImageToByte(obj?.NewsPhoto)
            };
            _context.News.Add(res);
            _context.SaveChanges();
            return res;
        }

        public byte[] ?ImageToByte(IFormFile ? image)
        {
            byte [] ? res = null;
            if (image != null && image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    image.CopyTo(memoryStream);
                    var imageData = memoryStream.ToArray();
                    res = imageData;
                }
            }

            return  res;
        }

        public string? ByteToBase64(byte[] ? image)
        {
            return image != null ? Convert.ToBase64String(image) : null;
        }
    }
}
