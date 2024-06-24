using Akka.Actor;

using ShoppingListSample.Core.Model;

namespace ShoppingListSample.Core.Actors;

public sealed class CustomerShoppingListsActor : UntypedActor
{
    private ShoppingList _state;

    public CustomerShoppingListsActor(CustomerId customerId)
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

    private void HandleGetShoppingList(GetShoppingList msg)
    {
        var actor = GetOrCreate(msg.ShoppingListId, msg.CustomerId);
        actor.Forward(msg);
    }

    public static Props Props(CustomerId customerId) => Akka.Actor.Props.Create(() => new CustomerShoppingListsActor(customerId));
    
    private static IActorRef GetOrCreate(ShoppingListId shoppingListId, CustomerId customerId)
    {
        var idStr = shoppingListId.Value.ToString();
        var child = Context.Child(idStr);
        if (Equals(child, ActorRefs.Nobody))
            child = Context.ActorOf(ShoppingListActor.Props(shoppingListId, customerId), idStr);
        return child;
    }
}