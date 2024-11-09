using _0_Framework.Domain;

namespace ShopManagement.Domain.ContactUsAgg
{
    public class ContactUs : EntityBase
    {
        public string Mobile { get; private set; }
        public string Email { get; private set; }
        public string InstagramAccount { get; private set; }

        public ContactUs(string mobile, string email, string instagramAccount)
        {
            Mobile = mobile;
            Email = email;
            InstagramAccount = instagramAccount;
        }

        public void Edit(string mobile, string email, string instagramAccount)
        {
            Mobile = mobile;
            Email = email;
            InstagramAccount = instagramAccount;
        }
    }
    
}
