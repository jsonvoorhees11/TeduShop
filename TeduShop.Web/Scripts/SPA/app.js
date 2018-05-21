

var myApp = angular.module('myModule', []);
myApp.controller("animalController", animalController);
myApp.controller("dogController", dogController);
myApp.controller("catController", catController);
//myController.$inject = ['$scope'];

function animalController($scope) {
    $scope.message = "animal message";
}
function dogController($scope) {
    $scope.message = "woof woof";
}
function catController($scope) {
    $scope.message = "mew mew";
}