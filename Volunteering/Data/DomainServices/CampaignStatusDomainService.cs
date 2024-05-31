using AutoMapper;
using Volunteering.Data.Interfaces;
using Volunteering.Data.Models;
using Volunteering.Data.ViewModels;

namespace Volunteering.Data.DomainServices
{
    public class CampaignStatusDomainService : IDomainService<CampaignStatusVm, CampaignStatus>
    {
        private AppDbContext _context;
        private readonly IMapper _mapper;
        public CampaignStatusDomainService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public CampaignStatusVm ModelToVm(CampaignStatus news) => _mapper.Map<CampaignStatusVm>(news);

        public List<CampaignStatusVm> ModelToVm(IEnumerable<CampaignStatus> newsList) => _mapper.Map<List<CampaignStatusVm>>(newsList);

        public CampaignStatus VmToModel(CampaignStatusVm vm) => _mapper.Map<CampaignStatus>(vm);

        public CampaignStatus? Get(Guid id)
        {
            return _context.CampaignStatuses.Find(id);
        }

        public IEnumerable<CampaignStatus> GetAll()
        {
            return _context.CampaignStatuses.Where(x => x.StatusName != "Новий" && x.StatusName != "Відхилено").ToList();
        }

        public CampaignStatus Add(CampaignStatusVm obj)
        {
            CampaignStatus res = VmToModel(obj);
            _context.CampaignStatuses.Add(res);
            _context.SaveChanges();
            return res;
        }

        public CampaignStatus? Update(CampaignStatusVm obj)
        {
            CampaignStatus? res = _context.CampaignStatuses.Find(obj.CampaignStatusId);
            _mapper.Map(obj, res);
            _context.SaveChanges();
            return res;
        }

        public bool Delete(Guid id)
        {
            var tmp = _context.CampaignStatuses.Find(id);
            if (tmp != null)
            {
                _context.CampaignStatuses.Remove(tmp);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
