namespace ShoppingListSample.Api.Responses;

public sealed class GetShoppingListsResponse
{
    public IReadOnlyList<ShoppingListBasicData> ShoppingLists { get; init; } = null!;
    public int Total { get; init; }
}

public sealed class ShoppingListBasicData
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
