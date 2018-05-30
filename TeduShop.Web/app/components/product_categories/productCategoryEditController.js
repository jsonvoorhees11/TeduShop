(function (app) {
    app.controller('productCategoryEditController', productCategoryEditController);

    productCategoryEditController.$inject = ['$scope', 'apiService', 'notificationService','commonService','$state','$stateParams'];

    function productCategoryEditController($scope, apiService, notificationService, commonService, $state, $stateParams) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        }
        $scope.parentCategories = [];

        $scope.updateProductCategory = updateProductCategory;
        $scope.getSeoTitle = getSeoTitle;

        function loadProductCategoryDetails() {    
            var config = {
                params: {
                    id: $stateParams.id
                }
            }
            apiService.get('api/productCategory/getbyid', config, function (result) {
                $scope.productCategory = result.data;
                console.log($scope.productCategory);
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function loadParentCategories() {
            apiService.get('api/productCategory/getallparents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function (error) {
                console.log("Can't get list parent");
            });
        };
        function updateProductCategory() {
            apiService.put('api/productCategory/update', $scope.productCategory,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật.');
                    $state.go('product_categories');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công');
                });
        };

        function getSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }

        loadParentCategories();
        loadProductCategoryDetails();
    }
})(angular.module('tedushop.product_categories'));