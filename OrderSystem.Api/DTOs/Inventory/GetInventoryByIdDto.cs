
namespace OrderSystem.Api.DTOs.Inventory
{
    public class GetInventoryByIdDto
    {
        public int Id { get; set; }
        
        public int ProductId { get; set; }

        public int QuantityOnHand { get; set; }

        public int ReorderLevel { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? EditedAt { get; set; }
    }
}