using System;

namespace TransportApi.Models
{
    public class Vehicle
    {
        /// <summary>主鍵</summary>
        public int? Id { get; set; }

        /// <summary>車牌號碼，例：ABC-1234</summary>
        public string? PlateNo { get; set; }

        /// <summary>廠牌車型，例：賓士 Actros</summary>
        public string? Brand { get; set; }

        /// <summary>車輛類型：小貨車 / 中型貨車 / 大型貨車 / 聯結車 / 冷凍車</summary>
        public string? Type { get; set; }

        /// <summary>載重噸數</summary>
        public decimal? Capacity { get; set; }

        /// <summary>目前里程數（公里）</summary>
        public int? Mileage { get; set; }

        /// <summary>下次保養日期（null 表示未設定）</summary>
        public DateTime? NextMaintain { get; set; }

        /// <summary>車輛狀態：0=可用 1=運送中 2=維修中</summary>
        public int? Status { get; set; }

        /// <summary>建立時間</summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>最後更新時間（null 表示從未更新）</summary>
        public DateTime? UpdatedAt { get; set; }
    }
}