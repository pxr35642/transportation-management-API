namespace TransportApi.DTOs
{
    /// <summary>新增訂單請求</summary>
    public class CreateOrderDto
    {
        /// <summary>客戶名稱</summary>
        public string? CustomerName { get; set; }

        /// <summary>起點</summary>
        public string? Origin { get; set; }

        /// <summary>終點</summary>
        public string? Destination { get; set; }

        /// <summary>負責司機姓名</summary>
        public string? Driver { get; set; }
    }

    /// <summary>更新訂單請求</summary>
    public class UpdateOrderDto
    {
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
    }
}