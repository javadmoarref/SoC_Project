using _0_Framework.Application;
using ShopManagement.Application.Contracts.Postage;
using ShopManagement.Domain.PostageAgg;

namespace ShopManagement.Application
{
    public class PostageApplication : IPostageApplication
    {
        private readonly IPostageRepository _postageRepository;

        public PostageApplication(IPostageRepository postageRepository)
        {
            _postageRepository = postageRepository;
        }

        public OperationResult Create(CreatePostage command)
        {
            var operation = new OperationResult();
            if (_postageRepository.Exists(x => x.PostagePrice == command.PostagePrice))
            {
                return operation.Failed(ApplicationMessage.DuplicatedRecord);
            }
            var postage = new Postage(command.PostagePrice);
            _postageRepository.Create(postage);
            _postageRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditPostage command)
        {
            var operation = new OperationResult();
            var postage = _postageRepository.Get(command.Id);
            if (postage == null)
            {
                return operation.Failed(ApplicationMessage.RecordNotFound);
            }
            if(_postageRepository.Exists(x=>x.PostagePrice==command.PostagePrice && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessage.DuplicatedRecord);
            }
            postage.Edit(command.PostagePrice);
            _postageRepository.SaveChanges();
            return operation.Succedded();
        }

        public List<PostageViewModel> GetList()
        {
            return _postageRepository.GetList();
        }

        public EditPostage GetPostage(long id)
        {
            return _postageRepository.GetPostage(id);
        }
    }
}
