using GraphQL.Types;

namespace GraphQLPOC.GraphQL;

public class MovieReviewSchema : Schema
{
    public MovieReviewSchema(QueryObject query, MutationObject mutation, IServiceProvider sp) : base(sp)
    {
        Query = query;
        Mutation = mutation;
    }
}
