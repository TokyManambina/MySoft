let ListUserDocument = [];
let selectedRecipient = "";

$(document).ready(function () {
    //$('[data-toggle="tooltip"]').tooltip();
    $('#message').summernote({
        lang: 'fr-FR',
        height: 200,
        toolbar: [
        ]
    });

    mailCc = lib.EmailsInput(document.querySelector('#cc'), { pasteSplitPattern: ',', placeholder: 'CC :' });
});

$("#ccContainer").hide();
$("#ccCheck").on("change", (k, v) => {
    if ($("#ccCheck").is(":checked")) $("#ccContainer").show();
    else {
        $("#cc").val("");
        $("#ccContainer").hide();
    }
});

$(`#receiver`).on("keyup", (e) => {
    $("#receiver").css("color", "black");
})

$(`#role`).on("keyup", (e) => {
    $("#role").css("color", "black");
})

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

    return test;
}

//#region Reset Recipient
$(`[data-action="resetRecipient"]`).on("click", (e) => {
    $("#receiver").val("");
    mailCc = lib.EmailsInput(document.querySelector('#cc'), { pasteSplitPattern: ',', placeholder: 'CC :' });
    $("#role").val("");
    $("#message").summernote('code', "");
});
//#endregion

//#region Save Mail
$(`[data-action="saveRecipient"]`).on("click", (e) => {
    if (!canSend())
        return;

    let receiver = $("#receiver").val();

    if (ListUserDocument.hasOwnProperty(receiver))
        return alert("Le mail existe déjà");

    let user = {
        role: $("#role").val(),
        mail: receiver,
        cc: $("#cc").val(),
        message: $("#message").summernote("code"),
        color: RandomColor(),
        fields: []
    };

    $(`#RecipientListTab`).before(RecipientAdd(user.mail, user.color));

    ListUserDocument[receiver] = user;

    $($(`[recipient-id="${user.mail}"]`).find('input')).click();

    $('#recipient').modal('toggle');
});
//#endregion

function RecipientAdd(mail, color) {
    return `
        <li class="nav-item active" recipient-id="${mail}">
            <input type="radio" name="recipient-radio" hidden/>
            <div class="nav-link d-flex align-items-center justify-content-between" data-action="checkRadio">
                <div class="d-flex align-items-center">
                    <div class="recipientRoundBox hidden" style="background-color: ${color}"></div>
                    <span style="padding-left: 10px">${mail}</span>
                </div>
                <i class="fa fa-times text-danger float-right" onclick="return removeRecipient('${mail}')"></i>
            </div>
        </li>
    `;
}

$(document).on('click', `[data-action="checkRadio"]`, (e) => {
    let header = $(e.target).closest("[recipient-id]");
    let input = header.find("input");
    $(input).click();
});

$(document).on('change', `[name=recipient-radio]`, (e) => {
    let header = $(e.target).closest("[recipient-id]");
    selectedRecipient = header.attr("recipient-id");

    $(".recipientRoundBox").each(function (i, obj) {
        if (!$(obj).hasClass("hidden"))
            $(obj).addClass("hidden")
    });

    let box = header.find('.recipientRoundBox');
    box.removeClass("hidden")

    $(`[card-id="field"]`).show();
});

function removeRecipient(id) {
    if (!ListUserDocument.hasOwnProperty(id))
        return;

    delete ListUserDocument[id];
    $(`[recipient-id="${id}"]`).remove();

    if (ListUserDocument.length == 0)
        $(`[card-id="field"]`).hide();
}

