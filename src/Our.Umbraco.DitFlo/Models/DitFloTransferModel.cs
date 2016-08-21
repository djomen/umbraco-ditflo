using System.Collections.Generic;
using Our.Umbraco.Ditto;

namespace Our.Umbraco.DitFlo.Models
{
    internal class DitFloTransferModel
    {
        public DitFloTransferModel(object model)
        {
            Model = model;
            ValueResolverContexts = new List<DittoProcessorContext>();
        }

        public DitFloTransferModel(object model, IEnumerable<DittoProcessorContext> resovlerContexts)
        {
            Model = model;
            ValueResolverContexts = new List<DittoProcessorContext>(resovlerContexts);
        }

        public object Model { get; set; }

        public List<DittoProcessorContext> ValueResolverContexts { get; set; }
    }
}
