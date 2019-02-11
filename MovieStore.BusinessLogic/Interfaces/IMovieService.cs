using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore.BusinessLogic.Infrastructure;
using MovieStore.BusinessLogic.ViewModels;

namespace MovieStore.BusinessLogic.Interfaces
{
    public interface IMovieService: IDisposable
    {
        Task<OperationResult> CreateAsync(MovieViewModel movieViewModel);
        Task<OperationResult> UpdateAsync(MovieViewModel movieViewModel);
        Task<OperationResult> DeleteAsync(int movieId);
        IEnumerable<MovieViewModel> GetAllMovies(int skippedItems = 0, int takenItems = 0);
        IEnumerable<MovieViewModel> GetMoviesByUserId(string UserId, int skippedItems = 0, int takenItems = 0);
        MovieViewModel GetMovieById(int id);

    }
}
