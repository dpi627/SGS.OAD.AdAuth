namespace SGS.OAD.AdAuth;

/// <summary>
/// 用於儲存 AD 使用者資訊
/// </summary>
public class AdInfoModel
{
    /// <summary>
    /// 是否啟用
    /// </summary>
    public bool? Enabled { get; set; }
    /// <summary>
    /// 使用者名稱，例如 Brian_Li
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// 員工編號
    /// </summary>
    public string? EmployeeId { get; set; }
    /// <summary>
    /// 顯示名稱，例如 Brian Li
    /// </summary>
    public string? DisplayName { get; set; }
    /// <summary>
    /// 描述，可能會取得部門資料，例如 TW_TPE_LABADM
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// 電子郵件
    /// </summary>
    public string? EmailAddress { get; set; }
    /// <summary>
    /// 中文姓名，取自HR資料(過渡時期使用)
    /// </summary>
    public string? CName { get; set; }
}
