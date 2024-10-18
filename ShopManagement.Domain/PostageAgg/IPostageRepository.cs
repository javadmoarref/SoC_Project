using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Postage;

namespace ShopManagement.Domain.PostageAgg
{
    public interface IPostageRepository:IRepository<long, Postage>
    {
        EditPostage GetPostage(long id);
        List<PostageViewModel> GetList();
    }
}
