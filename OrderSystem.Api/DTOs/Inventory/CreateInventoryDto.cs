using System.ComponentModel.DataAnnotations;

namespace OrderSystem.Api.DTOs.Inventory
{
    public class CreateInventoryDto
    {
        [Required]
        public int ProductId { get; set; }
        [Range(0, int.MaxValue)]
        public int QuantityOnHand { get; set; }
        [Range(0, int.MaxValue)]
        public int ReorderLevel { get; set; }
    }
}