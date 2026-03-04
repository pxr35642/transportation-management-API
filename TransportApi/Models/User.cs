using System;

namespace TransportApi.Models
{
    public class User
    {
        /// <summary>主鍵</summary>
        public int? Id { get; set; }

        /// <summary>登入帳號</summary>
        public string? Username { get; set; }

        /// <summary>密碼雜湊，使用 BCrypt 加密，資料庫不儲存明碼</summary>
        public string? PasswordHash { get; set; }

        /// <summary>顯示名稱，例：系統管理員</summary>
        public string? FullName { get; set; }

        /// <summary>角色：Admin=管理員 / Operator=操作員</summary>
        public string? Role { get; set; }

        /// <summary>帳號建立時間</summary>
        public DateTime? CreatedAt { get; set; }
    }
}