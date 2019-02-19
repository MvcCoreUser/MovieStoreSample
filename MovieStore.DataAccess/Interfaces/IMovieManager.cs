using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore.DataAccess.Entities;

namespace MovieStore.DataAccess.Interfaces
{
    public interface IMovieManager: IManager
    {
        Task CreateAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task DeleteAsync(int movieId);
        Movie GetMovieById(int movieId);
        IQueryable<Movie> GetMovies(int skippedItems=0, int takenItems=0);
    }
}
