

namespace OrderSystem.Api.Models.Inventory
{
    public class InventoryModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int QuantityOnHand { get; set; }

        public int ReorderLevel { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? EditedAt { get; set; }
    }
}