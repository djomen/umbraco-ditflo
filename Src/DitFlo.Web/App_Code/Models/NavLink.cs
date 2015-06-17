﻿using DitFlo.Ditto.ValueResolvers;
using Our.Umbraco.Ditto;

namespace DitFlo.Models
{
    public class NavLink : Link
    {
        [DittoValueResolver(typeof(ActiveNavLinkResolver))]
        public bool IsActive { get; set; }
    }
}
