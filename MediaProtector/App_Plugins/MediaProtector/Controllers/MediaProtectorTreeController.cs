using System;
using System.Net.Http.Formatting;
using System.Web.Http.ModelBinding;
using Umbraco.Core;
using Umbraco.Core.Scoping;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;
using Umbraco.Web.WebApi.Filters;

namespace MediaProtector.App_Plugins.MediaProtector.Controllers {
    [PluginController(MediaProtectorSettings.PluginAreaName)]
    [Tree(Constants.Applications.Settings,treeAlias: MediaProtectorSettings.Alias,TreeTitle = MediaProtectorSettings.TreeTitle,TreeGroup = Constants.Trees.Groups.ThirdParty,SortOrder = 35)]
    public class MediaProtectorTreeController:TreeController {
        private IScopeProvider _scopeProvider;
        public MediaProtectorTreeController(IScopeProvider scopeProvider) {
            _scopeProvider = scopeProvider;
        }

        protected override TreeNode CreateRootNode(FormDataCollection queryStrings) {
            var root = base.CreateRootNode(queryStrings);
            //optionally setting a routepath would allow you to load in a custom UI instead of the usual behaviour for a tree
            root.RoutePath = string.Format("{0}/{1}/{2}",Constants.Applications.Settings,MediaProtectorSettings.Alias,"content");
            // set the icon
            root.Icon = "icon-custom-account-balance-wallet";
            // set to false for a custom tree with a single node.
            root.HasChildren = false;
            //url for menu
            root.MenuUrl = null;

            return root;
        }

        protected override MenuItemCollection GetMenuForNode(string id,[ModelBinder(typeof(HttpQueryStringModelBinder))] FormDataCollection queryStrings) {
            throw new NotImplementedException();
        }

        protected override TreeNodeCollection GetTreeNodes(string id,[ModelBinder(typeof(HttpQueryStringModelBinder))] FormDataCollection queryStrings) {
            throw new NotImplementedException();
        }
    }
}