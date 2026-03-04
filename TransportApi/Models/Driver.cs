using System;

namespace TransportApi.Models
{
    public class Driver
    {
        /// <summary>主鍵</summary>
        public int? Id { get; set; }

        /// <summary>司機姓名</summary>
        public string? Name { get; set; }

        /// <summary>身分證號，格式：1碼英文 + 9碼數字</summary>
        public string? IdNo { get; set; }

        /// <summary>聯絡電話</summary>
        public string? Phone { get; set; }

        /// <summary>駕照類別：職業小型車 / 職業普通車 / 職業大型車 / 職業聯結車</summary>
        public string? LicenseType { get; set; }

        /// <summary>駕照到期日</summary>
        public DateTime LicenseExp { get; set; }

        /// <summary>到職日期</summary>
        public DateTime HireDate { get; set; }

        /// <summary>離職日期（null 表示尚未離職）</summary>
        public DateTime? ResignDate { get; set; }

        /// <summary>累計完成趟次</summary>
        public int? TotalTrips { get; set; }

        /// <summary>狀態：0=在職 1=休假 2=離職</summary>
        public int? Status { get; set; }

        /// <summary>建立時間</summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>最後更新時間（null 表示從未更新）</summary>
        public DateTime? UpdatedAt { get; set; }
    }
}