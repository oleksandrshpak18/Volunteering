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
            var user = _context.Users.Find(obj.UserId);
            if (user == null) { throw new KeyNotFoundException("User was not found"); }

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

        public IEnumerable<Campaign> GetAll(CampaignFilter? filter = null, string? sortBy = null, bool isDescending = true, int page = 1, int pageSize = 8, bool isAdmin = false)
        {
            var query = _context.Campaigns
                .Include(c => c.UserCampaigns).ThenInclude(c => c.User)
                .Include(c => c.Subcategory).ThenInclude(sc => sc.CategorySubcategories).ThenInclude(cs => cs.Category)
                .Include(c => c.CampaignPriority)
                .Include(c => c.CampaignStatus)
                .Include(c => c.Report)
                .AsQueryable(); 

            if(!isAdmin)
            {
                query = query.Where(c => c.CampaignStatus.StatusName != "Новий" && c.CampaignStatus.StatusName != "Відхилено");
            }

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

        public CampaignStatus? UpdateStatus(CampaignStatusUpdateRequest req)
        {
            var res = _context.Campaigns.Find(req.CampaignId);
            if (res == null) return null;
            var status = _context.CampaignStatuses.FirstOrDefault(x => x.StatusName.Equals(req.NewStatus));
            if (status == null) return null;
            res.CampaignStatus = status;
            _context.SaveChanges();
            return res.CampaignStatus;
        }

        public IEnumerable<Campaign> GetRecent(int count = 4)
        {
            return _context.Campaigns
                .Where(c => c.CampaignStatus.StatusName != "Новий" && c.CampaignStatus.StatusName != "Відхилено")
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
            if(res.CampaignStatus.StatusName != "Очікується звіт")
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
            res.CampaignStatus = _context.CampaignStatuses.FirstOrDefault(x => x.StatusName == "Завершений");
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

        public IEnumerable<Campaign> GetByUserId(Guid userId, bool isOwner=false)
        {
            var res = _context.Campaigns
                .Where(c => c.UserCampaigns.Any(x => x.User.UserId == userId))
                .OrderByDescending(x => x.CreateDate)
                .Include(c => c.UserCampaigns).ThenInclude(c => c.User)
                .Include(c => c.Subcategory).ThenInclude(sc => sc.CategorySubcategories).ThenInclude(cs => cs.Category)
                .Include(c => c.CampaignPriority)
                .Include(c => c.CampaignStatus)
                .Include(c => c.Report).ThenInclude(x => x.ReportReportPhotos).ThenInclude(y => y.ReportPhoto)
                .AsQueryable();

            if(!isOwner)
            {
                res = res.Where(c => c.CampaignStatus.StatusName != "Новий" && c.CampaignStatus.StatusName != "Відхилено");
            }

            return res.ToList();            
        }

        public NumberStatistics GetNumberStatistics()
        {
            var total = _context.Donations.Sum(x => x.DonationValue);
            var count = _context.Donations.Count();

            if(count == 0)
            {
                return new NumberStatistics();
            }

            return new NumberStatistics
            {
                AccumulatedTotal = total.Value,
                DonationsCount = count,
                DonationsAverage = total.Value / count
            };
        }


        public List<CategoryStatistics> GetCategoryStatistics()
        {
            var categoryStatistics = _context.Donations
                .Join(_context.Campaigns,
                      donation => donation.CampaignId,
                      campaign => campaign.CampaignId,
                      (donation, campaign) => new { donation, campaign })
                .Join(_context.CategorySubcategories,
                      campaign => campaign.campaign.SubcategoryId,
                      categorySubcategory => categorySubcategory.SubcategoryId,
                      (campaign, categorySubcategory) => new { campaign, categorySubcategory })
                .Join(_context.Categories,
                      categorySubcategory => categorySubcategory.categorySubcategory.CategoryId,
                      category => category.CategoryId,
                      (categorySubcategory, category) => new { categorySubcategory, category })
                .Join(_context.Subcategories,
                      x => x.categorySubcategory.categorySubcategory.SubcategoryId,
                      subcategory => subcategory.SubcategoryId,
                      (x, subcategory) => new { x.categorySubcategory, x.category, subcategory })
                .GroupBy(x => new { x.category.CategoryName, x.subcategory.SubcategoryName })
                .Select(g => new
                {
                    CategoryName = g.Key.CategoryName,
                    SubcategoryName = g.Key.SubcategoryName,
                    Accumulated = g.Sum(d => d.categorySubcategory.campaign.donation.DonationValue ?? 0)
                })
                .GroupBy(x => x.CategoryName)
                .Select(g => new CategoryStatistics
                {
                    Category = g.Key,
                    Accumulated = g.Sum(d => d.Accumulated),
                    Subcategories = g.Select(x => new SubcategoryStatistics
                    {
                        Subcategory = x.SubcategoryName,
                        Accumulated = x.Accumulated
                    }).ToList()
                }).ToList();

            return categoryStatistics;
        }







    }




}

