# Reference Resolver bug
This repo provides an example of a bug in the Hot Chocolate platform that prevents users of the library from creating a `[ReferenceResolver]` method that takes a `Guid` as an argument for resolving an object.

## Steps to Reproduce
1. Using a terminal, navigate to this folder `Subgraph` folder
1. Execute `dotnet run` or `dotnet watch` from the command line
1. Open a browse to http://localhost:5281/graphql
1. Submit a GraphQL query that uses the `_entities` query like so
```gql
query {
    _entities(representations: [
        {
            id: "<any guid value>",
            __typename: "Product"
        }
    ]) {
        ... on Product {
            id name
        }
    }
}
```

## Expected behavior
The API echos the provided Id back in a `Product` object:
```json
{
    "data": {
        "_entities": [
            {
                "id": "<id provided>",
                "name": "Product <id provided>"
            }
        ]
    }
}
```

## Actual behavior
The API produces the following response:
```json
{
  "errors": [
    {
      "message": "Unexpected Execution Error",
      "locations": [
        {
          "line": 2,
          "column": 5
        }
      ],
      "path": [
        "_entities"
      ]
    }
  ]
}
```

The API also produces an error log with the following content:
```
fail: ErrorFilter[0]
      An error occurred.
      System.InvalidCastException: Unable to cast object of type 'System.String' to type 'System.Guid'.
         at HotChocolate.ApolloFederation.Helpers.ArgumentParser.TryGetValue[T](IValueNode valueNode, IType type, String[] path, Int32 i, T& value)
         at HotChocolate.ApolloFederation.Helpers.ArgumentParser.TryGetValue[T](IValueNode valueNode, IType type, String[] path, Int32 i, T& value)
         at lambda_method28(Closure, IResolverContext)
         at HotChocolate.ApolloFederation.Helpers.EntitiesResolver.ResolveAsync(ISchema schema, IReadOnlyList`1 representations, IResolverContext context)
         at HotChocolate.Types.ResolveObjectFieldDescriptorExtensions.<>c__DisplayClass3_0`1.<<Resolve>b__0>d.MoveNext()
      --- End of stack trace from previous location ---
         at HotChocolate.Types.Helpers.FieldMiddlewareCompiler.<>c__DisplayClass9_0.<<CreateResolverMiddleware>b__0>d.MoveNext()
      --- End of stack trace from previous location ---
         at HotChocolate.Execution.Processing.Tasks.ResolverTask.ExecuteResolverPipelineAsync(CancellationToken cancellationToken)
         at HotChocolate.Execution.Processing.Tasks.ResolverTask.TryExecuteAsync(CancellationToken cancellationToken)
```