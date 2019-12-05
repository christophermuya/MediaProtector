angular.module("umbraco").controller("moveEventController", function ($http, mediaResource, editorService, $route, mpResource, usersResource, userService, mpPropertyEditor, notificationsService) {
    var vm = this;
    vm.Name = "Move Controller";
    vm.currentSelectedUsers = [];
    vm.selectedMediaIds = [];

    var eventModel = {
        mediaNodes: "",
        userExceptions: "",
        editedDate: "",
        editedby: ""
    };

    mpResource.getMoveEventModel().then(function (response) {
        if (response.data !== null) {
            eventModel.mediaNodes = response.data.mediaNodes;
            eventModel.userExceptions = response.data.userExceptions;
            eventModel.editedDate = response.data.lastEdited;
            eventModel.editedby = response.data.lastEditedBy;

            if (response.data.userExceptions !== "") {
                getExceptionUsers(response.data.userExceptions);
            }
            if (response.data.mediaNodes !== "") {
                getMediaNodes(response.data.mediaNodes);
            }
        }


    }).then(function () {


    }).then(function () {
        vm.editedDate = eventModel.editedDate;
        vm.editedByUser = eventModel.editedby;

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
    });
       
    vm.saveSettings = function () {

        computeUserExceptionIds(vm.currentSelectedUsers);
        computeMediaIds(vm.selectedMediaIds);

        vm.saveSettingsState = 'busy';

        mpResource.updateMoveEventModel(eventModel).then(function (response) {
            notificationsService.success('Saved ', " settings for move event");
            vm.saveSettingsState = 'success';

        }, function (error) {
            vm.saveSettingsState = 'error';
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
        eventModel.mediaNodes = "";
        for (let i = 0; i < items.length; i++) {
            if (i == (items.length - 1)) {
                eventModel.mediaNodes += items[i].id;
            }
            else {
                eventModel.mediaNodes += items[i].id + ",";
            }
        }
    }

    function computeUserExceptionIds(items) {
        eventModel.userExceptions = "";
        for (let i = 0; i < items.length; i++) {
            if (i == (items.length - 1)) {
                eventModel.userExceptions += items[i].id;
            }
            else {
                eventModel.userExceptions += items[i].id + ",";
            }
        }
    }
});
