import { apiUrl, webUrl } from "../apiConfig.js";
import * as Recipient from './NewDocument.recipient.js?v=0.1.0';

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

$("[showAll]").on('click', (e) => {
    let icon = $(e.target).find("i");

    icon.removeClass("fa-square");
    icon.removeClass("fa-square-check");

    if (icon.hasClass("fa-square")) icon.addClass("fa-square-check");
    else icon.addClass("fa-square");
        
})

$(`[data-action="addField"]`).on("click", (e) => {
    if (!existDocument())
        return alert("Veuillez uploader d'abord un document");

    let header = $(e.target).closest("[data-type]");
    console.log(header);
    let firstPage = parseInt(header.find(`[firstPage]`).val());
    let lastPage = parseInt(header.find(`[lastPage]`).val());

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
            $(`[recipient-id="${recipient}"]`).css("background-color", "");
        if ($(`[page-id="${id}"]`).css("background-color"))
            $(`[page-id="${id}"]`).css("background-color", "");

        $($(`[recipient-id="${recipient}"]`).find('span')).css("color", "");
        $(`[page-id="${id}"]`).find('span').css("color", "");
    });

    $(`[field-id="${id}"]`).mousemove((e) => {
        let divPos = {
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


        getPageSize(1)
            .then((pageSize) => {
                ListUserDocument[recipient].fields[id].pdfWidth = pageSize.width;
                ListUserDocument[recipient].fields[id].pdfHeight = pageSize.height;
            })
            .catch((error) => {
                console.error("Error:", error);
            });

       

        console.log(ListUserDocument)
        $(`[field-id="${id}"]`).draggable(dragOption);
    });
    $(`[field-id="${id}"]`).hover();
    $(`[field-id="${id}"]`).mousemove();

    $(`[field-id="${id}"]`).css({ "top": (posY + 25) + "px", "left": (posX + 25) + "px" });
}
function listField(type, page, id) {
    let fpage = page.split("-")[0].trim();
    let lpage = page.split("-")[1].trim();
    return `
        <li class="nav-item" page-id="${id}" by="${selectedRecipient}" data-type="${type}" data-value="${page}" field-firstPage="${fpage}" field-lastPage="${lpage}">
            <div class="nav-link float-right">
                <span id="${id}">Page : ${page}</span>
                <i class="fa fa-times text-danger" removeField></i>
            </div>
        </li>
    `;
}
function field(type, page, id, title) {
    let fpage = page.split("-")[0].trim();
    let lpage = page.split("-")[1].trim();
    return `
        <div class="boxSign" data-type="${type}" by="${selectedRecipient}" field-id="${id}" data-page="${page}" field-firstPage="${fpage}" field-lastPage="${lpage}" style="border: 5px dashed ${ListUserDocument[selectedRecipient].color}">
	        <div class="ribbon-wrapper">
		        <div class="ribbon text-white" style="background-color:${ListUserDocument[selectedRecipient].color}">
			        ${title}
		        </div>
	        </div>
        </div>
    `;
}

$(document).on('click', '[removeField]', (e) => {
    console.log(e);
    let id = $(e.target).closest("[page-id]").attr('page-id');
    console.log($(e.target).closest("[page-id]"))
    removeField(id);
});

function removeField(id) {
    let field = $(`[field-id="${id}"]`);
    let recipient = field.attr("by");
    let pageLine = $(`[page-id="${id}"]`);

    delete ListUserDocument[recipient].fields[id];

    field.remove();
    pageLine.remove();

}

$(document).on('refreshField', (e) => {
    console.log(e)
    let page = currentPDF.currentPage;
    let fpage;
    let lpage;

    $('[by]').show();

    $(`[by]`).each((k, v) => {
        fpage = Number.parseInt($(v).attr('field-firstPage'));
        lpage = Number.parseInt($(v).attr('field-lastPage'));
        
        if (fpage > page || lpage < page)
            $(v).hide();
    });
});