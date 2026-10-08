# Dự án ASP.NET CORE MVC - CỬA HÀNG TRỰC TUYẾN (EX03)

##  Giới thiệu dự án
Hệ thống cửa hàng trực tuyến bán hàng đơn giản được phát triển bằng **ASP.NET Core MVC**, **Entity Framework Core** và **SQL Server**. Hệ thống hỗ trợ phân hệ Khách hàng (xem sản phẩm, giỏ hàng, đặt hàng) và phân hệ Quản trị Admin (quản lý danh mục, sản phẩm, đơn hàng).

---

##  Công nghệ sử dụng
- **Framework:** .NET 8.0 / .NET 7.0 (ASP.NET Core MVC)
- **Database:** SQL Server, EF Core (Code First)
- **Xác thực (Authentication):** Cookie Authentication (`AdminAuth`)
- **Quản lý Giỏ hàng:** Session (Lưu trữ dạng JSON)
- **Giao diện (UI):** HTML5, CSS3, Bootstrap 5, FontAwesome 6

---

##  Các tính năng chính

1. Phân hệ Khách hàng (Client Area)
Hiển thị sản phẩm: Danh sách sản phẩm dạng thẻ Grid chuẩn Bootstrap 5.

Bộ lọc Danh mục: Sidebar hỗ trợ lọc sản phẩm theo từng Danh mục linh hoạt.

Phân trang (Client Pagination): Tích hợp phân trang 6 sản phẩm/trang, tự động giữ nguyên bộ lọc danh mục đang chọn khi bấm chuyển trang.

Chi tiết sản phẩm: Xem đầy đủ thông tin, hình ảnh và giá cả sản phẩm.

Giỏ hàng & Đặt hàng: Quản lý giỏ hàng bằng Session (thêm, cập nhật, xóa) và đặt hàng trực tuyến.

2. Phân hệ Quản trị (Admin Area)
Bảo mật: Đăng nhập / Đăng xuất an toàn bằng Cookie Auth (AdminAuth).

Quản lý Danh mục (CRUD Category): Thêm, sửa, xóa các danh mục sản phẩm.

Quản lý Sản phẩm (CRUD Product):

Hỗ trợ Upload ảnh sản phẩm trực tiếp lưu vào wwwroot/uploads.

Phân trang Admin: Tích hợp phân trang 5 sản phẩm/trang, tự động tính số thứ tự (STT) nối tiếp chuẩn xác qua từng trang.

Quản lý Đơn hàng: Xem danh sách đơn, chi tiết đơn hàng và cập nhật trạng thái xử lý (Chờ xử lý, Đang giao, Hoàn thành, Hủy).

3. Cơ sở dữ liệu & Sao lưu
Kết nối CSDL SQL Server tên ex03_db.

Hỗ trợ xuất/nhập toàn bộ CSDL bao gồm cả Schema và Data ra file ex03_database.sql

---

##  Hướng dẫn Chạy dự án

1. Mở cmd trong thư mục chứa dự án (ex03-main\ex03): dotnet tool install --global dotnet-ef -> dotnet ef database update
