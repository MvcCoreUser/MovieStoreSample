using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieStore.Web.Infrastructure
{
    public class BootGridResponceData<T> where T: class
    {
        public int current { get; set; }
        public int rowCount { get; set; }
        public IEnumerable<T> rows{ get; set; }
        public int total { get; set; }
    }
}