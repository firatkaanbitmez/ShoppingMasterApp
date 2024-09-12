using ShoppingMasterApp.Domain.Enums;

public class ShippingDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public ShippingStatus Status { get; set; } // Enum
    public DateTime ShippedDate { get; set; }
    public decimal ShippingCost { get; set; }
    public string ShippingCompany { get; set; }
    public string ShippingAddress { get; set; } // Formatted string address
}
