function EnterLogin() {
    var event = window.event || arguments.callee.caller.arguments[0];
    if (event.keyCode == 13) {
        Login();
    }
}

function Login() {
    var username = $("#username").val();
    var domain = $("#domain").val();
    var remember=true;
    var cok = getCookie("username");
    if (document.getElementById("Rememberme").checked) {
        setCookie("rememberme", remember);
        if (cok != null) {
            delCookie(username);
            delCookie(domain)
            setCookie("username", username);
            setCookie("domain", domain);
        } else {
            setCookie("username", username);
            setCookie("domain", domain);
        }
    } else {
        delCookie("rememberme");
        if (cok != null) {
            delCookie(username);
            delCookie(domain);
        }
    }
    setCookie("username", username);
    var domain = $("#domain").val();
   
    var password = $("#password").val();
    if (username == "") {
        
        $("#message").html("Username can not be empty");
        return;
    }
    if (password == "") {
        $("#message").html("Password can not be empty");
        return;
    }
    $("#message").html("Please Waiting..")
    $.ajax({
        type: "post",
        url: "/Home/Login",
        data: "domain=" + domain + "&username=" + username + "&password=" + password,
        dataType: "json",
        success: function (data) {
            if (data.logined) {
                window.location.href = "/Dashboard/ActivityandLog";
            }else
            {
                $("#message").html(data.Message)
            }
           
        }
       ,
        error: function () {

            $("#message").html("Network interrupt, networking timeout");
        }

    });
}
function LogOff() {
    $.ajax({
        type: "post",
        url: "/Home/Login",
        data: "domain=" + domain + "&username=" + username + "&password=" + password,
        dataType: "json",
        success: function (data) {
            if (data.logined) {
                window.location.href = "/Dashboard/ActivityandLog";
            } else {
                $("#message").html(data.Message);
            }

        }
      ,
        error: function () {

            $("#message").html("Network interrupt, networking timeout");
        }

    });
}

function getCookie(name) { 
    var arr,reg=new RegExp("(^| )"+name+"=([^;]*)(;|$)"); 
    if(arr=document.cookie.match(reg)) 
        return unescape(arr[2]); 
    else
        return null;
} 
function setCookie(name,value) {
    var Days = 30;
    var exp = new Date();
    exp.setTime(exp.getTime()+ Days*24*60*60*1000);
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
}
function delCookie(name) {
    var exp = new Date(); exp.setTime(exp.getTime() - 1); 
    var cval = getCookie(name); if (cval != null) document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
}
function getCookie(name) {
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null) {
        return unescape(arr[2]);
    } else {
        return null;
    }
}