var dragOption = {
    containment: '#pdfViewer',
    cursor: 'move',
    disabled: false,
    handle: '.ribbon'
}

let count = 0;

function existDocument() {
    return $("#inputFile").val() != "";
}

$(`[data-action="addField"]`).on("click", (e) => {
    if (!existDocument())
        return alert("Veuillez uploader d'abord un document");

    let header = $(e.target).closest("[data-type]");
    let firstPage = parseInt(header.find(`[data-id="firstPage"]`).val());
    let lastPage = parseInt(header.find(`[data-id="lastPage"]`).val());

    if (firstPage == NaN || lastPage == NaN)
        return alert("Vérifier le numero de page");

    let listContainer = $(e.target).attr("data-target");


    let page = firstPage.toString();
    if (lastPage != NaN || lastPage != 0)
        page += ` - ${lastPage}`;

    let type = `${header.attr("data-type")}`;
    let id = `${type}${count++}`;

    let title = `${header.find("[data-title]").text()}`;

    $(`#${listContainer}`).before(listField(type, page, id));

    $(`#signatureListBox`).append(field(type, page, id, title));

    $(`[field-id="${id}"]`).hover((e, x) => {
        $(`[page-id="${id}"]`).addClass("bg-primary");
        if (!$(`[page-id="${id}"]`).hasClass("bg-primary")) {
            $(`[page-id="${id}"]`).addClass("bg-primary");
            //$(`#signature${countSignature} .span`).css("color", "white");
        }
    }, (e) => {
        if ($(`[page-id="${id}"]`).hasClass("bg-primary"))
            $(`[page-id="${id}"]`).removeClass("bg-primary");
    });

    $(`[field-id="${id}"]`).mousemove((e) => {
        divPos = {
            left: e.pageX - $(e.target).offset().left,
            top: e.pageY - $(e.target).offset().top,
        };
        if (divPos.left > $(this).width() - 11 && divPos.top > $(this).height() - 11) dragOption.disabled = true;
        else dragOption.disabled = false;

        $(`[field-id="${id}"]`).draggable(dragOption);
        //$("#signatureBox${countSignature}").draggableTouch(dragOption);
    });
    $(`[field-id="${id}"]`).hover();
    $(`[field-id="${id}"]`).mousemove();

    var canvasOffset = $('#pdfViewer').offset();
    $(`[field-id="${id}"]`).css({ "top": (canvasOffset.top + 125) + "px", "left": (canvasOffset.left + 25) + "px" });

});
function listField(type, page, id) {
    return `
        <li class="nav-item parapheList" page-id="${id}" data-type="${type}" data-value="${page}">
            <div class="nav-link float-right">
                <span id="${id}">Page : ${page}</span>
                <i class="fa fa-times text-danger" onclick="return removeField('${id}')"></i>
            </div>
        </li>
    `;
}
function field(type, page, id, title) {
    return `
        <div class="boxSign" data-type="${type}" field-id="${id}" data-page="${page}" style="border: 5px dashed ${ListUserDocument[selectedRecipient].color}">
	        <div class="ribbon-wrapper">
		        <div class="ribbon text-white" style="background-color:${ListUserDocument[selectedRecipient].color}">
			        ${title}
		        </div>
	        </div>
        </div>
    `;
}