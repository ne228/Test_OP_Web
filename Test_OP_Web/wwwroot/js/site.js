



let showFullNavBtn = document.querySelector(".nav-show-btn");

let navContainer = document.querySelector(".nav-container");

var open = false;
showFullNavBtn.onclick = function () {


    navContainer.classList.toggle("nav-container-active");
    navContainer.classList.toggle("change");

    //if (open == false) {
    //    navContainer.classList.add("nav-container-active");

    //    open = true;
    //}
    //else {
    //    navContainer.classList.remove("nav-container-active");
    //    open = false;
    //}


};


//function burgerMenu(icon) {
//    icon.classList.toggle("change");
//}