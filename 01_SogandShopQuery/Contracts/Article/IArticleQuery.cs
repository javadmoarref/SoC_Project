namespace _01_SogandShopQuery.Contracts.Article;

public interface IArticleQuery
{
    List<ArticleQueryModel> LatestArticles();
    ArticleQueryModel GetArticleDetails(string slug);
}