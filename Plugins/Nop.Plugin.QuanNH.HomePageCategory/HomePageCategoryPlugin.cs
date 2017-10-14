using System.Web.Routing;
using Nop.Core.Plugins;
using Nop.Services.Common;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Web.Framework;
using System.Collections.Generic;
using Nop.Services.Configuration;
using Nop.Core;
using Nop.Services.Cms;

namespace Nop.Plugin.Widgets.HomePageCategory
{
    public class HomePageCategoryPlugin : BasePlugin, IWidgetPlugin
    {


        #region Methods

        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;

        public HomePageCategoryPlugin(ISettingService settingService, IWebHelper webHelper)
        {
            this._settingService = settingService;
            this._webHelper = webHelper;
        }

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            return new List<string> { "home_page_before_news" };
        }

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "WidgetsHomePageCategory";
            routeValues = new RouteValueDictionary() { { "Namespaces", "Nop.Plugin.Widgets.HomePageCategory.Controllers" }, { "area", null } };
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
            controllerName = "WidgetsHomePageCategory";
            routeValues = new RouteValueDictionary
            {
                {"Namespaces", "Nop.Plugin.Widgets.HomePageCategory.Controllers"},
                {"area", null},
                {"widgetZone", widgetZone}
            };
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            //settings
            var settings = new HomePageCategorySettings
            {
                DisplayFeaturedProduct = false,
                NumberFeaturedProduct = 3,
                NumberProduct = 6,
                Template = "PublicInfo",
                Enable=false
            };
            _settingService.SaveSetting(settings);

            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.HomePageCategory.DisplayFeaturedProduct", "Display Featured Product");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.HomePageCategory.NumberFeaturedProduct", "Number Featured Product");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.HomePageCategory.NumberProduct", "Number Product");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.HomePageCategory.Enable", "Enable");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.HomePageCategory.Template", "Template");
            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            _settingService.DeleteSetting<HomePageCategorySettings>();

            //locales
            this.DeletePluginLocaleResource("Plugins.Widgets.HomePageCategory.DisplayFeaturedProduct");
            this.DeletePluginLocaleResource("Plugins.Widgets.HomePageCategory.NumberFeaturedProduct");
            this.DeletePluginLocaleResource("Plugins.Widgets.HomePageCategory.NumberProduct");
            this.DeletePluginLocaleResource("Plugins.Widgets.HomePageCategory.Enable");
            this.DeletePluginLocaleResource("Plugins.Widgets.HomePageCategory.Template");

            base.Uninstall();
        }

        #endregion
    }
}
