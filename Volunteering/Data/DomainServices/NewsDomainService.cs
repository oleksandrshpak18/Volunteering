using System;
using System.IO;
using System.Net.Mime;
using Volunteering.Data.Interfaces;
using Volunteering.Data.Models;
using Volunteering.Data.ViewModels;
using Volunteering.Helpers;

namespace Volunteering.Data.DomainServices
{
    public class NewsDomainService : IDomainService<NewsVM, News>
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
            return new NewsVM()
            {
                Id = obj.NewsId,
                NewsTitle = obj.NewsTitle,
                NewsText = obj.NewsText,
                NewsPhotoBase64 = ImageProcessor.ByteToBase64(obj?.NewsPhoto)
            };
        }

        public News Add(NewsVM obj)
        {
            News res = new News()
            {
                NewsTitle = obj.NewsTitle,
                NewsText = obj.NewsText,
                NewsPhoto = ImageProcessor.ImageToByte(obj?.NewsPhoto)
            };
            _context.News.Add(res);
            _context.SaveChanges();
            return res;
        }

        public News Update(NewsVM obj)
        {
            News res = new News()
            {
                NewsTitle = obj.NewsTitle,
                NewsText = obj.NewsText,
                NewsPhoto = ImageProcessor.ImageToByte(obj?.NewsPhoto)
            };
            _context.News.Add(res);
            _context.SaveChanges();
            return res;
        }
    }
}
