angular.module("umbraco").controller("mp.content.controller", function (navigationService) {
    var vm = this;

    var backofficeView = Umbraco.Sys.ServerVariables.umbracoSettings.appPluginsPath + '/MediaProtector/backoffice/mediaProtector/';

    navigationService.syncTree({ tree: "mediaProtector", path: ['-1'] });

    vm.page = {
        title: 'Media Protector',
        description: 'Protect your media nodes by preventing certain actions from succeeding',
        navigation: [
            {
                name: 'Protection',
                alias: 'protection',
                icon: 'icon-custom-account-balance-wallet',
                view: backofficeView + 'protection.html',
                active: true
            },
            {
                name: 'Info',
                alias: 'info',
                icon: 'icon-info',
                view: backofficeView + 'info.html'
            }
        ]
    };

});
