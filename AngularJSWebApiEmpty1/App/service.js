app.factory('TitleService', ['$http', function ($http) {

    var urlBase = 'http://localhost:63869/api';
    var TitleService = {};
    TitleService.getTitles = function () {
        return $http.get(urlBase + '/Titles');
    };

    return TitleService;
}]);