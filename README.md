# 運輸管理系統 Transport Management System

## 專案說明

本系統為全端運輸管理平台，前端使用 Vue 3，後端使用 C# ASP.NET Core Web API，資料庫使用 SQL Server。

開發日期：2026-03-04

---

## 技術棧

| 層級 | 技術 |
|------|------|
| 前端框架 | Vue 3 + Vite |
| 前端 UI | Element Plus |
| 前端狀態管理 | Pinia |
| 前端路由 | Vue Router |
| 前端 HTTP | Axios |
| 地圖 | Leaflet + OpenStreetMap |
| 圖表 | ECharts |
| 後端框架 | ASP.NET Core Web API (.NET 10) |
| 後端驗證 | JWT Bearer Token |
| 密碼加密 | BCrypt.Net |
| ORM | Entity Framework Core |
| 資料庫 | SQL Server (SQLEXPRESS) |

---

## 專案結構

```
transport-frontend/          # Vue 3 前端
├── src/
│   ├── api/                 # Axios API 封裝
│   │   ├── http.js          # Axios 實例、攔截器
│   │   ├── authApi.js       # 登入 API
│   │   ├── orderApi.js      # 訂單 API
│   │   ├── vehicleApi.js    # 車輛 API
│   │   ├── driverApi.js     # 司機 API
│   │   └── customerApi.js   # 客戶 API
│   ├── stores/
│   │   └── auth.js          # Pinia 登入狀態管理
│   ├── views/               # 頁面元件
│   │   ├── LoginView.vue    # 登入頁
│   │   ├── DashboardView.vue # 儀表板
│   │   ├── OrderView.vue    # 訂單管理
│   │   ├── VehicleView.vue  # 車輛管理
│   │   ├── DriverView.vue   # 司機管理
│   │   ├── CustomerView.vue # 客戶管理
│   │   ├── RouteView.vue    # 路線規劃
│   │   ├── TrackingView.vue # 即時追蹤
│   │   └── ReportView.vue   # 報表統計
│   ├── router/
│   │   └── index.js         # 路由設定、登入守衛
│   └── App.vue              # 主版型（側邊欄 + Header）

transport-frontendAPI/       # C# 後端
└── TransportApi/
    ├── Controllers/         # API 端點
    │   ├── AuthController.cs
    │   ├── OrdersController.cs
    │   ├── VehiclesController.cs
    │   ├── DriversController.cs
    │   └── CustomersController.cs
    ├── Models/              # 資料庫 Entity
    │   ├── Order.cs
    │   ├── Vehicle.cs
    │   ├── Driver.cs
    │   ├── Customer.cs
    │   └── User.cs
    ├── DTOs/                # 前端傳入/回傳格式
    │   ├── AuthDto.cs
    │   ├── OrderDto.cs
    │   ├── VehicleDto.cs
    │   ├── DriverDto.cs
    │   └── CustomerDto.cs
    ├── Data/
    │   ├── AppDbContext.cs          # EF Core DbContext
    │   └── AppDbContextFactory.cs  # Migration 用 Factory
    ├── Helpers/
    │   └── JwtHelper.cs     # JWT Token 產生
    ├── Migrations/          # EF Core 資料庫版本紀錄
    ├── appsettings.json     # 連線字串、JWT 設定
    └── Program.cs           # 服務註冊、中介層設定
```

---

## 功能模組

### 儀表板
- 今日訂單、運送中、已完成、可用車輛統計卡片
- 本週訂單趨勢長條圖
- 車輛狀態分佈進度條
- 最新訂單列表

### 訂單管理
- 列表、分頁、關鍵字搜尋、狀態篩選
- 新增訂單（訂單編號自動產生：ORD-YYYYMMDD-XXX）
- 編輯、刪除（含確認 Dialog）
- 狀態：待派車 / 運送中 / 已完成 / 已取消

