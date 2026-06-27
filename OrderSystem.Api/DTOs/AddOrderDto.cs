using System.ComponentModel.DataAnnotations;


namespace OrderSystem.Api.DTOs
{
    public class AddOrderDto
    {
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Total amount must be > 0.")]
        public decimal TotalAmount { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string Status { get; set; } = string.Empty;
    }
}