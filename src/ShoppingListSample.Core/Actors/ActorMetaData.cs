namespace ShoppingListSample.Core.Actors;

public class ActorMetaData
{
    public ActorMetaData(string name, ActorMetaData? parent = null)
    {
        Name = name;
        Parent = parent;
        // if no parent, we assume a top-level actor
        var parentPath = parent != null ? parent.Path : "/user";
        Path = $"{parentPath}/{Name}";
    }

    public string Name { get; private set; }
    public ActorMetaData? Parent { get; set; }
    public string Path { get; private set; }
}