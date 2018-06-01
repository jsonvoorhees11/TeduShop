(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);

    productCategoryAddController.$inject = ['$scope', 'apiService', 'notificationService', 'commonService', '$state'];

    function productCategoryAddController($scope, apiService, notificationService,commonService, $state) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        }
        $scope.parentCategories = [];
        $scope.addProductCategory = addProductCategory;
        $scope.getSeoTitle = getSeoTitle;

        function loadParentCategories() {
            apiService.get('api/productCategory/getallparents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function (error) {
                console.log("Can't get list parent");
            });
        };
        function addProductCategory() {
            apiService.post('api/productCategory/create', $scope.productCategory,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm.');
                    $state.go('product_categories');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công');
                });
        };
        function getSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }

        loadParentCategories();
    }
})(angular.module('tedushop.product_categories'));