### 車輛管理
- 列表、分頁、搜尋、狀態篩選
- 新增、編輯、刪除
- 送修 / 完修快速切換
- 狀態：可用 / 運送中 / 維修中

### 司機管理
- 列表、分頁、搜尋、狀態篩選
- 新增、編輯、刪除
- 離職時顯示離職日期欄位
- 身分證格式驗證（1碼英文 + 9碼數字）
- 狀態：在職 / 休假 / 離職

### 客戶管理
- 列表、分頁、搜尋、等級篩選
- 新增、編輯、刪除
- 統一編號格式驗證（8位數字）
- Email 格式驗證
- 等級：VIP / 一般 / 停用

### 路線規劃
- 路線列表（含起點、終點、距離、預估時間）
- Leaflet 地圖顯示路線與停靠點
- 新增、編輯、刪除路線
- 動態新增停靠點

### 即時追蹤
- 運送中訂單列表（含進度條）
- Leaflet 地圖顯示車輛即時位置
- 開始/停止追蹤（目前為模擬，可串接 GPS API）

### 報表統計
- 訂單趨勢折線圖（ECharts）
- 訂單狀態圓餅圖
- 司機績效橫條圖
- 車輛使用率儀表圖

---

## 啟動方式

### 前端

```bash
cd transport-frontend
npm install
npm run dev
# 開啟 http://localhost:5173
```

### 後端

```bash
cd transport-frontendAPI/TransportApi
dotnet run
# 監聽 http://localhost:5107
```

### 資料庫初始化

```bash
cd transport-frontendAPI/TransportApi
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## API 端點

| 方法 | 路徑 | 說明 | 需要 Token |
|------|------|------|-----------|
| POST | /api/auth/login | 登入取得 JWT Token | 否 |
| GET | /api/orders | 取得訂單列表 | 是 |
| POST | /api/orders | 新增訂單 | 是 |
| PUT | /api/orders/{id} | 更新訂單 | 是 |
| DELETE | /api/orders/{id} | 刪除訂單 | 是 |
| GET | /api/vehicles | 取得車輛列表 | 是 |
| POST | /api/vehicles | 新增車輛 | 是 |
| PUT | /api/vehicles/{id} | 更新車輛 | 是 |
| DELETE | /api/vehicles/{id} | 刪除車輛 | 是 |
| GET | /api/drivers | 取得司機列表 | 是 |
| POST | /api/drivers | 新增司機 | 是 |
| PUT | /api/drivers/{id} | 更新司機 | 是 |
| DELETE | /api/drivers/{id} | 刪除司機 | 是 |
| GET | /api/customers | 取得客戶列表 | 是 |
| POST | /api/customers | 新增客戶 | 是 |
| PUT | /api/customers/{id} | 更新客戶 | 是 |
| DELETE | /api/customers/{id} | 刪除客戶 | 是 |

---

## 環境設定

### 前端 `src/api/http.js`
```javascript
baseURL: 'http://localhost:5107/api'  // 後端 API 位址
```

### 後端 `appsettings.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=DESKTOP-BOJ45R4\\SQLEXPRESS;Database=TransportDb;User Id=sa;Password=1234;TrustServerCertificate=True;"
  },
  "JwtSettings": {
    "SecretKey": "TransportSystem_JWT_SecretKey_2024_MustBe32CharsOrMore",
    "Issuer": "TransportApi",
    "Audience": "TransportFrontend",
    "ExpireMinutes": "480"
  }
}
```

---

## 預設帳號

| 帳號 | 密碼 | 角色 |
|------|------|------|
| admin | admin1234 | 管理員 |

---

## 待完成項目

- [ ] 前端各模組串接後端 API（目前使用模擬資料）
- [ ] 即時追蹤串接真實 GPS 裝置或 SignalR
- [ ] 路線規劃整合 Google Maps API
- [ ] 報表匯出（Excel / PDF）
- [ ] 操作員角色權限控制
- [ ] 帳號管理頁面
