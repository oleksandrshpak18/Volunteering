using AutoMapper;
using Volunteering.Data.Interfaces;
using Volunteering.Data.Models;
using Volunteering.Data.ViewModels;

namespace Volunteering.Data.DomainServices
{
    public class CampaignPriorityDomainService : IDomainService<CampaignPriorityVm, CampaignPriority>
    {
        private AppDbContext _context;
        private readonly IMapper _mapper;
        public CampaignPriorityDomainService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public CampaignPriorityVm ModelToVm(CampaignPriority news) => _mapper.Map<CampaignPriorityVm>(news);

        public List<CampaignPriorityVm> ModelToVm(IEnumerable<CampaignPriority> newsList) => _mapper.Map<List<CampaignPriorityVm>>(newsList);

        public CampaignPriority VmToModel(CampaignPriorityVm vm) => _mapper.Map<CampaignPriority>(vm);

        public CampaignPriority Add(CampaignPriorityVm obj)
        {
            CampaignPriority res = VmToModel(obj);
            _context.CampaignPriorities.Add(res);
            _context.SaveChanges();
            return res;
        }

        public bool Delete(CampaignPriorityVm obj)
        {
            var tmp = _context.CampaignPriorities.Find(obj.CampaignPriorityId);
            if (tmp != null)
            {
                _context.CampaignPriorities.Remove(tmp);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public CampaignPriority? Get(Guid id)
        {
            return _context.CampaignPriorities.Find(id);
        }

        public IEnumerable<CampaignPriority> GetAll()
        {
            return _context.CampaignPriorities.ToList();
        }

        public CampaignPriority Update(CampaignPriorityVm obj)
        {
            CampaignPriority? res = _context.CampaignPriorities.Find(obj.CampaignPriorityId);
            _mapper.Map(obj, res);
            _context.SaveChanges();
            return res;
        }
    }
}
