class TabController {
    constructor() {

    }


    tabs = document.querySelector('.tabs');
    nextTab = document.querySelector('.tabs');
    nextTab = document.querySelector('.tabs');


    goNextTab() {



    }



    getQuestion() {

        var data = $("#qusetionForm").serialize();

        data += `&AnswerId=${answerId}`

     
        $.ajax({
            type: 'GET',
            url: '/Session/Question',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            data: data,
            success: function (result) {
                return result;
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        })
    }

}