

var myApp = angular.module('myModule', []);
myApp.controller("animalController", animalController);
myApp.service("validator", validator);

animalController.$inject = ['$scope', 'validator'];
myApp.controller("dogController", dogController);
myApp.controller("catController", catController);

function animalController($scope, validator) {
    $scope.checkEven = function()
    {
        $scope.message = validator.checkNumber($scope.num);
    }
    $scope.num = 1;
}
function dogController($scope) {
    $scope.message = "woof woof";
}
function catController($scope) {
    $scope.message = "mew mew";
}

function validator($window) {
    return {
        checkNumber: checkNumber
    }
    function checkNumber(input) {
        if (input % 2 == 0) {
            return "This is even";
        }
        else {
            return "This is odd";
        }
    } 
}