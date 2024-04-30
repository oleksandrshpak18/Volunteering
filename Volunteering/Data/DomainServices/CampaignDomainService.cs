using Microsoft.EntityFrameworkCore;
using Volunteering.Data.Interfaces;
using Volunteering.Data.Models;
using Volunteering.Data.ViewModels;

namespace Volunteering.Data.DomainServices
{
    public class CampaignDomainService : IDomainService<CampaignVM, Campaign>
    {
        private AppDbContext _context;
        public CampaignDomainService(AppDbContext context)
        {
            _context = context;
        }

        public Campaign Add(CampaignVM obj)
        {
            throw new NotImplementedException();
        }

        public CampaignVM ConvertToVm(Campaign obj)
        {
            //TODO: implement ConvertToVm method
            throw new NotImplementedException();
        }

        public Campaign? Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Campaign> GetAll()
        {
            return _context.Campaigns
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
