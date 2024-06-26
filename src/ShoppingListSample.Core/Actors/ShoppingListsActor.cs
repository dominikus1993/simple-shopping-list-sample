using Akka.Actor;

using ShoppingListSample.Core.Model;

namespace ShoppingListSample.Core.Actors;

public sealed record GetShoppingList(ShoppingListId ShoppingListId, CustomerId CustomerId);
public sealed record GetShoppingListsResponse(IReadOnlyCollection<SimpleShoppingList> ShoppingLists, int Total);
public sealed record CreateNewShoppingList(CustomerId CustomerId, ShoppingListName Name);
public sealed record ShoppingListCreated(ShoppingListId Id, CustomerId CustomerId, string Name);
public sealed record GetCustomerShoppingLists(CustomerId CustomerId, int Page, int PageSize); // tu

public sealed class ShoppingListsActor : UntypedActor
{
    private readonly ActorMetaData ActorMetaData;

    public ShoppingListsActor()
    {
    }
    protected override void OnReceive(object message)
    {
        switch (message)
        {
            case GetShoppingList msg:
                HandleGetShoppingList(msg);
                break;
            case GetCustomerShoppingLists msg:
                HandleGetCustomerShoppingLists(msg);
                break;
            case CreateNewShoppingList msg:
                HandleCreateNewShoppingList(msg);
                break;
        }
    }

    private void HandleCreateNewShoppingList(CreateNewShoppingList msg)
    {
        var shoppingListActor = GetOrCreate(msg.CustomerId);
        shoppingListActor.Forward(msg);
    }

    private void HandleGetCustomerShoppingLists(GetCustomerShoppingLists msg)
    {
        var shoppingListActor = GetOrCreate(msg.CustomerId);
        shoppingListActor.Forward(msg);
    }

    private void HandleGetShoppingList(GetShoppingList msg)
    {
        var shoppingListActor = GetOrCreate(msg.CustomerId);
        shoppingListActor.Forward(msg);
    }


    private static IActorRef GetOrCreate(CustomerId customerId)
    {
        var idStr = customerId.Value.ToString();
        var child = Context.Child(idStr);
        if (Equals(child, ActorRefs.Nobody))
            child = Context.ActorOf(CustomerShoppingListsActor.Props(customerId), idStr);
        return child;
    }
    
    public static Props Props() => Akka.Actor.Props.Create(() => new ShoppingListsActor());
}