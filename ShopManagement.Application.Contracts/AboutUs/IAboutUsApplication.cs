using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.AboutUs
{
    public interface IAboutUsApplication
    {
        OperationResult Create(CreateAboutUs command);
        OperationResult Edit(EditAboutUs command);
        EditAboutUs GetAboutUs(long id);
        List<AboutUsViewModel> GetList();
    }
}
