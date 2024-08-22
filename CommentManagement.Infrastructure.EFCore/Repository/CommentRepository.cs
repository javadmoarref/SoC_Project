using _0_Framework.Application;
using _0_Framework.Infrastructure;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore.Storage;

namespace CommentManagement.Infrastructure.EFCore.Repository;

public class CommentRepository:RepositoryBase<long,Comment>,ICommentRepository
{
    private readonly CommentContext _context;
    public CommentRepository(CommentContext context) : base(context)
    {
        _context = context;
    } 

    public List<CommentViewModel> Search(CommentSearchModel searchModel)
    {
        var query = _context.Comments
            .Select(x => new CommentViewModel()
        {
            Id = x.Id,
            FullName = x.FullName,
            Email = x.Email,
            Message = x.Message,
            IsConfirmed = x.IsConfirmed,
            IsCanceled = x.IsCanceled,
            CommentDate = x.CreationDate.ToFarsi(),
            OwnerRecordId = x.OwnerRecordId,
            Type = x.Type
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