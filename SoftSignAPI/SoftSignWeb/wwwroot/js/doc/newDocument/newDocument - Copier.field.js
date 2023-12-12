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

    let firstPage = parseInt(header.find(`[firstPage]`).val());
    let lastPage = parseInt(header.find(`[lastPage]`).val());

    if (Number.isNaN(firstPage))
        return alert("Vérifier le numero de page");

    let listContainer = $(e.target).attr("data-target");

    if (!Number.isNaN(lastPage) && lastPage !== 0) {
        if (firstPage > lastPage) {
            let tempPage = firstPage;
            firstPage = lastPage;
            lastPage = tempPage;
        }
    } else {
        lastPage = firstPage;
    }


    let type = `${header.attr("data-type")}`;
    let id = `${type}${count++}`;

    let title = `${header.find("[data-title]").text()}`;

    $(`#${listContainer}`).before(listField(type, firstPage, lastPage, id));

    $(`#FieldsListBox`).append(field(type, firstPage, lastPage, id, title));

    let newfield = {
        x: 0,
        y: 0,
        width: 0,
        height: 0,
        type: parseInt($(header).attr("data-id")),
        firstPage: firstPage,
        lastPage: lastPage,
        variable: id,
        PDF_Width: 0,
        PDF_Height: 0
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
        let header = $(e.target).closest("[field-id]");

        let divPos = {
            left: e.pageX - header.offset().left,
            top: e.pageY - header.offset().top,
        };
        if (divPos.left > $(this).width() - 11 && divPos.top > $(this).height() - 11) dragOption.disabled = true;
        else dragOption.disabled = false;

        let pdfContainer = $('#pdfViewer').offset();
        var offsetSign = header.offset();
        let posSign = {
            top: offsetSign.top - pdfContainer.top,
            left: offsetSign.left - pdfContainer.left
        };
        ListUserDocument[recipient].fields[id].x = parseFloat(posSign.left);
        ListUserDocument[recipient].fields[id].y = parseFloat(posSign.top);
        ListUserDocument[recipient].fields[id].width = parseFloat(header.width());
        ListUserDocument[recipient].fields[id].height = parseFloat(header.height());

        getPageSize(ListUserDocument[recipient].fields[id].firstPage)
            .then((pageSize) => {
                ListUserDocument[recipient].fields[id].PDF_Width = pageSize.width;
                ListUserDocument[recipient].fields[id].PDF_Height = pageSize.height;
            })
            .catch((error) => {
                console.error("Error:", error);
            });
            
        $(`[field-id="${id}"]`).draggable(dragOption);
    });
    $(`[field-id="${id}"]`).hover();
    //$(`[field-id="${id}"]`).mousemove();

    $(`[field-id="${id}"]`).css({ "top": (posY + 25) + "px", "left": (posX + 25) + "px" });
}
function listField(type, firstPage, lastPage, id) {
    return `
        <li class="nav-item" page-id="${id}" by="${selectedRecipient}" data-type="${type}" data-value="${firstPage == lastPage ? `${firstPage}` : `${firstPage} - ${lastPage}`}" field-firstPage="${firstPage}" field-lastPage="${lastPage}">
            <div class="nav-link float-right">
                <span id="${id}">Page : ${firstPage == lastPage ? `${firstPage}` : `${firstPage} à ${lastPage}`} </span>
                <i class="fa fa-times text-danger" removeField></i>
            </div>
        </li>
    `;
}
function field(type, firstPage, lastPage, id, title) {
    return `
        <div class="boxSign" data-type="${type}" by="${selectedRecipient}" field-id="${id}" data-page="${firstPage == lastPage ? `${firstPage}` : `${firstPage} - ${lastPage}`}" field-firstPage="${firstPage}" field-lastPage="${lastPage}" style="border: 5px dashed ${ListUserDocument[selectedRecipient].color}">
	        <div class="ribbon-wrapper">
		        <div class="ribbon text-white" style="background-color:${ListUserDocument[selectedRecipient].color}">
			        ${title}
		        </div>
	        </div>
        </div>
    `;
}

$(document).on('click', '[removeField]', (e) => {
    let id = $(e.target).closest("[page-id]").attr('page-id');
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

$("[firstPage], [LastPage]").on('change', (e) => {
    var max = parseInt($(e.target).attr("max"));
    var val = parseInt($(e.target).val());
    var min = parseInt($(e.target).attr("min"));

    let firstPage = $(e.target).closest("[data-type]").find("[firstPage]");
    let lastPage = $(e.target).closest("[data-type]").find("[LastPage]");

    if (firstPage.val() > lastPage.val()) {
        let attr = $(e.target).attr("firstPage");
        if (typeof attr !== 'undefined' && attr !== false) lastPage.val(firstPage.val());
        else firstPage.val(lastPage.val());
    }
    
    if (val > max) $(e.target).val(max);
    else if (val <= 0) $(e.target).val(min);
});