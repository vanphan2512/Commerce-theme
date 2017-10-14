using System.Linq;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.SliderRevolution.Infrastructure.Cache;
using Nop.Plugin.Widgets.SliderRevolution.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Stores;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.SliderRevolution.Controllers
{
    public class WidgetsSliderRevolutionController : BasePluginController
    {
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IStoreService _storeService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly ICacheManager _cacheManager;
        private readonly ILocalizationService _localizationService;

        public WidgetsSliderRevolutionController(IWorkContext workContext,
            IStoreContext storeContext,
            IStoreService storeService, 
            IPictureService pictureService,
            ISettingService settingService,
            ICacheManager cacheManager,
            ILocalizationService localizationService)
        {
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._storeService = storeService;
            this._pictureService = pictureService;
            this._settingService = settingService;
            this._cacheManager = cacheManager;
            this._localizationService = localizationService;
        }

        protected string GetPictureUrl(int pictureId)
        {
            string cacheKey = string.Format(ModelCacheEventConsumer.REVOLUTION_PICTURE_URL_MODEL_KEY, pictureId);
            return _cacheManager.Get(cacheKey, () =>
            {
                var url = _pictureService.GetPictureUrl(pictureId, showDefaultPicture: false);
                //little hack here. nulls aren't cacheable so set it to ""
                if (url == null)
                    url = "";

                return url;
            });
        }

        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure()
        {
            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var revolutionSettings = _settingService.LoadSetting<SliderRevolutionSettings>(storeScope);
            var model = new ConfigurationModel();
            model.Picture1Id = revolutionSettings.Picture1Id;
            model.Text1 = revolutionSettings.Text1;
            model.Link1 = revolutionSettings.Link1;
            model.Picture2Id = revolutionSettings.Picture2Id;
            model.Text2 = revolutionSettings.Text2;
            model.Link2 = revolutionSettings.Link2;
            model.Picture3Id = revolutionSettings.Picture3Id;
            model.Text3 = revolutionSettings.Text3;
            model.Link3 = revolutionSettings.Link3;
            model.Picture4Id = revolutionSettings.Picture4Id;
            model.Text4 = revolutionSettings.Text4;
            model.Link4 = revolutionSettings.Link4;
            model.Picture5Id = revolutionSettings.Picture5Id;
            model.Text5 = revolutionSettings.Text5;
            model.Link5 = revolutionSettings.Link5;
            model.ActiveStoreScopeConfiguration = storeScope;
            if (storeScope > 0)
            {
                model.Picture1Id_OverrideForStore = _settingService.SettingExists(revolutionSettings, x => x.Picture1Id, storeScope);
                model.Text1_OverrideForStore = _settingService.SettingExists(revolutionSettings, x => x.Text1, storeScope);
                model.Link1_OverrideForStore = _settingService.SettingExists(revolutionSettings, x => x.Link1, storeScope);
                model.Picture2Id_OverrideForStore = _settingService.SettingExists(revolutionSettings, x => x.Picture2Id, storeScope);
                model.Text2_OverrideForStore = _settingService.SettingExists(revolutionSettings, x => x.Text2, storeScope);
                model.Link2_OverrideForStore = _settingService.SettingExists(revolutionSettings, x => x.Link2, storeScope);
                model.Picture3Id_OverrideForStore = _settingService.SettingExists(revolutionSettings, x => x.Picture3Id, storeScope);
                model.Text3_OverrideForStore = _settingService.SettingExists(revolutionSettings, x => x.Text3, storeScope);
                model.Link3_OverrideForStore = _settingService.SettingExists(revolutionSettings, x => x.Link3, storeScope);
                model.Picture4Id_OverrideForStore = _settingService.SettingExists(revolutionSettings, x => x.Picture4Id, storeScope);
                model.Text4_OverrideForStore = _settingService.SettingExists(revolutionSettings, x => x.Text4, storeScope);
                model.Link4_OverrideForStore = _settingService.SettingExists(revolutionSettings, x => x.Link4, storeScope);
                model.Picture5Id_OverrideForStore = _settingService.SettingExists(revolutionSettings, x => x.Picture5Id, storeScope);
                model.Text5_OverrideForStore = _settingService.SettingExists(revolutionSettings, x => x.Text5, storeScope);
                model.Link5_OverrideForStore = _settingService.SettingExists(revolutionSettings, x => x.Link5, storeScope);
            }

            return View("~/Plugins/Widgets.SliderRevolution/Views/Configure.cshtml", model);
        }

        [HttpPost]
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure(ConfigurationModel model)
        {
            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var revolutionSettings = _settingService.LoadSetting<SliderRevolutionSettings>(storeScope);

            //get previous picture identifiers
            var previousPictureIds = new[] 
            {
                revolutionSettings.Picture1Id,
                revolutionSettings.Picture2Id,
                revolutionSettings.Picture3Id,
                revolutionSettings.Picture4Id,
                revolutionSettings.Picture5Id
            };

            revolutionSettings.Picture1Id = model.Picture1Id;
            revolutionSettings.Text1 = model.Text1;
            revolutionSettings.Link1 = model.Link1;
            revolutionSettings.Picture2Id = model.Picture2Id;
            revolutionSettings.Text2 = model.Text2;
            revolutionSettings.Link2 = model.Link2;
            revolutionSettings.Picture3Id = model.Picture3Id;
            revolutionSettings.Text3 = model.Text3;
            revolutionSettings.Link3 = model.Link3;
            revolutionSettings.Picture4Id = model.Picture4Id;
            revolutionSettings.Text4 = model.Text4;
            revolutionSettings.Link4 = model.Link4;
            revolutionSettings.Picture5Id = model.Picture5Id;
            revolutionSettings.Text5 = model.Text5;
            revolutionSettings.Link5 = model.Link5;

            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            _settingService.SaveSettingOverridablePerStore(revolutionSettings, x => x.Picture1Id, model.Picture1Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(revolutionSettings, x => x.Text1, model.Text1_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(revolutionSettings, x => x.Link1, model.Link1_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(revolutionSettings, x => x.Picture2Id, model.Picture2Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(revolutionSettings, x => x.Text2, model.Text2_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(revolutionSettings, x => x.Link2, model.Link2_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(revolutionSettings, x => x.Picture3Id, model.Picture3Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(revolutionSettings, x => x.Text3, model.Text3_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(revolutionSettings, x => x.Link3, model.Link3_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(revolutionSettings, x => x.Picture4Id, model.Picture4Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(revolutionSettings, x => x.Text4, model.Text4_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(revolutionSettings, x => x.Link4, model.Link4_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(revolutionSettings, x => x.Picture5Id, model.Picture5Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(revolutionSettings, x => x.Text5, model.Text5_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(revolutionSettings, x => x.Link5, model.Link5_OverrideForStore, storeScope, false);
            
            //now clear settings cache
            _settingService.ClearCache();
            
            //get current picture identifiers
            var currentPictureIds = new[]
            {
                revolutionSettings.Picture1Id,
                revolutionSettings.Picture2Id,
                revolutionSettings.Picture3Id,
                revolutionSettings.Picture4Id,
                revolutionSettings.Picture5Id
            };

            //delete an old picture (if deleted or updated)
            foreach (var pictureId in previousPictureIds.Except(currentPictureIds))
            { 
                var previousPicture = _pictureService.GetPictureById(pictureId);
                if (previousPicture != null)
                    _pictureService.DeletePicture(previousPicture);
            }

            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));
            return Configure();
        }

        [ChildActionOnly]
        public ActionResult PublicInfo(string widgetZone, object additionalData = null)
        {
            var revolutionSettings = _settingService.LoadSetting<SliderRevolutionSettings>(_storeContext.CurrentStore.Id);

            var model = new PublicInfoModel();
            model.Picture1Url = GetPictureUrl(revolutionSettings.Picture1Id);
            model.Text1 = revolutionSettings.Text1;
            model.Link1 = revolutionSettings.Link1;

            model.Picture2Url = GetPictureUrl(revolutionSettings.Picture2Id);
            model.Text2 = revolutionSettings.Text2;
            model.Link2 = revolutionSettings.Link2;

            model.Picture3Url = GetPictureUrl(revolutionSettings.Picture3Id);
            model.Text3 = revolutionSettings.Text3;
            model.Link3 = revolutionSettings.Link3;

            model.Picture4Url = GetPictureUrl(revolutionSettings.Picture4Id);
            model.Text4 = revolutionSettings.Text4;
            model.Link4 = revolutionSettings.Link4;

            model.Picture5Url = GetPictureUrl(revolutionSettings.Picture5Id);
            model.Text5 = revolutionSettings.Text5;
            model.Link5 = revolutionSettings.Link5;

            if (string.IsNullOrEmpty(model.Picture1Url) && string.IsNullOrEmpty(model.Picture2Url) &&
                string.IsNullOrEmpty(model.Picture3Url) && string.IsNullOrEmpty(model.Picture4Url) &&
                string.IsNullOrEmpty(model.Picture5Url))
                //no pictures uploaded
                return Content("");


            return View("~/Plugins/Widgets.SliderRevolution/Views/PublicInfo.cshtml", model);
        }
    }
}