using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
            if (vm.DonationValue <= 0) { throw new Exception("Значення має бути більше 0"); }

            Campaign? campaign;

            if(vm.CampaignId != null)
            {
                campaign = _context.Campaigns
                    .Include(c => c.CampaignStatus)
                    .FirstOrDefault(c => c.CampaignId == vm.CampaignId);

                if (campaign == null) { throw new Exception("Такого збору не існує"); }
                if (campaign?.CampaignStatus?.StatusName == "Очікується звіт") {throw new Exception("Необхідну суму вже зібрано. Оберіть інший збір");}
            } 
            else
            {
                /// TODO: logic for choosing where to transfer money
                /// for now it will be just saved to the donations and transferred to the most recent campaign
                /// business logic to be improved later
                campaign = _context.Campaigns.OrderBy(x => x.CampaignGoal - x.Accumulated).First();
            }

            campaign.Accumulated += vm.DonationValue;
            if (campaign.Accumulated >= campaign.CampaignGoal)
            {
                campaign.CampaignStatus = _context.CampaignStatuses.FirstOrDefault(x => x.StatusName == "Очікується звіт");
            }
            _context.SaveChanges();

            Donation res = VmToModel(vm);
            _context.Donations.Add(res);
            _context.SaveChanges();

            return res;
        }

        public Donation GetById(Guid id) => _context.Donations.Include(x => x.Campaign).FirstOrDefault(x => x.DonationId == id);

        public List<Donation> GetByUserId(Guid userId)
        {
            return _context.Donations.Include(x => x.Campaign).Where(x => x.UserId == userId).OrderByDescending(x => x.CreateDate).ToList();
        }
    }
}
