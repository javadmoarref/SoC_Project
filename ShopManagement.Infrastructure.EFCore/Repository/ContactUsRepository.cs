using _0_Framework.Infrastructure;
using ShopManagement.Application.Contracts.ContactUS;
using ShopManagement.Domain.ContactUsAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ContactUsRepository:RepositoryBase<long,ContactUs>, IContactUsRepository
    {
        private readonly ShopContext _context;

        public ContactUsRepository(ShopContext context):base(context) 
        {
            _context = context;
        }

        public EditContactUs GetDetails(long id)
        {
            return _context.ContactUs.Select(x=>new EditContactUs() 
            {
                Id = id,
                Mobile=x.Mobile,
                Email=x.Email,
                InstagramAccount=x.InstagramAccount
            }).FirstOrDefault(x=>x.Id==id);
        }

        public List<ContactUSViewModel> GetList()
        {
            return _context.ContactUs.Select(x => new ContactUSViewModel() 
            {
                Id = x.Id,
                Mobile=x.Mobile,
                Email=x.Email,
                InstagramAccount=x.InstagramAccount
            }).ToList();
        }
    }
}
