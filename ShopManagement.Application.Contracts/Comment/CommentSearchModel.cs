namespace ShopManagement.Application.Contracts.Comment;

public class CommentSearchModel
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public long ProductId { get; set; }
    public string ProductName { get; set; }
}