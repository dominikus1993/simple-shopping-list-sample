using Akka.Hosting;

using FastEndpoints;

using Microsoft.AspNetCore.Mvc;

using ShoppingListSample.Core.Actors;

namespace ShoppingListSample.Api.Endpoints;

public sealed class GetShoppingListsRequest
{
    [QueryParam, BindFrom("page")]
    public int Page { get; set; } = 0;
    [QueryParam, BindFrom("pageSize")]
    public int PageSize { get; set; } = 12;
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

public sealed class GetShoppingListsEndpoint : Endpoint<GetShoppingListsRequest, GetShoppingListsResponse>
{
    private static readonly Guid DefaultUserId = new Guid("00000000-0000-0000-0000-000000000000");
    private IRequiredActor<ShoppingListsActor> _shoppingListsActorProvider;

    public GetShoppingListsEndpoint(IRequiredActor<ShoppingListsActor> shoppingListsActor)
    {
        _shoppingListsActorProvider = shoppingListsActor;
    }

    public override void Configure()
    {
        Get("/api/shoppingLists");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetShoppingListsRequest req, CancellationToken ct)
    {
        await SendOkAsync(new GetShoppingListsResponse()
        {
            Total = 12,
            ShoppingLists = [new ShoppingListBasicData(){ Id = Guid.NewGuid(), Name = "Shopping List 1"}]
        }, ct);
    }
}