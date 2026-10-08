using System.ComponentModel.DataAnnotations;

namespace ex03.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ giao hàng")]
        public string Address { get; set; } = string.Empty;

        public string? Note { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = "Chờ xử lý"; // Chờ xử lý, Đang giao, Hoàn thành, Hủy

        public List<OrderDetail> OrderDetails { get; set; } = new();
    }
}