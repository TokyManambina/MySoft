$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
    $('#message').summernote({
        lang: 'fr-FR',
        height: 200
    });
    $('.dropdown-toggle').dropdown();
});
/*$(function () {
    // Summernote
    $('#message').summernote({
        lang: 'fr-FR',
        height: 200
    });
})*/

$("#resetMail").click(function(){
    $("#receiver").val("");
    $("#cc").val("");
    $("#object").val("");
    $("#message").summernote('code', "");
});

$("#inputFile").change(function () {
    if (currentPDF.file == null) {
        if ($("#addSignature").hasClass("fa-ban")) {
            $("#addSignature").removeClass("fa-ban");
            $("#addSignature").addClass("fa-plus");
        }
        if ($("#addParaphe").hasClass("fa-ban")) {
            $("#addParaphe").removeClass("fa-ban");
            $("#addParaphe").addClass("fa-plus");
        }
    }
})

//addSign
function addSignBox() {
    if ($("#inputFile").val() != "" && $("#inputFile").val() != null) {
        if ($("#addSignature").hasClass("fa-plus")) {
            $("#addSignature").removeClass("fa-plus");
            $("#addSignature").addClass("fa-minus");

            if ($("#signatureListBox").hasClass("hidden")) $("#signatureListBox").removeClass("hidden");
            //var canvasOffset = $('#pdfViewer').offset();
            //$("#signature").css({ "top": (canvasOffset.top + 125) + "px", "left": (canvasOffset.left + 25) + "px" });

            $("#plussignature").removeClass("hidden");

            var listSignature = $("#actionmenu").find(`[data-type="signature"]`);
            listSignature.each(function () {
                if ($(this).hasClass("parapheList")) $(this).removeClass("hidden");
            });
        } else {
            $("#addSignature").removeClass("fa-minus");
            $("#addSignature").addClass("fa-plus");

            $("#signatureListBox").addClass("hidden");

            $("#plussignature").addClass("hidden");

            var listSignature = $("#actionmenu").find(`[data-type="signature"]`);
            listSignature.each(function () {
                if ($(this).hasClass("parapheList")) $(this).addClass("hidden");
            });
        }
    } else {
        alert("Veuillez uploader d'abord un document");
	}
}

function addParapheBox() {
    if ($("#inputFile").val() != "" && $("#inputFile").val() != null) {
        if ($("#addParaphe").hasClass("fa-plus")) {
            $("#addParaphe").removeClass("fa-plus");
            $("#addParaphe").addClass("fa-minus");

            $("#paraphe").removeClass("hidden");
            var canvas = $('#pdfViewer').offset();
            $("#paraphe").css({ "top": (canvas.top + 125) + "px", "left": (canvas.left + 25) + "px" });

            $("#plusparaphe").removeClass("hidden");

            var listParaphe = $("#actionmenu").find(`[data-type="paraphe"]`);
            var countParaphe = listParaphe.length;
            listParaphe.each(function () {
                if ($(this).hasClass("parapheList")) $(this).removeClass("hidden");
            });

        } else {
            $("#addParaphe").removeClass("fa-minus");
            $("#addParaphe").addClass("fa-plus");

            $("#paraphe").addClass("hidden");

            $("#plusparaphe").addClass("hidden");

            var listParaphe = $("#actionmenu").find(`[data-type="paraphe"]`);
            var countParaphe = listParaphe.length;
            listParaphe.each(function () {
                if ($(this).hasClass("parapheList")) $(this).addClass("hidden");
            });
        }
    } else {
        alert("Veuillez uploader d'abord un document");
	}
    
}

var dragOption = {
    containment: '#pdfViewer',
    cursor: 'move',
    disabled: false,
    stop: function () {
        /* var offset = $(this).offset();
        var xPos = offset.left;
        var yPos = offset.top;
        $('#test').text('X: ' + xPos+ " |  Y=" + yPos);
            */
        var containerPos = $('#pdfViewer').offset();
        var inputPos = $(this).offset();
        relativeOffset = {
            top: inputPos.top - containerPos.top,
            left: inputPos.left - containerPos.left
        };
        var w = $('#pdfViewer').width();
        var h = $('#pdfViewer').height();
        $("#pagination").text('[x: ' + relativeOffset.left + '| y: ' + relativeOffset.top +']'+
            ' [w:' + w + ' | h:' + h + ']' +
            ' [ sx: ' + $(this).width() + ' | sy: ' + $(this).height() +']');
        //$('#test').text('X: ' + relativeOffset.left+ " |  Y=" + relativeOffset.top);
    }
}

