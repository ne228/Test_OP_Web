let elements = document.querySelectorAll(".question");

function clearClasses() {

    for (var i = 0; i < elements.length; i++) {
        const k = i;
        elements[k].classList.remove("question-active");

        if (elements[k].getAttribute("checked") == "True")
            elements[k].classList.add("question-active");
        else
            elements[k].classList.remove("question-active");

    }
}
clearClasses();


// Навешивание событый
//Novariant == true
let elementInput = document.getElementById("Text");
console.log(elementInput);
if (elementInput != null)
    elementInput.addEventListener('focusout', (event) => {
        submitQuestion();
    });


if (elementInput == null)
    for (var i = 0; i < elements.length; i++) {
        const k = i;
        elements[k].onclick = function (event) {

            if (elements[k].getAttribute("checked") == "True")
                elements[k].setAttribute("checked", "False");
            else
                elements[k].setAttribute("checked", "True");

            var answerId = elements[k].childNodes[1].getAttribute("value");

            submitQuestion(answerId);

        };
    }





function submitQuestion(answerId) {

    var data = $("#qusetionForm").serialize();

    data += `&AnswerId=${answerId}`

    //alert(data);m,,kmn
    $.ajax({
        type: 'POST',
        url: '/Session/Question',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: data,
        success: function (result) {
            if (result == "blocked") {
                showCustomAlert("<h3>Ошибка</h3><p>Вы получили ответ на вопрос.</p> <p>Вы больше не можете менять варианты ответов</p>");
                return;
            }

            $(".notsaved").removeClass("notsaved");
            $(".notsaved").addClass("saved");
            //alert('Successfully received Data');
            clearClasses();
            console.log(result);
        },
        error: function () {
            alert('Failed to receive the Data');
            console.log('Failed ');
        }
    })
}

var showAnwserBtn = document.querySelector('.show-anwser-btn');
if (showAnwserBtn != null)
    showAnwserBtn.addEventListener('click', (event) => {
        event.preventDefault();
        showAnswer();
    });

function showAnswer() {

    var obj = document.querySelector('.show-anwser-btn');
    const NumQ = obj.getAttribute('NumQ')
    const SessionId = obj.getAttribute('SessionId');

    var data = `NumQ=${NumQ}&SessionId=${SessionId}`

    //alert(data);m,,kmn
    $.ajax({
        type: 'Get',
        url: '/Session/ShowAnwser',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: data,
        success: function (result) {

            drawAnwser(result);
        },
        error: function () {
            alert('Failed to receive the Data');
            console.log('Failed ');
        }
    })
}


var flagShowAnwser = false;
function drawAnwser(anwser) {

    var showAnwserContainer = document.querySelector('.show-anwser-container');
    showAnwserContainer.innerHTML = anwser;
    showAnwserContainer.classList.toggle('show');
    var obj = document.querySelector('.show-anwser-btn');
    if (flagShowAnwser) {
        obj.innerHTML = "Показать ответ";
        flagShowAnwser = !flagShowAnwser;
    }
    else {
        obj.innerHTML = "Спрятать ответ";
        flagShowAnwser = !flagShowAnwser;
    }

}




showAnswer();


function showCustomAlert(message) {
    document.getElementById('customAlert').style.display = 'flex';
    document.querySelector('.custom-alert-text').innerHTML = message;
    document.querySelector('..custom-alert-overlay').classList.add('show');
}
function hideCustomAlert() {
    document.getElementById('customAlert').style.display = 'none';
    document.querySelector('..custom-alert-overlay').classList.remove('show');
}