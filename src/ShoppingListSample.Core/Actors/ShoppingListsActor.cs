using Akka.Actor;

using ShoppingListSample.Core.Model;

using ShoppingListId = System.Guid;

namespace ShoppingListSample.Core.Actors;

public sealed record GetShoppingList(CustomerId Id);

public sealed class ShoppingListsActor : UntypedActor
{
    private readonly ActorMetaData ActorMetaData;

    public ShoppingListsActor()
    {
        ActorMetaData = new ActorMetaData();
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
        var shoppingListActor = GetOrCreate(msg.Id);
        shoppingListActor.Forward(msg);
    }


    private IActorRef GetOrCreate(CustomerId id)
    {
        var idStr = id.Value.ToString();
        var child = Context.Child(idStr);
        if (Equals(child, ActorRefs.Nobody))
            child = Context.ActorOf(ShoppingListActor.Props(id), idStr);
        return child;
    }
    
    public static Props Props() => Akka.Actor.Props.Create(() => new ShoppingListsActor());
}