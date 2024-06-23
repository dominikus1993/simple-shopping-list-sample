namespace ShoppingListSample.Core.Model;

public readonly record struct ItemId(Guid Value)
{
    public static ItemId New() => new ItemId(Guid.NewGuid());
}