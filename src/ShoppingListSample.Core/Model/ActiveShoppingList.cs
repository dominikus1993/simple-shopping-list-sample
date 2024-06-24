namespace ShoppingListSample.Core.Model;

public abstract partial class ShoppingList
{
    private sealed class ActiveShoppingList : ShoppingList
    {
        public ActiveShoppingList(ShoppingListId id, CustomerId customerId, Products items) : base(id, customerId)
        {
            Products = items;
        }

        public static ActiveShoppingList Zero(ShoppingListId id, CustomerId customerId)
        {
            return new ActiveShoppingList(id, customerId, Products.Empty);
        }

        public override bool IsEmpty => Products.IsEmpty;

        public override ShoppingList AddItem(Product item)
        {
            var items = Products.AddItem(item);
            return Active(this.Id, this.CustomerId, items);
        }

        public override ShoppingList AddItems(IEnumerable<Product> items)
        {
            var basketItems = Products.AddItems(items);
            if (basketItems.IsEmpty)
            {
                return new EmptyShoppingList(Id, CustomerId);
            }
            
            return new ActiveShoppingList(Id, CustomerId, basketItems);
        }

        public override ShoppingList RemoveItem(Product item)
        {
            var items = Products.RemoveOrDecreaseItem(item);
            if (items.IsEmpty)
            {
                return new EmptyShoppingList(Id, CustomerId);
            }

            return new ActiveShoppingList(Id, CustomerId, items);
        }

        public override Products Products { get; }
    }   
}