app.controller('TitleController', function ($scope, TitleService) {

    getTitles();

    function getTitles() {
        TitleService.getTitles()
            .success(function (titles) {
                $scope.titles = titles;

            })
            .error(function (error) {
                $scope.status = 'Unable to load title data: ' + error.message;

            });
    }
});