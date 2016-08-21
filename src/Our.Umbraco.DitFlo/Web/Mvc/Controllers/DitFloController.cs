using System.Collections.Generic;
using Our.Umbraco.Ditto;
using System.Web.Mvc;
using Our.Umbraco.DitFlo.Models;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace Our.Umbraco.DitFlo.Web.Mvc.Controllers
{
    public abstract class DitFloController : SurfaceController//, IRenderController
    {
        protected List<DittoProcessorContext> _processorContexts;

        protected DitFloController()
        {
            _processorContexts = new List<DittoProcessorContext>();
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

            var transferModel = new DitFloTransferModel(model, _processorContexts);

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

            var transferModel = new DitFloTransferModel(model, _processorContexts);

            return base.PartialView(viewName, transferModel);
        }

        protected virtual void RegisterProcessorContext<TContextType>(TContextType context)
            where TContextType : DittoProcessorContext
        {
            _processorContexts.Add(context);
        }
    }
}
