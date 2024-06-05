using Volunteering.Data.DomainServices;
using Volunteering.Data.Models;
using Volunteering.Data.ViewModels;
using Volunteering.Helpers;

namespace Volunteering.ApplicationServices
{
    public class DonationApplicationService
    {
        private DonationDomainService _domainService;
        public DonationApplicationService(DonationDomainService domainService)
        {
            _domainService = domainService;
        }

        public Response<DonationVM> Add (DonationVM vm)
        {
            try
            {
                var res = _domainService.Add(vm);
                if (res != null)
                {
                    var donationVm = _domainService.ModelToVm(res);
                    return new Response<DonationVM>
                    {
                        Data = donationVm
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<DonationVM>
                {
                    Error = ex.Message
                };
            }

            return new Response<DonationVM>
            {
                Error = "Unknown error or donation could not be added."
            };
        }

        public DonationVM GetById(Guid id)
        {
            return _domainService.ModelToVm(_domainService.GetById(id));
        }

        public Response<List<DonationVM>> GetByUserId(Guid userId)
        {
            try
            {
                var res = _domainService.GetByUserId(userId);
                if (res != null)
                {
                    var donationList = _domainService.ModelToVm(res);
                    return new Response<List<DonationVM>>
                    {
                        Data = donationList
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<List<DonationVM>>
                {
                    Error = ex.Message
                };
            }

            return new Response<List<DonationVM>>
            {
                Error = "Unknown error or donation could not be added."
            };

        }
    }
}
