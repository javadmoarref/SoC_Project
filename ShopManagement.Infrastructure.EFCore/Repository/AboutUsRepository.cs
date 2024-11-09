using _0_Framework.Infrastructure;
using ShopManagement.Application.Contracts.AboutUs;
using ShopManagement.Domain.AboutUsAgg;
using ShopManagement.Domain.PostageAgg;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class AboutUsRepository: RepositoryBase<long, AboutUs>, IAboutUsRepository
    {
        private readonly ShopContext _context;
        public AboutUsRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditAboutUs GetAboutUs(long id)
        {
            return _context.AboutUs.Select(x=>new EditAboutUs() 
            {
                Id = id,
                Title = x.Title,
                Description = x.Description
            }).FirstOrDefault(x=>x.Id == id);
        }

        public List<AboutUsViewModel> GetList()
        {
            return _context.AboutUs.Select(x => new AboutUsViewModel() 
            {
                Id=x.Id,
                Picture=x.Picture,
                Title=x.Title,
                Description=x.Description
            }).ToList();
        }
    }
}
