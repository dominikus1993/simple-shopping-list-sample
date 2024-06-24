using Akka.Actor;
using Akka.Hosting;

using FastEndpoints;

using Microsoft.AspNetCore.Mvc;

using ShoppingListSample.Core.Actors;
using ShoppingListSample.Core.Model;

namespace ShoppingListSample.Api.Endpoints;

public sealed class ShoppingListItemResponseDto
{
    public Guid ItemId { get; set; }
    public uint Quantity { get; set; }

    public ShoppingListItemResponseDto()
    {
        
    }
    
    public ShoppingListItemResponseDto(Product dto)
    {
        ItemId = dto.ItemId.Value;
        Quantity = dto.Quantity.Value;
    }
}

public sealed class GetShoppingListRequest
{
    public Guid ShoppingListId { get; init; }
}

public sealed class GetShoppingListResponse
{
    public Guid ShoppingListId { get; init; }
    public IReadOnlyList<ShoppingListItemResponseDto> Items { get; init; } = null!;
}


public sealed class GetShoppingListEndpoint : Endpoint<GetShoppingListRequest, GetShoppingListResponse>
{
    private static readonly Guid DefaultUserId = new Guid("00000000-0000-0000-0000-000000000000");
    private IRequiredActor<ShoppingListsActor> _shoppingListsActorProvider;

    public GetShoppingListEndpoint(IRequiredActor<ShoppingListsActor> shoppingListsActor)
    {
        _shoppingListsActorProvider = shoppingListsActor;
    }

    public override void Configure()
    {
        Get("/api/shoppingLists/{ShoppingListId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetShoppingListRequest req, CancellationToken ct)
    {
        var actor = await _shoppingListsActorProvider.GetAsync(ct);
        var response = await actor.Ask<Core.Actors.GetShoppingListResponse>(new GetShoppingList(new ShoppingListId(req.ShoppingListId), new CustomerId(DefaultUserId)), ct);
        var result = new GetShoppingListResponse
        {
            ShoppingListId = response.ShoppingList.CustomerId.Value,
            Items = response.ShoppingList.Products.MapItems(item => new ShoppingListItemResponseDto(item)).ToArray(),
        };

        await SendOkAsync(result, ct);
    }
}