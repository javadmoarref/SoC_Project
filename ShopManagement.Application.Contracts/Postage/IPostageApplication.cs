using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Postage
{
    public interface IPostageApplication
    {
        OperationResult Create(CreatePostage command);
        OperationResult Edit(EditPostage command);
        EditPostage GetPostage(long id);
        List<PostageViewModel> GetList();
    }
}
