using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ex03.Data;
using ex03.Models;
using ex03.Helpers;

namespace ex03.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const string CART_KEY = "MyCart";

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lấy danh sách giỏ hàng từ Session
        private List<CartItem> GetCart()
        {
            return HttpContext.Session.GetObjectFromJson<List<CartItem>>(CART_KEY) ?? new List<CartItem>();
        }

        // Lưu danh sách giỏ hàng vào Session
        private void SaveCart(List<CartItem> cart)
        {
            HttpContext.Session.SetObjectAsJson(CART_KEY, cart);
        }

        // 1. Trang danh sách Giỏ hàng
        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        // 2. Thêm vào giỏ
        public async Task<IActionResult> AddToCart(int id, int quantity = 1)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            var cart = GetCart();
            var item = cart.FirstOrDefault(p => p.ProductId == id);

            if (item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    Quantity = quantity
                });
            }

            SaveCart(cart);
            return RedirectToAction("Index");
        }

        // 3. Cập nhật số lượng
        [HttpPost]
        public IActionResult UpdateQuantity(int id, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(p => p.ProductId == id);

            if (item != null)
            {
                if (quantity > 0)
                    item.Quantity = quantity;
                else
                    cart.Remove(item);
            }

            SaveCart(cart);
            return RedirectToAction("Index");
        }

        // 4. Xóa khỏi giỏ
        public IActionResult RemoveFromCart(int id)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(p => p.ProductId == id);

            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }

            return RedirectToAction("Index");
        }

        // 5. Trang điền thông tin Đặt hàng (GET)
        public IActionResult Checkout()
        {
            var cart = GetCart();
            if (!cart.Any())
            {
                return RedirectToAction("Index");
            }

            ViewBag.Cart = cart;
            ViewBag.TotalAmount = cart.Sum(i => i.TotalPrice);
            return View();
        }

        // 6. Xử lý lưu Đơn hàng vào CSDL (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Order order)
        {
            var cart = GetCart();
            if (!cart.Any())
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                order.OrderDate = DateTime.Now;
                order.TotalAmount = cart.Sum(i => i.TotalPrice);
                order.Status = "Chờ xử lý";

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                // Lưu các mặt hàng vào bảng OrderDetail
                foreach (var item in cart)
                {
                    var detail = new OrderDetail
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        UnitPrice = item.Price,
                        Quantity = item.Quantity
                    };
                    _context.OrderDetails.Add(detail);
                }
                await _context.SaveChangesAsync();

                // Xóa sạch giỏ hàng sau khi đặt thành công
                HttpContext.Session.Remove(CART_KEY);

                return View("OrderSuccess", order);
            }

            ViewBag.Cart = cart;
            ViewBag.TotalAmount = cart.Sum(i => i.TotalPrice);
            return View(order);
        }
    }
}