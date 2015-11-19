using System.Collections.Generic;
using Our.Umbraco.Ditto;
using System.Web.Mvc;
using Our.Umbraco.DitFlo.Models;
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
            var viewName = ControllerContext.RouteData.Values["action"].ToString();

            return View(viewName, null, model);
        }

        protected override ViewResult View(string viewName, string masterName, object model)
        {
            if (model == null)
                model = CurrentPage;

            var transferModel = new DitFloTransferModel(model, _resolverContexts);

            return base.View(viewName, masterName, transferModel);
        }

        protected virtual PartialViewResult CurrentPartialView(object model = null)
        {
            var viewName = ControllerContext.RouteData.Values["action"].ToString();

            return PartialView(viewName, model);
        }

        protected override PartialViewResult PartialView(string viewName, object model)
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
