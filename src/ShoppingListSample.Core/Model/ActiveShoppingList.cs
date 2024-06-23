namespace ShoppingListSample.Core.Model;

public abstract partial class ShoppingList
{
    private sealed class ActiveShoppingList : ShoppingList
    {
        public ActiveShoppingList(CustomerId customerId, Products items) : base(customerId)
        {
            Products = items;
        }

        public static ActiveShoppingList Zero(CustomerId customerId)
        {
            return new ActiveShoppingList(customerId, Products.Empty);
        }

        public override bool IsEmpty => Products.IsEmpty;

        public override ShoppingList AddItem(Product item)
        {
            var items = Products.AddItem(item);
            return Active(this.CustomerId, items);
        }

        public override ShoppingList AddItems(IEnumerable<Product> items)
        {
            var basketItems = Products.AddItems(items);
            if (basketItems.IsEmpty)
            {
                return new EmptyShoppingList(CustomerId);
            }
            
            return new ActiveShoppingList(CustomerId, basketItems);
        }

        public override ShoppingList RemoveItem(Product item)
        {
            var items = Products.RemoveOrDecreaseItem(item);
            if (items.IsEmpty)
            {
                return new EmptyShoppingList(CustomerId);
            }

            return new ActiveShoppingList(CustomerId, items);
        }

        public override Products Products { get; }
    }   
}