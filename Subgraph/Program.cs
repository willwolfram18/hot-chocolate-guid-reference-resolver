var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddApolloFederation()
    .AddType<Product>()
    .AddQueryType()
    .AddErrorFilter<ErrorFilter>();

var app = builder.Build();

app.MapGraphQL();
app.MapBananaCakePop();

app.Run();
