import { apiUrl, webUrl } from "../../apiConfig.js";


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
    if (role == "") {
        $("#role").css("color", "crimson");
        test = false;
    }

    return test;
}

//#region Reset Recipient
function resetRecipient() {
    $("#receiver").val("");
    $("#role").val("");
    $("#message").summernote('code', "");
}
$(`[data-action="resetRecipient"]`).on("click", (e) => {
    resetRecipient();
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
        message: $("#message").summernote("code"),
        color: RandomColor(),
        fields: []
    };

    $(`#RecipientListTab`).before(RecipientAdd(user.mail, user.color));

    ListUserDocument[receiver] = user;

    $($(`[recipient-id="${user.mail}"]`).find('input')).click();

    $('#recipient').modal('toggle');
    resetRecipient();
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
                <div class="btn" removeRecipient>
                    <i class="fa fa-times text-danger float-right"></i>
                </div>
            </div>
        </li>
    `;
}

$(document).on('click', `[data-action="checkRadio"]`, (e) => {
    let header = $(e.target).closest("[recipient-id]");
    let input = header.find("input");
    $(input).click();

    if (lastSelectedRecipient == "")
        return;

    //$(`[by="${lastSelectedRecipient}"]`).hide();
    //$(`[by="${selectedRecipient}"]`).show();

});

$(document).on('change', `[name=recipient-radio]`, (e) => {
    let header = $(e.target).closest("[recipient-id]");
    if (lastSelectedRecipient != selectedRecipient)
        lastSelectedRecipient = selectedRecipient;
    selectedRecipient = header.attr("recipient-id");

    $(".recipientRoundBox").each(function (i, obj) {
        if (!$(obj).hasClass("hidden"))
            $(obj).addClass("hidden")
    });

    let box = header.find('.recipientRoundBox');
    box.removeClass("hidden")

    $(`[card-id="field"]`).show();
});


$(document).on('click', '[removeRecipient]', (e) => {
    let recipient = $(e.target).closest('[recipient-id]');
    let id = recipient.attr('recipient-id');

    if (!ListUserDocument.hasOwnProperty(id))
        return;

    delete ListUserDocument[id];

    recipient.remove();

    if (Object.keys(ListUserDocument).length === 0)
        $(`[card-id="field"]`).hide();

    $(`[by="${id}"]`).remove();
});

