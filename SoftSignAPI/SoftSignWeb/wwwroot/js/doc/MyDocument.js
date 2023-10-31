$(document).ready(() => {
	$("#sign").text("");
	MyDocument();
})

function MyDocument() {
	console.log("ok")
	$.ajax({
		type: "GET",
		url: "/Document/GetMyDocument",
		data: null,

		success: function (result) {
			var result = JSON.parse(result);
			
			if (result.type == "error") {
				return;
			}
			console.log(result);

			$(`#listDocument`).text("");

			let code = ``;
			$.each(result.data, (k, v) => {
				var date = v.DateSign;
				var icon = "fa-star";
				switch (v.Status) {
					case 0 : icon = "fa-spinner fa-spin"; break;
					case 1 : icon = "fa-file-signature"; break;
					case 2 : icon = "fa-file-archive"; break;
					default: icon = "fa-star"; break;

					/*
					case "Remaining": icon = "fa-spinner fa-spin"; break;
					case "Sign": icon = "fa-file-signature"; break;
					case "Archive": icon = "fa-file-archive"; break;
					default: icon = "fa-star"; break;
					*/
				}

				code += `
					<tr data-stat="${v.Status}" >
                        <td>
                          <div class="icheck-primary">
                            <input type="checkbox" value="" id="check1">
                            <label for="check1"></label>
                          </div>
                        </td>
                        <td class="mailbox-star"><a href="#"><i class="fa ${icon} text-secondary"></i></a></td>
                        <td onclick="ViewDocument('${v.Id}')" class="mailbox-name"><small style="color:grey;opacity:0.5">${v.Sender == "" ? "Pour" : "De"} : </small> <a href="#">${v.Sender == "" ? v.Receiver : v.Sender}</a></td>
                        <td onclick="ViewDocument('${v.Id}')" class="mailbox-subject pointer" style="min-width : 250px">
							<b>${v.Object}</b>
							<span>${convertToPlain(v.Message)}</span>`;
				/*
				if (v.Progress) {
					//v.vlast
					//v.vcurrent
					var bar = parseInt(parseInt(v.vcurrent) * 100 / parseInt(v.vlast));
					if (bar < 100)
						code += `</br>
						<div class="progress">
							<div class="progress-bar progress-bar-striped progress-bar-animated" style="width: ${bar}%"></div>
						</div>
					`;
				}
				*/
				code += `
                        </td>
                        <td class="mailbox-date">${DateLast(v.DateSend)}</td>
                        <td> <div></div> </td>
					</tr>`;
			});

			$(`#listDocument`).append(code);


			//Remaining, Validate, Sign, Archive
			$("[data-id='ct_DocRemain']").text($('#listDocument').find("[data-stat='Remaining']").length);
			$("[data-id='ct_DocSign']").text($('#listDocument').find("[data-stat='Sign']").length);
			$("[data-id='ct_DocArc']").text($('#listDocument').find("[data-stat='Archive']").length);
			$("[data-id='ct_DocTot']").text(result.data.length);

			$(`#all_doc`).text($(`#p_MyDocument`).find(`tr:visible`).length);
		},

		Error: function (x, e) {
			alert("Please contact the administrator");
		}
	});
}

$(`[data-action="newDocument"]`).on('click', (k, v) => {
	return;
	$.ajax({
		type: "GET",
		url: "/NewDocument",
		data: null,
		cache: false,
		contentType: false,
		processData: false,
		async: true,
		Error: function (x, e) {
			alert("Some error");
		}
	});
});