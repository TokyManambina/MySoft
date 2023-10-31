//sendMail
$("#sendMail").click(function () {
    if (verifyMailExp() && !$(this).hasClass("disabled")) {

        if (!$(this).hasClass("disabled")) $(this).addClass("disabled");

        /*if (validateEmail($("receiver").val())) {
            alert("email du destinataire invalide");
            return;
        }*/


        var formData = new FormData($('form#getfile')[0]);
        var a = input.files[0];

        //Mail
        formData.append("Object", $("#object").val());
        formData.append("Message", encodeHTMLEntities($("#message").val()));
        formData.append("Receiver", $("#receiver").val());
        formData.append("Sender", $("#expmail").val());

        formData.append("CodeDoc", $("#selectSource").val()[0] + $("#selectDest").val()[0] + $("#selectType").val()[0]);

        var pdfContainer = $('#pdfViewer').offset();
        var posSign = { top: null, left: null };

        //Signature
        if (!$("#signature").hasClass("hidden")) {
            var offsetSign = $("#signature").offset();

            posSign = {
                top: offsetSign.top - pdfContainer.top,
                left: offsetSign.left - pdfContainer.left
            };

            formData.append("SignInfo", $("#Page").val() + "|" + posSign.left.replace('.', ',') + "|" + posSign.top.replace('.', ',') + "|" + $("#signature").width().replace('.', ',') + "|" + $("#signature").height().replace('.', ',') + "|" + $("#pdfViewer").width().replace('.', ',') + "|" + $("#pdfViewer").height().replace('.', ','));
        }

        if (!$("#paraphe").hasClass("hidden")) {
            var offsetSign = $("#paraphe").offset();

            posSign = {
                top: offsetSign.top - pdfContainer.top,
                left: offsetSign.left - pdfContainer.left
            };

            var parapheList = $("#showListParaphe").find("span");

            var countparapheList = parapheList.length;
            if (countparapheList != 0) {
                var list = [];
                parapheList.each(function () {
                    list.push($(this).text() + "|" + posSign.top.replace('.', ',') + "|" + posSign.left.replace('.', ',') + "|" + $("#paraphe").width().replace('.', ',') + "|" + $("#paraphe").height().replace('.', ',') + "|" + $("#pdfViewer").width().replace('.', ',') + "|" + $("#pdfViewer").height().replace('.', ','));
                });

                formData.append("ParapheInfo", list.toString());
            }
        }

        if ($("#valid").prop("checked")) {
            var validList = $("#listValidator").find("span");
            var countValid = validList.length;
            if (countValid != 0) {
                validList.each(function () {
                    formData.append("validator", $(this).text());
                });
            }
        }

        formData.append("expediteur.Mail", $("#expmail").val());
        formData.append("expediteur.Password", $("#expmailmdp").val());
        if ($("#remember").prop("checked") == true) {
            formData.append("expediteur.Save", true);
        }

        formData.append("url[]", $("#selectSource").val());
        formData.append("url[]", $("#selectType").val());
        formData.append("url[]", $("#selectDest").val());

        formData.append("Username", User.Username);
        formData.append("Password", User.Password);

        $.ajax({
            type: "POST",
            url: "/Main/SendDocument",
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            success: function (result) {
                if (result == "send") {
                    setTimeout(function () {
                        window.location = window.location.origin + "/Document/MyDocument";
                    }, 2000);
                    alert("Document envoye");
                } else if (result == "format") {
                    alert("Veuillez verifier le document svp!");
                    if ($("#sendMail").hasClass("disabled")) $("#sendMail").removeClass("disabled");
                    input.click();
                } else if (result == "required") {
                    alert("Veuillez uploader un document");
                    if ($("#sendMail").hasClass("disabled")) $("#sendMail").removeClass("disabled");
                    input.click();
                } else if (result == "login") {
                    alert("Veuillez vous reconnecter");
                    window.location = window.location.origin;
                } else {
                    alert("Mail non envoye");
                    if ($("#sendMail").hasClass("disabled")) $("#sendMail").removeClass("disabled");
                }

            },
            error: function (e) {
                if ($(this).hasClass("disabled")) $(this).removeClass("disabled");
                alert("Document non Envoye, svp verifier votre internet ou votre mail");
                if ($(this).hasClass("disabled")) $(this).removeClass("disabled");
            }
        });
    }
});