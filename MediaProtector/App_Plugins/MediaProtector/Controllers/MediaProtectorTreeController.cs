using System;
using System.Net.Http.Formatting;
using Umbraco.Core;
using Umbraco.Core.Scoping;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;

namespace MediaProtector.App_Plugins.MediaProtector.Controllers {
    [PluginController(MediaProtectorSettings.PluginAreaName)]
    [Tree(Constants.Applications.Settings,treeAlias: MediaProtectorSettings.Alias,TreeTitle = MediaProtectorSettings.TreeTitle,TreeGroup = Constants.Trees.Groups.ThirdParty,SortOrder = 6)]
    public class MediaProtectorTreeController:TreeController {
        private IScopeProvider _scopeProvider;
        public MediaProtectorTreeController(IScopeProvider scopeProvider) {
            _scopeProvider = scopeProvider;
        }

        protected override TreeNodeCollection GetTreeNodes(string id,FormDataCollection queryStrings) {
            // check if we're rendering the root node's children
            if(id == Constants.System.Root.ToInvariantString()) {

                // create our node collection
                var nodes = new TreeNodeCollection();

                var save = CreateTreeNode("1","-1",queryStrings,"Save","icon-custom-save",false);
                save.MenuUrl = null;

                var move = CreateTreeNode("2","-1",queryStrings,"Move","icon-custom-open-with",false);
                move.MenuUrl = null;

                var trash = CreateTreeNode("3","-1",queryStrings,"Trash","icon-trash",false);
                trash.MenuUrl = null;

                var delete = CreateTreeNode("4","-1",queryStrings,"Delete","icon-custom-delete",false);
                delete.MenuUrl = null;

                nodes.Add(save);
                nodes.Add(move);
                nodes.Add(trash);
                nodes.Add(delete);                
                return nodes;
            }

            // this tree doesn't support rendering more than 1 level
            throw new NotSupportedException();
        }

        protected override TreeNode CreateRootNode(FormDataCollection queryStrings) {
            var root = base.CreateRootNode(queryStrings);
            //optionally setting a routepath would allow you to load in a custom UI instead of the usual behaviour for a tree
            root.RoutePath = string.Format("{0}/{1}/{2}",Constants.Applications.Settings,MediaProtectorSettings.Alias,"content");
            // set the icon
            root.Icon = "icon-custom-account-balance-wallet";
            // set to false for a custom tree with a single node.
            root.HasChildren = true;
            //url for menu
            root.MenuUrl = null;

            return root;
        }

        protected override MenuItemCollection GetMenuForNode(string id,FormDataCollection queryStrings) {
            // create a Menu Item Collection to return so people can interact with the nodes in your tree
            var menu = new MenuItemCollection();

            if(id == Constants.System.Root.ToInvariantString()) {
                // root actions, perhaps users can create new items in this tree, or perhaps it's not a content tree, it might be a read only tree, or each node item might represent something entirely different...
                // add your menu item actions or custom ActionMenuItems
                menu.Items.Add(new CreateChildEntity(Services.TextService));
                // add refresh menu item (note no dialog)
                menu.Items.Add(new RefreshNode(Services.TextService,true));
                return menu;
            }
            return menu;
        }
    }
}