using _0_Framework.Application;
using ShopManagement.Application.Contracts.Postage;

namespace ShopManagement.Application.Contracts.ContactUS
{
    public interface IContactUsApplication
    {
        OperationResult Create(CreateContactUs command);
        OperationResult Edit(EditContactUs command);
        EditContactUs GetDetails(long id);
        List<ContactUSViewModel> GetList();
    }
}
