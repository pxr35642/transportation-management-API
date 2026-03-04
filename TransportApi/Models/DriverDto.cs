using System;

namespace TransportApi.DTOs
{
    /// <summary>新增司機請求</summary>
    public class CreateDriverDto
    {
        /// <summary>姓名</summary>
        public string? Name { get; set; }

        /// <summary>身分證號</summary>
        public string? IdNo { get; set; }

        /// <summary>聯絡電話</summary>
        public string? Phone { get; set; }

        /// <summary>駕照類別</summary>
        public string? LicenseType { get; set; }

        /// <summary>駕照到期日</summary>
        public DateTime? LicenseExp { get; set; }

        /// <summary>到職日期</summary>
        public DateTime? HireDate { get; set; }

        /// <summary>離職日期（在職時為 null）</summary>
        public DateTime? ResignDate { get; set; }

        /// <summary>狀態：0=在職 1=休假 2=離職</summary>
        public int? Status { get; set; }
    }

    /// <summary>更新司機請求（欄位與新增相同）</summary>
    public class UpdateDriverDto : CreateDriverDto { }
}