﻿//#region PDF
let currentPDF = {}
let test = false;
var lastFile;
var lastInput;

function resetCurrentPDF() {
	currentPDF = {
		file: null,
		countOfPages: 0,
		currentPage: 1,
		zoom: 1.5
	}
}

$('#inputFile').on('change', (e) => {
	const inputFile = e.target.files[0];

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
})

function loadPDF(data) {
	const pdfFile = pdfjsLib.getDocument(data);
	resetCurrentPDF();
	pdfFile.promise.then((doc) => {
		currentPDF.file = doc;
		currentPDF.countOfPages = doc.numPages;
		$("#pdfImg").addClass("hidden");
		renderCurrentPage();
	});
}

function getPageSize(p) {
	return new Promise((resolve, reject) => {
		try {
			currentPDF.file.getPage(p).then((page) => {
				var viewport = page.getViewport({ scale: currentPDF.zoom })

				resolve({
					width: viewport.width,
					height: viewport.height
				});
			}, (e) => {
				reject("Page out of range");
			});
		} catch (error) {
			console.error(error);
			reject("An error occurred");
		}
	});
}
//appel de la fonction
//getPageSize(9)
//	.then((pageSize) => {
//		console.log("Page size:", pageSize);
//	})
//	.catch((error) => {
//		console.error("Error:", error);
//	});

function renderCurrentPage() {
	currentPDF.file.getPage(currentPDF.currentPage).then((page) => {
		let viewer = $("#pdfViewer")[0];
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
	$("#current_page").html(currentPDF.currentPage + ' sur ' + currentPDF.countOfPages);
	initPage();
}

function initPage() {
	if (!test || lastFile != currentPDF.file) {
		lastFile = currentPDF.file;

		$("[firstPage]").val("1");
		$("[firstPage]").attr("max", currentPDF.countOfPages);
		$("[firstPage]").attr("min", 1);
		
		$("[LastPage]").val(1);
		$("[LastPage]").attr("max", currentPDF.countOfPages);
		$("[LastPage]").attr("min", 1);
		test = !test;
	}
}
//#endregion



//#region next/previous Page
$('#next').on('click', (e) => {
	const isValidPage = currentPDF.currentPage < currentPDF.countOfPages;
	if (!isValidPage) return;
			
	currentPDF.currentPage += 1;
	renderCurrentPage();
	$(document).trigger("refreshField");
});

$('#previous').on('click', (e) => {
	const isValidPage = currentPDF.currentPage - 1 > 0;
	if (!isValidPage) return;

	currentPDF.currentPage -= 1;
	renderCurrentPage();
	$(document).trigger("refreshField");
});

//#endregion