$("#signature").mousemove(function (e) {
    divPos = {
        left: e.pageX - $(this).offset().left,
        top: e.pageY - $(this).offset().top,
    };
    if (divPos.left > $(this).width() - 11 && divPos.top > $(this).height() - 11) dragOption.disabled = true;
    else dragOption.disabled = false;

    $("#signature").draggable(dragOption);
    //$("#signature").draggableTouch(dragOption);
});
$("#paraphe").mousemove(function (e) {
    divPos = {
        left: e.pageX - $(this).offset().left,
        top: e.pageY - $(this).offset().top,
    };
    if (divPos.left > $(this).width() - 11 && divPos.top > $(this).height() - 11) dragOption.disabled = true;
    else dragOption.disabled = false;

    $("#paraphe").draggable(dragOption);
});

$("#valid").change(function () {
    if ($(this).prop("checked") == true) {
        $("#btn-menu").removeClass("hidden");
        $("#valid-menu").removeClass("hidden");
        if ($("#minus").hasClass("fa-minus")) {
            $("#valid-menu").css("display", "block");
        }
    } else {
        $("#btn-menu").addClass("hidden");
        $("#valid-menu").addClass("hidden");
        if ($("#valid-menu").css("display") == "block") {
            $("#valid-menu").css("display", "none");
        }
    }
});

//validator
var countValidator = 0;
function addValidator() {
    var autorite = $("#autorite").val();

    if (validateEmail(autorite)) {
        $("#listValidator").append(
            '<li class="nav-item" id="validator' + countValidator + '">' +
            '<div class="nav-link float-right">' +
            '<span id="validator' + countValidator + '">' + autorite + '</span>' +
            ' <i class="fa fa-times text-danger" onclick="return removeValidator(\'validator' + countValidator + '\')"></i>' +
            '</div>' +
            '</li>'
        );
        countValidator++;
        $("#autorite").val("");

        if (!$("#errorValidator").hasClass("hidden")) $("#errorValidator").addClass("hidden");

    } else {
        if ($("#errorValidator").hasClass("hidden")) $("#errorValidator").removeClass("hidden");
	}
}

$("#autorite").on('keypress', function (e) {
    if (e.which == 13) {
        addValidator();
    }
});

var canvasOffset = $('#pdfViewer').offset();

