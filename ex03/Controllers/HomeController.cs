using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ex03.Data;
using ex03.Helpers; // 1. Import Helper phân trang
using ex03.Models;

namespace ex03.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Trang chủ: Hiển thị sản phẩm, Lọc theo Danh mục & Phân trang
        public async Task<IActionResult> Index(int? categoryId, int? pageNumber)
        {
            int pageSize = 6; // Số sản phẩm hiển thị trên 1 trang Client

            // Lấy danh sách danh mục làm sidebar / menu lọc
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.CurrentCategory = categoryId;

            var products = _context.Products
                .Include(p => p.Category)
                .AsNoTracking();

            // Nếu người dùng chọn lọc theo danh mục
            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value);
            }

            // Sắp xếp sản phẩm mới nhất lên đầu
            products = products.OrderByDescending(p => p.Id);

            // Thực hiện phân trang
            var pagedProducts = await PaginatedList<Product>.CreateAsync(products, pageNumber ?? 1, pageSize);

            return View(pagedProducts);
        }

        // 2. Trang Chi tiết sản phẩm
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }
    }
}