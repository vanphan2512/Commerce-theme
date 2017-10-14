using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Plugin.Widgets.HomePageCategory.Models;
using Nop.Plugin.Widgets.HomePageCategory.Services;
using Nop.Web.Models.Catalog;
using Nop.Services.Catalog;
using Nop.Web.Framework.Controllers;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Core.Domain;
using Nop.Services.Media;
using Nop.Services.Localization;
using Nop.Web.Models.Media;
using Nop.Web.Controllers;
using Nop.Core.Caching;
using Nop.Core;
using Nop.Services.Directory;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Media;
using Nop.Web.Extensions;
using Nop.Web.Infrastructure.Cache;
using Nop.Services.Seo;
using Nop.Services.Configuration;
using Nop.Services.Tax;
using Nop.Web.Factories;

namespace Nop.Plugin.Widgets.HomePageCategory.Controllers
{
    public class WidgetsHomePageCategoryController : BasePluginController
    {

        #region Fields
        private readonly IProductModelFactory _productModelFactory;
        private readonly ICategoryService _categoryService;
        private readonly IAclService _aclService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IPictureService _pictureService;
        private readonly ILocalizationService _localizationService;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IWebHelper _webHelper;
        private readonly IPriceCalculationService _priceCalculationService;
        private readonly IProductService _productService;
        private readonly IPriceFormatter _priceFormatter;
        private readonly IPermissionService _permissionService;
        private readonly ICurrencyService _currencyService;
        private readonly CatalogSettings _catalogSettings;
        private readonly MediaSettings _mediaSettings;
        private readonly ICateService _widgetsCategoryService;
        private readonly IStoreService _storeService;
        private readonly ISettingService _settingService;
        private readonly ISpecificationAttributeService _specificationAttributeService;
        private readonly ITaxService _taxService;

        #endregion

        #region Constructors

        public WidgetsHomePageCategoryController(
            IProductModelFactory productModelFactory,
            ICategoryService categoryService,
            IAclService aclService,
            IStoreMappingService storeMappingService,
            IPictureService pictureService,
            ILocalizationService localizationService,
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStoreContext storeContext,
            IWebHelper webHelper,
            IPriceCalculationService priceCalculationService,
            IProductService productService,
            IPriceFormatter priceFormatter,
            IPermissionService permissionService,
            ICurrencyService currencyService,
            CatalogSettings catalogSettings,
            MediaSettings mediaSettings,
            ICateService widgetsCategoryService,
            IStoreService storeService,
            ISettingService settingService,
            ISpecificationAttributeService specificationAttributeService,
            ITaxService taxService
            )
        {
            this._productModelFactory = productModelFactory;
            this._categoryService = categoryService;
            this._aclService = aclService;
            this._storeMappingService = storeMappingService;
            this._pictureService = pictureService;
            this._localizationService = localizationService;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._webHelper = webHelper;
            this._priceCalculationService = priceCalculationService;
            this._productService = productService;
            this._priceFormatter = priceFormatter;
            this._permissionService = permissionService;
            this._currencyService = currencyService;
            this._catalogSettings = catalogSettings;
            this._mediaSettings = mediaSettings;
            this._widgetsCategoryService = widgetsCategoryService;
            this._storeService = storeService;
            this._settingService = settingService;
            this._specificationAttributeService = specificationAttributeService;
            this._taxService = taxService;
        }

        #endregion

        #region Categories Extened

        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure()
        {
            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var homePageCategorySettings = _settingService.LoadSetting<HomePageCategorySettings>(storeScope);
            var model = new ConfigurationModel();
            model.DisplayFeaturedProduct = homePageCategorySettings.DisplayFeaturedProduct;
            model.NumberFeaturedProduct = homePageCategorySettings.NumberFeaturedProduct;
            model.NumberProduct = homePageCategorySettings.NumberProduct;
            model.Template = homePageCategorySettings.Template;
            model.Enable = homePageCategorySettings.Enable;
            model.ActiveStoreScopeConfiguration = storeScope;
            if (storeScope > 0)
            {
                model.DisplayFeaturedProduct_OverrideForStore = _settingService.SettingExists(homePageCategorySettings, x => x.DisplayFeaturedProduct, storeScope);
                model.NumberFeaturedProduct_OverrideForStore = _settingService.SettingExists(homePageCategorySettings, x => x.NumberFeaturedProduct, storeScope);
                model.NumberProduct_OverrideForStore = _settingService.SettingExists(homePageCategorySettings, x => x.NumberProduct, storeScope);
                model.Template_OverrideForStore = _settingService.SettingExists(homePageCategorySettings, x => x.Template, storeScope);
                model.Enable_OverrideForStore = _settingService.SettingExists(homePageCategorySettings, x => x.Enable, storeScope);
            }

            return View("~/Plugins/Widgets.HomePageCategory/Views/WidgetsHomePageCategory/Configure.cshtml", model);
        }

