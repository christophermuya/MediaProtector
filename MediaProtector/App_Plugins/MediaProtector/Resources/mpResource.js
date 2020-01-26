angular.module("umbraco.resources").factory("mpResource", function ($http) {
    return {
        getById: function (id) {
            return $http.get("backoffice/api/MediaProtectorApi/GetById?id=" + id);
        },
        getAll: function () {
            return $http.get("backoffice/api/MediaProtectorApi/GetAll");
        },
        save: function (model) {
            return $http.post("backoffice/api/MediaProtectorApi/Save", angular.toJson(model));
        } 
    };
}); 