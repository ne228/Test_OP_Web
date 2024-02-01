

function setDivSize() {
    var bodyHeight = document.body.clientHeight;
    var back = document.querySelector(".background");
    var back_left = document.querySelector(".background-left");
    var back_right = document.querySelector(".background-right");
    bodyHeight = 0;
    bodyHeight = back.clientHeight;
    bodyHeight += 15;
    back_right.style.height = bodyHeight + "px";
    back_left.style.height = bodyHeight + "px";

}

// Вызываем функцию при загрузке страницы и при изменении размеров окна
window.addEventListener('load', setDivSize);
window.addEventListener('resize', setDivSize);



//function setBackgroundHeight() {

//    var back_left = document.querySelector(".background-left");
//    var back_right = document.querySelector(".background-right");

//    var documentHeight = document.body.clientHeight;
//    back_right.style.height = back_left.style.height - documentHeight + "px";
//    back_left.style.height = back_left.style.height - documentHeight + "px";

//    console.log(documentHeight + "px");
//}


//setBackgroundHeight()
setInterval(function () { setDivSize(); }, 10);


// Если на главной то ставим фон красный
const pathWithoutLeadingSlash = window.location.pathname.substring(1);

if (pathWithoutLeadingSlash == "") {
    document.querySelector("body").classList.add("red-background");
    document.querySelector("#footer").classList.add("text-white");
    var footer = document.querySelector("footer");
    footer.style.display = "none";
}


setInterval(function () {
    //const pathWithoutLeadingSlash = window.location.pathname.substring(1);

    //console.log("Current URL:", currentUrl);

}, 1000);