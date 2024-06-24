namespace ShoppingListSample.Core.Model;

public abstract partial class ShoppingList
{
    public ShoppingListId Id { get; }
    public CustomerId CustomerId { get; }

    private ShoppingList(ShoppingListId id, CustomerId customerId)
    {
        CustomerId = customerId;
        Id = id;
    }
    
    public abstract bool IsEmpty { get; }
    
    public static ShoppingList Empty(ShoppingListId id, CustomerId customerId) => EmptyShoppingList.Zero(id, customerId);

    public static ShoppingList Active(ShoppingListId id, CustomerId customerId, Products items) => new ActiveShoppingList(id, customerId, items);
    public static ShoppingList Active(ShoppingListId id, CustomerId customerId, IEnumerable<Product> items) => new ActiveShoppingList(id, customerId, Products.Create(items));

    public abstract ShoppingList AddItem(Product item);
    
    public abstract ShoppingList AddItems(IEnumerable<Product> items);

    public abstract ShoppingList RemoveItem(Product item);

    public abstract Products Products { get; }
}