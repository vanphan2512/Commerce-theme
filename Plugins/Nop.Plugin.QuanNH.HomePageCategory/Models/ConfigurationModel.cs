using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Widgets.HomePageCategory.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.HomePageCategory.DisplayFeaturedProduct")]
        public bool DisplayFeaturedProduct { get; set; }
        public bool DisplayFeaturedProduct_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.HomePageCategory.NumberFeaturedProduct")]

        public int NumberFeaturedProduct { get; set; }
        public bool NumberFeaturedProduct_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.HomePageCategory.NumberProduct")]

        public int NumberProduct { get; set; }
        public bool NumberProduct_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.HomePageCategory.Template")]
        public string Template { get; set; }

        public bool Template_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.HomePageCategory.Enable")]
        public bool Enable { get; set; }

        public bool Enable_OverrideForStore { get; set; }
    }
}