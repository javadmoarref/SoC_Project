using _0_Framework.Application;
using ShopManagement.Application.Contracts.AboutUs;
using ShopManagement.Application.Contracts.Postage;
using ShopManagement.Domain.AboutUsAgg;
using ShopManagement.Domain.PostageAgg;

namespace ShopManagement.Application
{
    public class AboutUsApplication : IAboutUsApplication
    {

        private readonly IAboutUsRepository _aboutUsRepository;
        private readonly IFileUploader _fileUploader;

        public AboutUsApplication(IAboutUsRepository aboutUsRepository, IFileUploader fileUploader)
        {
            _aboutUsRepository = aboutUsRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateAboutUs command)
        {
            var operation = new OperationResult();
            var fileName = _fileUploader.Upload(command.Picture, "aboutUs");
            var aboutUs = new AboutUs(fileName,command.Title,command.Description);
            _aboutUsRepository.Create(aboutUs);
            _aboutUsRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditAboutUs command)
        {
            var operation = new OperationResult();
            var aboutUs = _aboutUsRepository.Get(command.Id);
            if (aboutUs == null)
            {
                return operation.Failed(ApplicationMessage.RecordNotFound);
            }
            var fileName = _fileUploader.Upload(command.Picture, "aboutUs");
            aboutUs.Edit(fileName,command.Title,command.Description);
            _aboutUsRepository.SaveChanges();
            return operation.Succedded();
        }

        public EditAboutUs GetAboutUs(long id)
        {
            throw new NotImplementedException();
        }

        public List<AboutUsViewModel> GetList()
        {
            return _aboutUsRepository.GetList();
        }
    }
}
