public class Product
{
    [Key]
    [GraphQLType(typeof(NonNullType<IdType>))]
    public Guid Id { get; set; }

    public string Name { get; set; }

    [ReferenceResolver]
    public static Product? ResolveProduct(Guid id)
    {
        return new Product
        {
            Id = id,
            Name = $"Product {id}"
        };
    }
}