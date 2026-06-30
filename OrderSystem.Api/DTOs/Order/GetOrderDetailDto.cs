using System.ComponentModel.DataAnnotations;

namespace OrderSystem.Api.DTOs
{
    public class GetOrderDetailDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Total amount must be > 0.")]
        public decimal TotalAmount { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? EditedAt {get; set;}
    }
}