var Msg = focusid = "";

function validateSBSLogin() {
    var flag = false;
    if ($("#txtUserName").val() == "") {
        Msg = "Mobile number/Email ID is required.";
        focusid = "txtUserName";
        flag = true;
    }
    else if ($("#txtPassword").val() == "") {
        Msg = "Password is required.";
        focusid = "txtPassword";
        flag = true;
    }
    return flag;
}

function Login() {
    if (!validateSBSLogin()) {
        $("#btnLogin").html('<i class="fa fa-spinner fa-spin"></i>&nbsp;Please wait');
        $("#btnLogin").css("pointer-events", "none");
        $("#btnLogin").css("cursor", "no-drop");

        PushData("UserName", $("#txtUserName").val());
        PushData("Password", $("#txtPassword").val());
        CallWebMethod('/Login/MemberLogin', WebMethodData, OnPassMemberLogin, OnFailMemberLogin);
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


function OnPassMemberLogin(res) {
    var Shtml = "";
    if (res.success == true) {
        var Data = res.data.split("|");
        if (Data[0] == "S") {
            var LoginData = JSON.parse(Data[1]);
            var singOutHtml = "<br/><a href=\"javascript:void(0)\" onclick=\"SingOut()\" style=\"text-decoration: underline;\">Sing Out</a>";
            var SingInHtml = "Hello, " + LoginData[0]["FirstName"] + singOutHtml;
            $("#sign_inText").html(SingInHtml);
            localStorage.setItem("SingInHtml", SingInHtml);
            $("#txtUserName").val("");
            $("#txtPassword").val("");
            window.location.href = '/Home/Index/';
        }
        else
            Shtml = Errorhtml + Data[1] + Close;

    }
    else
        Shtml = Errorhtml + "Something went worng please try again later." + Close;

    $("#alertMsg").removeAttr("style");
    $("#alertMsg").html(Shtml);
    setTimeout(function () {
        $('#alertMsg').fadeOut(300, function () { $(this).html(); });
    }, 8000);

    $("#btnLogin").html('Sign in');
    $("#btnLogin").css("pointer-events", "all");
    $("#btnLogin").css("cursor", "pointer");
}

function OnFailMemberLogin(res) {
    $("#btnLogin").html('Sign in');
    $("#btnLogin").css("pointer-events", "all");
    $("#btnLogin").css("cursor", "pointer");
}



