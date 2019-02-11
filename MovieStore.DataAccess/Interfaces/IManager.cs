using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore.DataAccess.EF;

namespace MovieStore.DataAccess.Interfaces
{
    public interface IManager: IDisposable
    {
        ApplicationContext Database { get; set; }
    }
}
