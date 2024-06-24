namespace ShoppingListSample.Core.Model;

public readonly record struct CustomerId(Guid Value)
{
    public static CustomerId New() => new CustomerId(Guid.NewGuid());
}

public readonly record struct ShoppingListId(Guid Value)
{
    public static ShoppingListId New() => new ShoppingListId(Guid.NewGuid());
}