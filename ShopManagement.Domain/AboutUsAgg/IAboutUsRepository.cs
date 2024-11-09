using _0_Framework.Domain;
using ShopManagement.Application.Contracts.AboutUs;
using ShopManagement.Application.Contracts.Postage;
using ShopManagement.Domain.PostageAgg;

namespace ShopManagement.Domain.AboutUsAgg
{
    public interface IAboutUsRepository:IRepository<long, AboutUs>
    {
        EditAboutUs GetAboutUs(long id);
        List<AboutUsViewModel> GetList();
    }
}
