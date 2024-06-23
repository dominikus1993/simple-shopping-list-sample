namespace ShoppingListSample.Core.Model;

public abstract partial class ShoppingList
{
    public CustomerId CustomerId { get; }

    private ShoppingList(CustomerId customerId)
    {
        CustomerId = customerId;
    }
    
    public abstract bool IsEmpty { get; }
    
    public static ShoppingList Empty(CustomerId id) => EmptyShoppingList.Zero(id);

    public static ShoppingList Active(CustomerId id, Products items) => new ActiveShoppingList(id, items);
    public static ShoppingList Active(CustomerId id, IEnumerable<Product> items) => new ActiveShoppingList(id, Products.Create(items));

    public abstract ShoppingList AddItem(Product item);
    
    public abstract ShoppingList AddItems(IEnumerable<Product> items);

    public abstract ShoppingList RemoveItem(Product item);

    public abstract Products Products { get; }
}