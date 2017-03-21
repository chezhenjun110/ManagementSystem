
var Name;
var dNSHostName;
var site;
var Description;
var operatingSystem;
var operatingSystemVersion;
var oupathHidde;
var formated;
var ComputerModifySubmited = false;
function Computer_Clear() {
    document.getElementById("searchkeyword").value = "";
    document.getElementById("searchclear").disabled = true;
}
function Computer_Detail_Cancle() {
    $("#MiddleMess").html("");
    document.getElementById("modifybtn").disabled = true;
    document.getElementById("Description").style.color = "black";
     document.getElementById("Description").value = Description;
    document.getElementById("operatingSystem").value = operatingSystem;
    document.getElementById("operatingSystemVersion").value = operatingSystemVersion;
    document.getElementById("canclebtn").disabled = true;
 
}
function Computer_entersearch() {                    //回车键搜索
    //alert(dd);
    var event = window.event || arguments.callee.caller.arguments[0];
    if (event.keyCode == 13) {
        computer_search();
    }
}
var table;
function computer_search() {
    $("#message").html("");
    $("#MiddleMess").html("");
    document.getElementById("Name").value = "";
    document.getElementById("dNSHostName").value = "";
    document.getElementById("site").value = ""; 
    document.getElementById("Description").value = ""; 
    document.getElementById("operatingSystem").value = ""; 
    document.getElementById("operatingSystemVersion").value = "";
    $("#tablespace").html("<h4 style=\"color:orange\">Searching....Please wait</h4>");
    $.ajax({
        type: "post",
        url: "/ComputerManagement/SearchModify",
        data: "domain=" + $("#domain").val() + " &searchcriteria=" + $("#searchcriteria").val() + "&searchkeyword=" + $("#searchkeyword").val(),
        dataType: "json",
        success: function (data) {
            $("#tablespace").html(data.Message);
            table= $('#example').DataTable();
            $('#example tbody').on('click', 'tr', function () {
                if ($(this).hasClass('selected')) {
                    $(this).removeClass('selected');
                }
                else {
                    table.$('tr.selected').removeClass('selected');
                    $(this).addClass('selected');
                }
            });
        }
        ,
        error: function () {
            $("#message").html("Network interrupt, networking timeout");
        }
    });
}
var targrt;
var computer_detail = function (obj) {                       //获取点击用户的详细信息
    Clear();
    document.getElementById("Name").style.color = "gray";
    document.getElementById("site").style.color = "gray";
    document.getElementById("Description").style.color = "black";
    document.getElementById("dNSHostName").style.color = "gray";
    document.getElementById("operatingSystem").style.color = "gray";
    document.getElementById("operatingSystemVersion").style.color = "gray";
    
   
    $("#MiddleMess").html(""); document.getElementById("TabContent").style.visibility = "visible";
    var computername = $(obj).children('td').next().html();
    var upName = $(obj).children('td').next().next().html();
    var domain = "@";
    var ou = upName.split(",DC=");
    var len = ou.length;
    for (var j = 1; j < len; j++) {
        var item = "." + upName.split(",DC=")[j];
        domain += item;
    }
    var fin = domain.split("@.")[1];
    if (ComputerModifySubmited) {
        if (confirm("Whether to submit the current user's modification?")) {
            Computer_update();
        
        }
        else {
            ComputerModifySubmited = false;
            table.$('tr.ModifyState').removeClass('ModifyState');
        }
    }
    $.ajax({
        type: "post",
        url: "/ComputerManagement/Details",
        data: "computername=" + computername+"&domainname="+fin,
        dataType: "json",
        success: function (data) {
           
            $('#combotree').combotree('setText', data.OUName);
            table.$('tr.ModifyState').removeClass('ModifyState');
            targrt = obj;
            document.getElementById("deletebtn").disabled = false;
            table.$('tr.selected').addClass('ModifyState');
            $("#key").html(data.Name+","+fin);
            document.getElementById("Name").value = data.Name;
            Name=data.Name;
            document.getElementById("dNSHostName").value = data.dNSHostName; dNSHostName = data.dNSHostName;
            document.getElementById("site").value = data.site; site = data.site;
            document.getElementById("Description").value = data.Description; Description = data.Description;
            document.getElementById("operatingSystem").value = data.operatingSystem; operatingSystem = data.operatingSystem;
            document.getElementById("operatingSystemVersion").value = data.operatingSystemVersion; operatingSystemVersion = data.operatingSystemVersion;
            console.log(data.manageby);
            document.getElementById("OUpathhide").innerHTML = data.managedBy;
            oupathHidde = data.managedBy;
            if (data.managedBy != "") {
                var host = "1";
                var CNOUDC = data.managedBy;

                var CNOU = CNOUDC.split(",DC=");

                for (var i = 1; i < CNOU.length; i++) {
                    host = host + "." + CNOU[i];
                }
                var CN = CNOU[0].split(",OU=");
                for (var i = 1; i < CN.length; i++) {
                    host = host + "/" + CN[i];
                }
                host = host + "/" + CN[0].split("CN=")[1];
                host = host.split("1.")[1];
                console.log(host);
                document.getElementById("managedBy").style.color = "gray";
                document.getElementById("managedBy").value = host;
                formated = host;
                console.log(formated);
            }
        }
           ,
        error: function () {
            $("#MiddleMess").html("Network interrupt, networking timeout");
        }
    });
};
function Computer_update_Confirm() {
    if (confirm("Sure to submit the current user's modification?")) {
        Computer_update();
    }
    else {
        return;
    }
}
function Computer_update() {
   
    $("#MiddleMess").html("");//更新用户的信息
    var name = document.getElementById("key").innerHTML.split(",")[0];
    var domain = document.getElementById("key").innerHTML.split(",")[1];
    var computername = $("#Name").val();
    var site = $("#site").val();
    var Description = $("#Description").val();
    var manageby = document.getElementById("OUpathhide").innerHTML;
    var oupath;
    var ou1 = document.getElementById("ChoosedOu").innerHTML;
    console.log(ou1);
    if (ou1 != "") {
        var temp = document.getElementById("ChoosedOu").innerHTML.split('LDAP://')[1];
        oupath = temp.split('/')[1];
    } else {
        oupath = "";
    }
   
    $.ajax({
        type: "post",
        url: "/ComputerManagement/Update_Computer",
        data: "name=" + name + "&computername=" + computername + "&site=" + site + "&Description=" + Description + "&domain=" + domain + "&manageby=" + manageby + "&oupath=" + oupath,
        dataType: "json",
        success: function (data) {
            $("#MiddleMess").html(data.Message);
            if (data.logined) {
                
                var pageindex = table.page();
                console.log(table.row(targrt).data());
                var d = table.row(targrt).data();
                d[1] = computername;
             
                table.row(targrt).data(d).draw();
                table.page(pageindex).draw(false);
                table.$('tr.ModifyState').addClass('modified');
                table.$('tr.ModifyState').removeClass('ModifyState');
                table.$('tr.selected').addClass('ModifyState');
                document.getElementById("modifybtn").disabled = true;
                document.getElementById("managedBy").style.color = "black";
                ComputerModifySubmited = false;
                document.getElementById("Name").style.color = "black";
                document.getElementById("site").style.color = "black";
                document.getElementById("Description").style.color = "black";
                document.getElementById("canclebtn").disabled = true;
            }
        }
        ,
        error: function () {
            $("#MiddleMess").html("Network interrupt, networking timeout");
        }
    });
}
function Computer_Delete_firm() {
    //利用对话框返回的值 （true 或者 false）
    if (confirm("Are you sure you want to delete the computer?")) {
        //如果是true ，那么就把页面转向thcjp.cnblogs.com
        Computer_delete();
    }
    else {
    }
}
function Computer_delete() {
    table.row('.selected').remove().draw(false);
    $("#MiddleMess").html("");
    var domain = document.getElementById("key").innerHTML.split(",")[1];
    var computername = $("#Name").val();
    $.ajax({
        type: "post",
        url: "/ComputerManagement/Delete",
        data: "computername=" + computername + "&domain=" + domain,
        dataType: "json",
        success: function (data) {
            $("#MiddleMess").html(data.Message);
        }
        ,
        error: function () {
            $("#MiddleMess").html("Network interrupt, networking timeout");
        }
    });
}
function Computer_create() {                    //创建用户
    $("#message").html("Computer being created, please wait. . .");
    var domain = document.getElementById("Computerdomain").value;
    var computername = $("#computername").val();
    if (document.getElementById("ChoosedOu").innerHTML == "") {
        alert("OuPath cannot be empty!!");
        return;
    }
    var temp = document.getElementById("ChoosedOu").innerHTML.split('LDAP://')[1];
    var OuPath = temp.split('/')[1];
   
    var description = $("#description").val();
    $.ajax({
        type: "post",
        url: "/ComputerManagement/Create",
        data: "domain=" + domain + "&computername=" + computername + "&ou=" + OuPath + "&description=" + description,
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
function Computer_Create_Cancle() {
    document.getElementById("computername").value = "";
    document.getElementById("ou").value = "";
    document.getElementById("description").value = "";
    document.getElementById("Create_Cancle").disabled = true;
}
function Clear() {
    $('#combotree').combotree('setText', "");
   document.getElementById("Name").value = "";
    
    document.getElementById("dNSHostName").value = "";
 
    document.getElementById("site").value = "";

    document.getElementById("Description").value = "";
  
    document.getElementById("operatingSystem").value = "";
  
    document.getElementById("operatingSystemVersion").value = "";
    document.getElementById("managedBy").value = "";
       
}
var Computerchange = function (obj) {                       //获取点击用户的详细信息
    ComputerModifySubmited = true;
    $(obj).context.style.color = "green";
    document.getElementById("canclebtn").disabled = false;
    document.getElementById("modifybtn").disabled = false;
};
var times = 0;
$("#Computerdomain").on("change", function () {
    times += 1;
    if (times == 1) {
        var chosed = $("#Userdomain").val();
        if (document.getElementById("combotree") != null) {
            $('#combotree').combotree({
                url: '/UserManagement/Outree',
                required: true,
                onSelect: function (node) {
                    //alert(node.id);
                    document.getElementById("ChoosedOu").innerHTML = node.path;
                }
            });
        }
        times = 0;
    }
});
var ManageByTable;
function Computer_UserSearch() {
    $("#MiddleMess").html("");
    $("#message").html("");
    $("#ManageBytablespace").html("<h4 style=\"color:orange\">Searching....Please wait</h4>");

    $.ajax({
        type: "post",
        url: "/ComputerManagement/Computer_UserSearch",
        data: "domain=" + $("#Udomain").val() + "&searchfield=" + $("#Usearchfield").val() + " &searchcriteria=" + $("#Usearchcriteria").val() + "&searchkeyword=" + $("#Usearchkeyword").val(),
        dataType: "json",
        success: function (data) {

            $("#ManageBytablespace").html(data.Message);
            ManageByTable = $('#example2').DataTable({
                "bAutoWidth": false,                   //是否启用自动适应列宽
                "aoColumns": [                          //设定各列宽度   
                                       { "sWidth": "50px" },
                                       { "sWidth": "*" }, { "sWidth": "*" }]
            });
            $('#example2 tbody').on('click', 'tr', function () {
                var d = ManageByTable.row(this).data();
                console.log(d[2]);
                document.getElementById("OUpathhide").innerHTML = d[2];
                var host = "1";
                var b = d[2].split(",");
                var CNOUDC = "";
                for (var i = 0; i < b.length; i++) {
                    CNOUDC += b[i];
                    console.log(CNOUDC);
                }

                var CNOU = CNOUDC.split("DC=");

                for (var i = 1; i < CNOU.length; i++) {
                    host = host + "." + CNOU[i];
                }
                var CN = CNOU[0].split("OU=");
                for (var i = 1; i < CN.length; i++) {
                    host = host + "/" + CN[i];
                }
                var c = CN[0].split("CN=");
                for (var i = 1; i < c.length; i++) {
                    host = host + "/" + c[i];
                }

                host = host.split("1.")[1];
                console.log(host);
                document.getElementById("managedBy").value = host;
                document.getElementById("managedBy").style.color = "green";
              
                document.getElementById("canclebtn").disabled = false;
                UserModifySubmited = true;
                document.getElementById("modifybtn").disabled = false;
            });
        }
        ,
        error: function () {
            $("#message").html("Network interrupt, networking timeout");
        }
    });
}
function ouchange() {
    document.getElementById("ManageByTab").style.visibility = "visible";
}
function clearousearch() {
    document.getElementById("ManageByTab").style.visibility = "hidden";
    document.getElementById("OUpathhide").innerHTML = oupathHidde;
    console.log(formated);
    document.getElementById("managedBy").value = formated;
   

}
function Computer_MoveOu() {
    var name = document.getElementById("key").innerHTML.split(",")[0];
    
    var domain = document.getElementById("key").innerHTML.split(",")[1];
   

    $.ajax({
        type: "post",
        url: "/ComputerManagement/Computer_MoveOu",
        data: "name=" + name + "&domain=" + domain + "&oupath=" + oupath,
        dataType: "json",
        success: function (data) {
            
           
        }
        ,
        error: function () {
            $("#MiddleMess").html("Network interrupt, networking timeout");
        }
    });
}