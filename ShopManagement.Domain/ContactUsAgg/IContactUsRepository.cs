using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ContactUS;

namespace ShopManagement.Domain.ContactUsAgg
{
    public interface IContactUsRepository:IRepository<long, ContactUs>
    {
        EditContactUs GetDetails(long id);
        List<ContactUSViewModel> GetList();
    }
}
