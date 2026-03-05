using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportApi.Data;
using TransportApi.DTOs;
using TransportApi.Models;

namespace TransportApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // 所有端點都需要 JWT Token
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _db;

        public OrdersController(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>取得訂單列表，支援關鍵字搜尋、狀態篩選、分頁</summary>
        [HttpGet]
        public async Task<IActionResult> GetList(
            [FromQuery] int     page     = 1,
            [FromQuery] int     pageSize = 10,
            [FromQuery] string  keyword  = "",
            [FromQuery] int?    status   = null)
        {
            var query = _db.Orders.AsQueryable();

            // 關鍵字同時比對訂單編號和客戶名稱
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(o =>
                    o.OrderNo!.Contains(keyword) ||
                    o.CustomerName!.Contains(keyword));

            // 狀態篩選，null 代表查全部
            if (status.HasValue)
                query = query.Where(o => o.Status == status.Value);

            // 先算總筆數，再取當頁資料
            var total = await query.CountAsync();
            var items = await query
                .OrderByDescending(o => o.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new { total, items });
        }

        /// <summary>取得單筆訂單</summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _db.Orders.FindAsync(id);
            if (order == null)
                return NotFound(new { message = "訂單不存在" });
            return Ok(order);
        }

        /// <summary>新增訂單，訂單編號由後端自動產生 ORD-YYYYMMDD-XXX</summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
        {
            // 產生訂單編號
            var dateStr    = DateTime.Now.ToString("yyyyMMdd");
            var todayCount = await _db.Orders
                .CountAsync(o => o.OrderNo!.Contains(dateStr));
            var orderNo    = $"ORD-{dateStr}-{(todayCount + 1):D3}";

            var order = new Order
            {
                OrderNo      = orderNo,
                CustomerName = dto.CustomerName,
                Origin       = dto.Origin,
                Destination  = dto.Destination,
                Driver       = dto.Driver,
                Status       = 0,               // 預設待派車
                CreatedAt    = DateTime.Now,
            };

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            // 回傳 201 Created 並附上新資源的 URL
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        /// <summary>更新訂單</summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderDto dto)
        {
            var order = await _db.Orders.FindAsync(id);
            if (order == null)
                return NotFound(new { message = "訂單不存在" });

            order.CustomerName = dto.CustomerName;
            order.Origin       = dto.Origin;
            order.Destination  = dto.Destination;
            order.Driver       = dto.Driver;
            order.Status       = dto.Status;
            order.UpdatedAt    = DateTime.Now;

            await _db.SaveChangesAsync();
            return NoContent();  // 204 更新成功不回傳內容
        }

        /// <summary>刪除訂單</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _db.Orders.FindAsync(id);
            if (order == null)
                return NotFound(new { message = "訂單不存在" });

            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}