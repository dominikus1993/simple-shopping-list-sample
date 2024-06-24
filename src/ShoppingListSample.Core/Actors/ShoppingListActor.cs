using Akka.Actor;

using ShoppingListSample.Core.Model;

namespace ShoppingListSample.Core.Actors;

public sealed record GetShoppingListResponse(ShoppingList ShoppingList);

public sealed class ShoppingListActor : UntypedActor
{
    
    private ShoppingList _state;

    public ShoppingListActor(ShoppingListId id, CustomerId customerId)
    {
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
        Sender.Forward(new GetShoppingListResponse(_state));
    }

    public static Props Props(ShoppingListId id, CustomerId customerId) => Akka.Actor.Props.Create(() => new ShoppingListActor(id, customerId));
}