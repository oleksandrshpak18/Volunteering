using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;
        public NewsDomainService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<News> GetAll()
        {
            return _context.News
                 .Include(n => n.User)
                 .ToList();
        }
        public NewsVM ModelToVm(News news) => _mapper.Map<NewsVM>(news);
    
        public List<NewsVM> ModelToVm(IEnumerable<News> newsList) => _mapper.Map<List<NewsVM>>(newsList);
        
        public News VmToModel(NewsVM vm) => _mapper.Map<News>(vm);

        public News Add(NewsVM obj)
        {
            News res = _mapper.Map<News>(obj);
            _context.News.Add(res);
            _context.SaveChanges();
            return res;
        }

        public News? Update(NewsVM obj)
        {
            News ?res = _context.News.Find(obj.NewsId);
            _mapper.Map(obj, res);
            _context.SaveChanges();
            return res;
        }
    }
}
