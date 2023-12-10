let ListUserDocument = [];
let selectedRecipient = "";
let lastSelectedRecipient = "";

let canvasOffset = $('#pdfViewer').offset();
let posX = canvasOffset.left;
let posY = canvasOffset.top;

//field
let showAll = true;

function toggleSidebar() {
	document.querySelector('.sidebar')
		.classList.toggle('closed');
}

function RemoveAllField() {
    if (Object.keys(ListUserDocument).length > 0) {
        $("[page-id]").remove();
        $("[field-id]").remove();
        $("[recipient-id]").remove();

        ListUserDocument = [];
        selectedRecipient = "";
    }
}
function fullHide() {
    RemoveAllField();
    $(".BoxSettingInfo").hide();
    $(`[card-id="dynamic-required-field"]`).hide();
    $(`[card-id="recipient"]`).hide();
    $(`[card-id="field"]`).hide();
    $(`[card-id="type"]`).hide();
    $("#ISign").hide();
    $("#YouSign").hide();
}

$(`#toggleBoxSetting`).on('click', (e) => {
	$("#BoxSettingMenu").removeClass('closed');
})
$(`#closeSideMenu`).on('click', (e) => {
	$("#BoxSettingMenu").addClass('closed');
})

$(`#pdfImg`).on('click', (e) => {
	if ($(`#inputFile`).val() == "")
		$('#inputFile').click();
})

$(`[data-action="openPDF"]`).on('click', (e) => {
	$('#inputFile').click();
})


$(document).ready(function () {
    //$('[data-toggle="tooltip"]').tooltip();
    $('#message').summernote({
        lang: 'fr-FR',
        height: 200,
        toolbar: [
        ]
    });
    $('#mailMessage').summernote({
        lang: 'fr-FR',
        height: 300,
        toolbar: [
            //['style', ['bold', 'italic', 'underline', 'clear']],
            //['font', ['strikethrough', 'superscript', 'subscript']],
            ////['fontsize', ['fontsize']],
            //['color', ['color']],
            //['para', ['ul', 'ol']],//, 'paragraph']],
            ////['height', ['height']]
        ]
    });
});

function resetMail() {
    $("#objectId").val("");
    $("#mailMessage").summernote('code', "");
}
$(`[data-action="resetMail"]`).on("click", (e) => {
    resetMail();
});