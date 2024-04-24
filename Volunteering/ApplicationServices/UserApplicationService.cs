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

        public AuthResult Register (UserVM vm)
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


        public AuthResult Login(UserLoginRequestVM vm)
        {
            var existingUser = _domainService.FindByEmail(vm.Email);
            if ( existingUser == null)
            {
                return new AuthResult()
                {
                    Result = false,
                    Messages = new List<string>()
                    {
                        $"Email: {vm.Email}. Invalid payload"
                    }
                };
            }

            var isPasswordCorrect = _domainService.CheckPassword(existingUser, vm.Password);

            if (!isPasswordCorrect)
            {
                return new AuthResult()
                {
                    Result = false,
                    Messages = new List<string>()
                    {
                        "Invalid credentials" // no more info for security reasons
                    }
                };
            }

            var jwtToken = _domainService.GenerateJwtToken(existingUser);

            return new AuthResult()
            {
                Token = jwtToken,
                Result = true
            };

        }
        public IEnumerable<UserVM> GetAll()
        {
            return _domainService.GetAll().Select(x => _domainService.ConvertToVm(x));
        }
    }
}
