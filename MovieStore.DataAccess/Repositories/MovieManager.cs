using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore.DataAccess.EF;
using MovieStore.DataAccess.Entities;
using MovieStore.DataAccess.Interfaces;

namespace MovieStore.DataAccess.Repositories
{
    public class MovieManager : IMovieManager
    {
        public ApplicationContext Database{ get; set; }
        public MovieManager(ApplicationContext db)
        {
            Database = db;
        }
        public async Task CreateAsync(Movie movie)
        {
            Database.Movies.Add(movie);
            await Database.SaveChangesAsync();
        }

        public async Task DeleteAsync(int movieId)
        {
            Movie existingMovie = Database.Movies.Include(m => m.UserProfile).FirstOrDefault(m => m.Id == movieId);
            if (existingMovie != null)
            {
                Database.Movies.Remove(existingMovie);
                await Database.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Фильм с Id {movieId} не найден.");
            }
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public async Task UpdateAsync(Movie movie)
        {
            Movie existingMovie = Database.Movies.Include(m => m.UserProfile).FirstOrDefault(m => m.Id == movie.Id);
            if (existingMovie!=null)
            {
                existingMovie.Name = movie.Name;
                existingMovie.Description = movie.Description;
                existingMovie.Producer = movie.Producer;
                existingMovie.Poster = movie.Poster;
                existingMovie.YearOfProduction = movie.YearOfProduction;
                this.Database.Entry(existingMovie).State = EntityState.Modified;
                await Database.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Фильм с Id {movie.Id} не найден.");
            }
        }

        public Movie GetMovieById(int movieId)
        => Database.Movies.FirstOrDefault(m => m.Id == movieId);

        public IQueryable<Movie> GetMovies(int skippedItems = 0, int takenItems = 0)
        {
            IQueryable<Movie> res = null;
            if (skippedItems != 0 && takenItems != 0)
            {
               res= Database.Movies.OrderBy(m => m.Id).Skip(skippedItems).Take(skippedItems);
            }
            else if (skippedItems != 0)
            {
                res = Database.Movies.OrderBy(m => m.Id).Skip(skippedItems);
            }
            else if (takenItems != 0)
            {
                res = Database.Movies.OrderBy(m => m.Id).Take(takenItems);
            }
            else
            {
                res = Database.Movies;
            }
            return res;
        }
    }
}
