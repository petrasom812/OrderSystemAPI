using System.ComponentModel.DataAnnotations;
namespace OrderSystem.Api.DTOs
{
    public class UpdateOrderDto
    {
        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string Status { get; set; } = string.Empty;
    }
}