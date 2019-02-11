using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DataAccess.Entities
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int YearOfProduction { get; set; }
        public string Producer { get; set; }
        public string Poster { get; set; }
        public string PosterFileExtension { get; set; }
        [Required]
        public string UserProfileId { get; set; }
        public UserProfile UserProfile{ get; set; }
    }
}
