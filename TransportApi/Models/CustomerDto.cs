namespace TransportApi.DTOs
{
    /// <summary>新增客戶請求</summary>
    public class CreateCustomerDto
    {
        /// <summary>客戶公司名稱</summary>
        public string? Name { get; set; }

        /// <summary>統一編號</summary>
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
    }

    /// <summary>更新客戶請求（欄位與新增相同）</summary>
    public class UpdateCustomerDto : CreateCustomerDto { }
}