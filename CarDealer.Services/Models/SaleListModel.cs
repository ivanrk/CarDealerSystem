namespace CarDealer.Services.Models
{
    public class SaleListModel : SaleModel
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public bool IsYoungDriver { get; set; }

        public decimal DiscountPrice => this.Price * (1 - ((decimal)this.Discount + (IsYoungDriver ? 0.05m : 0)));
    }
}