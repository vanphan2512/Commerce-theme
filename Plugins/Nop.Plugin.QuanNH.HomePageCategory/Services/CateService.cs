using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Widgets.HomePageCategory.Services
{
    /// <summary>
    /// Category service
    /// </summary>
    public partial class CateService : ICateService
    {

        #region Fields

        private readonly IRepository<ProductCategory> _productCategoryRepository;
        private readonly IRepository<Product> _productRepository;

        #endregion
        
        #region Ctor

        public CateService(IRepository<ProductCategory> productCategoryRepository,IRepository<Product> productRepository)
        {
            this._productCategoryRepository = productCategoryRepository;
            this._productRepository = productRepository;
        }

        #endregion

        
        #region Categories Extened

        public virtual IList<Product> GetAllProductsByCategoryId(List<int> categoryIds, bool featuredProduct, int numberProduct)
        {
            var query = from pc in _productCategoryRepository.Table
                        join p in _productRepository.Table on pc.ProductId equals p.Id
                        where categoryIds.Contains(pc.CategoryId) && pc.IsFeaturedProduct == featuredProduct
                              && !p.Deleted && p.Published
                        orderby pc.Product.DisplayOrder descending
                        select p;
            var products = query.Skip(0).Take(numberProduct).ToList();
            return products;
        }
        #endregion
    }
}
