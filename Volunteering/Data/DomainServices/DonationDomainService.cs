using AutoMapper;
using Volunteering.Data.Models;
using Volunteering.Data.ViewModels;

namespace Volunteering.Data.DomainServices
{
    public class DonationDomainService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public DonationDomainService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public DonationVM ModelToVm(Donation news) => _mapper.Map<DonationVM>(news);

        public List<DonationVM> ModelToVm(IEnumerable<Donation> newsList) => _mapper.Map<List<DonationVM>>(newsList);

        public Donation VmToModel(DonationVM vm) => _mapper.Map<Donation>(vm);

        public Donation Add (DonationVM vm)
        {
            Donation res = VmToModel(vm);
            _context.Donations.Add(res);
            _context.SaveChanges();
            return res;
        }

        public Donation GetById(Guid id) => _context.Donations.FirstOrDefault(x => x.DonationId == id);
    }
}
