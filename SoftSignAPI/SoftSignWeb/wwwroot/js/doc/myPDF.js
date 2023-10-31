//#region PDF

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

$(`.toggleBoxSetting`).on('click', (e) => {
	let width = $("#BoxSettingMenu").css("width");
	if ($('#BoxSettingMenu').offset().left < 0)
		$("#BoxSettingMenu").css("left", `5px`);
	else
		$("#BoxSettingMenu").css("left", `-${width}`);

})


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

$(`#pdftext`).on('click', (e) => {
	if ($(`#inputFile`).val() == "")
		$('#inputFile').click();
})

$(`[data-action="openPDF"]`).on('click', (e) => {
	$('#inputFile').click();
})

function loadPDF(data) {
	const pdfFile = pdfjsLib.getDocument(data);
	resetCurrentPDF();
	pdfFile.promise.then((doc) => {
		currentPDF.file = doc;
		currentPDF.countOfPages = doc.numPages;
		$("#pdftext").addClass("hidden");
		renderCurrentPage();
	});
}

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
	changePage();
}

function changePage() {
	if (!test || lastFile != currentPDF.file) {
		lastFile = currentPDF.file;
		$("#signpage").val("1");
		$("#firstPage").val("1");
		$("#LastPage").val(currentPDF.countOfPages);
		$("#LastPage").attr("max", currentPDF.countOfPages);
		$("#firstPage").attr("max", currentPDF.countOfPages);
		$("#signpage").attr("max", currentPDF.countOfPages);
		$("#LastPage").attr("min", 1);
		$("#firstPage").attr("min", 1);
		$("#signpage").attr("min", 1);
		test = !test;
	}
}
//#endregion

$("#firstPage, #LastPage, #signpage").on('change', (e) => {
	var max = parseInt($( e.target).attr("max"));
	var val = parseInt($(e.target).val());
	var min = parseInt($(e.target).attr("min"));

	if (val > max) $(e.target).val(max);
	else if (val <= 0) $(e.target).val(min);
});


//#region next/previous Page
$('#next').on('click', (e) => {
	const isValidPage = currentPDF.currentPage < currentPDF.countOfPages;
	if (isValidPage) {
		currentPDF.currentPage += 1;
		renderCurrentPage();
	}
});

$('#previous').on('click', (e) => {
	const isValidPage = currentPDF.currentPage - 1 > 0;
	if (isValidPage) {
		currentPDF.currentPage -= 1;
		renderCurrentPage();
	}
});

//#endregion