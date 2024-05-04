using Volunteering.Data.DomainServices;
using Volunteering.Data.ViewModels;

namespace Volunteering.ApplicationServices
{
    public class CategoryApplicationService
    {
        private CategoryDomainService _domainService;
        public CategoryApplicationService(CategoryDomainService domainService)
        {
            _domainService = domainService;
        }

        public IEnumerable<CategoryVM> GetAll()
        {
            return _domainService.ModelToVm(_domainService.GetAll());
        }

        public CategoryVM Add(CategoryVM vm)
        {
            return _domainService.ModelToVm(_domainService.AddCategoryWithSubcategories(vm));
        }

        public CategoryVM Update(CategoryVM vm)
        {
            var res = _domainService.Update(vm);
            if (res == null)
            {
                return null;
            }

            return _domainService.ModelToVm(res);
        }

        public bool Delete(CategoryVM vm)
        {
            return _domainService.Delete(vm);
        }
    }
}
