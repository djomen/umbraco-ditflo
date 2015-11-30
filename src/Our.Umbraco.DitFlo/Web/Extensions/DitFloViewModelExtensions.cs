using Our.Umbraco.DitFlo.Models;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Our.Umbraco.DitFlo.Web.Extensions
{
    public static class DitFloViewModelExtensions
    {
        public static IPublishedContent HomePage(this IDitFloViewModel model)
        {
            return model.CurrentPage.AncestorOrSelf(1);
        }
    }
}
