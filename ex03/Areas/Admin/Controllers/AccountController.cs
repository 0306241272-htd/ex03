using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ex03.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        // GET: Admin/Account/Login
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Product");
            }
            return View();
        }

        // POST: Admin/Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Kiểm tra tài khoản admin cố định (hoặc truy vấn CSDL nếu cần)
            if (username == "admin" && password == "123456")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var identity = new ClaimsIdentity(claims, "AdminAuth");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("AdminAuth", principal);

                return RedirectToAction("Index", "Product");
            }

            ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng!";
            return View();
        }

        // GET: Admin/Account/Logout
        // GET: Admin/Account/Logout
        public async Task<IActionResult> Logout()
        {
            // Xóa Cookie đăng nhập Admin
            await HttpContext.SignOutAsync("AdminAuth");

            // Chuyển hướng về Trang chủ phía Client (Trang người dùng)
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}