using Akka.Actor;

using ShoppingListSample.Core.Model;

namespace ShoppingListSample.Core.Actors;

public sealed record CustomerShoppingListCreated(ShoppingListId ShoppingListId, CustomerId CustomerId, ShoppingListName Name);

public sealed class CustomerShoppingListsActor : UntypedActor
{
    private ShoppingList _state;
    private CustomerId _customerId;
    private IActorRef _allCustomerShoppingListsActor;
    public CustomerShoppingListsActor(CustomerId customerId)
    {
        _customerId = customerId;
        _allCustomerShoppingListsActor = Context.ActorOf(AllCustomerShoppingListsActor.Props(customerId), "all");
    }
    
    protected override void OnReceive(object message)
    {
        switch (message)
        {
            case GetShoppingList msg:
                HandleGetShoppingList(msg);
                break;
            case GetCustomerShoppingLists msg:
                HandleGetShoppingLists(msg);
                break;
            case CreateNewShoppingList msg:
                HandleCreateNewShoppingList(msg);
                break;
        }
    }

    private void HandleGetShoppingLists(GetCustomerShoppingLists msg)
    {
        _allCustomerShoppingListsActor.Forward(msg);
    }

    private void HandleCreateNewShoppingList(CreateNewShoppingList msg)
    {
        var listId = ShoppingListId.New();
        Create(listId, msg.CustomerId, msg.Name);
        var message = new CustomerShoppingListCreated(listId, msg.CustomerId, msg.Name);
        _allCustomerShoppingListsActor.Tell(message);
        Sender.Tell(message);
    }

    private void HandleGetShoppingList(GetShoppingList msg)
    {
        var actor = Get(msg.ShoppingListId, msg.CustomerId);
        if (actor is null)
        {
            Sender.Tell(new GetShoppingListResponse(null));
            return;
        }
        actor.Forward(msg);
    }

    public static Props Props(CustomerId customerId) => Akka.Actor.Props.Create(() => new CustomerShoppingListsActor(customerId));
    
    private static IActorRef? Get(ShoppingListId shoppingListId, CustomerId customerId)
    {
        var idStr = shoppingListId.Value.ToString();
        var child = Context.Child(idStr);
        if (Equals(child, ActorRefs.Nobody))
            return null;
        return child;
    }
    
    private static IActorRef Create(ShoppingListId shoppingListId, CustomerId customerId, ShoppingListName name)
    {
        var idStr = shoppingListId.Value.ToString();
        var child = Context.Child(idStr);
        if (Equals(child, ActorRefs.Nobody))
            child = Context.ActorOf(ShoppingListActor.Props(shoppingListId, customerId, name), idStr);
        return child;
    }
}

public sealed record SimpleShoppingList(ShoppingListId Id, ShoppingListName Name);
public sealed class AllCustomerShoppingListsActor : UntypedActor
{
    private List<SimpleShoppingList> _state;

    public AllCustomerShoppingListsActor(CustomerId customerId)
    {
        _state = [];
    }
    
    protected override void OnReceive(object message)
    {
        switch (message)
        {
            case GetCustomerShoppingLists msg:
                HandleGetShoppingLists(msg);
                break;
        }
    }

    private void HandleGetShoppingLists(GetCustomerShoppingLists _)
    {
        Sender.Tell(new GetShoppingListsResponse(_state, _state.Count));
    }

    public static Props Props(CustomerId customerId) => Akka.Actor.Props.Create(() => new AllCustomerShoppingListsActor(customerId));
    
}