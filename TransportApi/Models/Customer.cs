using System;

namespace TransportApi.Models
{
    public class Customer
    {
        /// <summary>主鍵</summary>
        public int? Id { get; set; }

        /// <summary>客戶公司名稱</summary>
        public string? Name { get; set; }

        /// <summary>統一編號，8位數字</summary>
        public string? TaxId { get; set; }

        /// <summary>聯絡人姓名</summary>
        public string? Contact { get; set; }

        /// <summary>聯絡電話</summary>
        public string? Phone { get; set; }

        /// <summary>聯絡 Email</summary>
        public string? Email { get; set; }

        /// <summary>公司地址</summary>
        public string? Address { get; set; }

        /// <summary>客戶等級：VIP / 一般 / 停用</summary>
        public string? Level { get; set; }

        /// <summary>備註（選填）</summary>
        public string? Remark { get; set; }

        /// <summary>累計訂單數</summary>
        public int? TotalOrders { get; set; }

        /// <summary>建立時間</summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>最後更新時間（null 表示從未更新）</summary>
        public DateTime? UpdatedAt { get; set; }
    }
}