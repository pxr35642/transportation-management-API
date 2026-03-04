using System;

namespace TransportApi.DTOs
{
    /// <summary>新增車輛請求</summary>
    public class CreateVehicleDto
    {
        /// <summary>車牌號碼</summary>
        public string? PlateNo { get; set; }

        /// <summary>廠牌車型</summary>
        public string? Brand { get; set; }

        /// <summary>車輛類型</summary>
        public string? Type { get; set; }

        /// <summary>載重噸數</summary>
        public decimal? Capacity { get; set; }

        /// <summary>目前里程數（公里）</summary>
        public int? Mileage { get; set; }

        /// <summary>下次保養日期</summary>
        public DateTime? NextMaintain { get; set; }

        /// <summary>車輛狀態：0=可用 1=運送中 2=維修中</summary>
        public int? Status { get; set; }
    }

    /// <summary>更新車輛請求（欄位與新增相同）</summary>
    public class UpdateVehicleDto : CreateVehicleDto { }
}