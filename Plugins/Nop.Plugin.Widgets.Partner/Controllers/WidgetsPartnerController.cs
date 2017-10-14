using System.Linq;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.Partner.Infrastructure.Cache;
using Nop.Plugin.Widgets.Partner.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Stores;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.Partner.Controllers
{
    public class WidgetsPartnerController : BasePluginController
    {
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IStoreService _storeService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly ICacheManager _cacheManager;
        private readonly ILocalizationService _localizationService;

        public WidgetsPartnerController(IWorkContext workContext,
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
            string cacheKey = string.Format(ModelCacheEventConsumer.PARTNER_PICTURE_URL_MODEL_KEY, pictureId);
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
            var nivoSliderSettings = _settingService.LoadSetting<PartnerSettings>(storeScope);
            var model = new ConfigurationModel();
            model.Picture1Id = nivoSliderSettings.Picture1Id;
            model.Text1 = nivoSliderSettings.Text1;
            model.Link1 = nivoSliderSettings.Link1;
            model.Picture2Id = nivoSliderSettings.Picture2Id;
            model.Text2 = nivoSliderSettings.Text2;
            model.Link2 = nivoSliderSettings.Link2;
            model.Picture3Id = nivoSliderSettings.Picture3Id;
            model.Text3 = nivoSliderSettings.Text3;
            model.Link3 = nivoSliderSettings.Link3;
            model.Picture4Id = nivoSliderSettings.Picture4Id;
            model.Text4 = nivoSliderSettings.Text4;
            model.Link4 = nivoSliderSettings.Link4;
            model.Picture5Id = nivoSliderSettings.Picture5Id;
            model.Text5 = nivoSliderSettings.Text5;
            model.Link5 = nivoSliderSettings.Link5;
            model.Picture6Id = nivoSliderSettings.Picture6Id;
            model.Text6 = nivoSliderSettings.Text6;
            model.Link6 = nivoSliderSettings.Link6;
            model.Picture7Id = nivoSliderSettings.Picture7Id;
            model.Text7 = nivoSliderSettings.Text7;
            model.Link7 = nivoSliderSettings.Link7;
            model.Picture8Id = nivoSliderSettings.Picture8Id;
            model.Text8 = nivoSliderSettings.Text8;
            model.Link8 = nivoSliderSettings.Link8;
            model.Picture9Id = nivoSliderSettings.Picture9Id;
            model.Text9 = nivoSliderSettings.Text9;
            model.Link9 = nivoSliderSettings.Link9;
            model.Picture10Id = nivoSliderSettings.Picture10Id;
            model.Text10 = nivoSliderSettings.Text10;
            model.Link10 = nivoSliderSettings.Link10;
            model.ActiveStoreScopeConfiguration = storeScope;
            if (storeScope > 0)
            {
                model.Picture1Id_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Picture1Id, storeScope);
                model.Text1_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Text1, storeScope);
                model.Link1_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Link1, storeScope);
                model.Picture2Id_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Picture2Id, storeScope);
                model.Text2_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Text2, storeScope);
                model.Link2_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Link2, storeScope);
                model.Picture3Id_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Picture3Id, storeScope);
                model.Text3_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Text3, storeScope);
                model.Link3_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Link3, storeScope);
                model.Picture4Id_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Picture4Id, storeScope);
                model.Text4_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Text4, storeScope);
                model.Link4_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Link4, storeScope);
                model.Picture5Id_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Picture5Id, storeScope);
                model.Text5_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Text5, storeScope);
                model.Link5_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Link5, storeScope);

                model.Picture6Id_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Picture6Id, storeScope);
                model.Text6_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Text6, storeScope);
                model.Link6_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Link6, storeScope);
                model.Picture7Id_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Picture7Id, storeScope);
                model.Text7_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Text7, storeScope);
                model.Link7_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Link7, storeScope);
                model.Picture8Id_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Picture5Id, storeScope);
                model.Text8_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Text8, storeScope);
                model.Link8_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Link8, storeScope);
                model.Picture9Id_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Picture9Id, storeScope);
                model.Text9_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Text9, storeScope);
                model.Link9_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Link9, storeScope);
                model.Picture10Id_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Picture5Id, storeScope);
                model.Text10_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Text10, storeScope);
                model.Link10_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Link10, storeScope);
            }

            return View("~/Plugins/Widgets.Partner/Views/Configure.cshtml", model);
        }

        [HttpPost]
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure(ConfigurationModel model)
        {
            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var partnerSettings = _settingService.LoadSetting<PartnerSettings>(storeScope);

            //get previous picture identifiers
            var previousPictureIds = new[] 
            {
                partnerSettings.Picture1Id,
                partnerSettings.Picture2Id,
                partnerSettings.Picture3Id,
                partnerSettings.Picture4Id,
                partnerSettings.Picture5Id,
                partnerSettings.Picture6Id,
                partnerSettings.Picture7Id,
                partnerSettings.Picture8Id,
                partnerSettings.Picture9Id,
                partnerSettings.Picture10Id
            };

            partnerSettings.Picture1Id = model.Picture1Id;
            partnerSettings.Text1 = model.Text1;
            partnerSettings.Link1 = model.Link1;
            partnerSettings.Picture2Id = model.Picture2Id;
            partnerSettings.Text2 = model.Text2;
            partnerSettings.Link2 = model.Link2;
            partnerSettings.Picture3Id = model.Picture3Id;
            partnerSettings.Text3 = model.Text3;
            partnerSettings.Link3 = model.Link3;
            partnerSettings.Picture4Id = model.Picture4Id;
            partnerSettings.Text4 = model.Text4;
            partnerSettings.Link4 = model.Link4;
            partnerSettings.Picture5Id = model.Picture5Id;
            partnerSettings.Text5 = model.Text5;
            partnerSettings.Link5 = model.Link5;

            partnerSettings.Picture6Id = model.Picture6Id;
            partnerSettings.Text6 = model.Text6;
            partnerSettings.Link6 = model.Link6;
            partnerSettings.Picture7Id = model.Picture7Id;
            partnerSettings.Text7 = model.Text7;
            partnerSettings.Link7 = model.Link7;
            partnerSettings.Picture8Id = model.Picture8Id;
            partnerSettings.Text8 = model.Text8;
            partnerSettings.Link8 = model.Link8;
            partnerSettings.Picture9Id = model.Picture5Id;
            partnerSettings.Text9 = model.Text9;
            partnerSettings.Link9 = model.Link9;
            partnerSettings.Picture10Id = model.Picture5Id;
            partnerSettings.Text10 = model.Text10;
            partnerSettings.Link10 = model.Link10;

            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Picture1Id, model.Picture1Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Text1, model.Text1_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Link1, model.Link1_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Picture2Id, model.Picture2Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Text2, model.Text2_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Link2, model.Link2_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Picture3Id, model.Picture3Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Text3, model.Text3_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Link3, model.Link3_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Picture4Id, model.Picture4Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Text4, model.Text4_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Link4, model.Link4_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Picture5Id, model.Picture5Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Text5, model.Text5_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Link5, model.Link5_OverrideForStore, storeScope, false);


            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Picture6Id, model.Picture6Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Text6, model.Text6_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Link6, model.Link6_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Picture7Id, model.Picture7Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Text7, model.Text7_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Link7, model.Link7_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Picture8Id, model.Picture8Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Text8, model.Text8_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Link8, model.Link8_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Picture9Id, model.Picture9Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Text9, model.Text9_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Link9, model.Link9_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Picture10Id, model.Picture10Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Text10, model.Text10_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(partnerSettings, x => x.Link10, model.Link10_OverrideForStore, storeScope, false);

            //now clear settings cache
            _settingService.ClearCache();
            
            //get current picture identifiers
            var currentPictureIds = new[]
            {
                partnerSettings.Picture1Id,
                partnerSettings.Picture2Id,
                partnerSettings.Picture3Id,
                partnerSettings.Picture4Id,
                partnerSettings.Picture5Id,
                partnerSettings.Picture6Id,
                partnerSettings.Picture7Id,
                partnerSettings.Picture8Id,
                partnerSettings.Picture9Id,
                partnerSettings.Picture10Id
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
            var partnerSettings = _settingService.LoadSetting<PartnerSettings>(_storeContext.CurrentStore.Id);

            var model = new PublicInfoModel();
            model.Picture1Url = GetPictureUrl(partnerSettings.Picture1Id);
            model.Text1 = partnerSettings.Text1;
            model.Link1 = partnerSettings.Link1;

            model.Picture2Url = GetPictureUrl(partnerSettings.Picture2Id);
            model.Text2 = partnerSettings.Text2;
            model.Link2 = partnerSettings.Link2;

            model.Picture3Url = GetPictureUrl(partnerSettings.Picture3Id);
            model.Text3 = partnerSettings.Text3;
            model.Link3 = partnerSettings.Link3;

            model.Picture4Url = GetPictureUrl(partnerSettings.Picture4Id);
            model.Text4 = partnerSettings.Text4;
            model.Link4 = partnerSettings.Link4;

            model.Picture5Url = GetPictureUrl(partnerSettings.Picture5Id);
            model.Text5 = partnerSettings.Text5;
            model.Link5 = partnerSettings.Link5;

            model.Picture6Url = GetPictureUrl(partnerSettings.Picture6Id);
            model.Text6 = partnerSettings.Text6;
            model.Link6 = partnerSettings.Link6;

            model.Picture7Url = GetPictureUrl(partnerSettings.Picture7Id);
            model.Text7 = partnerSettings.Text7;
            model.Link7 = partnerSettings.Link7;

            model.Picture8Url = GetPictureUrl(partnerSettings.Picture8Id);
            model.Text8 = partnerSettings.Text8;
            model.Link8 = partnerSettings.Link8;

            model.Picture9Url = GetPictureUrl(partnerSettings.Picture9Id);
            model.Text9 = partnerSettings.Text9;
            model.Link9 = partnerSettings.Link9;

            model.Picture10Url = GetPictureUrl(partnerSettings.Picture10Id);
            model.Text10 = partnerSettings.Text10;
            model.Link10 = partnerSettings.Link10;

            if (string.IsNullOrEmpty(model.Picture1Url) && string.IsNullOrEmpty(model.Picture2Url) &&
                string.IsNullOrEmpty(model.Picture3Url) && string.IsNullOrEmpty(model.Picture4Url) &&
                string.IsNullOrEmpty(model.Picture5Url) && string.IsNullOrEmpty(model.Picture6Url) &&
                string.IsNullOrEmpty(model.Picture7Url) && string.IsNullOrEmpty(model.Picture8Url) &&
                string.IsNullOrEmpty(model.Picture9Url) && string.IsNullOrEmpty(model.Picture10Url))
                //no pictures uploaded
                return Content("");


            return View("~/Plugins/Widgets.Partner/Views/PublicInfo.cshtml", model);
        }
    }
}