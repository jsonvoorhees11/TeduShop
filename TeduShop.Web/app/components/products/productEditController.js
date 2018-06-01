(function (app) {
    app.controller('productEditController', productEditController);

    productEditController.$inject = ['$scope', 'apiService', 'notificationService', 'commonService', '$state', '$stateParams'];

    function productEditController($scope, apiService, notificationService, commonService, $state, $stateParams) {
        $scope.product = {};
        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        };
        $scope.productCategories = [];
        $scope.loadProductDetails = loadProductDetails;
        $scope.updateProduct = updateProduct;
        $scope.getSeoTitle = getSeoTitle;
        $scope.loadProductCategories = loadProductCategories;

        function loadProductDetails(){
            var config = {
                params: {
                    id: $stateParams.id
                }
            }
            apiService.get('api/product/getbyid', config, function (result) {
                $scope.product = result.data;
                console.log($scope.product);
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function updateProduct() {
            apiService.put('api/product/update', $scope.product,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật.');
                    $state.go('products');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công');
                });
        }

        function getSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        function loadProductCategories() {
            apiService.get('api/productCategory/getallparents', null, function (result) {
                $scope.productCategories = result.data;
            }, function (error) {
                console.log("Can't get list parent");
            });
        }

        $scope.chooseImage = function chooseImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.product.Image = fileUrl;
            };
            finder.popup();
        };

        loadProductCategories();
        loadProductDetails();
    }

})(angular.module('tedushop.products'));