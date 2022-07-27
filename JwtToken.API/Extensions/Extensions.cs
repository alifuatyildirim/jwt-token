using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace JwtToken.API.Extensions
{
    public static class Extensions
    {
        public static TConfig SetConfig<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            string sectionName = typeof(TConfig).Name;
            var section = configuration.GetSection(sectionName);


            if (section.GetChildren().Count() == 0) throw new Exception("section boş olamaz");

            //create instance of config
            var config = new TConfig();

            //bind it to the appropriate section of configuration
            configuration.Bind(sectionName, config);
            //and register it as a service
            services.AddSingleton(config);
            services.Configure<TConfig>(configuration);

            return config;
        }
    }
}
