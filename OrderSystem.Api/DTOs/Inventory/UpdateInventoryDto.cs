using System.ComponentModel.DataAnnotations;

namespace OrderSystem.Api.DTOs.Inventory
{
    public class UpdateInventoryDto
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int QuantityOnHand { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int ReorderLevel  { get; set; }
    }
}