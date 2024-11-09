using _0_Framework.Application;
using ShopManagement.Application.Contracts.ContactUS;
using ShopManagement.Application.Contracts.Postage;
using ShopManagement.Domain.ContactUsAgg;
using ShopManagement.Domain.PostageAgg;

namespace ShopManagement.Application
{
    public class ContactUsApplication : IContactUsApplication
    {
       private readonly IContactUsRepository _contactUsRepository;

        public ContactUsApplication(IContactUsRepository contactUsRepository)
        {
            _contactUsRepository = contactUsRepository;
        }

        public OperationResult Create(CreateContactUs command)
        {
            var operation = new OperationResult();
            var contactUs = new ContactUs(command.Mobile,command.Email,command.InstagramAccount);
            _contactUsRepository.Create(contactUs);
            _contactUsRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditContactUs command)
        {
            var operation = new OperationResult();
            var contactUs = _contactUsRepository.Get(command.Id);
            if (contactUs == null)
            {
                return operation.Failed(ApplicationMessage.RecordNotFound);
            }
            contactUs.Edit(command.Mobile, command.Email, command.InstagramAccount);
            _contactUsRepository.SaveChanges();
            return operation.Succedded();
        }

        public List<ContactUSViewModel> GetList()
        {
            return _contactUsRepository.GetList();
        }

        public EditContactUs GetDetails(long id)
        {
            return _contactUsRepository.GetDetails(id);
        }
    }
}
