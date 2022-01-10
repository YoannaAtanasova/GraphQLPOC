using Autofac;
using GraphQL.SystemTextJson;
using GraphQLPOC.Database;
using GraphQLPOC.GraphQL;

namespace GraphQLPOC;

public class ConfModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
        builder.RegisterType<MovieRepository>().As<IMovieRepository>().InstancePerLifetimeScope();
        builder.RegisterType<DocumentWriter>().AsImplementedInterfaces().SingleInstance();
        builder.RegisterType<QueryObject>().AsSelf().SingleInstance();
        builder.RegisterType<MovieReviewSchema>().AsSelf().SingleInstance();
    }
}
