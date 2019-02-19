using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore.BusinessLogic.Infrastructure;
using MovieStore.BusinessLogic.Interfaces;
using MovieStore.BusinessLogic.ViewModels;
using MovieStore.DataAccess.Entities;
using MovieStore.DataAccess.Interfaces;

namespace MovieStore.BusinessLogic.Services
{
    public class MovieService : IMovieService
    {
        public IContext Database{ get; set; }
        public MovieService(IContext db)
        {
            Database = db;
        }
        public async Task<OperationResult> CreateAsync(MovieViewModel movieViewModel)
        {
            try
            {
                Movie movie = new Movie();
                movie.Name = movieViewModel.Name;
                movie.Description = movieViewModel.Description;
                movie.Producer = movieViewModel.Producer;
                movie.YearOfProduction = movieViewModel.YearOfProduction;
                movie.UserProfile = Database.UserProfileManager.GetUserProfileById(movieViewModel.UserId);
                movie.PosterFileExtension = Path.GetExtension(movieViewModel.Poster.FileName);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    if (!movie.PosterFileExtension.Equals(".png", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Bitmap bitmap = (Bitmap)Bitmap.FromStream(movieViewModel.Poster.InputStream);
                        bitmap.Save(memoryStream, ImageFormat.Png);
                        movie.Poster = Convert.ToBase64String(memoryStream.ToArray());
                        bitmap.Dispose();
                    }
                    else
                    {
                        byte[] bytes = new Byte[movieViewModel.Poster.InputStream.Length];
                        movieViewModel.Poster.InputStream.Read(bytes, 0, (int)movieViewModel.Poster.InputStream.Length);
                        movieViewModel.Poster.InputStream.Seek(0, SeekOrigin.Begin);
                        movieViewModel.Poster.InputStream.Close();
                        movie.Poster = Convert.ToBase64String(bytes);
                    }

                }

                await Database.MovieManager.CreateAsync(movie);

                return new OperationResult(true, "Фильм успешно сохранен");
            }
            catch (Exception ex)
            {
                return new OperationResult(false, ex.Message, null, ex);
            }
            
        }

        public async Task<OperationResult> DeleteAsync(int movieId)
        {
            await Database.MovieManager.DeleteAsync(movieId);
            return new OperationResult(true, "Фильм успешно удален");
        }

        public IEnumerable<MovieViewModel> GetMovies(int skippedItems = 0, int takenItems = 0)
        => Database.MovieManager.GetMovies(skippedItems, takenItems).ToList().Select(m => MovieViewModel.FromMovie(m));

        public IEnumerable<MovieViewModel> GetMoviesByUserId(string UserId, int skippedItems = 0, int takenItems = 0)
        => GetMovies(skippedItems, takenItems).ToList().Where(m => m.UserId == UserId);

        public async Task<OperationResult> UpdateAsync(MovieViewModel movieViewModel)
        {
            try
            {
                Movie movie = Database.MovieManager.GetMovieById(movieViewModel.Id);
                if (movie == null)
                {
                    return new OperationResult(false, $"Фильм с Id {movieViewModel.Id} не существует");
                }
                movie.Name = movieViewModel.Name;
                movie.Description = movieViewModel.Description;
                movie.Producer = movieViewModel.Producer;
                movie.YearOfProduction = movieViewModel.YearOfProduction;
                //movie.UserProfile = Database.UserProfileManager.GetUserProfileById(movieViewModel.UserId);
                if (movieViewModel.Poster != null)
                {
                    movie.PosterFileExtension = Path.GetExtension(movieViewModel.Poster.FileName);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {

                        if (!movie.PosterFileExtension.Equals(".png", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Bitmap bitmap = (Bitmap)Bitmap.FromStream(movieViewModel.Poster.InputStream);
                            bitmap.Save(memoryStream, ImageFormat.Png);
                            movie.Poster = Convert.ToBase64String(memoryStream.ToArray());
                            bitmap.Dispose();
                        }
                        else
                        {
                            byte[] bytes = new Byte[movieViewModel.Poster.InputStream.Length];
                            movieViewModel.Poster.InputStream.Read(bytes, 0, (int)movieViewModel.Poster.InputStream.Length);
                            movieViewModel.Poster.InputStream.Seek(0, SeekOrigin.Begin);
                            movieViewModel.Poster.InputStream.Close();
                            movie.Poster = Convert.ToBase64String(bytes);
                        }

                    }
                }


                await Database.MovieManager.UpdateAsync(movie);
                return new OperationResult(true, "Фильм успешно обновлен");
            }
            catch (Exception ex)
            {
                return new OperationResult(false, ex.Message, null, ex);
            }
            
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public MovieViewModel GetMovieById(int id)
         => MovieViewModel.FromMovie(Database.MovieManager.GetMovieById(id));
    }
}
