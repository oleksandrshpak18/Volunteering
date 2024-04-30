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
            throw new NotImplementedException();
        }

        public IEnumerable<Campaign> GetAll(CampaignFilter? filter = null)
        {
            var query = _context.Campaigns
                .Include(c => c.UserCampaigns).ThenInclude(c => c.User)
                .Include(c => c.Subcategory).ThenInclude(sc => sc.CategorySubcategories).ThenInclude(cs => cs.Category)
                .Include(c => c.CampaignPriority)
                .Include(c => c.CampaignStatus)
                .AsQueryable(); 

            if (filter != null)
            {
                // Filtering by Category
                if (!string.IsNullOrEmpty(filter.Category))
                {
                    query = query.Where(c => c.Subcategory.CategorySubcategories.Any(cs => cs.Category.CategoryName == filter.Category));
                }

                // Filtering by Priority
                if (filter.Priority.HasValue)
                {
                    query = query.Where(c => c.CampaignPriority.PriorityValue == filter.Priority);
                }

                // Filtering by Status
                if (!string.IsNullOrEmpty(filter.Status))
                {
                    query = query.Where(c => c.CampaignStatus.StatusName == filter.Status);
                }
            }

            return query.ToList(); 
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
    }
}
