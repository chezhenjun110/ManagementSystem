var emailname;
var Database;
var SendAsDelegates;
var FMADelegates;
var DispayName;
var BlackBerryEnabled;
var RestrictedUsage;
var HideFromOAB;
var IMAPEnabled;
var POP3Enabled;
var EmailDomian1;
var DisplayName;
var ActiveSyncMailboxPolicys1;
var Emailboxquotas1;
function Exchange_Cancle() {
    document.getElementById("Emailname").value =emailname;
    document.getElementById("Database").value = Database;
    document.getElementById("EmailDomian").value = EmailDomian1;
    document.getElementById("ActiveSyncMailboxPolicys").value = ActiveSyncMailboxPolicys1;
    document.getElementById("Emailboxquotas").value = Emailboxquotas1;
    document.getElementById("SendAsDelegates").value =SendAsDelegates;
    document.getElementById("FMADelegates").value =FMADelegates;
    if (BlackBerryEnabled) {
        document.getElementById("BlackBerryEnabled").checked = true;
    } else {
        document.getElementById("BlackBerryEnabled").checked = false;
    } 
   
    if (RestrictedUsage) {
        document.getElementById("RestrictedUsage").checked = true;
    } else {
        document.getElementById("RestrictedUsage").checked = false;
    }
    if (HideFromOAB) {
        document.getElementById("HideFromOAB").checked = true;
    } else {
        document.getElementById("HideFromOAB").checked = false;
    }
        if (IMAPEnabled) {
        document.getElementById("IMAPEnabled").checked = true;
        } else {
            document.getElementById("IMAPEnabled").checked = false;
        }
        if (POP3Enabled) {
        document.getElementById("POP3Enabled").checked = true;

        } else {
            document.getElementById("POP3Enabled").checked = false;
        }
        document.getElementById("canclebtn").disabled = true;
}
function Exchange_Search() {
    detailClear();
    $("#message").html("");
    $("#tablespace").html("<h4 style=\"color:orange\">Searching....Please wait</h4>");
    $.ajax({
        type: "post",
        url: "/ExchangeManagement/SearchModify",
        data: "domain=" + $("#domain").val() + "&searchfield=" + $("#searchfield").val() + " &searchcriteria=" + $("#searchcriteria").val() + "&searchtype=" + $("#searchtype").val() + "&searchkeyword=" + $("#searchkeyword").val(),
        dataType: "json",
        success: function (data) {
            $("#tablespace").html(data.Message);
            table = $('#example').DataTable();
        }
        ,
        error: function () {
            $("#message").html("Network interrupt, networking timeout");
        }
    });
}
function Exchange_EnterSearch() {
    var event = window.event || arguments.callee.caller.arguments[0];
    if (event.keyCode == 13) {
        Exchange_Search();
    }
}
var Exchange_detail = function (obj) {                       //获取点击用户的详细信息
    detailClear();
    $("#message").html("");  
    var oObj = event.srcElement;                               //表格选中变色
    if (oObj.tagName.toLowerCase() == "td") {
        var oTr = oObj.parentNode;
        for (var i = 1; i < document.all.example.rows.length; i++) {
            document.all.example.rows[i].style.backgroundColor = "";
            document.all.example.rows[i].tag = false;
        }
        oTr.style.backgroundColor = "#AED0EA";
        oTr.tag = true;
    }
    var userid = $(obj).children('td').next().html();
    var domain= $("#domain").val();
    $.ajax({
        type: "post",
        url: "/ExchangeManagement/Details",
        data: "userid=" + userid + "&domainname=" + domain,
        dataType: "json",
        success: function (data) {
            document.getElementById("ExchangeTab").style.visibility = "visible";
            $("#key").html(userid + "@" + domain);
           var db = "<option value=\"" + data.homeMDB + "\">" + data.homeMDB + "</option>";
            if (data.MDBs.length>0) {
                for (var i = 0; i < data.MDBs.length; i++) {
                    var mess = data.MDBs[i].Name;
                    if (mess != data.homeMDB) {
                        db += "<option value=\"" + mess + "\">" + mess + "</option>";
                    }

                }
            }
            document.getElementById("Database").innerHTML = db;
            Database = data.homeMDB;
            document.getElementById("Emailname").value = data.PrimaryEmailAddress.split("@")[0];
            emailname = data.PrimaryEmailAddress.split("@")[0];
             document.getElementById("AccountType").value = data.AccountType;
            document.getElementById("SendAsDelegates").value = data.SendAsDelegates;
            SendAsDelegates = data.SendAsDelegates;
            document.getElementById("FMADelegates").value = data.FMADelegates;
            FMADelegates = data.FMADelegates;
            document.getElementById("DisplayName").value = data.DisplayName;
            DisplayName = data.DisplayName;
            document.getElementById("DataCenter").value = data.DataCenter;
            document.getElementById("LinkedAccountName").value = data.LinkedAccount[1];
            document.getElementById("LinkedAccountDomain").value = data.LinkedAccount[0] + "\\";
            if (data.BlackBerryEnabled) {
                document.getElementById("BlackBerryEnabled").checked = true;
            }
            if (data.RestrictedUsage) {
                document.getElementById("RestrictedUsage").checked = true;
            }
            if (data.HideFromOAB) {
                document.getElementById("HideFromOAB").checked = true;
            } if (data.IMAPEnabled) {
                document.getElementById("IMAPEnabled").checked = true;
            } if (data.POP3Enabled) {
                document.getElementById("POP3Enabled").checked = true;
                
            } if (data.RestrictedUsage) {
                document.getElementById("RestrictedUsage").checked = true;
            }
            BlackBerryEnabled = data.BlackBerryEnabled;
            RestrictedUsage = data.RestrictedUsage;
            HideFromOAB = data.HideFromOAB;
            IMAPEnabled = data.IMAPEnabled;
            POP3Enabled = data.POP3Enabled;
            var EmailDomian = "<option value=\"" + data.PrimaryEmailAddress.split("@")[1] + "\">" + data.PrimaryEmailAddress.split("@")[1] + "</option>";              //emailaddress的动态下拉框
            for (var i = 0; i < data.AcceptedDomain.length; i++) {
                var mess = data.AcceptedDomain[i];
                if (mess != data.PrimaryEmailAddress.split("@")[1]) {
                    EmailDomian += "<option value=\"" + mess + "\">" + mess + "</option>"
                }
            }
            document.getElementById("EmailDomian").innerHTML = EmailDomian;
            EmailDomian1 = data.PrimaryEmailAddress.split("@")[1];
           var ASMP= "";
for (var i = 0; i < data.ActiveSyncMailboxPolicys.length; i++) {
                var mess = data.ActiveSyncMailboxPolicys[i];
                ASMP += "<option value=\"" + mess + "\">" + mess + "</option>"
            }
            
            document.getElementById("ActiveSyncMailboxPolicys").innerHTML = ASMP;
            ActiveSyncMailboxPolicys1 = data.ActiveSyncMailboxPolicys[0];
            var Emailboxquotas = "<option value=\"" + data.MailboxQuota.Name + "\">" + data.MailboxQuota.Name + "</option>";
           
            for (var i = 0; i < data.QuotaPlan.length; i++) {
                var mess = data.QuotaPlan[i].Name;
                if (mess != data.MailboxQuota.Name) {
                    Emailboxquotas += "<option value=\"" + mess + "\">" + mess + "</option>"
                }
            }
            document.getElementById("Emailboxquotas").innerHTML = Emailboxquotas;
            Emailboxquotas1 = data.MailboxQuota.Name;
        }
           ,
        error: function () {

            $("#message").html("Network interrupt, networking timeout");
        }

    });
};
function ExchangModfiy() {
    var userid = document.getElementById("key").innerHTML.split("@")[0];
    var domain = $("#domain").val();
    var DisplayName= document.getElementById("DisplayName").value;
    var emailname = document.getElementById("Emailname").value;
    var Database = document.getElementById("Database").value;
   var AccountType = document.getElementById("AccountType").value;
    var SendAsDelegates = document.getElementById("SendAsDelegates").value;
    var FMADelegates = document.getElementById("FMADelegates").value;
   var BlackBerryEnabled = document.getElementById("BlackBerryEnabled").checked ? true : false;
    var RestrictedUsage = document.getElementById("RestrictedUsage").checked ? true : false;
     var HideFromOAB = document.getElementById("HideFromOAB").checked ? true : false;
        var IMAPEnabled = document.getElementById("IMAPEnabled").checked ? true : false;
        var POP3Enabled = document.getElementById("POP3Enabled").checked ? true : false;
         var EmailDomian = document.getElementById("EmailDomian").value;
      var ActiveSyncMailboxPolicys = document.getElementById("ActiveSyncMailboxPolicys").value;
      var Emailboxquotas = document.getElementById("Emailboxquotas").value;
      $.ajax({
        type: "post",
        url: "/ExchangeManagement/ExchangModfiy",
        data: "userid=" + userid + "&domain=" + domain + "&FMADelegates=" + FMADelegates 
            + "&BlackBerryEnabled=" + BlackBerryEnabled+ "&RestrictedUsage=" + RestrictedUsage + "&HideFromOAB=" + HideFromOAB + "&IMAPEnabled=" + IMAPEnabled + "&POP3Enabled=" + POP3Enabled
      + "&ActiveSyncMailboxPolicys=" + ActiveSyncMailboxPolicys + "&Emailboxquotas=" + Emailboxquotas
     + "&Database=" + Database + "&EmailDomian=" + EmailDomian + "&emailname=" + emailname + "&DisplayName=" + DisplayName,
        dataType: "json",
        success: function (data) {
            $("#message").html(data.Message);

        }
         ,
        error: function () {

            $("#message").html("Network interrupt, networking timeout");
        }

    });
}


