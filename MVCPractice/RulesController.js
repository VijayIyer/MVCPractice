(function () {
    'use strict';

    angular
        .module('app')
        .controller('RulesController', RulesController);

    RulesController.$inject = ['$scope']; 

    function RulesController($scope) {
        $scope.title = 'RulesController';

        activate();

        function activate() { }
    }
})();
