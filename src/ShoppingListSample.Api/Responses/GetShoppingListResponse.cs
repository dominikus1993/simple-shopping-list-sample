namespace ShoppingListSample.Api.Responses;

public sealed class ShoppingListItemResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
}
public sealed class GetShoppingListResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IReadOnlyList<ShoppingListItemResponse> Items { get; set; } = null!;
}


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
