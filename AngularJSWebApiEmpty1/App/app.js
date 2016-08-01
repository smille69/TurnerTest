(function () {
    'use strict';

    var app = angular.module('app', [
        // Angular modules
        'ngAnimate',
        'ngRoute',
        'ui.bootstrap'
        // Custom modules

        // 3rd Party Modules
        
    ]);

    app.factory('TitleService', ['$http', function ($http) {

        var urlBase = 'http://localhost:63869/api';
        var TitleService = {};

        TitleService.getTitles = function () {
            return $http.get(urlBase + '/Titles');
        };

        TitleService.getTitle = function(id) {
            return $http.get(urlBase + '/Titles/' + id);
        };

        TitleService.searchTitles = function(searchstr) {
            return $http.get(urlBase + '/Titles/Search/' + searchstr);
        };

        return TitleService;
    }]);

    app.controller('TitleController', function ($scope, TitleService, $uibModal) {

        getTitles();
        //searchTitles();

        $scope.searchstr = '';
        $scope.items = ['item1', 'item2', 'item3'];

        $scope.$watch('searchstr',
            function () {
                if ($scope.searchstr !== null && $scope.searchstr.length > 0)
                    searchTitles();
                else {
                    getTitles();
                }
            });

        function getTitles() {
            TitleService.getTitles()
                .success(function (titles) {
                    $scope.titles = titles;

                })
                .error(function (error) {
                    $scope.status = 'Unable to load title data: ' + error.message;

                });
        }

        function getTitle(id) {
            TitleService.getTitle(id)
                .success(function(title) {
                    $scope.title = title;
                })
                .error(function(error) {
                    $scope.status = 'Unable to load title data: ' + error.message;
                });
        }

        function searchTitles() {
            
            TitleService.searchTitles($scope.searchstr)
                .success(function(titles) {
                    $scope.titles = titles;
                })
                .error(function(error) {
                    $scope.status = 'Unable to load title data: ' + error.message;
                });
        }

        $scope.open = function (titleId) {
            
            TitleService.getTitle(titleId)
                .success(function(data) {
                    $scope.title = data;
                })
                .error(function(error) {
                    $scope.status = 'Unable to load title data: ' + error.message;
                });

            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: 'myModalContent.html',
                controller: 'ModalInstanceCtrl',
                scope: $scope,
                title: $scope.title
        });

            modalInstance.result.then(function(selectedItem) {
                    $scope.selected = selectedItem;
                });
        };
    });

    app.controller('ModalInstanceCtrl', 
        function ($scope, $uibModalInstance) {
            $scope.title = $scope.$parent.title;
            
            $scope.ok = function () {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };
        });
})();
