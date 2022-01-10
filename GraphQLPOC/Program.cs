using Autofac;
using Autofac.Extensions.DependencyInjection;
using GraphQL.Server;
using GraphQLPOC;
using GraphQLPOC.Database;
using GraphQLPOC.GraphQL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEntityFrameworkInMemoryDatabase()
        .AddDbContext<MovieContext>(context => { context.UseInMemoryDatabase("MovieDb"); });

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddGraphQL()
    //.AddGraphQL(
    //    (options, provider) =>
    //    {
    //        // Load GraphQL Server configurations
    //        var graphQLOptions = builder.Configuration
    //            .GetSection("GraphQL")
    //            .Get<GraphQLOptions>();
    //        options.ComplexityConfiguration = graphQLOptions.ComplexityConfiguration;
    //        options.EnableMetrics = graphQLOptions.EnableMetrics;
    //        //// Log errors
    //        //var logger = provider.GetRequiredService<ILogger<Startup>>();
    //        //options.UnhandledExceptionDelegate = ctx =>
    //        //    logger.LogError("{Error} occurred", ctx.OriginalException.Message);
    //    })
    // Adds all graph types in the current assembly with a singleton lifetime.
    .AddGraphTypes()
    // Add GraphQL data loader to reduce the number of calls to our repository. https://graphql-dotnet.github.io/docs/guides/dataloader/
    .AddDataLoader()
    .AddSystemTextJson();

builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new ConfModule()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGraphQL<MovieReviewSchema>();
app.UseGraphQLAltair("/");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
