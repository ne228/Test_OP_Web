let btns = document.getElementsByClassName("question-edit-submit");

for (var i = 0; i < btns.length; i++) {
    btns[i].addEventListener("click", function (event) {
        event.preventDefault();

        let btn = event.target;
        var parentBtn = btn.parentElement;

        var form = parentBtn.parentElement;

        var formId = form.id;

        var data = $(`#${formId}`).serialize();

        editQuestionString(data);
        console.log(data);
    });

}


function editQuestionString(data) {

    $.ajax({
        type: 'POST',
        url: '/Option/EditQuestion',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: data,
        success: function (result) {

            console.log(result);
        },
        error: function () {
            alert('Failed to receive the Data');
            console.log('Failed ');
        }
    })
}