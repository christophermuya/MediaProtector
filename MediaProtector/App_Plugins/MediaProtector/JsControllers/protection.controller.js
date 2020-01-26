angular.module("umbraco").controller("mp.protection.controller", function (mpResource) {
    var vm = this;
    vm.Name = "mp.protection";
    vm.model = [];

    mpResource.getAll().then(function (response) {
        if (response.data !== null) {
            vm.model = response.data;
        }        

    }).then(function () {
       
    });   
});
