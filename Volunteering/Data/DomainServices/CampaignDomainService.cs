using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Volunteering.Data.Interfaces;
using Volunteering.Data.Models;
using Volunteering.Data.ViewModels;
using Volunteering.Helpers;

namespace Volunteering.Data.DomainServices
{
    public class CampaignDomainService 
    {
        private AppDbContext _context;
        private readonly IMapper _mapper;
        public CampaignDomainService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public CampaignVM ModelToVm(Campaign news) => _mapper.Map<CampaignVM>(news);

        public List<CampaignVM> ModelToVm(IEnumerable<Campaign> list) => _mapper.Map<List<CampaignVM>>(list);

        public Campaign VmToModel(CampaignVM vm) => _mapper.Map<Campaign>(vm);

        public Campaign Add(CampaignVM obj)
        {
            Campaign res = VmToModel(obj);

            var priority = _context.CampaignPriorities.FirstOrDefault(p => p.PriorityValue == obj.CampaignPriority);
            if(priority == null) { throw new KeyNotFoundException("Priority was not found"); }

            var status = _context.CampaignStatuses.FirstOrDefault(p => p.StatusName == "Новий");
            if (status == null) { throw new KeyNotFoundException("Status was not found"); }

            var subcategory = _context.Subcategories
                .Include(x => x.CategorySubcategories)
                    .ThenInclude(x => x.Category)
                .FirstOrDefault(p => p.SubcategoryName == obj.Subcategory);
            if (subcategory == null) { throw new KeyNotFoundException("Subcategory was not found"); }

            res.CampaignPriority = priority;
            res.CampaignStatus = status;    
            res.Subcategory = subcategory;

            _context.Campaigns.Add(res);
            _context.SaveChanges();

            var user = _context.Users.Find(obj.UserId);
            if (user == null) { throw new KeyNotFoundException("User was not found"); }

            _context.UserCampaigns.Add(new UserCampaign { User = user, Campaign = res });
            _context.SaveChanges();

            return res;
        }

        public Campaign? Get(Guid id)
        {
           return _context.Campaigns
                .Include(c => c.UserCampaigns).ThenInclude(c => c.User)
                .Include(c => c.Subcategory).ThenInclude(sc => sc.CategorySubcategories).ThenInclude(cs => cs.Category)
                .Include(c => c.CampaignPriority)
                .Include(c => c.CampaignStatus)
                .Include(c => c.Report).ThenInclude(x => x.ReportReportPhotos).ThenInclude(y => y.ReportPhoto)
                .FirstOrDefault(c => c.CampaignId == id);
        }

        public IEnumerable<Campaign> GetAll(CampaignFilter? filter = null, string? sortBy = null, bool isDescending = true, int page = 1, int pageSize = 8)
        {
            var query = _context.Campaigns
                .Include(c => c.UserCampaigns).ThenInclude(c => c.User)
                .Include(c => c.Subcategory).ThenInclude(sc => sc.CategorySubcategories).ThenInclude(cs => cs.Category)
                .Include(c => c.CampaignPriority)
                .Include(c => c.CampaignStatus)
                .Include(c => c.Report)
                .AsQueryable(); 

            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Category))
                {
                    query = query.Where(c => c.Subcategory.CategorySubcategories.Any(cs => cs.Category.CategoryName == filter.Category));
                }

                if (filter.Priority.HasValue)
                {
                    query = query.Where(c => c.CampaignPriority.PriorityValue == filter.Priority);
                }

