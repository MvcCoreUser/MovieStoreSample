using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieStore.BusinessLogic.Infrastructure;
using MovieStore.BusinessLogic.ViewModels;

namespace MovieStore.Web.Infrastructure
{
    public static class GridHelper
    {
        public static List<MovieViewModel> GetMovies(int? page, int? limit, string sortBy, string direction, string searchString, out int total)
        {

            var records = ServiceFactory.MovieService.GetAllMovies().AsQueryable();
            total = records.Count();
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                records = records.Where(p => p.Name.Contains(searchString) || p.Description.Contains(searchString) || p.Producer.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(direction))
            {
                if (direction.Trim().ToLower() == "asc")
                {
                    records = SortHelper.OrderBy(records, sortBy);
                }
                else
                {
                    records = SortHelper.OrderByDescending(records, sortBy);
                }
            }

            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = records.Skip(start).Take(limit.Value);
            }

            return records.ToList();
        }

        //public static OperationResult Save(MovieViewModel movieVM)
        //{
        //    var movie = ServiceFactory.MovieService.GetMovieById(movieVM.Id);
        //    OperationResult operationResult;
        //    if (movie!=null)
        //    {
        //        operationResult= ServiceFactory.MovieService.UpdateAsync(movieVM).GetAwaiter().GetResult();
        //    }
        //    else
        //    {
        //        operationResult= ServiceFactory.MovieService.CreateAsync(movieVM).GetAwaiter().GetResult();
        //    }
        //    return operationResult;
        //}

        //public static OperationResult Remove(int movieId)
        //{
        //    OperationResult operationResult = ServiceFactory.MovieService.DeleteAsync(movieId).GetAwaiter().GetResult();
        //    return operationResult;
        //}
    }
}