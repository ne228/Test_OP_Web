var questionElements = document.getElementsByClassName("numVar");
var NumVar = document.getElementById("NumVar");
var Name = document.getElementById("NameInput");
var form = document.getElementById("form");
for (var i = 0; i < questionElements.length; i++) {
    questionElements[i].onclick = function (event) {
        // Получение элемента, по которому кликнули
        var clickedElement = event.target;
        var numVarValue = clickedElement.getAttribute("NumVar");
        var nameValue = clickedElement.getAttribute("Name");
        console.log(numVarValue + "  " + nameValue);
        NumVar.value = numVarValue;
        nameValue.value = nameValue;
        var textDiv = `Начать вариант ${numVarValue}?`
        console.log(NumVar.value + "  " + nameValue.value);
        showCustomAlert(textDiv);
        return;

        // Ваш код для обработки клика на элементе
        console.log("Кликнули на элемент:", clickedElement.textContent);
    };
}

var flag_arrow_down = true;
// Получаем ссылки на элементы SVG
var arrow_up = document.getElementById('arrow_up');
var arrow_down = document.getElementById('arrow_down');
var question_create_list = document.getElementById("question-create-list");
function changeSVG() {
    flag_arrow_down = !flag_arrow_down;
    if (flag_arrow_down) {
        // Изменяем их видимость
        arrow_up.style.display = 'inline';
        arrow_down.style.display = 'none'; // или 'block', в зависимости от ваших потребностей
        question_create_list.classList.toggle("question-create-list-show");
    }
    else {
        arrow_down.style.display = 'inline';
        arrow_up.style.display = 'none';
        question_create_list.classList.toggle("question-create-list-show");
    }
}

const heightVarBlock = 65

if (document.getElementById("variant-list-today") != null) {
    var flag_arrow_today = false;
    // Получаем ссылки на элементы SVG
    var arrow_right_today = document.getElementById('arrow_right_today');
    var arrow_left_today = document.getElementById('arrow_left_today');
    var variant_list_today = document.getElementById("variant-list-today");
}
function changeSVGToday() {
    flag_arrow_today = !flag_arrow_today;
    variant_list_today.classList.toggle("variant-btn-list-show");
    if (flag_arrow_today) {
        // Изменяем их видимость
        arrow_right_today.style.display = 'none';
        arrow_left_today.style.display = 'inline'; // или 'block', в зависимости от ваших потребностей
    }
    else {
        arrow_right_today.style.display = 'inline';
        arrow_left_today.style.display = 'none';
    }
}



// yesterday
if (document.getElementById("variant-list-yesterday") != null) {
    var flag_arrow_yesterday = false;
    // Получаем ссылки на элементы SVG
    var arrow_right_yesterday = document.getElementById('arrow_right_yesterday');
    var arrow_left_yesterday = document.getElementById('arrow_left_yesterday');
    var variant_list_yesterday = document.getElementById("variant-list-yesterday");


}
function changeSVGyesterday() {
    flag_arrow_yesterday = !flag_arrow_yesterday;
    variant_list_yesterday.classList.toggle("variant-btn-list-show");
    if (flag_arrow_yesterday) {
        // Изменяем их видимость
        arrow_right_yesterday.style.display = 'none';
        arrow_left_yesterday.style.display = 'inline'; // или 'block', в зависимости от ваших потребностей
        console.log("open");
    }
    else {
        arrow_right_yesterday.style.display = 'inline';
        arrow_left_yesterday.style.display = 'none';
        console.log("close");
    }
}



// sessions
if (document.getElementById("variant-list-sessions") != null) {
    var flag_arrow_sessions = false;
    // Получаем ссылки на элементы SVG
    var arrow_right_sessions = document.getElementById('arrow_right_sessions');
    var arrow_left_sessions = document.getElementById('arrow_left_sessions');
    var variant_list_sessions = document.getElementById("variant-list-sessions");
}
function changeSVGsessions() {
    flag_arrow_sessions = !flag_arrow_sessions;
    variant_list_sessions.classList.toggle("variant-btn-list-show");
    if (flag_arrow_sessions) {
        // Изменяем их видимость
        arrow_right_sessions.style.display = 'none';
        arrow_left_sessions.style.display = 'inline'; // или 'block', в зависимости от ваших потребностей
    }
    else {
        arrow_right_sessions.style.display = 'inline';
        arrow_left_sessions.style.display = 'none';
    }
}

function showCustomAlert(message) {
    document.getElementById('customAlert').style.display = 'flex';
    document.querySelector('.custom-alert-text').innerHTML = message;
    document.querySelector('..custom-alert-overlay').classList.add('show');
}

function okCustomAlert() {
    form.submit();
}
function hideCustomAlert() {
    document.getElementById('customAlert').style.display = 'none';
    document.querySelector('..custom-alert-overlay').classList.remove('show');
}