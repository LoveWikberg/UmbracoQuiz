$(function () {
    var quiz = $.connection.quizHub;
    var nodeId = $('#nodeIdInput').val();

    quiz.client.playerJoin = function (lobbyHtml) {
        $('#quiz-room__container').html(lobbyHtml);
    };

    quiz.client.letPlayerAnswer = function (playerBuzzedHtml) {
        $('#quiz-room__container').html(playerBuzzedHtml);
    };

    $.connection.hub.start().done(function () {

        quiz.server.connectedToBuzzQuizRoom(nodeId);

        $('#buzzerBtn').click(function () {
            quiz.server.playerBuzz(nodeId);
        });



    });
});