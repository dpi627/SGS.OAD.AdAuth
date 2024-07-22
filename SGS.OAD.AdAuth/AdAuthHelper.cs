using SGS.OAD.AdAuth.Models;
using System.DirectoryServices.AccountManagement;

namespace SGS.OAD.AdAuth;

public class AdAuthHelper
{
    /// <summary>
    /// 驗證使用者帳號密碼是否正確
    /// </summary>
    /// <param name="UserId">帳號</param>
    /// <param name="Password">密碼</param>
    /// <param name="Domain">AD域名</param>
    /// <returns></returns>
    public static bool IsValid(string UserId, string Password, string Domain = "APAC")
    {
        using PrincipalContext context = new(ContextType.Domain, Domain);
        return context.ValidateCredentials(UserId, Password);
    }

    /// <summary>
    /// 取得使用者資訊
    /// </summary>
    /// <param name="UserId">帳號</param>
    /// <param name="Password">密碼</param>
    /// <param name="Domain">AD域名</param>
    /// <returns></returns>
    public static AdInfoModel? GetInfo(string UserId, string Password, string Domain = "APAC")
    {
        using PrincipalContext context = new(ContextType.Domain, Domain);

        bool vaild = context.ValidateCredentials(UserId, Password);

        if (!vaild)
            return null;

        UserPrincipal user = UserPrincipal.FindByIdentity(context, UserId);

        if (user == null)
            return null;

        return new AdInfoModel() {
            Enabled = user.Enabled,
            Name = user.Name,
            EmployeeId = user.EmployeeId,
            DisplayName = user.DisplayName,
            Description = user.Description,
            EmailAddress = user.EmailAddress
        };
    }
}
