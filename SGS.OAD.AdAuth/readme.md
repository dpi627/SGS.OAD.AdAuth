![](https://img.shields.io/badge/SGS-OAD-orange) 
![](https://img.shields.io/badge/proj-AD%20Authentication-purple) 
![](https://img.shields.io/badge/-4.7-3484D2?logo=dotnet)
![](https://img.shields.io/badge/-4.8-3484D2?logo=dotnet)
![](https://img.shields.io/badge/-Standard%202.0-056473?logo=dotnet)
![](https://img.shields.io/badge/-6-512BD4?logo=dotnet)
![](https://img.shields.io/badge/-8-512BD4?logo=dotnet)
![](https://img.shields.io/badge/-NuGet-004880?logo=nuget)

# ✨How to Use

以下說明套件使用方式

# 🌳Using Namespace

```csharp
using SGS.OAD.AdAuth;
```

# 🚀Quick Start

輸入帳號密碼，取得驗證結果 (布林值)

```csharp
bool isValid = AdAuthHelper.IsValid(UserID, Password);
```

# 💼Extra AD Information

取得常用 AD 資訊 (會先進行驗證，通過才會取出)

```csharp
AdInfoModel info = AdAuthHelper.GetInfo(UserID, Password);
```

# 🌐Specific AD Domain

預設 Domain 為 `APAC`，可自訂

```csharp
var valid = AdAuthHelper.IsValid(uid, pwd, "YourDomain");
var info = AdAuthHelper.GetInfo(uid, pwd, "YourDomain");
```

# 🚨工號

- `2024-12-19` 台灣工號資料異常，有進行修改
- `2025-02-05` 移除工號相關修改，回歸 AD 驗證

HR 相關資訊，例如工號、部門、中文姓名等等，請改用套件 `SGS.OAD.HrInfo`