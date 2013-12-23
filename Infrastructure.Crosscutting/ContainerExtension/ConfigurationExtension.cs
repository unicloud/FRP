using UniCloud.Infrastructure.Crosscutting.Caching;

namespace UniCloud.Infrastructure.Utilities.Container
{
    public static class ConfigurationExtension
    {
        //使用缓存
        public static Configuration UserCaching(this Configuration configuration)
        {
            configuration.Register<ICacheProvider,DefaultCacheProvider>();
            return configuration;
        }
    }
}