                if (!string.IsNullOrEmpty(filter.Status))
                {
                    query = query.Where(c => c.CampaignStatus.StatusName == filter.Status);
                }
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                query = ApplySorting(query, sortBy, isDescending);
            }

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            return query.ToList(); 
        }

        private IQueryable<Campaign> ApplySorting(IQueryable<Campaign> query, string sortBy, bool descending)
        {
            switch (sortBy.ToLower())
            {
                case "createdate":
                    return descending ? query.OrderByDescending(c => c.CreateDate) : query.OrderBy(c => c.CreateDate);
                case "finishdate":
                    return descending ? query.OrderByDescending(c => c.FinishDate) : query.OrderBy(c => c.FinishDate);
                case "priority": // value 1 is considered bigger than 2 3 4 etc in our domain
                    return !descending ? query.OrderByDescending(c => c.CampaignPriority.PriorityValue) : query.OrderBy(c => c.CampaignPriority.PriorityValue);
                case "remaining":
                    return descending ? query.OrderByDescending(c => c.CampaignGoal - c.Accumulated) : query.OrderBy(c => c.CampaignGoal - c.Accumulated);
                default:
                    return query; 
            }
        }


        public Campaign Update(CampaignVM obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(CampaignVM obj)
        {
            throw new NotImplementedException();
        }

        public Campaign? UpdateStatus(CampaignStatusUpdateRequest req)
        {
            var res = _context.Campaigns.Find(req.StatusId);
            if (res == null) return null;
            var status = _context.CampaignStatuses.FirstOrDefault(x => x.StatusName.Equals(req.NewStatus));
            if (status == null) return null;
            res.CampaignStatus = status;
            _context.SaveChanges();
            return res;
        }

        public IEnumerable<Campaign> GetRecent(int count = 4)
        {
            return _context.Campaigns
                .OrderByDescending(c => c.CreateDate) 
                .Take(count) 
                .Include(c => c.UserCampaigns).ThenInclude(c => c.User)
                .Include(c => c.Subcategory).ThenInclude(sc => sc.CategorySubcategories).ThenInclude(cs => cs.Category)
                .Include(c => c.CampaignPriority)
                .Include(c => c.CampaignStatus)
                .Include(c => c.Report).ThenInclude(x => x.ReportReportPhotos).ThenInclude(y => y.ReportPhoto)
                .ToList(); 
        }

        public Campaign AddReport(Guid userId, ReportVM vm)
        {
            var res = _context.Campaigns

                .Include(c => c.UserCampaigns).ThenInclude(c => c.User)
                .Include(c => c.Subcategory).ThenInclude(sc => sc.CategorySubcategories).ThenInclude(cs => cs.Category)
                .Include(c => c.CampaignPriority)
                .Include(c => c.CampaignStatus)
                .Include(c => c.Report)
                .FirstOrDefault(c => c.CampaignId == vm.CampaignId);

            if (res == null)
            {
                return null;
            }
            if(res.UserCampaigns.First().User.UserId != userId)
            {
                return null;
            }
            if(res.Report != null)
            {
                return null; 
            }

            res.Report = new Report()
            {
                ReportName = vm.ReportName,
                ReportDescription = vm.ReportDescription,
                ReportReportPhotos = vm.ReportPhotos?
                    .Select(p => new ReportReportPhoto
                    {
                        ReportId = vm.ReportId,
                        ReportPhoto = new ReportPhoto
                        {
                            Photo = ImageProcessor.ImageToByte(p)
                        }
                    }).ToList() ?? new List<ReportReportPhoto>()
            };

            _context.SaveChanges();
            return res;
                
        }

        public StatisticsResponse GetStatistics()
        {
            var accumulated = _context.Campaigns.Select(s => s.Accumulated).Sum();
            var registeredUsers = _context.Users.Count();
            var finishedCampaigns = _context.Campaigns.Where(x => x.CampaignStatus.StatusName.Equals("Завершений")).Count();
            var donations = _context.Donations.Count();

            return new StatisticsResponse()
            {
                Donations = donations,
                Accumulated = accumulated,
                RegisteredUsers = registeredUsers,
                FinishedCampaigns = finishedCampaigns
            };
        }

        public IEnumerable<Campaign> GetByUserId(Guid userId)
        {
            return _context.Campaigns
                .Where(c => c.UserCampaigns.Any(x => x.User.UserId == userId))
                .Include(c => c.UserCampaigns).ThenInclude(c => c.User)
                .Include(c => c.Subcategory).ThenInclude(sc => sc.CategorySubcategories).ThenInclude(cs => cs.Category)
                .Include(c => c.CampaignPriority)
                .Include(c => c.CampaignStatus)
                .Include(c => c.Report).ThenInclude(x => x.ReportReportPhotos).ThenInclude(y => y.ReportPhoto)
                .ToList();
        }
    }
}