        [HttpPost]
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure(ConfigurationModel model)
        {
            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var homePageCategorySettings = _settingService.LoadSetting<HomePageCategorySettings>(storeScope);
            homePageCategorySettings.DisplayFeaturedProduct = model.DisplayFeaturedProduct;
            homePageCategorySettings.NumberFeaturedProduct = model.NumberFeaturedProduct;
            homePageCategorySettings.NumberProduct = model.NumberProduct;
            homePageCategorySettings.Template = model.Template;
            homePageCategorySettings.Enable = model.Enable;

            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            if (model.DisplayFeaturedProduct_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(homePageCategorySettings, x => x.DisplayFeaturedProduct, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(homePageCategorySettings, x => x.DisplayFeaturedProduct, storeScope);

            if (model.NumberFeaturedProduct_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(homePageCategorySettings, x => x.NumberFeaturedProduct, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(homePageCategorySettings, x => x.NumberFeaturedProduct, storeScope);

            if (model.NumberProduct_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(homePageCategorySettings, x => x.NumberProduct, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(homePageCategorySettings, x => x.NumberProduct, storeScope);

            if (model.Template_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(homePageCategorySettings, x => x.Template, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(homePageCategorySettings, x => x.Template, storeScope);

            if (model.Enable_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(homePageCategorySettings, x => x.Enable, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(homePageCategorySettings, x => x.Enable, storeScope);

            //now clear settings cache
            _settingService.ClearCache();

            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));
            return Configure();
        }

        [NonAction]
        protected virtual List<int> GetChildCategoryIds(int parentCategoryId)
        {
            var customerRolesIds = _workContext.CurrentCustomer.CustomerRoles
               .Where(cr => cr.Active).Select(cr => cr.Id).ToList();
            string cacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_CHILD_IDENTIFIERS_MODEL_KEY, parentCategoryId, string.Join(",", customerRolesIds), _storeContext.CurrentStore.Id);
            return _cacheManager.Get(cacheKey, () =>
            {
                var categoriesIds = new List<int>();
                var categories = _categoryService.GetAllCategoriesByParentCategoryId(parentCategoryId);
                foreach (var category in categories)
                {
                    categoriesIds.Add(category.Id);
                    categoriesIds.AddRange(GetChildCategoryIds(category.Id));
                }
                return categoriesIds;
            });
        }

        [ChildActionOnly]
        public ActionResult PublicInfo(string widgetZone = "home_page_before_news", object additionalData = null)
        {
            var homePageCategorySettings = _settingService.LoadSetting<HomePageCategorySettings>(_storeContext.CurrentStore.Id);
            if(homePageCategorySettings.Enable==false)
                return Content("");

            var customerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                 .Where(cr => cr.Active).Select(cr => cr.Id).ToList();

            var pictureSize = _mediaSettings.CategoryThumbPictureSize;

            string categoriesCacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_HOMEPAGE_KEY,
               string.Join(",", customerRolesIds),
               pictureSize,
               _storeContext.CurrentStore.Id,
               _workContext.WorkingLanguage.Id,
               _webHelper.IsCurrentConnectionSecured());

            var model = _cacheManager.Get(categoriesCacheKey, () =>
                _categoryService.GetAllCategoriesDisplayedOnHomePage()
                .Select(x =>
                {
                    var catModel = new CategoryModel
                    {
                        Id = x.Id,
                        Name = x.GetLocalized(l => l.Name),
                        Description = x.GetLocalized(l => l.Description),
                        MetaKeywords = x.GetLocalized(l => l.MetaKeywords),
                        MetaDescription = x.GetLocalized(l => l.MetaDescription),
                        MetaTitle = x.GetLocalized(l => l.MetaTitle),
                        SeName = x.GetSeName(),
                    };
                    //prepare picture model
                    var categoryPictureCacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_PICTURE_MODEL_KEY, x.Id, pictureSize, true, _workContext.WorkingLanguage.Id, _webHelper.IsCurrentConnectionSecured(), _storeContext.CurrentStore.Id);
                    catModel.PictureModel = _cacheManager.Get(categoryPictureCacheKey, () =>
                    {
                        var picture = _pictureService.GetPictureById(x.PictureId);
                        var pictureModel = new PictureModel
                        {
                            FullSizeImageUrl = _pictureService.GetPictureUrl(picture),
                            ImageUrl = _pictureService.GetPictureUrl(picture, pictureSize),
                            Title = string.Format(_localizationService.GetResource("Media.Category.ImageLinkTitleFormat"), catModel.Name),
                            AlternateText = string.Format(_localizationService.GetResource("Media.Category.ImageAlternateTextFormat"), catModel.Name)
                        };
                        return pictureModel;
                    });
                    //subcategories
                    string subCategoriesCacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_SUBCATEGORIES_KEY,
                        x.Id,
                        pictureSize,
                        string.Join(",", customerRolesIds),
                        _storeContext.CurrentStore.Id,
                        _workContext.WorkingLanguage.Id,
                        _webHelper.IsCurrentConnectionSecured());
                    catModel.SubCategories = _cacheManager.Get(subCategoriesCacheKey, () =>
                        _categoryService.GetAllCategoriesByParentCategoryId(x.Id)
                        .Select(s =>
                        {
                            var subCatModel = new CategoryModel.SubCategoryModel
                            {
                                Id = s.Id,
                                Name = s.GetLocalized(y => y.Name),
                                SeName = s.GetSeName(),
                                Description = s.GetLocalized(y => y.Description)
                            };
                            return subCatModel;
                        })
                        .ToList()
                    );
                    return catModel;
                })
                .ToList()
            );
            if (model.Count == 0)
                return Content("");

            string strTemplate = string.Format("~/Plugins/Widgets.HomePageCategory/Views/WidgetsHomePageCategory/{0}.cshtml", homePageCategorySettings.Template);
            return PartialView(strTemplate, model);
        }

        [ChildActionOnly]
        public ActionResult ProductsPublicInfo(int categoryId)
        {
            var model = new PublicInfoModel();
            var homePageCategorySettings = _settingService.LoadSetting<HomePageCategorySettings>(_storeContext.CurrentStore.Id);
            int numberProduct = homePageCategorySettings.NumberProduct;
            int numberFeaturedProduct = homePageCategorySettings.NumberFeaturedProduct;
            
            var categoryIds = new List<int>();
            categoryIds.Add(categoryId);
            if (_catalogSettings.ShowProductsFromSubcategories)
            {
                //include subcategories
                categoryIds.AddRange(GetChildCategoryIds(categoryId));
            }

            var products = _widgetsCategoryService.GetAllProductsByCategoryId(categoryIds, false, numberProduct);
           
            if (_catalogSettings.IgnoreStoreLimitations || _catalogSettings.IgnoreAcl)
            {
                //ACL and store mapping
                products = products.Where(p => _aclService.Authorize(p) && _storeMappingService.Authorize(p)).ToList();
            }

            if (homePageCategorySettings.DisplayFeaturedProduct)
            {
                var featuredProduct = _widgetsCategoryService.GetAllProductsByCategoryId(categoryIds, true, numberFeaturedProduct);

                if (_catalogSettings.IgnoreStoreLimitations || _catalogSettings.IgnoreAcl)
                {

                    featuredProduct = featuredProduct.Where(p => _aclService.Authorize(p) && _storeMappingService.Authorize(p)).ToList();
                }
                
                model.FeaturedProducts = _productModelFactory.PrepareProductOverviewModels(featuredProduct, true, true).ToList();
            }

           model.Products = _productModelFactory.PrepareProductOverviewModels(products, true, true).ToList();

           string strTemplate = string.Format("~/Plugins/Widgets.HomePageCategory/Views/WidgetsHomePageCategory/Products{0}.cshtml", homePageCategorySettings.Template);

           return PartialView(strTemplate, model);
        }

        #endregion
    }
}
