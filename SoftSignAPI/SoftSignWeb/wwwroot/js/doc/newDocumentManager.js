let ListUserDocument = [];
let selectedRecipient = "";
let lastSelectedRecipient = "";

let canvasOffset = $('#pdfViewer').offset();
let posX = canvasOffset.left;
let posY = canvasOffset.top;

// Save the initial window size
var initialWidth = window.innerWidth;
var initialHeight = window.innerHeight;
window.addEventListener('resize', function () {
    // Reset the window size to the initial size
    window.resizeTo(initialWidth, initialHeight);
});



function toggleSidebar() {
	document.querySelector('.sidebar')
		.classList.toggle('closed');
}

$(`#toggleBoxSetting`).on('click', (e) => {
	$("#BoxSettingMenu").removeClass('closed');
})
$(`#closeSideMenu`).on('click', (e) => {
	$("#BoxSettingMenu").addClass('closed');
})

$(`#pdfImg`).on('click', (e) => {
	if ($(`#inputFile`).val() == "")
		$('#inputFile').click();
})

$(`[data-action="openPDF"]`).on('click', (e) => {
	$('#inputFile').click();
})