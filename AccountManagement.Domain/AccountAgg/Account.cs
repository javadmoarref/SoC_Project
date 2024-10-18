using _0_Framework.Domain;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Domain.AccountAgg;

public class Account:EntityBase
{
    public string Fullname { get; private set; }
    public string Address { get; private set; }
    public string Password { get; private set; }
    public string Mobile { get; private set; }
    public long RoleId { get; private set; }
    public Role Role { get; private set; }
    public string ProfilePhoto { get; private set; }
    public string PostalCode { get; private set; }

    public Account(string fullname, string address, string password, string mobile, long roleId,
        string profilePhoto,string postalCode)
    {
        Fullname = fullname;
        Address = address;
        Password = password;
        Mobile = mobile;
        RoleId = roleId;
        if (roleId == 0)
        {
            RoleId = 2;
        }
        ProfilePhoto = profilePhoto;
        PostalCode = postalCode;
    }

    public void Edit(string fullname, string address, string mobile, long roleId,
        string profilePhoto,string postalCode)
    {
        Fullname = fullname;
        Address =address;
        Mobile = mobile;
        RoleId = roleId;
        if (!string.IsNullOrWhiteSpace(profilePhoto))
        {
            ProfilePhoto = profilePhoto;
        }
        PostalCode = postalCode;
    }

    public void ChangePassword(string password)
    {
        Password=password;
    }
}