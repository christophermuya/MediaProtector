angular.module("umbraco.resources").factory("mpResource", function ($http) {
    return {
        getSaveEventModel: function () {
            return $http.get("backoffice/api/MediaProtectorApi/GetSaveEventModel");
        },
        updateSaveEventModel: function (eventModel) {
            return $http.post("backoffice/api/MediaProtectorApi/UpdateSaveEventModel", angular.toJson(eventModel));
        },

        getMoveEventModel: function () {
            return $http.get("backoffice/api/MediaProtectorApi/GetMoveEventModel");
        },
        updateMoveEventModel: function (eventModel) {
            return $http.post("backoffice/api/MediaProtectorApi/UpdateMoveEventModel", angular.toJson(eventModel));
        },  

        getTrashEventModel: function () {
            return $http.get("backoffice/api/MediaProtectorApi/GetTrashEventModel");
        },
        updateTrashEventModel: function (eventModel) {
            return $http.post("backoffice/api/MediaProtectorApi/UpdateTrashEventModel", angular.toJson(eventModel));
        },  

        getDeleteEventModel: function () {
            return $http.get("backoffice/api/MediaProtectorApi/GetDeleteEventModel");
        },
        updateDeleteEventModel: function (eventModel) {
            return $http.post("backoffice/api/MediaProtectorApi/UpdateDeleteEventModel", angular.toJson(eventModel));
        }  
    };
}); 