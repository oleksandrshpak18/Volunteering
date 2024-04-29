using System.Linq;
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

        public AuthResult Register (UserRegisterRequest vm)
        {
            if (_domainService.FindByEmail(vm.Email) != null)
            {
                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Email already exists"
                    }
                };
            }

            try
            {
                User user = _domainService.Register(vm);

                if (user != null)
                {
                    string token = _domainService.GenerateJwtToken(user);

                    return new AuthResult()
                    {
                        Result = true,
                        Token = token
                    };
                }
            }
            catch (Exception ex)
            {
                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Error on a server side when creating a user.",
                        ex.Message
                    }
                };
            }

            return new AuthResult()
            {
                Result = false,
                Errors = new List<string>()
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
                    Errors = new List<string>()
                    {
                        $"Email: {vm.Email}. Invalid payload"
                    }
                };
            }

            var isPasswordCorrect = _domainService.VerifyPassword(existingUser, vm.Password);

            if (!isPasswordCorrect)
            {
                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
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
            // return _domainService.ModelToVm(_domainService.GetAll());
            return _domainService.GetAll().Select(x => _domainService.ConvertToVm(x));
        }

        public bool ?IsInfoFilled(Guid userId)
        {
            return _domainService.IsInfoFilled(userId);
        }

        public UserVM Update(Guid userId, UserDetailsVM user)
        {
            var res = _domainService.Update(userId, user);

            if (res == null)
            {
                return null;
            }

            //return _domainService.ModelToVm(res);
            return _domainService.ConvertToVm(res);
        }
    }
}
