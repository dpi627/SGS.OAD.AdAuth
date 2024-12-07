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