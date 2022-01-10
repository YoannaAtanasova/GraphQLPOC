using GraphQL;
using GraphQL.Types;
using GraphQLPOC.Database;
using GraphQLPOC.Models;
using GraphQLPOC.Types;

namespace GraphQLPOC.GraphQL;

public class QueryObject : ObjectGraphType<object>
{
    public QueryObject(IMovieRepository repository)
    {
        Name = "Queries";
        Description = "The base query for all the entities in our object graph.";

        FieldAsync<MovieObject, Movie>(
            "movie",
            "Gets a movie by its unique identifier.",
            new QueryArguments(
                new QueryArgument<NonNullGraphType<IdGraphType>>
                {
                    Name = "id",
                    Description = "The unique GUID of the movie."
                }),
            context => repository.GetMovieByIdAsync(context.GetArgument("id", Guid.Empty)));
    }
}
