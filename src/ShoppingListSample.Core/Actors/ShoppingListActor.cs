using Akka.Actor;

using ShoppingListSample.Core.Model;

using ShoppingListId = System.Guid;
namespace ShoppingListSample.Core.Actors;

public sealed record GetShoppingListResponse(ShoppingList ShoppingList);

public sealed class ShoppingListActor : UntypedActor
{
    private ShoppingList _state;

    public ShoppingListActor(CustomerId id)
    {
        _state = ShoppingList.Empty(id);
        _state = _state.AddItem(new Product(new ItemId(Guid.NewGuid()), new ItemQuantity((uint)Random.Shared.Next(0, 20))));
    }
    
    protected override void OnReceive(object message)
    {
        switch (message)
        {
            case GetShoppingList msg:
                HandleGetShoppingList(msg);
                break;
        }
    }

    private void HandleGetShoppingList(GetShoppingList _)
    {
        Sender.Tell(new GetShoppingListResponse(_state));
    }

    public static Props Props(CustomerId Id) => Akka.Actor.Props.Create(() => new ShoppingListActor(Id));
}