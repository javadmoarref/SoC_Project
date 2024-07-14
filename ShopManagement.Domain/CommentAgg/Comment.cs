using _0_Framework.Domain;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Domain.CommentAgg;

public class Comment:EntityBase
{
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string Message { get; private set; }
    public bool IsConfirmed { get; private set; }
    public bool IsCanceled { get; private set; }
    public long ProductId { get; private set; }
    public Product Product { get; private set; }

    public Comment(string fullName, string email, string message, long productId)
    {
        FullName = fullName;
        Email = email;
        Message = message;
        ProductId = productId;
        IsConfirmed=false;
        IsCanceled=false;
    }

    public void Confirm()
    {
        IsConfirmed=true;
        IsCanceled = false;
    }

    public void Cancel()
    {
        IsCanceled=true;
        IsConfirmed=false;
    }
}