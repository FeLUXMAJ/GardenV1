
(function (Window) {

    var app = angular.module('myApp', []);

    app.controller('myCtrl', function ($scope, $http) {

        $scope.firstName = "John";
        $scope.lastName = "Doe";

        $http({
            method: 'GET',
            url: 'api/RedisTest/StringGet',
            params: { 'key': 'name' }
        }).then(function (response) {
            console.log(response);
            $scope.lastName = response.data;
        }, function (response) {

        });

        $scope.WriteReadData = function () {
            //写数据
            $http({
                method: 'GET',
                url: 'api/MySQLTest/WriteData',
                params: {
                    nBlogId: $scope.WriteMySQLDataForBlogID,
                    sName: $scope.WriteMySQLDataForName
                }
            }).then(function (response) {
                console.log(response);
                //读数据
                $http({
                    method: 'GET',
                    url: 'api/MySQLTest/ReadData',
                    params: { nBlogID: $scope.WriteMySQLDataForBlogID }
                }).then(function (response) {
                    console.log(response);
                    $scope.ReadMySQLData = response.data;
                }, function (response) {
                    console.log(response);
                });
            }, function (response) {
                console.log(response);
            });
        };

        $scope.SendKafkaMsg = function () {
            $http({
                method: 'GET',
                url: 'api/KafkaTest/SendMessage',
                params: { sTopic: 'test', sMsg: $scope.sKafkaMsg }
            }).then(function (response) {
                console.log(response);
                //$scope.lastName = response.data;
            }, function (response) {
                console.log(response);
            });
        };

    });

    $(function () {
        // the generated client-side hub proxy
        var ticker = $.connection.KafkaTestBySignalR;

        // Add client-side hub methods that the server will call
        $.extend(ticker.client, {
            receiveKafkaMessage: function (sMsg) {
                $("#MessageList").append('<p>' + sMsg + '</p>');
            },
            messageMonitorState: function (state) {
            }
        });

        // Start the connection
        $.connection.hub.start().done(function () {
            // Wire up the buttons
            ticker.server.startMonitorKafkaMessage('test');
        });

    });

}(Window));
