using System;

namespace TransportApi.Models
{
    public class Order
    {
        /// <summary>主鍵</summary>
        public int? Id { get; set; }

        /// <summary>訂單編號，格式 ORD-YYYYMMDD-XXX</summary>
        public string? OrderNo { get; set; }

        /// <summary>客戶名稱</summary>
        public string? CustomerName { get; set; }

        /// <summary>起點</summary>
        public string? Origin { get; set; }

        /// <summary>終點</summary>
        public string? Destination { get; set; }

        /// <summary>負責司機姓名</summary>
        public string? Driver { get; set; }

        /// <summary>訂單狀態：0=待派車 1=運送中 2=已完成 3=已取消</summary>
        public int? Status { get; set; }

        /// <summary>建立時間</summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>最後更新時間（null 表示從未更新）</summary>
        public DateTime? UpdatedAt { get; set; }
    }
}