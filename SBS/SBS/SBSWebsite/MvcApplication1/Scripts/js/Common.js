var Rathi = new Array();
var WebMethodData = new Array();

function PushData(name, value) {
    var NewVale = "";
    if (value.toString().indexOf("'") >= 0)
        NewVale = value.replace(/'/g, '♦');
    else
        NewVale = value;

    WebMethodData.push("'" + name + "':'" + NewVale + "'");
}

function CallWebMethod(MethodName, Data, OnPass, OnFail) {

    var Data = "{" + WebMethodData.toString().replace("[", "{").replace("]", "}") + "}";
    try {

        $.ajax({
            type: "POST",
            url: MethodName,
            data: Data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnPass,
            error: OnFail
        });
    }
    catch (e) {
        alert(e.message);
    }
}

var Errorhtml = '<div class="alert alert-danger alert-dismissible"><a href="javascript:void(0);" class="close" data-dismiss="alert" aria-label="close">&times;</a><label>';
var succHtml = '<div class="alert alert-success alert-dismissible"><a href="javascript:void(0);" class="close" data-dismiss="alert" aria-label="close">&times;</a><label>';
var Close = '</label></div>';


$(document).ready(function () {
    if (localStorage.getItem("SingInHtml") != null && localStorage.getItem("SingInHtml") != undefined) {
        $("#sign_inText").html(localStorage.getItem("SingInHtml"));
        $("#sign_inText").closest("a").attr("href", "javascript:void(0)");
        var url = window.location.pathname.split("/");
        var questions = url[1];
        if (questions == "Login")
            window.location.href = '/Home/Index/';
    }
});

function SingOut() {
    CallWebMethod('/Login/SingOut', WebMethodData, OnPassMemberSingout, OnFailMemberSingout);
}

function OnPassMemberSingout(res) {
    localStorage.removeItem("SingInHtml");
    $("#sign_inText").html("Hello, sign in");
    $("#sign_inText").closest("a").attr("href", "/Login");
    window.location.href = '/Login/Index/';
}
function OnFailMemberSingout(res)
{ }