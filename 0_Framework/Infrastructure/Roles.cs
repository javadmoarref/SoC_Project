namespace _0_Framework.Infrastructure;

public static class Roles
{
    public const string Administrator = "1";
    public const string SystemUser = "2";
    public const string ColleagueUser = "10002";

    public static string GetRoleBy(long id)
    {
        switch (id)
        {
            case 1:
                return "مدیر سیستم";
            case 2:
                return "کاربر سیستم";
            case 10002:
                return "کاربر همکار";
            default:
                return "";
        }
    }
}