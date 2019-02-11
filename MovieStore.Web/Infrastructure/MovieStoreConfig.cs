using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MovieStore.Web
{
    public class MovieStoreConfig
    {
        private MovieStoreConfig()
        {

        }

        public static MovieStoreConfig Current =>
            new MovieStoreConfig();
        public string AppName
           => ConfigurationManager.AppSettings[nameof(AppName)];

    }
}