using System.Collections.Generic;
using System.Globalization;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;
using Umbraco.Web.Models;

namespace Our.Umbraco.DitFlo.Models
{
    public abstract class BaseDitFloViewModel : RenderModel, IDitFloViewModel
    {
        protected BaseDitFloViewModel(
            IPublishedContent content,
            CultureInfo culture = null,
            IEnumerable<DittoProcessorContext> processorContexts = null)
            : base(content, culture)
        {
            ProcessorContexts = processorContexts ?? new List<DittoProcessorContext>();
        }

        public IPublishedContent CurrentPage { get { return Content; } }

        internal IEnumerable<DittoProcessorContext> ProcessorContexts { get; set; }
    }

    public class DitFloViewModel<TViewModel> : BaseDitFloViewModel
        where TViewModel : class
    {
        public DitFloViewModel(
            IPublishedContent content,
            CultureInfo culture = null,
            IEnumerable<DittoProcessorContext> processorContexts = null,
            TViewModel viewModel = null)
            : base(content, culture, processorContexts)
        {
            if (viewModel != null)
                View = viewModel;
        }

        private TViewModel _view;
        public TViewModel View
        {
            get
            {
                if (_view == null)
                {
                    if (Content is TViewModel)
                    {
                        _view = Content as TViewModel;
                    }
                    else
                    {
                        _view = Content.As<TViewModel>(processorContexts: ProcessorContexts);
                    }
                }

                return _view;
            }
            internal set
            {
                _view = value;
            }
        }

    }

    public class DitFloViewModel : DitFloViewModel<IPublishedContent>
    {
        protected DitFloViewModel(
            IPublishedContent content,
            CultureInfo culture = null,
            IEnumerable<DittoProcessorContext> processorContexts = null)
            : base(content, culture, processorContexts)
        { }
    }
}