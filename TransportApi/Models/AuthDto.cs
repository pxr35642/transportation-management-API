namespace TransportApi.DTOs
{
    /// <summary>登入請求</summary>
    public class LoginRequestDto
    {
        /// <summary>帳號</summary>
        public string? Username { get; set; }

        /// <summary>密碼（明碼，後端驗證後不儲存）</summary>
        public string? Password { get; set; }
    }

    /// <summary>登入成功回傳</summary>
    public class LoginResponseDto
    {
        /// <summary>JWT Token，前端存入 localStorage 後每次請求帶入 Header</summary>
        public string? Token    { get; set; }

        /// <summary>顯示名稱，用於畫面右上角顯示</summary>
        public string? FullName { get; set; }

        /// <summary>角色，用於前端控制選單權限</summary>
        public string? Role     { get; set; }
    }
}