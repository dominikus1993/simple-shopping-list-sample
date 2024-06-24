namespace ShoppingListSample.Core.Model;

public abstract partial class ShoppingList
{
    private sealed class EmptyShoppingList : ShoppingList
    {
        public static EmptyShoppingList Zero(ShoppingListId id, CustomerId customerId) => new(id, customerId);

        internal EmptyShoppingList(ShoppingListId id, CustomerId customerId) : base(id, customerId)
        {
        }

        public override bool IsEmpty => true;

        public override ShoppingList AddItem(Product item)
        {
            return ActiveShoppingList.Zero(Id, CustomerId).AddItem(item);
        }

        public override ShoppingList AddItems(IEnumerable<Product> items)
        {
            return ActiveShoppingList.Zero(Id, CustomerId).AddItems(items);
        }

        public override ShoppingList RemoveItem(Product item)
        {
            return this;
        }

        public override Products Products { get; } = Products.Empty;
    }
}