using Akka.Actor;
using Akka.Hosting;

using Microsoft.AspNetCore.Mvc;
using ShoppingListSample.Api.Responses;
using ShoppingListSample.Core.Actors;
using ShoppingListSample.Core.Model;

using GetShoppingListResponse = ShoppingListSample.Api.Responses.GetShoppingListResponse;
using GetShoppingListsResponse = ShoppingListSample.Api.Responses.GetShoppingListsResponse;

namespace ShoppingListSample.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ShoppingListsController: ControllerBase
{
    private static readonly CustomerId _defaultCustomerId = CustomerId.New();
    private readonly IRequiredActor<ShoppingListsActor> _requiredActor;

    public ShoppingListsController(IRequiredActor<ShoppingListsActor> requiredActor)
    {
        _requiredActor = requiredActor;
    }

    [HttpGet]
    public async Task<ActionResult<GetShoppingListsResponse>> GetCustomerShoppingLists([FromQuery]int page = 1, [FromQuery]int pageSize = 12, CancellationToken cancellationToken = default)
    {
        var actor = await _requiredActor.GetAsync(cancellationToken);
        var data = await actor.Ask<ShoppingListSample.Core.Actors.GetShoppingListsResponse>(new GetCustomerShoppingLists(_defaultCustomerId, page, pageSize), cancellationToken: cancellationToken);
        var response = new GetShoppingListsResponse()
        {
            Total = data.Total,
            ShoppingLists = data.ShoppingLists.Select(x => new ShoppingListBasicData() { Id = x.Id.Value, Name = x.Name.Value }).ToList()
        };
        return Ok(response);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetShoppingListResponse>> GetCustomerShoppingList(Guid id, CancellationToken cancellationToken = default)
    {
        var actor = await _requiredActor.GetAsync(cancellationToken);
        var data = await actor.Ask<ShoppingListSample.Core.Actors.GetShoppingListResponse>(new GetShoppingList(new ShoppingListId(id), _defaultCustomerId), cancellationToken: cancellationToken);
        if (data.ShoppingList is null)
        {
            return NotFound();
        }
        
        var response = new GetShoppingListResponse()
        {
            Id = data.ShoppingList.Id.Value,
            Name = data.ShoppingList.Name.Value,
            Items = data.ShoppingList.Products.MapItems(x => new ShoppingListItemResponse() { Id = x.ItemId.Value, Name = x.Name.Value })
        };
        return Ok(response);
    }
}