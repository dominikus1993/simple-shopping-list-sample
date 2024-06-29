namespace ShoppingListSample.Core.Model;

public sealed record ProductName(string Value)
{
    public static ProductName Empty => new(string.Empty);
}

public sealed record Product(ItemId ItemId, ItemQuantity Quantity, ProductName Name)
{
    public bool Equals(Product? other)
    {
        if (other is null)
        {
            return false;
        }

        return other.ItemId == ItemId;
    }

    public bool HasElements => !Quantity.IsZero;
    
    public override int GetHashCode()
    {
        return ItemId.GetHashCode();
    }

    public Product IncreaseQuantity(ItemQuantity quantity)
    {
        return this with { Quantity = this.Quantity + quantity };
    }

    public Product DecreaseQuantity(ItemQuantity itemQuantity)
    {
        return this with { Quantity = this.Quantity - itemQuantity };
    }
}