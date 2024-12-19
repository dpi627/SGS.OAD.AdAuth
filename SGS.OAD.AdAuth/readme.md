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

## 🌳Using Namespace

```csharp
using SGS.OAD.AdAuth;
```

## 🚀Quick Start

基本驗證方式：輸入帳號密碼，取得驗證結果(布林值)

```csharp
bool isValid = AdAuthHelper.IsValid(UserID, Password);
```

## 💼Extra AD Information

以下可取得常用 AD 資訊 (會先進行驗證，成功才會取出)

```csharp
AdInfoModel info = AdAuthHelper.GetInfo(UserID, Password);
```

## 🌐Specific AD Domain

預設 Domain 為 `APAC`，若要指定請使用下列方法

```csharp
var valid = AdAuthHelper.IsValid(uid, pwd, "YourDomain");
var info = AdAuthHelper.GetInfo(uid, pwd, "YourDomain");
```

## 🚨工號異常處理

```csharp
AdInfoModel? info = AdAuthHelper.GetInfo(
	userId, 
	password, 
	connectionString: "YourConnectionString"
	);
```

- 傳入外部資料連結 (現為 IT 提供 HR 資料來源)
- 非必填，如未提供會顯示原始 AD 資料 (可能是錯的)