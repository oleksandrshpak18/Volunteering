﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing.Printing;
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
        public NewsVM ModelToVm(News news) => _mapper.Map<NewsVM>(news);

        public List<NewsVM> ModelToVm(IEnumerable<News> newsList) => _mapper.Map<List<NewsVM>>(newsList);

        public News VmToModel(NewsVM vm) => _mapper.Map<News>(vm);

        public IEnumerable<News> GetAll()
        {
            return _context.News
                .Include(n => n.User)
                .OrderByDescending(n => n.CreateDate)
                .ToList();
        }

        public IEnumerable<News> GetPage(int page = 1, int pageSize = 5)
        {
            var query = _context.News
                .Include(n => n.User) 
                .OrderByDescending(n => n.CreateDate) 
                .AsQueryable();

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            return query.ToList(); 
        }

        public IEnumerable<News> GetRecent(int count = 8)
        {
            return _context.News
                .Include(n => n.User) 
                .OrderByDescending(n => n.CreateDate) 
                .Take(count) 
                .ToList(); 
        }

        public News Add(NewsVM obj)
        {
            News res = VmToModel(obj);
            _context.News.Add(res);
            _context.SaveChanges();
            return res;
        }

        public News? Update(NewsVM obj)
        {
            News ?res = _context.News.Find(obj.NewsId);
            _mapper.Map(obj, res);
            //if(obj.NewsPhoto == null)
            _context.SaveChanges();
            return res;
        }

        public News? Get(Guid id)
        {
            return _context.News
                .Include(n => n.User)
                .FirstOrDefault(c => c.NewsId == id);
        }

        public bool Delete(Guid id)
        {
            var news = _context.News.Find(id);
            if(news != null)
            {
                _context.News.Remove(news);
                _context.SaveChanges();
                return true;
            }
            return false;
            
        }

        
    }
}
