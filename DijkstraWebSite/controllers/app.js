angular.module('DijkstraWeb', ['cgNotify', 'HttpService'])
.controller('DijkstraWebController', function($scope, $http, $document, notify, $httpFunctions) {
    $scope.questao1 = "1";
    $scope.questao2 = "";
    $scope.questao3 = "";
    $scope.questao4 = "";
    $scope.questao5 = "";
    $scope.questao6 = "Não desenvolvida!";
    $scope.questao7 = "Não desenvolvida!";
    $scope.questao8 = "";
    $scope.questao9 = "Não desenvolvida!";
    $scope.questao10 = "Não desenvolvida!";

    $scope.questoes = function (rota, callback) {
        var url = "http://localhost:55783/api/dijkstra/";

        $httpFunctions.get(url + "questions/" + rota, null,
            function (response) {
                callback(response.data);
            }, function (error) {
                if(error.data.ExceptionType === 'DijkstraEngine.Exception.PathNotFoundException'){
                    //não encontrou o caminho
                    callback(error.data.ExceptionMessage);
                } else{
                    notify({
                        message: 'Erro ao acessar a API!',
                        duration: 3000,
                        classes: ['danger-notify'],
                        position: 'center'
                    });
                }
            }
        );
    }

    $scope.dijkstra = function (obj, callback) {
        var url = "http://localhost:55783/api/dijkstra/";

        $httpFunctions.post(url + "execute/", obj,
            function (response) {
                callback(response.data);
            }, function (error) {
                if(error.data.ExceptionType === 'DijkstraEngine.Exception.PathNotFoundException'){
                    //não encontrou o caminho
                    callback(error.data.ExceptionMessage);
                } else{
                    notify({
                        message: 'Erro ao acessar a API!',
                        duration: 3000,
                        classes: ['danger-notify'],
                        position: 'center'
                    });
                }
            }
        );
    }

    $document.ready(function(){
        //questão 1
        $scope.questoes("0_1_2", function(result){
            $scope.questao1 = result;
        });
        //questão 2
        $scope.questao2 = $scope.questoes("0_3", function(result){
            $scope.questao2 = result;
        });
        //questão 3
        $scope.questao3 = $scope.questoes("0_3_2", function(result){
            $scope.questao3 = result;
        });
        //questão 4
        $scope.questao4 = $scope.questoes("0_4_1_2_3", function(result){
            $scope.questao4 = result;
        });
        //questão 5
        $scope.questao5 = $scope.questoes("0_4_3", function(result){
            $scope.questao5 = result;
        });
        //questão 8
        //origem 0 (A) e destino 2 (C)
        $scope.questao8 = $scope.dijkstra(
        {
            "origem" : 0,
            "destino" : 2
        }, function(result){
            console.log(result);
            $scope.questao8 = {
                "caminho" : trataResultadoQuestao8(result.caminho),
                "distancia" : result.distancia
            };
        });
    });
});

function trataResultadoQuestao8(result){
    var caminho = "";

    for(var item in result){
        caminho += "-" + buscaLetraCidade(item.replace("Nodo_", ""));
    }

    return caminho.substring(1);
}

function buscaLetraCidade(nodo){
    console.log(nodo)
    switch(parseInt(nodo)){
        case 0 : return "A";
        case 1 : return "B";
        case 2 : return "C";
        case 3 : return "D";
        default : return "E";
    }
}