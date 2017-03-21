
function searchbegin() {
  
    document.getElementById("searchclear").disabled = false;
}
function Createchange(){
    document.getElementById("Create_Cancle").disabled = false;
};
var Createchange_firstname=function(obj) {
    var girlfirend = $(obj).context;
    var text = girlfirend.value;
    document.getElementById("fullname").value = text + " " + document.getElementById("lastname").value;
}
var  Createchange_lastname=function(obj) {
    var girlfirend = $(obj).context;
    var text = girlfirend.value;
    document.getElementById("fullname").value = document.getElementById("firstname").value + " " + text;

}
var change = function (obj) {                       //获取点击用户的详细信息
    $(obj).context.style.color = "green";
    document.getElementById("canclebtn").disabled = false;
    document.getElementById("modifybtn").disabled = false;
   
};
function checkPass() {
    var pass = document.getElementById("password").value;
    if (pass.length < 8) {
        $("#passmess").html("Password length is at least 8!");
    }
    if (!pass.match(/([a-z])+/)) {
        $("#passmess").html("The password should contain at least one lower case.");
    }
    if (!pass.match(/([0-9])+/)) {
        $("#passmess").html("The password should contain at least one digit");
    }
    if (!pass.match(/([A-Z])+/)) {
        $("#passmess").html("The password contains at least one upper case");
    }

}

function Save_authority() {
    var DeletePer;
    var codestr = "a";
    var groupname = document.getElementById("groupname").innerHTML.split("<h3>")[1].split("</h3>")[0];
    for (var i = 1; i < 14; i++) {
        var code = "checkbox" + i;
        if (document.getElementById(code).checked) {
            codestr = codestr + "," + i;
        }
    }
 
    var code = codestr.split("a,")[1];
    $.ajax({
        type: "post",
        url: "/GroupsManagement/SetAuthorityCode",
        data: "code=" + code + "&groupname=" + groupname + "&domain=" + $("#domain").val(),
        dataType: "json",
        success: function (data) {
            $("#MiddleMess").html("The modification has been saved for the next logon");
        }
          ,
        error: function () {
            $("#message").html("<h5 style=\"color:red\">Network interrupt, networking timeout</h5>");
        }
    });
}
var GetAuthorityCode = function (obj) {                       //获取点击用户的详细信息
    var data = $(obj).context.innerHTML;
    for (var i = 1; i < 14; i++) {
        var code = "checkbox" + i;
        document.getElementById(code).checked = false;
    }
    var firstsplit = data.split("</td><td>")[1];
    var groupname = firstsplit.split("</td>")[0];
    document.getElementById("groupname").innerHTML = "<h3>" + groupname + "</h3>";
    $("#MiddleMess").html("");
    $.ajax({
        type: "post",
        url: "/GroupsManagement/GetAuthorityCode",
        data: "groupname=" + groupname + "&domain=" + $("#domain").val(),
        dataType: "json",
        success: function (data) {
            $("#MiddleMess").html("Load is complete, please modify and save..");
            
            for (var i = 0; i < data.length; i++) {
                if (data[i] != null) {
                    var code = "checkbox" + data[i].toString()
                    document.getElementById(code).checked = true;
                }
            }

        }
           ,
        error: function () {

            $("#message").html("<h5 style=\"color:red\">Network interrupt, networking timeout</h5>");
        }

    });
};
function Group_authority() {    //修改组的权限
    for (var i = 1; i < 14; i++) {
        var code = "checkbox" + i;
            document.getElementById(code).checked = false;
    }
    $("#MiddleMess").html("");
    $("#tablespace").html("<h4 style=\"color:orange\">Searching....Please wait</h4>");
    $.ajax({
        type: "post",
        url: "/GroupsManagement/AuthorityManagement",
        data: "domain=" + $("#domain").val() + " &searchcriteria=" + $("#searchcriteria").val() + "&searchkeyword=" + $("#searchkeyword").val(),
        dataType: "json",
        success: function (data) {

            $("#tablespace").html(data.Message);
            $("#example").DataTable();

        }
       ,
        error: function () {

            $("#message").html("<h5 style=\"color:red\">Network interrupt, networking timeout</h5>");
        }

    });
}


function confirmpassword() {
    var pass1 = document.getElementById("password").value;
    var pass2 = document.getElementById("confirmpassword").value;
    if (pass1 !== pass2) {
        document.getElementById("passwordremind").innerHTML = "Passwords do not match!";
        document.getElementById("confirmpassword").value = "";
    }
}
function clearremind() {
    document.getElementById("passwordremind").innerHTML = "";
}
function clearPass() {
    document.getElementById("passmess").innerHTML = "";
}

function checkbox1() {
    document.getElementById("uccp").checked = false;
    document.getElementById("pne").checked = false;
}
function checkbox2() {
    document.getElementById("umcpanl").checked = false;
}
function CheckEmail() {

    var email = document.getElementById("email").value;
    var reg = /^([a-z0-9_\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6})$/;//邮箱
    if (!reg.test(email)) {
        document.getElementById("emailcheck").innerHTML = "Please enter the mailbox in the correct format.";
    }
}
function ClearMess() {
    document.getElementById("emailcheck").innerHTML = "";
}
function Log_Clear(){
    var logname = $("#Logfile").val();
    $.ajax({
        type: "post",
        url: "/Dashboard/ClearLog",
        data: "logname=" + logname,
        dataType: "json",
        success: function (data) {
            $("#tablespace").html(data.Message.split("@")[0]);
            document.getElementById("Logfile").innerHTML = data.Message.split("@")[1];
            $('#example').dataTable({
               
            });
        }
       ,
        error: function () {

            $("#message").html("<h5 style=\"color:red\">Network interrupt, networking timeout</h5>");
        }

    });
}


