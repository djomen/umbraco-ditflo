using System.Collections.Generic;
using Our.Umbraco.Ditto;
using System.Web.Mvc;
using Our.Umbraco.DitFlo.Models;
using Umbraco.Core.Logging;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace Our.Umbraco.DitFlo.Web.Mvc.Controllers
{
    public abstract class DitFloController : SurfaceController, IRenderMvcController
    {
        protected List<DittoValueResolverContext> _resolverContexts;

        protected DitFloController()
        {
            _resolverContexts = new List<DittoValueResolverContext>();
        }

        public virtual ActionResult Index(RenderModel model)
        {
            return CurrentView(model);
        }

        protected virtual ActionResult CurrentView(object model = null)
        {
            if (model == null)
                model = CurrentPage;

            var transferModel = new DitFloTransferModel(model, _resolverContexts);

            var viewName = ControllerContext.RouteData.Values["action"].ToString();

            return View(viewName, transferModel);
        }

        protected virtual ActionResult CurrentPartialView(object model = null)
        {
            if (model == null)
                model = CurrentPage;

            var transferModel = new DitFloTransferModel(model, _resolverContexts);

            var viewName = ControllerContext.RouteData.Values["action"].ToString();

            return PartialView(viewName, transferModel);
        }

        /// <summary>
        /// Allows returning a specific named view rather than using the one from the route data
        /// </summary>
        /// <param name="viewName">The view path/name to return</param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected new ViewResult View(string viewName, object model = null)
        {
            if (model == null)
                model = CurrentPage;

            var transferModel = new DitFloTransferModel(model, _resolverContexts);

            return base.View(viewName, transferModel);

        }

        /// <summary>
        /// See above - but this one is for returning a specific named partial view
        /// </summary>
        protected new ActionResult PartialView(string viewName, object model = null)
        {
            if (model == null)
                model = CurrentPage;

            var transferModel = new DitFloTransferModel(model, _resolverContexts);

            return base.PartialView(viewName, transferModel);
        }

        protected virtual void RegisterValueResolverContext<TContextType>(TContextType context)
            where TContextType : DittoValueResolverContext
        {
            _resolverContexts.Add(context);
        }
    }
}
