namespace ShoppingMasterApp.Domain.Enums
{
    public enum OrderStatus
    {
        Unknown = 0,
        Pending = 1,
        Processing = 2,
        Shipped = 3,
        Delivered = 4,
        Completed = 5,
        Cancelled = 6
    }
}
