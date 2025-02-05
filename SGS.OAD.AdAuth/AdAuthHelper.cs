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
    /// <returns>如果帳號密碼正確，返回 true，否則返回 false</returns>
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
    /// <returns>如果帳號密碼正確，返回 AdInfoModel 物件，否則返回 null</returns>
    public static AdInfoModel? GetInfo(string UserId, string Password, string Domain = "APAC")
    {
        using PrincipalContext context = new(ContextType.Domain, Domain);

        if (!context.ValidateCredentials(UserId, Password))
            return null;

        UserPrincipal user = UserPrincipal.FindByIdentity(context, UserId);

        if (user == null)
            return null;

        return new AdInfoModel()
        {
            Enabled = user.Enabled,
            Name = user.Name,
            DisplayName = user.DisplayName,
            Description = user.Description,
            EmailAddress = user.EmailAddress
        };
    }

    /// <summary>
    /// 非同步驗證使用者帳號密碼是否正確
    /// </summary>
    /// <param name="UserId">帳號</param>
    /// <param name="Password">密碼</param>
    /// <param name="Domain">AD域名</param>
    /// <param name="cancellationToken">取消操作的標記</param>
    /// <returns>如果帳號密碼正確，返回 true，否則返回 false</returns>
    public static async Task<bool> IsValidAsync(string UserId, string Password, string Domain = "APAC", CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            return IsValid(UserId, Password, Domain);
        }, cancellationToken);
    }

    /// <summary>
    /// 非同步取得使用者資訊
    /// </summary>
    /// <param name="UserId">帳號</param>
    /// <param name="Password">密碼</param>
    /// <param name="Domain">AD域名</param>
    /// <param name="cancellationToken">取消操作的標記</param>
    /// <returns>如果帳號密碼正確，返回 AdInfoModel 物件，否則返回 null</returns>
    public static async Task<AdInfoModel?> GetInfoAsync(string UserId, string Password, string Domain = "APAC", CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            return GetInfo(UserId, Password, Domain);
        }, cancellationToken);
    }
}
