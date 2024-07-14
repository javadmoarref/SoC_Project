namespace ShopManagement.Application.Contracts.Comment;

public class AddComment
{
    public string FullName { get;  set; }
    public string Email { get;  set; }
    public string Message { get;  set; }
    public long ProductId { get;  set; }
}