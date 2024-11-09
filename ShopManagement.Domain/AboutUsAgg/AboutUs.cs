using _0_Framework.Domain;

namespace ShopManagement.Domain.PostageAgg
{
    public class AboutUs : EntityBase
    {
        public string Picture { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }

        public AboutUs(string picture, string title, string description)
        {
            Picture = picture;
            Title = title;
            Description = description;
        }

        public void Edit(string picture, string title, string description)
        {
            if (!string.IsNullOrWhiteSpace(picture))
            {
                Picture = picture;
            }
            Title=title;
            Description=description;
        }
    }
}
