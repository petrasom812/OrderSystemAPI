using System.ComponentModel.DataAnnotations;


namespace OrderSystem.Api.DTOs.Product
{
    public class UpdateProductDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;
    }
}