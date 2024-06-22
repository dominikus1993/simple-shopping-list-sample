using FastEndpoints;

namespace ShoppingListSample.Api.Endpoints;

public sealed class GetShoppingListRequest
{
    
}

public class GetShoppingListResponse
{
}


public sealed class GetShoppingListEndpoint : Endpoint<GetShoppingListRequest, GetShoppingListResponse>
{
    public override void Configure()
    {
        Get("/api/shoppingLists");
        AllowAnonymous();
    }

    public override Task HandleAsync(GetShoppingListRequest req, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}