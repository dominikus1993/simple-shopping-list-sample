using Microsoft.AspNetCore.Mvc;
using ShoppingListSample.Api.Responses;

namespace ShoppingListSample.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ShoppingListsController: ControllerBase
{
    public async Task<ActionResult<GetShoppingListsResponse>> GetCustomerShoppingList([FromQuery]int page = 1, [FromQuery]int pageSize = 12, CancellationToken cancellationToken = default)
    {
        await Task.Yield();
        var response = new GetShoppingListsResponse()
        {
            Total = 12,
            ShoppingLists = [new ShoppingListBasicData() { Id = Guid.NewGuid(), Name = "Shopping List 1" }]
        };
        return Ok(response);
    }
}