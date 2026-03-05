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
    [Authorize]
    public class VehiclesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public VehiclesController(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>取得車輛列表，支援關鍵字搜尋、狀態篩選、分頁</summary>
        [HttpGet]
        public async Task<IActionResult> GetList(
            [FromQuery] int    page     = 1,
            [FromQuery] int    pageSize = 10,
            [FromQuery] string keyword  = "",
            [FromQuery] int?   status   = null)
        {
            var query = _db.Vehicles.AsQueryable();

            // 關鍵字比對車牌和廠牌
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(v =>
                    v.PlateNo!.Contains(keyword) ||
                    v.Brand!.Contains(keyword));

            if (status.HasValue)
                query = query.Where(v => v.Status == status.Value);

            var total = await query.CountAsync();
            var items = await query
                .OrderByDescending(v => v.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new { total, items });
        }

        /// <summary>取得單筆車輛</summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vehicle = await _db.Vehicles.FindAsync(id);
            if (vehicle == null)
                return NotFound(new { message = "車輛不存在" });
            return Ok(vehicle);
        }

        /// <summary>新增車輛</summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleDto dto)
        {
            var vehicle = new Vehicle
            {
                PlateNo      = dto.PlateNo,
                Brand        = dto.Brand,
                Type         = dto.Type,
                Capacity     = dto.Capacity,
                Mileage      = dto.Mileage,
                NextMaintain = dto.NextMaintain,
                Status       = dto.Status,
                CreatedAt    = DateTime.Now,
            };

            _db.Vehicles.Add(vehicle);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = vehicle.Id }, vehicle);
        }

        /// <summary>更新車輛</summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateVehicleDto dto)
        {
            var vehicle = await _db.Vehicles.FindAsync(id);
            if (vehicle == null)
                return NotFound(new { message = "車輛不存在" });

            vehicle.PlateNo      = dto.PlateNo;
            vehicle.Brand        = dto.Brand;
            vehicle.Type         = dto.Type;
            vehicle.Capacity     = dto.Capacity;
            vehicle.Mileage      = dto.Mileage;
            vehicle.NextMaintain = dto.NextMaintain;
            vehicle.Status       = dto.Status;
            vehicle.UpdatedAt    = DateTime.Now;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>刪除車輛</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var vehicle = await _db.Vehicles.FindAsync(id);
            if (vehicle == null)
                return NotFound(new { message = "車輛不存在" });

            _db.Vehicles.Remove(vehicle);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}