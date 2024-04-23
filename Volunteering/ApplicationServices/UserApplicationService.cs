using Volunteering.Data.DomainServices;
using Volunteering.Data.Interfaces;
using Volunteering.Data.Models;
using Volunteering.Data.ViewModels;
using Volunteering.Helpers;

namespace Volunteering.ApplicationServices
{
    public class UserApplicationService 
    {
        private UserDomainService _domainService;
        public UserApplicationService(UserDomainService domainService)
        {
            _domainService = domainService;
        }

        public AuthResult Add (UserVM vm)
        {
            if (_domainService.FindByEmail(vm.Email) != null)
            {
                return new AuthResult()
                {
                    Result = false,
                    Messages = new List<string>()
                    {
                        "Email already exists"
                    }
                };
            }

            try
            {
                User user = _domainService.Add(vm);
                
                if (user != null)
                {
                    string token = _domainService.GenerateJwtToken(user);

                    return new AuthResult()
                    {
                        Result = true,
                        Token = token,
                        Messages = new List<string>() { "Register successfull"}
                    };
                }
            }
            catch (Exception ex)
            {
                return new AuthResult()
                {
                    Result = false,
                    Messages = new List<string>()
                    {
                        "Error on a server side when creating a user.",
                        ex.Message
                    }
                };
            }

            return new AuthResult()
            {
                Result = false,
                Messages = new List<string>()
                    {
                        "Internal error on the server when creating a user.",
                    }
            };
        }

        public IEnumerable<UserVM> GetAll()
        {
            return _domainService.GetAll().Select(x => _domainService.ConvertToVm(x));
        }
    }
}
