namespace CommentManagement.Application.Contracts.Comment;

public class CommentSearchModel
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public long OwnerRecordId { get; set; }
}