//paraphe
var countParaphe = 0;
function addListParaphe() {
    var firstPage = $("#firstPage").val();
    var LastPage = $("#LastPage").val();
    if (firstPage != "" && firstPage != null && LastPage != "" && LastPage != null) {
        var code =
            '<li class="nav-item parapheList" id="paraphe' + countParaphe + '" data-type="paraphe" data-value="' + firstPage + '|' + LastPage + '">' +
                '<div class="nav-link float-right">' +
                    '<span id="paraphe' + countParaphe + '">' + firstPage + ' - ' + LastPage + '</span>' +
                    ' <i class="fa fa-times text-danger" onclick="return removeListParaphe(\'paraphe' + countParaphe + '\')"></i>' +
                '</div>' +
            '</li>';
        $(code).insertBefore($("#showListParaphe"));
        countParaphe++;

        $("#firstPage").val($("#firstPage").attr("min"));
    }
}
//signature
var countSignature = 0;
function addListSignature() {
    var Page = $("#signpage").val();
    if (Page != "" && Page != null) {
        var code =
            `<li class="nav-item parapheList" id="signature${countSignature}" data-type="signature" data-value="${Page}">
                <div class="nav-link float-right">
                    <span id="signature' + countSignature + '">${Page}</span>
                    <i class="fa fa-times text-danger" onclick="return removeListSignature('${countSignature}')"></i>
                </div>
            </li>`;
        $(code).insertBefore($("#showListSignature"));

        code = `
            <div class="boxSign" id="signatureBox${countSignature}" data-type="signature" data-id="${countSignature}">
	            <div class="ribbon-wrapper">
		            <div class="ribbon bg-primary">
			            sign
		            </div>
	            </div>
            </div>
            <script>
                $("#signatureBox${countSignature}").hover(function () {
                    if(!$("#signature${countSignature}").hasClass("bg-primary")) $("#signature${countSignature}").addClass("bg-primary");
                }, function(){
                    if($("#signature${countSignature}").hasClass("bg-primary")) $("#signature${countSignature}").removeClass("bg-primary");
                });

                $("#signatureBox${countSignature}").mousemove(function (e) {
                    divPos = {
                        left: e.pageX - $(this).offset().left,
                        top: e.pageY - $(this).offset().top,
                    };
                    if (divPos.left > $(this).width() - 11 && divPos.top > $(this).height() - 11) dragOption.disabled = true;
                    else dragOption.disabled = false;

                    $("#signatureBox${countSignature}").draggable(dragOption);
                    //$("#signatureBox${countSignature}").draggableTouch(dragOption);
                });

                $("#signatureBox${countSignature}").css({ "top": (canvasOffset.top + 125) + "px", "left": (canvasOffset.left + 25) + "px" });
            </script>
        `;

        $(`#signatureListBox`).append(code);

        $("#signpage").val($("#signpage").attr("min"));

        countSignature++;
    }
}

function removeListParaphe(id) {
    $("#" + id).remove();
}
function removeListSignature(id) {
    $(`#signature${id}`).remove();
    $(`#signatureBox${id}`).remove();
}

$("#receiver,#object").change(function () {
    canSend();
});
$("#message").on('summernote.change',function () {
    canSend();
});

$("#saveMail").click(function () {
    canSend();
});


function canSend() {
    if ($("#receiver").val() != "" && $("#object").val() != "" && $("#message").val() != "" && validateEmail($("#receiver").val()) && !$("#cc").hasClass("text-danger")) {
        
        $("#sendDocument").removeClass("disabled");
        $("#sendDocument").attr("data-original-title", "");
        $("#sendDocument").attr("data-toggle", "modal");
    } else {
        if (!$("#sendDocument").hasClass("disabled")) $("#sendDocument").addClass("disabled");
        $("#sendDocument").attr("data-toggle", "tooltip");
        $("#sendDocument").attr("data-original-title", "verifier les champs dans Destinataire");
    }
}

function decodeHTMLEntities(text) {
    return $("<textarea/>").html(text).text();
}
function encodeHTMLEntities(text) {
    return $("<textarea/>").text(text).html();
}

function verifyMailExp() {
    if (validateEmail($("#expmail").val()) && $("#expmailmdp").val() != "" && $("#expmailmdp").val() != null  ) {
        if (!$("#expmailerror").hasClass("hidden")) $("#expmailerror").addClass("hidden");
        return true;
    } else {
        if ($("#expmailerror").hasClass("hidden")) $("#expmailerror").removeClass("hidden");
        $("#expmail").val("");
        $("#expmailmdp").val("");
        return false;
    }
}


//validation
/*function validateEmail(email) {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}*/

$("#receiver").focusout(function () {
    if (validateEmail($(this).val())) {
        if ($("#receiver").hasClass("text-danger")) $("#receiver").removeClass("text-danger");
    } else {
        if (!$("#receiver").hasClass("text-danger")) $("#receiver").addClass("text-danger");
	}
});
$("#cc").focusout(function () {
    var ccList = $(this).val().split(",");

    if ($(this).hasClass("text-danger")) $(this).removeClass("text-danger");
    for (var i = 0; i < ccList.length; i++) {
        if (!validateEmail(ccList[i])) {
            if (!$(this).hasClass("text-danger")) $(this).addClass("text-danger");
            return;
        }
    }
});



function getid() {
    $("#listValidator").find("span").each(function () {
        alert($(this).text());
    });
}
