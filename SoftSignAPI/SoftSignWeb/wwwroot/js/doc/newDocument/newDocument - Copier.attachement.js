import { apiUrl, webUrl } from "../apiConfig.js";

let attachementList = [];

$('#attachement').on('click', (e) => {
	$(`[attachement-id][hidden]`).remove();
	let count = attachementList.length + 1;
	$('#attachementListTab').before(attachementUI(count));

	$(`[file-id="${count}"]`).click();
});


$(document).on('change', `[file-id]`, (e) => {
	let header = $(e.target).closest('[attachement-id]');
	const inputFile = e.target.files[0];

	if (inputFile) {
		header.removeAttr("hidden");
		console.log(header.find('[attachement-name]'));
		header.find('[attachement-name]').text(inputFile.name);
		attachementList.push({
			id: header.attr('[attachement-id]'),
			name: inputFile.name
		});
	} else {
		header.remove();
	}
})


function attachementUI(id) {
    return `
        <li class="nav-item" attachement-id="${id}" attachement hidden>
			<input type="file" file-id="${id}" hidden/>
			<div class="nav-link d-flex align-items-center justify-content-between">
				<div class="d-flex align-items-baseline">
					<i class="fa fa-file"> </i><span style="padding-left: 10px" attachement-name></span>
				</div>
				<i class="fa fa-times text-danger float-right" remove-attachement></i>
			</div>
		</li>
`;
}