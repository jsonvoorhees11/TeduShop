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
        $scope.moreImages = [];
        $scope.loadProductDetails = loadProductDetails;
        $scope.updateProduct = updateProduct;
        $scope.getSeoTitle = getSeoTitle;
        $scope.chooseImage = chooseImage;
        $scope.chooseMoreImage = chooseMoreImage;
        $scope.loadProductCategories = loadProductCategories;

        function loadProductDetails(){
            var config = {
                params: {
                    id: $stateParams.id
                }
            }
            apiService.get('api/product/getbyid', config, function (result) {
                $scope.product = result.data;
                $scope.moreImages = JSON.parse($scope.product.MoreImages);
                if ($scope.moreImages == null) {
                    $scope.moreImages = [];
                }
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function updateProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
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

        function chooseImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                })
            };
            finder.popup();
        };

        function chooseMoreImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                })
            };
            finder.popup();
        }

        loadProductCategories();
        loadProductDetails();
    }

})(angular.module('tedushop.products'));