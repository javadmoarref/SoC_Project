using _0_Framework.Infrastructure;
using ShopManagement.Application.Contracts.Postage;
using ShopManagement.Domain.PostageAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class PostageRepository : RepositoryBase<long, Postage>, IPostageRepository
    {
        private readonly ShopContext _shopContext;

        public PostageRepository(ShopContext context):base(context) 
        {
            _shopContext = context;
        }

        public List<PostageViewModel> GetList()
        {
            return _shopContext.Postages.Select(x => new PostageViewModel()
            {
                Id = x.Id,
                PostagePrice = x.PostagePrice
            }).OrderByDescending(x=>x.Id).ToList();
        }

        public EditPostage GetPostage(long id)
        {
            return _shopContext.Postages.Select(x=>new EditPostage()
            {
                Id = x.Id,
                PostagePrice=x.PostagePrice
            }).FirstOrDefault(x=>x.Id== id);
        }
    }
}
