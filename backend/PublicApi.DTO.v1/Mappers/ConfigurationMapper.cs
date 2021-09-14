using System;
using PublicApi.DTO.v1.APIs;

namespace PublicApi.DTO.v1.Mappers
{
    public static class ConfigurationMapper
    {
        public static BLL.App.DTO.Configuration MapToBll(ConfigurationAdd configurationAdd)
        {
            var res = new BLL.App.DTO.Configuration
            {
                AppUserId = configurationAdd.AppUserId,
                ConfigurationString = configurationAdd.ConfigurationString,
                CreationDt = DateTime.Now
            };
            return res;
        }
        
        public static BLL.App.DTO.Configuration MapToBll(Configuration configuration)
        {
            var res = new BLL.App.DTO.Configuration
            {
                Id = configuration.Id,
                AppUserId = configuration.AppUserId,
                ConfigurationString = configuration.ConfigurationString,
                CreationDt = DateTime.Parse(configuration.CreationDt)
            };
            return res;
        }
        
        public static Configuration MapToApi(BLL.App.DTO.Configuration configuration)
        {
            var res = new Configuration
            {
                Id = configuration.Id,
                AppUserId = configuration.AppUserId,
                ConfigurationString = configuration.ConfigurationString,
                CreationDt = configuration.CreationDt.ToString("yyyy-MM-dd HH:mm:ss")
            };
            return res;
        }
    }
}