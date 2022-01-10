using GraphQLPOC.Models;

namespace GraphQLPOC.Database;

public interface IMovieRepository
{
    Task<Movie> GetMovieByIdAsync(Guid id);
    Task<Movie> AddReviewToMovieAsync(Guid id, Review review);
}