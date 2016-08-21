using System.Collections.Generic;
using Our.Umbraco.DitFlo.Models;
using System.Web.Mvc;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace Our.Umbraco.DitFlo.Web.Mvc
{
    public abstract class DitFloView : DitFloView<IPublishedContent>
    { }

    public abstract class DitFloView<TViewModel>
        : UmbracoViewPage<DitFloViewModel<TViewModel>>
        where TViewModel : class
    {
        protected override void SetViewData(ViewDataDictionary viewData)
        {
            // If model is already ditflo view model, use it
            if (viewData.Model is DitFloViewModel<TViewModel>)
            {
                base.SetViewData(viewData);
                return;
            }

            // Gather dit flow view model properties
            var model = viewData.Model;
            var resolverContexts = new List<DittoProcessorContext>();

            var transferModel = model as DitFloTransferModel;
            if (transferModel != null)
            {
                model = transferModel.Model;
                resolverContexts = transferModel.ValueResolverContexts;
            }

            // We need to give each view it's own view data dictonary
            // to allow them to have different model types
            var newViewData = new ViewDataDictionary(viewData);

            // Get current content / culture
            var content = UmbracoContext.PublishedContentRequest.PublishedContent;
            var culture = UmbracoContext.PublishedContentRequest.Culture;

            // Process model
            var publishedContent = model as IPublishedContent;
            if (publishedContent != null) 
            {
                content = publishedContent;
            }

            var renderModel = model as RenderModel;
            if (renderModel != null)
            {
                content = renderModel.Content;
                culture = renderModel.CurrentCulture;
            }

            var typedModel = model as TViewModel;

            // Create view model
            newViewData.Model = new DitFloViewModel<TViewModel>(content, culture, resolverContexts, typedModel);

            base.SetViewData(newViewData);
        }
    }
}