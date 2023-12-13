import { apiUrl, webUrl } from "../../apiConfig.js";

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
    console.log("ok");
    let header = $(e.target).closest("[showAll]");
    let icon = header.find("i");

    if (icon.hasClass("fa-square")) {
        showAll = true;
        icon.removeClass("fa-square");
        icon.addClass("fa-square-check");
    }
    else {
        showAll = false;
        icon.removeClass("fa-square-check");
        icon.addClass("fa-square");
    }
    $(document).trigger("refreshField");
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
        fieldType: parseInt($(header).attr("data-id")),
        firstPage: firstPage,
        lastPage: lastPage,
        variable: id,
        PDF_Width: 0,
        PDF_Height: 0
    };

    ListUserDocument[selectedRecipient].fields[id] = newfield;

    activeField(id, selectedRecipient);
    $(document).trigger("refreshField");
    $(`[field-id]`).mousemove();
});

function activeField(id, recipient) {

    $(`[field-id="${id}"],[page-id="${id}"],[recipient-id="${recipient}"]`).hover((e, x) => {
        console.log("aaa");
        if ($(`[recipient-id="${recipient}"]`).css("background-color") !== ListUserDocument[recipient].color)
            $(`[recipient-id="${recipient}"]`).css("background-color", ListUserDocument[recipient].color);
        if ($(`[page-id="${id}"]`).css("background-color") !== ListUserDocument[recipient].color)
            $(`[page-id="${id}"]`).css("background-color", ListUserDocument[recipient].color);
        if ($(`[field-id="${id}"]`).css("background-color") !== ListUserDocument[recipient].color + "60")
            $(`[field-id="${id}"]`).css("background-color", ListUserDocument[recipient].color + "60");

        $($(`[recipient-id="${recipient}"]`).find('span')).css("color", "white");
        $(`[page-id="${id}"]`).find('span').css("color", "white");
    }, (e) => {
        if ($(`[recipient-id="${recipient}"]`).css("background-color"))
            $(`[recipient-id="${recipient}"]`).css("background-color", "");
        if ($(`[page-id="${id}"]`).css("background-color"))
            $(`[page-id="${id}"]`).css("background-color", "");
            
        $(`[field-id="${id}"]`).css("background-color", "rgba(68, 65, 65, 0.59)");

        $($(`[recipient-id="${recipient}"]`).find('span')).css("color", "");
        $(`[page-id="${id}"]`).find('span').css("color", "");
    });

    $(`[field-id="${id}"]`).mousemove((e) => {
        console.log('er');
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
                <div class="btn btn-sm" removeField>
                    <i class="fa fa-times text-danger" style="font-size:1.1rem"></i>
                </div>
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

    $('[by][field-id]').show();

    if (showAll) return;

    $(`[by][field-id]`).each((k, v) => {
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

    if (Number.parseInt(firstPage.val()) >= Number.parseInt(lastPage.val())) {
        let attr = $(e.target).attr("firstPage");
        if (typeof attr !== 'undefined' && attr !== false) lastPage.val(firstPage.val());
        else firstPage.val(lastPage.val());
    }
    
    if (val > max) $(e.target).val(max);
    else if (val <= 0) $(e.target).val(min);
});