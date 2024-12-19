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
    /// 取得員工編號
    /// 如果系統本身已經可取得AD帳號，可直接呼叫此方法
    /// </summary>
    /// <param name="ConnectionString">外部員工資料連線字串</param>
    /// <param name="AdAccount">AD帳號</param>
    /// <returns>員工編號</returns>
    public static string GetEmpId(string ConnectionString, string AdAccount)
    {
        return new HrService(ConnectionString).GetStaffCode(AdAccount);
    }

    /// <summary>
    /// 取得使用者資訊
    /// </summary>
    /// <param name="UserId">帳號</param>
    /// <param name="Password">密碼</param>
    /// <param name="Domain">AD域名</param>
    /// <param name="ConnectionString">外部資料連線</param>
    /// <returns></returns>
    public static AdInfoModel? GetInfo(string UserId, string Password, string Domain = "APAC", string? ConnectionString = null)
    {
        using PrincipalContext context = new(ContextType.Domain, Domain);

        bool vaild = context.ValidateCredentials(UserId, Password);

        if (!vaild)
            return null;

        UserPrincipal user = UserPrincipal.FindByIdentity(context, UserId);

        if (user == null)
            return null;

        // 如有提供外部資料連截，工號改由外部連結取得
        if (ConnectionString != default)
            user.EmployeeId = GetEmpId(ConnectionString, user.Name);

        return new AdInfoModel()
        {
            Enabled = user.Enabled,
            Name = user.Name,
            EmployeeId = user.EmployeeId,
            DisplayName = user.DisplayName,
            Description = user.Description,
            EmailAddress = user.EmailAddress
        };
    }

    public static async Task<bool> IsValidAsync(string UserId, string Password, string Domain = "APAC", CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            return IsValid(UserId, Password, Domain);
        }, cancellationToken);
    }

    public static async Task<AdInfoModel?> GetInfoAsync(string UserId, string Password, string Domain = "APAC", CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            return GetInfo(UserId, Password, Domain);
        }, cancellationToken);
    }
}
