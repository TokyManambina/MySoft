var mailReceiver, mailCc;
let ListUserDocument = [];
$(document).ready(function () {
    //$('[data-toggle="tooltip"]').tooltip();
    $('#message').summernote({
        lang: 'fr-FR',
        height: 200,
        toolbar: [
            //['style', ['style']],
            /*
            ['font', ['bold', 'underline', 'clear']],
            ['color', ['color']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['table', ['table']],
            ['insert', ['link']],
            ['view', ['fullscreen']]
            */
        ]
    });
    //$('.dropdown-toggle').dropdown();

    mailCc = lib.EmailsInput(document.querySelector('#cc'), { pasteSplitPattern: ',', placeholder : 'CC :' });
});

$("#ccContainer").hide();
$("#ccCheck").on("change", (k, v) => {
    if ($("#ccCheck").is(":checked")) $("#ccContainer").show();
    else {
        $("#cc").val("");
        $("#ccContainer").hide();
    }
});

//#region Reset Destinataire
$("#resetMail").on("click", (e) => {
    $("#receiver").val("");
    mailCc = lib.EmailsInput(document.querySelector('#cc'), { pasteSplitPattern: ',', placeholder: 'CC :' });
    $("#role").val("");
    $("#message").summernote('code', "");
});
//#endregion

//#region Save Mail
$("#saveMail").on("click", (e) => {
    //if (!canSend())
        //return;
    alert("pl")
    let role = $("#role").val();
    let receiver = $("#receiver").val();

    let user = {
        role: $("#role").val(),
        mail: $("#receiver").val(),
        cc: $("#cc").val(),
        message: $("#message").summernote("code"),
        Field: []
    };

    $(`#showListDestinataire`).before(DestinataireAdd(user.mail));

    ListUserDocument.push(user);


    console.log(ListUserDocument);
    
    $('#destinataire').modal('hide');
});

function DestinataireAdd(mail) {
    return `
        <li class="nav-item" destinataire-id="${mail}">
            <div class="nav-link float-right">
                <span id="validator0">${mail}</span>
                <i class="fa fa-times text-danger" onclick="return removeValidator('${mail}')"></i>
            </div>
        </li>
    `;
}

function canSend() {
    let test = true;
    let role = $("#role").val();
    let receiver = $("#receiver").val();

    if (receiver == "" || !verifyMail(receiver)) {
        $("#receiver").css("color", "crimson");
        test = false;
    }
    if ($("#ccCheck").is(":checked") ? (mailCc.getInvalidValue().length) : false) {
        test = false;
    }
    if (role == "") {
        $("#role").css("color", "crimson");
        test = false;
    }

    /*
    if (test) {
        if ($(`[data-action="sendDocument"]`).hasClass("disabled"))
            $(`[data-action="sendDocument"]`).removeClass("disabled")
    } else {
        if (!$(`[data-action="sendDocument"]`).hasClass("disabled"))
            $(`[data-action="sendDocument"]`).addClass("disabled")
    }
    */
        
    return test;
}

function Insert() {

}

$(`#receiver`).on("keyup", (e) => {
    $("#receiver").css("color", "black");
})

$(`#role`).on("keyup", (e) => {
    $("#role").css("color", "black");
})

//#endregion