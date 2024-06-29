namespace ShoppingListSample.Core.Model;

public abstract partial class ShoppingList
{
    private sealed class EmptyShoppingList : ShoppingList
    {
        public static EmptyShoppingList Zero(ShoppingListId id, CustomerId customerId, ShoppingListName name) => new(id, customerId, name);

        internal EmptyShoppingList(ShoppingListId id, CustomerId customerId, ShoppingListName name) : base(id, customerId, name)
        {
        }

        public override bool IsEmpty => true;

        public override ShoppingList AddItem(Product item)
        {
            return ActiveShoppingList.Zero(Id, CustomerId, Name).AddItem(item);
        }

        public override ShoppingList AddItems(IEnumerable<Product> items)
        {
            return ActiveShoppingList.Zero(Id, CustomerId, Name).AddItems(items);
        }

        public override ShoppingList RemoveItem(Product item)
        {
            return this;
        }

        public override Products Products { get; } = Products.Empty;
    }
}