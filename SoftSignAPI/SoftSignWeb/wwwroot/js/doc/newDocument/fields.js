var dragOption = {
    containment: '#pdfViewer',
    cursor: 'move',
    disabled: false,
    handle: '.ribbon'
}

function existDocument() {
    return $("#inputFile").val() == "";
}

$(`[data-action="addField"]`).on("click", (e) => {
    if (!existDocument) {
        return alert("Veuillez uploader d'abord un document");
    }


});

function field() {
    return `
        <div class="boxSign" id="signatureBox" data-type="signature" field-id="${id}">
	        <div class="ribbon-wrapper">
		        <div class="ribbon text-white" style="background-color:${color}">
			        ${title}
		        </div>
	        </div>
        </div>
    `;
}