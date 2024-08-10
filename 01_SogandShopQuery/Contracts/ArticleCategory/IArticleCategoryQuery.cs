namespace _01_SogandShopQuery.Contracts.ArticleCategory;

public interface IArticleCategoryQuery
{
    List<ArticleCategoryQueryModel> GetArticleCategories();
    ArticleCategoryQueryModel GetArticleCategoryWithArticles(string slug);
}