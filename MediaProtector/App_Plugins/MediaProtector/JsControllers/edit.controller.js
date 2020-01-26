angular.module("umbraco").controller("mp.edit.controller", function (navigationService, $routeParams, mediaResource, editorService, $route, mpResource, usersResource, notificationsService) {
    var vm = this;
    vm.currentSelectedUsers = [];
    vm.selectedMediaIds = [];
    vm.disableEvent = undefined;
    vm.eventName = "";

    var model = {
        id: undefined,
        eventType: undefined,
        mediaNodes: undefined,
        disableEvent: undefined,
        userExceptions: undefined,
        editedDate: undefined,
        editedby: undefined
    };   

    vm.showView = false;
    vm.failToRetrieve = false;

    mpResource.getById($routeParams.id).then(function (response) {
        if (response.data !== null) {
            //Highlight current node onclick
            navigationService.syncTree({ tree: "mediaProtector", path: [$routeParams.id] });
            model.id = response.data.id;
            model.eventType = response.data.eventType;
            model.mediaNodes = response.data.mediaNodes;
            model.disableEvent = response.data.disableEvent;
            model.userExceptions = response.data.userExceptions;
            model.editedDate = response.data.lastEdited;
            model.editedby = response.data.lastEditedBy;

            vm.disableEvent = response.data.disableEvent;
            vm.eventName = response.data.eventType.charAt(0).toUpperCase() + response.data.eventType.slice(1);

            if (response.data.userExceptions !== "") {
                getExceptionUsers(response.data.userExceptions);
            }
            if (response.data.mediaNodes !== undefined) {
                if (response.data.mediaNodes !== "") {
                    getMediaNodes(response.data.mediaNodes);
                }
            }            
        }

    }).then(function () {
        //Temporary
    }).then(function () {
        vm.editedDate = model.editedDate;
        vm.editedByUser = model.editedby;

        vm.openUserPicker = function () {
            var userPicker = {
                title: "User Exceptions",
                selection: vm.currentSelectedUsers,
                submit: function submit(model) {
                    vm.currentSelectedUsers = model.selection;
                    computeUserExceptionIds(model.selection);
                    editorService.close();
                },
                close: function close() {
                    editorService.close();
                }
            };
            editorService.userPicker(userPicker);
        };

        vm.openMediaPicker = function openMediaPicker() {
            var mediaPicker = {
                title: "Select Media to Protect",
                section: 'media',
                treeAlias: 'media',
                entityType: 'media',
                multiPicker: true,
                hideSubmitButton: false,
                hideHeader: true,
                show: true,
                submit: function submit(model) {
                    if (model.selection) {
                        angular.forEach(model.selection, function (item) {
                            if (item.id === '-1') {
                                item.name = "Media Root";
                                item.icon = 'icon-folder';
                            }
                            multiSelectItem(item, vm.selectedMediaIds);
                        });
                    }
                    computeMediaIds(model.selection);
                    editorService.close();
                },
                close: function close() {
                    editorService.close();
                }
            };
            editorService.treePicker(mediaPicker);
        };
    }).then(function () {       
        if (model.id === undefined || ($routeParams.id < 1 || $routeParams.id > 4)) {
            vm.showView = true;
            vm.failToRetrieve = true;
        }
        else {
            vm.showView = true;
        }
    });

    vm.cancel = function () {
        $route.reload();
    };

    vm.toggleDisableAction = function () {        
        if (vm.disableEvent === false) {
            vm.disableEvent = true;
        }
        else if (vm.disableEvent === true) {
            vm.disableEvent = false;
        }        
    };

    vm.save = function () {

        computeUserExceptionIds(vm.currentSelectedUsers);
        computeMediaIds(vm.selectedMediaIds);
        model.disableEvent = vm.disableEvent;
        vm.saveButtonState = 'busy';  

        mpResource.save(model).then(function (response) {
            notificationsService.success('Saved ', " settings for " + model.eventType + " action.");
            vm.saveButtonState = 'success';

        }, function (error) {
            vm.saveButtonState = 'error';
            notificationsService.error('Error saving...', error.data.ExceptionMessage);
        });
    };

    vm.removeSelectedItem = function (index, selection) {
        selection.splice(index, 1);
    };

    function getExceptionUsers(users) {
        for (item of users.split(",")) {
            usersResource.getUser(item).then(function (data) {
                vm.currentSelectedUsers.push(data);
            });
        }
    }


    function getMediaNodes(mediaNodes) {
        mediaResource.getByIds(mediaNodes.split(",")).then(function (data) {
            vm.selectedMediaIds = data;
        });
    }

    function multiSelectItem(item, selection) {
        var found = false;
        // check if item is already in the selected list
        if (selection.length > 0) {
            angular.forEach(selection, function (selectedItem) {
                if (selectedItem.udi === item.udi) {
                    found = true;
                }
            });
        }
        // only add the selected item if it is not already selected
        if (!found) {
            selection.push(item);
        }
    }

    function computeMediaIds(items) {
        model.mediaNodes = "";
        for (let i = 0; i < items.length; i++) {
            if (i == (items.length - 1)) {
                model.mediaNodes += items[i].id;
            }
            else {
                model.mediaNodes += items[i].id + ",";
            }
        }
    }

    function computeUserExceptionIds(items) {
        model.userExceptions = "";
        for (let i = 0; i < items.length; i++) {
            if (i == (items.length - 1)) {
                model.userExceptions += items[i].id;
            }
            else {
                model.userExceptions += items[i].id + ",";
            }
        }
    }
});
