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
    console.log(header);
    let firstPage = parseInt(header.find(`[data-id="firstPage"]`).val());
    let lastPage = parseInt(header.find(`[data-id="lastPage"]`).val());

    if (Number.isNaN(firstPage))
        return alert("Vérifier le numero de page");

    let listContainer = $(e.target).attr("data-target");


    let page = `${firstPage}`;
    
    if (!Number.isNaN(lastPage) && lastPage !== 0) {
        if (firstPage > lastPage)
            page = `${lastPage} - ${firstPage}`;
        else
            page += ` - ${lastPage}`;
    }
        

    let type = `${header.attr("data-type")}`;
    let id = `${type}${count++}`;

    let title = `${header.find("[data-title]").text()}`;

    $(`#${listContainer}`).before(listField(type, page, id));

    $(`#FieldsListBox`).append(field(type, page, id, title));

    let newfield = {
        x: 0,
        y: 0,
        width: 0,
        height: 0,
        type: parseInt($(header).attr("data-id")),
        page: page
    };

    ListUserDocument[selectedRecipient].fields[id] = newfield;

    activeField(id, selectedRecipient);
});

function activeField(id, recipient) {
    $(`[field-id="${id}"]`).hover((e, x) => {
        if ($(`[recipient-id="${recipient}"]`).css("background-color") !== ListUserDocument[recipient].color)
            $(`[recipient-id="${recipient}"]`).css("background-color", ListUserDocument[recipient].color);
        if ($(`[page-id="${id}"]`).css("background-color") !== ListUserDocument[recipient].color)
            $(`[page-id="${id}"]`).css("background-color", ListUserDocument[recipient].color);

        $($(`[recipient-id="${recipient}"]`).find('span')).css("color", "white");
        $(`[page-id="${id}"]`).find('span').css("color", "white");
    }, (e) => {
        if ($(`[recipient-id="${recipient}"]`).css("background-color"))
            $(`[recipient-id="${recipient}"]`).css("background-color","");
        if ($(`[page-id="${id}"]`).css("background-color"))
            $(`[page-id="${id}"]`).css("background-color", "");

        $($(`[recipient-id="${recipient}"]`).find('span')).css("color", "");
        $(`[page-id="${id}"]`).find('span').css("color", "");
    });

    $(`[field-id="${id}"]`).mousemove((e) => {
        divPos = {
            left: e.pageX - $(e.target).offset().left,
            top: e.pageY - $(e.target).offset().top,
        };
        if (divPos.left > $(this).width() - 11 && divPos.top > $(this).height() - 11) dragOption.disabled = true;
        else dragOption.disabled = false;

        let pdfContainer = $('#pdfViewer').offset();
        var offsetSign = $(e.target).offset();
        let posSign = {
            top: offsetSign.top - pdfContainer.top,
            left: offsetSign.left - pdfContainer.left
        };
        ListUserDocument[recipient].fields[id].x = parseFloat(posSign.left);
        ListUserDocument[recipient].fields[id].y = parseFloat(posSign.top);
        ListUserDocument[recipient].fields[id].width = parseFloat($(e.target).width());
        ListUserDocument[recipient].fields[id].height = parseFloat($(e.target).height());

        $(`[field-id="${id}"]`).draggable(dragOption);
    });
    $(`[field-id="${id}"]`).hover();
    $(`[field-id="${id}"]`).mousemove();

    var canvasOffset = $('#pdfViewer').offset();
    $(`[field-id="${id}"]`).css({ "top": (canvasOffset.top + 125) + "px", "left": (canvasOffset.left + 25) + "px" });
}
function listField(type, page, id) {
    return `
        <li class="nav-item" page-id="${id}" by="${selectedRecipient}" data-type="${type}" data-value="${page}">
            <div class="nav-link float-right">
                <span id="${id}">Page : ${page}</span>
                <i class="fa fa-times text-danger" onclick="return removeField('${id}')"></i>
            </div>
        </li>
    `;
}
function field(type, page, id, title) {
    return `
        <div class="boxSign" data-type="${type}" by="${selectedRecipient}" field-id="${id}" data-page="${page}" style="border: 5px dashed ${ListUserDocument[selectedRecipient].color}">
	        <div class="ribbon-wrapper">
		        <div class="ribbon text-white" style="background-color:${ListUserDocument[selectedRecipient].color}">
			        ${title}
		        </div>
	        </div>
        </div>
    `;
}

function removeField(id) {
    let field = $(`[field-id="${id}"]`);
    let recipient = field.attr("by");
    let pageLine = $(`[page-id="${id}"]`);

    field.remove();
    pageLine.remove();

    delete ListUserDocument[recipient].fields[id];

}