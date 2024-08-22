namespace _01_SogandShopQuery.Contracts.Comment;

public class CommentQueryModel
{
    public long Id { get; set; }
    public string FullName { get; set; }
    public string Message { get; set; }
    public string CreationDate { get; set; }
    public long ParentId { get; set; }
    public string ParentName { get; set; }
    //public string ParentCommentDate { get; set; }
    //public string ParentCommentMessage { get; set; }
}