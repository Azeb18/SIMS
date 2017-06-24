(function () {
    var app = angular.module('SMIS', []);
    app.controller('RegisterationController', function ($scope) {
        $scope.tab_index = 1;
        $scope.checkTabIndex = function (i) {
            return $scope.tab_index == i;
        };
        $scope.setTabIndex = function (i) {
            $scope.tab_index = i;
        };
    });
    app.controller('ResultController', function ($scope) {
        $scope.results = r;
    });
    app.controller('DashboardController', function ($scope) {
        $scope.new_results = r[1].courses;
    });




    var r = [
    {
        year: 'I',
        semester: 'I',
        academic_year: '2016/2017',
        courses: [
            { name: 'Fundamentals of Theology', code: 'HTTC:1002', ch: 4.00, ECTS: 7.00, grade: 'A+' },
            { name: 'Negere Mariam', code: 'HTTC:1002', ch: 3.00, ECTS: 6.00, grade: 'B' },
            { name: 'Ge\'ez', code: 'HTTC:1002', ch: 4.00, ECTS: 7.00, grade: 'A-' },
            { name: 'Negere Mariam', code: 'HTTC:1002', ch: 4.00, ECTS: 7.00, grade: 'NG' },
            { name: 'Ge\'ez', code: 'HTTC:1002', ch: 3.00, ECTS: 6.00, grade: 'C+' },
        ],
    },
    {
        year: 'I',
        semester: 'II',
        academic_year: '2016/2017',
        courses: [
            { name: 'Fundamentals of Theology', code: 'HTTC:1002', ch: 4.00, ECTS: 7.00, grade: 'A+' },
            { name: 'Negere Mariam', code: 'HTTC:1002', ch: 3.00, ECTS: 6.00, grade: 'F' },
            { name: 'Ge\'ez', code: 'HTTC:1002', ch: 4.00, ECTS: 7.00, grade: 'A-' },
            { name: 'Negere Mariam', code: 'HTTC:1002', ch: 4.00, ECTS: 7.00, grade: 'A' },
            { name: 'Ge\'ez', code: 'HTTC:1002', ch: 3.00, ECTS: 6.00, grade: 'C+' },
        ],
    }
    ];
})();