namespace ShoppingListSample.Core.Model;

public readonly record struct CustomerId(Guid Value)
{
    public static CustomerId New() => new CustomerId(Guid.NewGuid());
}