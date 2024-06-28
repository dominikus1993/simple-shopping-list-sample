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
        await Task.Yield();
        var response = new GetShoppingListResponse()
        {
            Id = id,
            Name = "Shopping List 1",
            Items = [new ShoppingListItemResponse() { Id = Guid.NewGuid(), Name = "Item 1", Quantity = 1 }]
        };
        return Ok(response);
    }
}