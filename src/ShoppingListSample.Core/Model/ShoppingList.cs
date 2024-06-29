namespace ShoppingListSample.Core.Model;

public abstract partial class ShoppingList
{
    public ShoppingListId Id { get; }
    public CustomerId CustomerId { get; }
    public ShoppingListName Name { get; }

    private ShoppingList(ShoppingListId id, CustomerId customerId, ShoppingListName name)
    {
        CustomerId = customerId;
        Id = id;
        Name = name;
    }
    
    public abstract bool IsEmpty { get; }
    
    public static ShoppingList Empty(ShoppingListId id, CustomerId customerId, ShoppingListName name) => EmptyShoppingList.Zero(id, customerId, name);

    public static ShoppingList Active(ShoppingListId id, CustomerId customerId, ShoppingListName name, Products items) => new ActiveShoppingList(id, customerId, name, items);
    public static ShoppingList Active(ShoppingListId id, CustomerId customerId, ShoppingListName name, IEnumerable<Product> items) => new ActiveShoppingList(id, customerId, name, Products.Create(items));

    public abstract ShoppingList AddItem(Product item);
    
    public abstract ShoppingList AddItems(IEnumerable<Product> items);

    public abstract ShoppingList RemoveItem(Product item);

    public abstract Products Products { get; }
}