var Email = 0;
$("#Emailboxquotas").on("change", function () {
    Email += 1;
  
    if (Email == 1) {
        var Emailboxquotas = $("#Emailboxquotas").val();
       
        if (Emailboxquotas == "Unlimited") {
            
            document.getElementById("RestrictedUsage").checked = true;
        }

        Email = 0;
    }

});
function Restrictedusage() {
    if (document.getElementById("RestrictedUsage").checked) {
        
        document.getElementById("Emailboxquotas").value = "Unlimited";
    }
}
var Exchange_Cache;
var Exchange_Gettextlen = function (obj) {
    Exchange_Cache = $(obj).context.value;
    console.log(Exchange_Cache);
}
var Exchange_change = function (obj) {                       //获取点击用户的详细信息
    
    var reg = /^[\s　]|[ ]$/gi;
    var girlfirend = $(obj).context;
    var text = girlfirend.value;
    console.log(text);
    if (text == Exchange_Cache) {
        girlfirend.style.color = "black";
        return;
    }
    if (!reg.test(text)) {
        girlfirend.style.color = "green";
        UserModifySubmited = true;

        document.getElementById("canclebtn").disabled = false;
        document.getElementById("modifybtn").disabled = false;
        console.log("没有空格");
    } else {
        console.log("有空格");
        return;
    }

};
function detailClear() {
    document.getElementById("DisplayName").value = "";
    document.getElementById("AccountType").value = "";
    document.getElementById("DataCenter").value = "";
    document.getElementById("LinkedAccountDomain").value = "";
    document.getElementById("LinkedAccountName").value = "";
    document.getElementById("Emailname").value = "";
    document.getElementById("Database").value = "";
    document.getElementById("EmailDomian").value = "";
    document.getElementById("ActiveSyncMailboxPolicys").value = "";
    document.getElementById("Emailboxquotas").value = "";
    document.getElementById("SendAsDelegates").value = "";
    document.getElementById("FMADelegates").value = "";
    document.getElementById("BlackBerryEnabled").checked = false;
    document.getElementById("HideFromOAB").checked = false;
    document.getElementById("IMAPEnabled").checked = false;
    document.getElementById("POP3Enabled").checked = false;
    document.getElementById("RestrictedUsage").checked = false;
    document.getElementById("canclebtn").disabled = true;
}
function colorResort() {
    document.getElementById("DisplayName").style.color = "black";
    document.getElementById("Emailname").style.color = "black";
   document.getElementById("SendAsDelegates").style.color = "black";
    document.getElementById("FMADelegates").style.color = "black";
    document.getElementById("BlackBerryEnabled").checked = false;
    document.getElementById("HideFromOAB").checked = false;
    document.getElementById("IMAPEnabled").checked = false;
    document.getElementById("POP3Enabled").checked = false;
    document.getElementById("RestrictedUsage").checked = false;
    document.getElementById("canclebtn").disabled = true;
}