using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Volunteering.Data.Interfaces;
using Volunteering.Data.Models;
using Volunteering.Data.ViewModels;

namespace Volunteering.Data.DomainServices
{
    public class CampaignDomainService : IDomainService<CampaignVM, Campaign>
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

        public IEnumerable<Campaign> GetAll()
        {
            return _context.Campaigns
                .Include(c => c.UserCampaigns)
                    .ThenInclude(c => c.User)
                .Include(c => c.Subcategory)
                    .ThenInclude(c => c.CategorySubcategories)
                        .ThenInclude(c => c.Category)
                .Include(c => c.CampaignPriority)
                .Include(c => c.CampaignStatus)
                .ToList();
        }

        public Campaign Update(CampaignVM obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(CampaignVM obj)
        {
            throw new NotImplementedException();
        }
    }
}
