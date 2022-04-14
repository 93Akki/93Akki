var Msg = focusid = "";
var phoneno = /^\d{10}$/;
var mailformat = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

function validateSBSRegister() {
    var flag = false;
    if ($("#txtfirstName").val() == "") {
        Msg = "First name is required.";
        focusid = "txtfirstName";
        flag = true;
    }
    else if ($("#txtlastName").val() == "") {
        Msg = "Last name is required.";
        focusid = "txtlastName";
        flag = true;
    }
    else if ($("#txtMobileNumber").val() == "") {
        Msg = "Mobile number is required.";
        focusid = "txtMobileNumber";
        flag = true;
    }
    else if (!($("#txtMobileNumber").val().match(phoneno))) {
        Msg = "Invalid mobile number.";
        focusid = "txtMobileNumber";
        flag = true;
    }
    else if ($("#txtEmail").val() == "") {
        Msg = "E-mail is required.";
        focusid = "txtEmail";
        flag = true;
    }
    else if (!($("#txtEmail").val().match(mailformat))) {
        Msg = "Invalid Email ID.";
        focusid = "txtEmail";
        flag = true;
    }
    else if ($("#txtCity").val() == "") {
        Msg = "City is required.";
        focusid = "txtCity";
        flag = true;
    }

    return flag;
}

function SBSRegister() {
    if (!validateSBSRegister()) {
        if ($("#TandC").prop("checked") == true) {
            $(".btn-primary").html('<i class="fa fa-spinner fa-spin"></i>&nbsp;Please wait');
            $(".btn-primary").css("pointer-events", "none");
            $(".btn-primary").css("cursor", "no-drop");

            PushData("FirstName", $("#txtfirstName").val());
            PushData("LastName", $("#txtlastName").val());
            PushData("Mobile", $("#txtMobileNumber").val());
            PushData("Email", $("#txtEmail").val());
            PushData("Gender", $('input[name="gender"]:checked').val());
            PushData("City", $("#txtCity").val());
            PushData("Country", $("#drpCountry option:selected").val());
            PushData("ReferralCode", $("#txtreferralcode").val());
            CallWebMethod('/Registration/MemberRegistration', WebMethodData, OnPassMemberRegistration, OnFailMemberRegistration);
        }
        else {
            var Shtml = Errorhtml + "Please accecpt Terms and Conditions." + Close;
            $("#alertMsg").removeAttr("style");
            $("#alertMsg").html(Shtml);
            $(".mx-auto").scrollTop();
            setTimeout(function () {
                $('#alertMsg').fadeOut(300, function () { $(this).html(); });
            }, 5000);
        }
    }
    else {
        var Shtml = Errorhtml + Msg + Close;
        $("#alertMsg").removeAttr("style");
        $("#alertMsg").html(Shtml);
        $(".mx-auto").scrollTop();
        setTimeout(function () {
            $('#alertMsg').fadeOut(300, function () { $(this).html(); });
        }, 5000);        
        $("#" + focusid + "").focus();        
    }
}


function OnPassMemberRegistration(res) {
    var Shtml = "";
    if (res.success == true) {
        var data = JSON.parse(res.data);
        data = data[0]["Msg"];

        var resCheck = data.split("|");
        if (resCheck[0] == "S") {
            Shtml = succHtml + "Thank you for register with SBS. Please check your mailbox for SBS login details." + Close;
            reset();
        }
        else
            Shtml = Errorhtml + resCheck[1] + Close;
    }
    else
        Shtml = Errorhtml + "Something went worng please try again later." + Close;

    $("#alertMsg").removeAttr("style");
    $("#alertMsg").html(Shtml);
    setTimeout(function () {
        $('#alertMsg').fadeOut(300, function () { $(this).html(); });
    }, 8000);

    $(".btn-primary").html('Register');
    $(".btn-primary").css("pointer-events", "all");
    $(".btn-primary").css("cursor", "pointer");
}

function OnFailMemberRegistration(res) {
    $(".btn-primary").html('Register');
    $(".btn-primary").css("pointer-events", "all");
    $(".btn-primary").css("cursor", "pointer");
}

function reset() {
    $("#txtfirstName").val("");
    $("#txtlastName").val("");
    $("#txtMobileNumber").val("");
    $("#txtEmail").val("");
    $("#txtCity").val("");
    $("#TandC").prop("checked", false);
    $("#txtreferralcode").val("");
}


