using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.BusinessLogic.ViewModels
{
    public class AModel
    {
        public static string GetDisplayName<T>(string propertyName)
        {
            MemberInfo property = typeof(T).GetProperty(propertyName);
            var attribute = property.GetCustomAttributes(typeof(DisplayAttribute), true)
                  .Cast<DisplayAttribute>().Single();

            string displayName = attribute.Name;
            return displayName;
        }
    }
}
