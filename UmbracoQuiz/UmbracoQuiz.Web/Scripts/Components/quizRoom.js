$(function () {
    var quiz = $.connection.quizHub;
    var nodeId = $('#nodeIdInput').val();

    quiz.client.playerJoin = function (html) {
        console.log(html);
        $('#quiz-room__container').html(html);
    };

    quiz.client.letPlayerAnswer = function (html) {
        $('#quiz-room__container').html(html);
    };

    quiz.client.determineIfAnswerIsCorrect = function (html) {
        $('#quiz-room__container').html(html);
    };

    quiz.client.showAnswerResult = function (html) {
        $('#quiz-room__container').html(html);
    };

    quiz.client.showBuzzerQuestion = function (html) {
        $('#quiz-room__container').html(html);
    };

    $.connection.hub.start().done(function () {

        quiz.server.connectedToBuzzQuizRoom(nodeId);

        $(document).on('click', '#buzzerBtn', function () {
            quiz.server.playerBuzz(nodeId);
        });

        $(document).on('click', '#correctAnswerBtn', function () {
            $('#determine-answer-wrapper').addClass('hide-element');
            $('#award-points-wrapper').removeClass('hide-element');
        });

        $(document).on('click', '#inCorrectAnswerBtn', function () {
            quiz.server.showBuzzerQuestion(nodeId);
        });

        $(document).on('click', '#awardOnePointBtn', function () {
            var pointsGiven = 1;
            quiz.server.correctAnswer(nodeId, pointsGiven);

        });

        $(document).on('click', '#awardChosenPointsBtn', function () {
            var pointsGiven = $('#chosenPoints').children("option:selected").val();
            quiz.server.correctAnswer(nodeId, pointsGiven);
        });

    });
});