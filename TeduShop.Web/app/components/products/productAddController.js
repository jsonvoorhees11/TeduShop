(function (app) {
    app.controller('productAddController', productAddController);

    productAddController.$inject = ['$scope', 'apiService', 'notificationService','commonService', '$state'];

    function productAddController($scope, apiService, notificationService, commonService, $state) {
        $scope.product = {
            CreatedDate: new Date(),
            Status: true
        }
        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }
        $scope.productCategories = [];
        $scope.addProduct = addProduct;
        $scope.getSeoTitle = getSeoTitle;
        $scope.loadProductCategories = loadProductCategories;
        

        function addProduct() {
            apiService.post('api/product/create', $scope.product,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm.');
                    $state.go('products');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công');
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
        };

        $scope.chooseImage = function chooseImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.product.Image = fileUrl;
            }
            finder.popup();
        }

        loadProductCategories();
    }


})(angular.module('tedushop.products'));