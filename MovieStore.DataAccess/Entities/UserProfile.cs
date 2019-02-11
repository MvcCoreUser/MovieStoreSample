using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DataAccess.Entities
{
    public class UserProfile
    {
        [Key]
        [ForeignKey(nameof(ApplicationUser))]
        public string Id { get; set; }

        public string Name { get; set; }
        public string Phone { get; set; }

        public virtual ApplicationUser ApplicationUser{ get; set; }
        public ICollection<Movie> Movies { get; set; }

        public UserProfile()
        {
            Movies = new List<Movie>();
        }
    }
}
