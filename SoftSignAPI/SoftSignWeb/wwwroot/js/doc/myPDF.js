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
	changePage();
}

function changePage() {
	if (!test || lastFile != currentPDF.file) {
		lastFile = currentPDF.file;
		$("#signpage").val("1");
		$("#signpage").attr("max", currentPDF.countOfPages);
		$("#signpage").attr("min", 1);

		$("[firstPage]").val("1");
		$("[firstPage]").attr("max", currentPDF.countOfPages);
		$("[firstPage]").attr("min", 1);

		//$("[LastPage]").val(currentPDF.countOfPages);
		$("[LastPage]").val(1);
		$("[LastPage]").attr("max", currentPDF.countOfPages);
		$("[LastPage]").attr("min", 1);
		test = !test;
	}
}


//#endregion

$("[firstPage], [LastPage], #signpage").on('change', (e) => {
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