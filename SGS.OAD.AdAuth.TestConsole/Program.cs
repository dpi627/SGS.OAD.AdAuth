using Microsoft.Extensions.Configuration;
using SGS.OAD.DB;
using System.Reflection;
using System.Text;

namespace SGS.OAD.AdAuth.TestConsole;

internal class Program
{
    static void Main()
    {
        Console.InputEncoding = System.Text.Encoding.UTF8;
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // 讀取組態檔
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddUserSecrets<Program>() //如果有 User Secrets 優先讀取
            .Build();

        string uid = config["UserID"]!;
        string pwd = config["Password"]!;
        //string dmn = config["Domain"]!; // 預設為 APAC，如非此域名則需要設定

        // 驗證帳號密碼是否有效
        bool valid = AdAuthHelper.IsValid(uid, pwd);
        Console.WriteLine($"IsValid: {valid}\n");

        var connectionString = DbInfoBuilder
                .Init()
                .SetServer("APSE-IDB029")
                .SetDatabase("HR_DataSharing")
                .SetAppName("AdAuthTest")
                .Build()
                .ConnectionString;

        // 取得使用者資訊，會先驗證，如驗證無效取得 null
        AdInfoModel? info = AdAuthHelper.GetInfo(
            uid,
            pwd,
            ConnectionString: connectionString
            );

        Console.WriteLine($"EmpId: {AdAuthHelper.GetEmpId(connectionString, info.Name)}{Environment.NewLine}");

        if (info != null)
            Console.WriteLine(Serialize<AdInfoModel>(info));
        else
            Console.WriteLine("查無資料");

        // 單獨測試取得工號
        var adId = "Nancy-Tw_Hu";
        var empNo = AdAuthHelper.GetEmpId(connectionString, adId);
        Console.WriteLine($"{Environment.NewLine}AdId: {adId}, EmpNo: {empNo}");

        var hrInfo = AdAuthHelper.GetHrInfo(connectionString, adId);
        if (hrInfo != null)
            Console.WriteLine(Serialize<HrInfoModel>(hrInfo));
        else
            Console.WriteLine("查無資料");
    }

    #region other methods
    /// <summary>
    /// 序列化類別之屬性與值
    /// </summary>
    public static string Serialize<T>(T model)
    {
        var sb = new StringBuilder();
        var properties = model!.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var prop in properties)
        {
            var value = prop.GetValue(model);
            if (value != null)
            {
                if (sb.Length > 0) sb.AppendLine();
                sb.Append($"{prop.Name}: {value}");
            }
        }

        return sb.ToString();
    }
    #endregion
}
