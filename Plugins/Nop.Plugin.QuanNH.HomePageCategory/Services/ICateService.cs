using Nop.Core.Domain.Catalog;
using System.Collections.Generic;


namespace Nop.Plugin.Widgets.HomePageCategory.Services
{
    /// <summary>
    /// Category service interface
    /// </summary>
    public partial interface ICateService
    {
        IList<Product> GetAllProductsByCategoryId(List<int> categoryIds, bool featuredProduct, int numberProduct);
    }
}
