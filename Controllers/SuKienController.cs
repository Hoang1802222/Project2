using EventHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace EventHub.Controllers
{
    public class SuKienController : Controller
    {
        private readonly EventHubContext _context;
        public SuKienController(EventHubContext context)
        {
            _context = context;
        }
        // GET: SuKien
        public async Task<IActionResult> Index(int? maChuDe,string? tuKhoa)
        {
            var query = _context.SuKiens.Include(s=>s.MaChuDeNavigation).Include(s=>s.MaNguoiToChucNavigation).AsQueryable();
            if(maChuDe.HasValue&& maChuDe.Value > 0)
            {
                query = query.Where(s => s.MaChuDe == maChuDe.Value);
            }
            if(!string.IsNullOrEmpty(tuKhoa))
            {
                var keyword = tuKhoa.Trim();
                query = query.Where(s => s.TieuDe.Contains(keyword)||s.DiaDiem!=null && s.DiaDiem.Contains(keyword));
            }
            ViewBag.chuDe= await _context.ChuDes.OrderBy(c => c.TenChuDe).ToListAsync();
            ViewBag.tuKhoa = tuKhoa;
            ViewBag.maChuDe = maChuDe;
            var danhsachSuKien = await query.OrderByDescending(s=>s.ThoiGianBatDau).ToListAsync();
            return View(danhsachSuKien);
        }
        // GET: /SuKien/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var suKien = await _context.SuKiens
                .Include(s => s.MaChuDeNavigation)
                .Include(s => s.MaNguoiToChucNavigation)
                .Include(s => s.PhienHoiThaos)
                    .ThenInclude(p => p.MaDienGia)
                .FirstOrDefaultAsync(s => s.MaSuKien == id);

            if (suKien == null)
            {
                return NotFound();
            }

            var soCho = await _context.VwSoChoConLais
                .FirstOrDefaultAsync(v => v.MaSuKien == id);

            ViewBag.SoChoConLai = soCho;

            return View(suKien);
        }
    }
};