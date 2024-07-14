using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository;

public class CommentRepository:RepositoryBase<long,Comment>,ICommentRepository
{
    private readonly ShopContext _context;
    public CommentRepository(ShopContext context) : base(context)
    {
        _context = context;
    }

    public List<CommentViewModel> Search(CommentSearchModel searchModel)
    {
        var query = _context.Comments
            .Include(x=>x.Product)
            .Select(x => new CommentViewModel()
        {
            Id = x.Id,
            FullName = x.FullName,
            Email = x.Email,
            Message = x.Message,
            IsConfirmed = x.IsConfirmed,
            IsCanceled = x.IsCanceled,
            ProductId = x.ProductId,
            ProductName = x.Product.Name,
            CommentDate = x.CreationDate.ToFarsi()
        });

        if (!string.IsNullOrWhiteSpace(searchModel.FullName))
        {
            query = query.Where(x => x.FullName.Contains(searchModel.FullName));
        }

        if (!string.IsNullOrWhiteSpace(searchModel.Email))
        {
            query = query.Where(x => x.Email.Contains(searchModel.Email));
        }

        return query.OrderByDescending(x=>x.Id).ToList();
    }
}