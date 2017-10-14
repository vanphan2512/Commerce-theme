using System.Collections.Generic;
using System.IO;
using System.Web.Routing;
using Nop.Core;
using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;

namespace Nop.Plugin.Widgets.Partner
{
    /// <summary>
    /// PLugin
    /// </summary>
    public class NivoSliderPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;

        public NivoSliderPlugin(IPictureService pictureService,
            ISettingService settingService, IWebHelper webHelper)
        {
            this._pictureService = pictureService;
            this._settingService = settingService;
            this._webHelper = webHelper;
        }

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            return new List<string> { "content_partner" };
        }

        /// <summary>
        /// Gets a route for provider configuration
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "WidgetsPartner";
            routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.Widgets.Partner.Controllers" }, { "area", null } };
        }

        /// <summary>
        /// Gets a route for displaying widget
        /// </summary>
        /// <param name="widgetZone">Widget zone where it's displayed</param>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetDisplayWidgetRoute(string widgetZone, out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "PublicInfo";
            controllerName = "WidgetsPartner";
            routeValues = new RouteValueDictionary
            {
                {"Namespaces", "Nop.Plugin.Widgets.Partner.Controllers"},
                {"area", null},
                {"widgetZone", widgetZone}
            };
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            //pictures
            var sampleImagesPath = CommonHelper.MapPath("~/Plugins/Widgets.Partner/Content/partner/sample-images/");


            //settings
            var settings = new PartnerSettings
            {
                Picture1Id = _pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "1-130x50.png"), MimeTypes.ImagePJpeg, "banner_1").Id,
                Text1 = "",
                Link1 = _webHelper.GetStoreLocation(false),
                Picture2Id = _pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "2-130x50.png"), MimeTypes.ImagePJpeg, "banner_2").Id,
                Text2 = "",
                Link2 = _webHelper.GetStoreLocation(false),
                Picture3Id = _pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "3-130x50.png"), MimeTypes.ImagePJpeg, "banner_2").Id,
                Text3 = "",
                Link3 = _webHelper.GetStoreLocation(false),
                Picture4Id = _pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "4-130x50.png"), MimeTypes.ImagePJpeg, "banner_2").Id,
                Text4 = "",
                Link4 = _webHelper.GetStoreLocation(false),
                Picture5Id = _pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "5-130x50.png"), MimeTypes.ImagePJpeg, "banner_2").Id,
                Text5 = "",
                Link5 = _webHelper.GetStoreLocation(false),
                Picture6Id = _pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "2-130x50.png"), MimeTypes.ImagePJpeg, "banner_2").Id,
                Text6 = "",
                Link6 = _webHelper.GetStoreLocation(false),
                Picture7Id = _pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "1-130x50.png"), MimeTypes.ImagePJpeg, "banner_2").Id,
                Text7 = "",
                Link7 = _webHelper.GetStoreLocation(false),
            };
            _settingService.SaveSetting(settings);


            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Partner.Picture1", "Picture 1");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Partner.Picture2", "Picture 2");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Partner.Picture3", "Picture 3");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Partner.Picture4", "Picture 4");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Partner.Picture5", "Picture 5");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Partner.Picture6", "Picture 6");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Partner.Picture7", "Picture 7");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Partner.Picture8", "Picture 8");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Partner.Picture9", "Picture 9");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Partner.Picture10", "Picture 10");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Partner.Picture", "Picture");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Partner.Picture.Hint", "Upload picture.");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Partner.Text", "Comment");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Partner.Text.Hint", "Enter comment for picture. Leave empty if you don't want to display any text.");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Partner.Link", "URL");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Partner.Link.Hint", "Enter URL. Leave empty if you don't want this picture to be clickable.");

            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            //settings
            _settingService.DeleteSetting<PartnerSettings>();

            //locales
            this.DeletePluginLocaleResource("Plugins.Widgets.Partner.Picture1");
            this.DeletePluginLocaleResource("Plugins.Widgets.Partner.Picture2");
            this.DeletePluginLocaleResource("Plugins.Widgets.Partner.Picture3");
            this.DeletePluginLocaleResource("Plugins.Widgets.Partner.Picture4");
            this.DeletePluginLocaleResource("Plugins.Widgets.Partner.Picture5");
            this.DeletePluginLocaleResource("Plugins.Widgets.Partner.Picture6");
            this.DeletePluginLocaleResource("Plugins.Widgets.Partner.Picture7");
            this.DeletePluginLocaleResource("Plugins.Widgets.Partner.Picture8");
            this.DeletePluginLocaleResource("Plugins.Widgets.Partner.Picture9");
            this.DeletePluginLocaleResource("Plugins.Widgets.Partner.Picture10");
            this.DeletePluginLocaleResource("Plugins.Widgets.Partner.Picture");
            this.DeletePluginLocaleResource("Plugins.Widgets.Partner.Picture.Hint");
            this.DeletePluginLocaleResource("Plugins.Widgets.Partner.Text");
            this.DeletePluginLocaleResource("Plugins.Widgets.Partner.Text.Hint");
            this.DeletePluginLocaleResource("Plugins.Widgets.Partner.Link");
            this.DeletePluginLocaleResource("Plugins.Widgets.Partner.Link.Hint");

            base.Uninstall();
        }
    }
}
