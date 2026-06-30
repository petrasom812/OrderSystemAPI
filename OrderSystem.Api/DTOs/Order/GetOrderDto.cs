using System.ComponentModel.DataAnnotations;

namespace OrderSystem.Api.DTOs
{
    public class GetOrderDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Total amount must be > 0.")]
        public decimal TotalAmount { get; set; }
    }
}