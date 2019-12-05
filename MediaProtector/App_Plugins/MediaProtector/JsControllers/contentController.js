angular.module("umbraco").controller("contentController", function () {
    var vm = this;

    var backofficeView = Umbraco.Sys.ServerVariables.umbracoSettings.appPluginsPath + '/MediaProtector/backoffice/mediaProtector/';

    vm.page = {
        title: 'Media Protector',
        description: '1.0.0 Protect your media nodes by preventing certain event from occurring',
        navigation: [
            {
                name: 'Save',
                alias: 'saveEvent',
                icon: 'icon-custom-save2',
                view: backofficeView + 'saveEvent.html',
                active: true                
            },
            {
                name: 'Move',
                alias: 'moveEvent',
                icon: 'icon-custom-open-with',
                view: backofficeView +  'moveEvent.html'
            },
            {
                name: 'Trash',
                alias: 'trashEvent',
                icon: 'icon-trash',
                view: backofficeView +  'trashEvent.html'
            },
            {
                name: 'Delete',
                alias: 'deleteEvent',
                icon: 'icon-custom-delete',
                view: backofficeView +  'deleteEvent.html'
            },
            {
                name: 'Add ons',
                alias: 'expansion',
                icon: 'icon-box',
                view: backofficeView +  'expansion.html'
            }
        ]
    };

});
