namespace Basket.Api.Entities
{
    public class ShoppingCartModel
    {

        public string UserName { get; set; } = null!;
        public List<ShoppingCartItemsModel> Items { get; set; } = new List<ShoppingCartItemsModel>();

        public decimal TotalPrice { get; set; }

    }
}