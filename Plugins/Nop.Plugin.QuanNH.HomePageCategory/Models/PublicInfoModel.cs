using System.Collections.Generic;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.Media;
using Nop.Web.Models.Catalog;

namespace Nop.Plugin.Widgets.HomePageCategory.Models
{
    public partial class PublicInfoModel : BaseNopEntityModel
    {
        public PublicInfoModel()
        {
            FeaturedProducts = new List<ProductOverviewModel>();
            Products = new List<ProductOverviewModel>();
        }
        public IList<ProductOverviewModel> FeaturedProducts { get; set; }
        public IList<ProductOverviewModel> Products { get; set; }
    }
}
