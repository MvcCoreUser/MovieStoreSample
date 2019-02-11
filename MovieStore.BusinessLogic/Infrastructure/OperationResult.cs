using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.BusinessLogic.Infrastructure
{
    public class OperationResult
    {
        public static string ErrorSeparator { get; set; } = "; ";
        public bool Succedeed { get; set; }
        public string Message { get; set; }
        public string Property { get; set; }
        public object Tag { get; set; }

        public OperationResult(bool succedeed, string message, string property=null, object tag=null)
        {
            this.Succedeed = succedeed;
            this.Message = message;
            this.Property = property;
            this.Tag = tag;
        }
    }
}
