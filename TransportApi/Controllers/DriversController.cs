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
    public class DriversController : ControllerBase
    {
        private readonly AppDbContext _db;

        public DriversController(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>取得司機列表，支援關鍵字搜尋、狀態篩選、分頁</summary>
        [HttpGet]
        public async Task<IActionResult> GetList(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string keyword = "",
            [FromQuery] int? status = null)
        {
            var query = _db.Drivers.AsQueryable();

            // 關鍵字比對姓名、身分證、電話
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(d =>
                    d.Name!.Contains(keyword) ||
                    d.IdNo!.Contains(keyword) ||
                    d.Phone!.Contains(keyword));

            if (status.HasValue)
                query = query.Where(d => d.Status == status.Value);

            var total = await query.CountAsync();
            var items = await query
                .OrderByDescending(d => d.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new { total, items });
        }

        /// <summary>取得單筆司機</summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var driver = await _db.Drivers.FindAsync(id);
            if (driver == null)
                return NotFound(new { message = "司機不存在" });
            return Ok(driver);
        }

        /// <summary>新增司機</summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDriverDto dto)
        {
            var driver = new Driver
            {
                Name = dto.Name,
                IdNo = dto.IdNo,
                Phone = dto.Phone,
                LicenseType = dto.LicenseType,
                LicenseExp = dto.LicenseExp.HasValue ? dto.LicenseExp.Value : dto.LicenseExp.GetValueOrDefault(),
                HireDate = dto.HireDate.HasValue ? dto.HireDate.Value : dto.HireDate.GetValueOrDefault(),
                ResignDate = dto.ResignDate,
                Status = dto.Status,
                TotalTrips = 0,
                CreatedAt = DateTime.Now,
            };

            _db.Drivers.Add(driver);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = driver.Id }, driver);
        }

        /// <summary>更新司機</summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDriverDto dto)
        {
            var driver = await _db.Drivers.FindAsync(id);
            if (driver == null)
                return NotFound(new { message = "司機不存在" });

            driver.Name = dto.Name;
            driver.IdNo = dto.IdNo;
            driver.Phone = dto.Phone;
            driver.LicenseType = dto.LicenseType;
            driver.LicenseExp = dto.LicenseExp.HasValue ? dto.LicenseExp.Value : dto.LicenseExp.GetValueOrDefault();
            driver.HireDate = dto.HireDate.HasValue ? dto.HireDate.Value : dto.HireDate.GetValueOrDefault();
            driver.ResignDate = dto.ResignDate;
            driver.Status = dto.Status;
            driver.UpdatedAt = DateTime.Now;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>刪除司機</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var driver = await _db.Drivers.FindAsync(id);
            if (driver == null)
                return NotFound(new { message = "司機不存在" });

            _db.Drivers.Remove(driver);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}