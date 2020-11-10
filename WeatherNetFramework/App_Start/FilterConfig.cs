using System.Web;
using System.Web.Mvc;
using WeatherNetFramework.Services;

namespace WeatherNetFramework
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        
        }
    }
}
