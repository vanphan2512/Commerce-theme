
using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.HomePageCategory
{
    public class HomePageCategorySettings : ISettings
    {
        public bool DisplayFeaturedProduct { get; set; }

        public int NumberFeaturedProduct { get; set; }

        public int NumberProduct { get; set; }
        public string Template { get; set; }

        public bool Enable { get; set; }
    }
}