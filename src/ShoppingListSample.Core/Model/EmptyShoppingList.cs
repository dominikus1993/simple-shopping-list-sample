namespace ShoppingListSample.Core.Model;

public abstract partial class ShoppingList
{
    private sealed class EmptyShoppingList : ShoppingList
    {
        public static EmptyShoppingList Zero(CustomerId customerId) => new(customerId);

        internal EmptyShoppingList(CustomerId customerId) : base(customerId)
        {
        }

        public override bool IsEmpty => true;

        public override ShoppingList AddItem(Product item)
        {
            return ActiveShoppingList.Zero(CustomerId).AddItem(item);
        }

        public override ShoppingList AddItems(IEnumerable<Product> items)
        {
            return ActiveShoppingList.Zero(CustomerId).AddItems(items);
        }

        public override ShoppingList RemoveItem(Product item)
        {
            return this;
        }

        public override Products Products { get; } = Products.Empty;
    }
}