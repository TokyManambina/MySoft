//const zoomButton = document.getElementById('zoom');
const input = document.getElementById('inputFile');
const openFile = document.getElementById('openPDF');
const pdftext = document.getElementById('pdftext');
const currentPage = document.getElementById('current_page');
const viewer = document.querySelector('.pdf-viewer');
let currentPDF = {}
let test = false;
var lastFile;

function resetCurrentPDF() {
	currentPDF = {
		file: null,
		countOfPages: 0,
		currentPage: 1,
		zoom: 1.5
	}
}

pdftext.addEventListener('click', () => {
    if (input.value == "") input.click();
});
openFile.addEventListener('click', () => {
	input.click();
});

var lastInput = "";
input.addEventListener('change', event => {
	const inputFile = event.target.files[0];
	if (inputFile) {
		lastInput = inputFile;
		if (inputFile.type == 'application/pdf') {
			const reader = new FileReader();
			reader.readAsDataURL(inputFile);
			reader.onload = () => {
				loadPDF(reader.result);
				//zoomButton.disabled = false;
			}
		}
		else {
			alert("Veuillez sélectionner un fichier \".pdf\" !!!")
		}
	} else {
		inputFile.value = lastInput;
	}
});

/*
zoomButton.addEventListener('input', () => {
	if (currentPDF.file) {
		document.getElementById('zoomValue').innerHTML = zoomButton.value + "%";
		currentPDF.zoom = parseInt(zoomButton.value) / 100;
		console.log(currentPDF.zoom);
		renderCurrentPage();
	}
});*/

document.getElementById('next').addEventListener('click', () => {
	const isValidPage = currentPDF.currentPage < currentPDF.countOfPages;
	if (isValidPage) {
		currentPDF.currentPage += 1;
		renderCurrentPage();
	}
});

document.getElementById('previous').addEventListener('click', () => {
	const isValidPage = currentPDF.currentPage - 1 > 0;
	if (isValidPage) {
		currentPDF.currentPage -= 1;
		renderCurrentPage();
	}
});

function loadPDF(data) {
	const pdfFile = pdfjsLib.getDocument(data);
	resetCurrentPDF();
	pdfFile.promise.then((doc) => {
		currentPDF.file = doc;
		currentPDF.countOfPages = doc.numPages;
		document.getElementById("pdftext").classList.add("hidden");
		renderCurrentPage();
	});
}

function renderCurrentPage() {
	currentPDF.file.getPage(currentPDF.currentPage).then((page) => {
		var context = viewer.getContext('2d');
		var viewport = page.getViewport({ scale: currentPDF.zoom, });
		viewer.height = viewport.height;
		viewer.width = viewport.width;

		var renderContext = {
			canvasContext: context,
			viewport: viewport
		};
		page.render(renderContext);
	});
	currentPage.innerHTML = currentPDF.currentPage + ' sur ' + currentPDF.countOfPages;
	changePage();
}

function changePage() {
	if (!test || lastFile != currentPDF.file) {
		lastFile = currentPDF.file;
		$("#signpage").val("1");
		$("#firstPage").val("1");
		$("#LastPage").val(currentPDF.countOfPages);
		$("#LastPage").attr("max",currentPDF.countOfPages);
		$("#firstPage").attr("max",currentPDF.countOfPages);
		$("#signpage").attr("max", currentPDF.countOfPages);
        $("#LastPage").attr("min", 1);
        $("#firstPage").attr("min", 1);
		$("#signpage").attr("min", 1);
		test = !test;
	}
}

$("#firstPage, #LastPage, #signpage").change(function () {
	var max = parseInt($(this).attr("max"));
	var val = parseInt($(this).val());
	var min = parseInt($(this).attr("min"));

    if ( val > max) $(this).val(max);
    else if (val <= 0) $(this).val(min);
});