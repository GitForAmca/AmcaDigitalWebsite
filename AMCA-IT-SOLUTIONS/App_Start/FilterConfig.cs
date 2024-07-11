using System.Web;
using System.Web.Mvc;

namespace AMCA_IT_SOLUTIONS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
