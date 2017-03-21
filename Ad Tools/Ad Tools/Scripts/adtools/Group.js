var groupname1;
var description;
var email;
var isSecurityGroup;
var note;
var GroupModifySubmited = false;
var oupathHidde;
var formatedou;
var MemberOfTable;
var Members;
function Group_Memberof_Search_begin() {
    var event = window.event || arguments.callee.caller.arguments[0];
    if (event.keyCode == 13) {
        Group_Memberof_Search();
    }
}
function Group_radioClick() {
    document.getElementById("canclebtn").disabled = false;
    document.getElementById("modifybtn").disabled = false;
}
function Group_Detail_Cancle() {
    $("#MiddleMess").html("");
    
    document.getElementById("canclebtn").disabled = true;
    document.getElementById("modifybtn").disabled = true;
    document.getElementById("groupname").style.color = "black";
    document.getElementById("description").style.color = "black";
    document.getElementById("email").style.color = "black";
    document.getElementById("groupname").value = groupname1;
    document.getElementById("description").value = description;
    document.getElementById("email").value =email;
    document.getElementById("note").value = note;
    if (isSecurityGroup) {
        document.getElementById("Security").checked = true;
    }
    else {
        document.getElementById("Distribution").checked = true;
    }
    $("#membertable").html(MemberOfTable);
    Memberoftable = $('#Memberof').DataTable({
        "bAutoWidth": false,                   //是否启用自动适应列宽
        "aoColumns": [                          //设定各列宽度   
                               { "sWidth": "50px" },
                               { "sWidth": "*" }]
    });
    $('#Memberof tbody').on('click', 'tr', function () {
        console.log(Memberoftable.row(this).data());
        Memberoftable.row(this).remove().draw(false);
        document.getElementById("canclebtn").disabled = false;
        document.getElementById("modifybtn").disabled = false;
        UserModifySubmited = true;
    });
    $("#Memberstable").html(Members);
  
    MembersTable = $('#Mem').DataTable({
        "bAutoWidth": false,
        //是否启用自动适应列宽
        "aoColumns": [                          //设定各列宽度   
                               { "sWidth": "50px" },
                               { "sWidth": "*" }]
    });
    $('#Mem tbody').on('click', 'tr', function () {
        console.log(MembersTable.row(this).data());
        MembersTable.row(this).remove().draw(false);
        document.getElementById("canclebtn").disabled = false;
        document.getElementById("modifybtn").disabled = false;
        UserModifySubmited = true;
    });
}
function Group_entersearch() {                    //回车键搜索
    //alert(dd);
    var event = window.event || arguments.callee.caller.arguments[0];
    if (event.keyCode == 13) {
        Group_search();
    }
}
function Group_create_clear() {
    document.getElementById("groupname").value = "";
    document.getElementById("Create_Cancle").disabled = true;
}
function Group_search_clear() {
    document.getElementById("searchkeyword").value = "";
    document.getElementById("searchclear").disabled = true;
}

var table;
function Group_search() {
    document.getElementById("note").value = "";
    document.getElementById("groupname").value = "";
    document.getElementById("description").value = "";
    document.getElementById("email").value = "";
    document.getElementById("ManageBytablespace").value = "";
    document.getElementById("ManageByTab").style.visibility = "hidden";
    document.getElementById("Usearchkeyword").value = "";
    for (var i = 0; i < 3; i++) {
        var scope = "GroupScope" + i;
        document.getElementById(scope).checked = false;
    }
   
        document.getElementById("Security").checked = false;
        document.getElementById("Distribution").checked = false;
    $("#message").html("");
    $("#tablespace").html("<h4 style=\"color:orange\">Searching....Please wait</h4>");
    $.ajax({
        type: "post",
        url: "/GroupsManagement/Search",
        data: "domain=" + $("#domain").val()  + "&searchkeyword=" + $("#searchkeyword").val(),
        dataType: "json",
        success: function (data) {

            $("#tablespace").html(data.Message);
             table= $("#example").DataTable();
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
var Memberoftable;
var targrt;
var MembersTable;
var Group_detail = function (obj) {                       //获取点击用户的详细信息    
    document.getElementById("ManageBytablespace").value = "";
    document.getElementById("ManageByTab").style.visibility = "hidden";
    document.getElementById("MembersAddSearch").style.visibility = "hidden";
    document.getElementById("Usearchkeyword").value = "";
    $('#combotree').combotree('setText', "");
    document.getElementById("ManageByName").value = ""; document.getElementById("TabContent").style.visibility = "visible";
    var groupname = $(obj).children('td').next().html();
    var upName = $(obj).children('td').next().next().html();
    var domain = "@";
    var ou = upName.split(",DC=");
    var len = ou.length;
    for (var j = 1; j < len; j++) {
        var item = "." + upName.split(",DC=")[j];
        domain += item;
    }
    var fin = domain.split("@.")[1];
    $("#MiddleMess").html("");
 
    document.getElementById("groupname").style.color = "black";
    document.getElementById("description").style.color = "black";
    document.getElementById("email").style.color = "black";
    
    if (GroupModifySubmited) {
        if (confirm("Whether to submit the current user's modification?")) {
            Group_update();
           
        }
        else {
            GroupModifySubmited = false;
            table.$('tr.ModifyState').removeClass('ModifyState');
        }
    }
    $.ajax({
        type: "post",
        url: "/GroupsManagement/Details",
        data: "groupname=" + groupname + "&domain=" + fin,
        dataType: "json",
        success: function (data) {
            
            $('#combotree').combotree('setText', data.BelongsOUPath);
            targrt = obj;
            table.$('tr.ModifyState').removeClass('ModifyState');
            table.$('tr.selected').addClass('ModifyState');
            $("#key").html(data.SamAccountName + "," + fin);
            document.getElementById("deletebtn").disabled = false;
            document.getElementById("note").value = data.Note;
            note = data.Note;
            document.getElementById("groupname").value = data.SamAccountName;
            groupname1 = data.SamAccountName;
            document.getElementById("description").value = data.Description; description = data.Description;
            document.getElementById("email").value = data.Email; email = data.Email;
            isSecurityGroup = data.isSecurityGroup;
            var scope="GroupScope"+data.GroupScope;
            document.getElementById(scope).checked = true;
            $("#membertable").html(data.memebertable);
            MemberOfTable = data.memebertable;
            Memberoftable = $('#Memberof').DataTable({
                "bAutoWidth": false,
                                    //是否启用自动适应列宽
                "aoColumns": [                          //设定各列宽度   
                                       { "sWidth": "50px" },
                                       { "sWidth": "*" }]
            });
            $('#Memberof tbody').on('click', 'tr', function () {
                console.log(Memberoftable.row(this).data());
                Memberoftable.row(this).remove().draw(false);
                document.getElementById("canclebtn").disabled = false;
                document.getElementById("modifybtn").disabled = false;
                UserModifySubmited = true;
            });
            $("#Memberstable").html(data.members);
            Members= data.members;
            MembersTable = $('#Mem').DataTable({
                "bAutoWidth": false,
                //是否启用自动适应列宽
                "aoColumns": [                          //设定各列宽度   
                                       { "sWidth": "50px" },
                                       { "sWidth": "*" }]
            });
            $('#Mem tbody').on('click', 'tr', function () {
                console.log(MembersTable.row(this).data());
                MembersTable.row(this).remove().draw(false);
                document.getElementById("canclebtn").disabled = false;
                document.getElementById("modifybtn").disabled = false;
                UserModifySubmited = true;
            });
            
            if (data.isSecurityGroup) {
                document.getElementById("Security").checked = true;
            }
            else {
                document.getElementById("Distribution").checked = true;
            }
            document.getElementById("OUpathhide").innerHTML = data.ManagedBy;
            oupathHidde = data.ManagedBy;
           
            if (data.ManagedBy != "") {
                var host = "1";
                var b = data.ManagedBy.split(",");
                var CNOUDC = "";
                for (var i = 0; i < b.length; i++) {
                    CNOUDC += b[i];
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
                document.getElementById("ManageByName").style.color = "gray";
                document.getElementById("ManageByName").value = host;
                formatedou = host;
            }
           
        }
           ,
        error: function () {

            $("#MiddleMess").html("Network interrupt, networking timeout");
        }

    });
};
function Group_update_Confirm() {
    if (confirm("Sure to submit the current group's modification?")) {
        Group_update();
    }
    else {
        return;
    }
}
function Group_update() {
    var oupath;
    var ou1 = document.getElementById("ChoosedOu").innerHTML;
    console.log(ou1);
    if (ou1 != "") {
        var temp = document.getElementById("ChoosedOu").innerHTML.split('LDAP://')[1];
        oupath = temp.split('/')[1];
    } else {
        oupath = "";
    }
    var groupscope;
    var grouptype;
    if (document.getElementById("Security").checked) {
        grouptype = "Security";
    } else {
        grouptype = "Distribution";
    }
    if (document.getElementById("GroupScope0").checked) {
        groupscope = "DomainLocale";
    } else if ((document.getElementById("GroupScope1").checked)) {
        groupscope = "Gloable";
    } else {
        groupscope = "Universal";
    }
    var name = document.getElementById("key").innerHTML.split(",")[0];     //更新用户的信息
    $("#MiddleMess").html("");
    var groupname = $("#groupname").val();
    var domain = document.getElementById("key").innerHTML.split(",")[1];
    var description = $("#description").val();
    var email = $("#email").val();
    var note = $("#note").val();
    var manageby = document.getElementById("OUpathhide").innerHTML;
    var numberof = "";
    var rownum1 = document.getElementById("Memberof_info").innerHTML.split(" ")[5];
    for (var i = 0; i < rownum1; i++) {
        console.log(Memberoftable.row(i).data()[1]);
        numberof += Memberoftable.row(i).data()[1] + ",";
    }
    console.log(numberof);
    var Members = "";
    var rownum = document.getElementById("Mem_info").innerHTML.split(" ")[5];
    for (var i = 0; i < rownum; i++) {
        console.log(MembersTable.row(i).data()[1]);
        Members += MembersTable.row(i).data()[1] + ",";
    }
    $.ajax({
        type: "post",
        url: "/GroupsManagement/Update",
        data: "name=" + name + "&groupname=" + groupname + "&domain=" + domain + "&description=" + description + "&email=" + email +
            "&note=" + note + "&manageby=" + manageby + "&numberof=" + numberof + "&Members=" + Members + "&oupath=" + oupath + "&groupscope=" + groupscope + "&grouptype=" + grouptype,
        dataType: "json",
        success: function (data) {
            $("#MiddleMess").html(data.Message);
            if (data.logined) {
                var pageindex = table.page();
                console.log(table.row(targrt).data());
                var d = table.row(targrt).data();
                d[1] = groupname;
                d[3] = description;
                table.row(targrt).data(d).draw();
                table.page(pageindex).draw(false);
                table.$('tr.ModifyState').addClass('modified');
                table.$('tr.ModifyState').removeClass('ModifyState');
                table.$('tr.selected').addClass('ModifyState');
                document.getElementById("modifybtn").disabled = true;
                document.getElementById("ManageByName").style.color = "black";
                GroupModifySubmited = false;
                document.getElementById("groupname").style.color = "black";
                document.getElementById("description").style.color = "black";
                document.getElementById("email").style.color = "black";
                document.getElementById("note").style.color = "black";
                document.getElementById("canclebtn").disabled = true;
            }
            
        }
        ,
        error: function () {

            $("#MiddleMess").html("Network interrupt, networking timeout");
        }

    });


}
function Group_delete_confirm() {

    //利用对话框返回的值 （true 或者 false）

    if (confirm("Are you sure you want to delete the group?")) {

        //如果是true ，那么就把页面转向thcjp.cnblogs.com

        Group_delete();

    }

    else {

        //alert("你按了取消，那就是返回false");

    }
}
function Group_delete() {                                  //点击删除某个用户
    table.row('.selected').remove().draw(false);
    var groupname = document.getElementById("key").innerHTML.split(",")[0];     //更新用户的信息
    $("#MiddleMess").html("");
   
    var domain = document.getElementById("key").innerHTML.split(",")[1];
    
    $.ajax({
        type: "post",
        url: "/GroupsManagement/Delete",
        data: "groupname=" + groupname + "&domain=" + domain,
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
function Group_create() {
    $("#message").html("Creating, please wait");
    if (document.getElementById("ChoosedOu").innerHTML == "") {
        alert("OuPath cannot be empty！！");
        return;
    }

    var temp = document.getElementById("ChoosedOu").innerHTML.split('LDAP://')[1];
    var OuPath = temp.split('/')[1];
    var groupname = document.getElementById("groupname").value;
    var domain = $("#domain").val();
    var grouptype;
    var groupscope;
    var EnableExchange;
    if (document.getElementById("Security").checked) {
        grouptype = "Security";
    } else {
        grouptype = "Distribution";
    }
    if (document.getElementById("DomainLocale").checked) {
        groupscope = "DomainLocale";
    } else if ((document.getElementById("Gloable").checked)) {
        groupscope = "Gloable";
    } else {
        groupscope = "Universal";
    }
    if (document.getElementById("EnableExchange").checked) {
        EnableExchange = "true";
        groupscope = "Universal";
        document.getElementById("Universal").checked = true;
    } else {
        EnableExchange = "false";
    }
    


    $.ajax({
        type: "post",
        url: "/GroupsManagement/Create",
        data: "domain=" + domain + "&groupname=" + groupname + "&grouptype=" + grouptype + "&groupscope=" + groupscope + "&EnableExchange=" + EnableExchange + "&OuPath=" + OuPath,
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
var Groupchange = function (obj) {                       //获取点击用户的详细信息
    var reg = /^[\s　]|[ ]$/gi;
    var girlfirend = $(obj).context;
    var text = girlfirend.value;
    console.log(text);
    if (text == Group_Cache) {
        girlfirend.style.color = "black";
        GroupModifySubmited = false;
        return;
    }
    if (!reg.test(text)) {
        girlfirend.style.color = "green";
        GroupModifySubmited = true;

        document.getElementById("canclebtn").disabled = false;
        document.getElementById("modifybtn").disabled = false;
        console.log("没有空格");
    } else {
        console.log("有空格");
        return;
    }


};
var Group_Cache;
var Group_Gettextlen = function (obj) {
 
        Group_Cache = $(obj).context.value;
        console.log(Group_Cache);
 
}
function Group_Memberof_Search() {
$.ajax({
        type: "post",
        url: "/UserManagement/Group_GroupSearch",
        data: "groupdomain=" + $("#groupdomain").val() + "&searchkeyword=" + $("#groupkeyword").val(),
        dataType: "json",
        success: function (data) {
            $("#Grouptablespace").html(data.Message);
            var GroupChooser = $("#GroupChose").DataTable();
            $('#GroupChose tbody').on('click', 'tr', function () {
                console.log(GroupChooser.row(this).data());
                var rownum = document.getElementById("Memberof_info").innerHTML.split(" ")[5];
                var d = GroupChooser.row(this).data();
                console.log(d[1]);
                var arr = new Array()
                for (var i = 0; i < rownum; i++) {
                    arr[i] = Memberoftable.row(i).data()[1];
                }
                if (arr.indexOf(d[1]) != -1) {
                    alert("You selected group name already exists!");
                    console.log("存在");
                }
                else {
                    document.getElementById("canclebtn").disabled = false;
                    document.getElementById("modifybtn").disabled = false;
                    Memberoftable.row.add([
                         rownum,
                         d[1],
                    ]).draw().nodes().to$().addClass('modified');
                    Memberoftable.row(1).$('tr').addClass('ModifyState');
                    GroupChooser.row(this).remove().draw(false);
                    UserModifySubmited = true;
                    console.log("不存在");
                }
            });
        }
       ,
        error: function () {
            $("#MiddleMess").html("Network interrupt, networking timeout");
        }

    });
}
var ManageByTable;
function Group_UserSearch() {
    $("#MiddleMess").html("");
    $("#message").html("");
    $("#ManageBytablespace").html("<h4 style=\"color:orange\">Searching....Please wait</h4>");

    $.ajax({
        type: "post",
        url: "/GroupsManagement/Group_UserSearch",
        data: "Udomain=" + $("#Udomain").val() + "&Usearchfield=" + $("#Usearchfield").val() + " &Usearchcriteria=" + $("#Usearchcriteria").val() + "&Usearchkeyword=" + $("#Usearchkeyword").val(),
        dataType: "json",
        success: function (data) {

            $("#ManageBytablespace").html(data.Message);
            ManageByTable = $('#example1').DataTable({
                "bAutoWidth": false,                   //是否启用自动适应列宽
                "aoColumns": [                          //设定各列宽度   
                                       { "sWidth": "50px" },
                                       { "sWidth": "*" },
                { "sWidth": "*" },
                ]
            });
            $('#example1 tbody').on('click', 'tr', function () {
                var d = ManageByTable.row(this).data();
                console.log(d[2]);
                document.getElementById("OUpathhide").innerHTML = d[2];
                var host = "1";
                var b = d[2].split(",");
                var CNOUDC = "";
                for (var i = 0; i<b.length; i++) {
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
                document.getElementById("ManageByName").value = host;
                document.getElementById("ManageByName").style.color = "green";
                document.getElementById("canclebtn").disabled = false;
                UserModifySubmited = true;
                document.getElementById("modifybtn").disabled = false;
                
               
              
            });
        }
        ,
        error: function () {
            $("#MiddleMess").html("Network interrupt, networking timeout");
        }
    });
}
function ouchange() {
    document.getElementById("ManageByTab").style.visibility = "visible";
}
function clearOusearch() {
    document.getElementById("ManageByTab").style.visibility = "hidden";
    document.getElementById("OUpathhide").innerHTML = oupathHidde;
    document.getElementById("ManageByName").value = formatedou;
  


}
function MembersAdd() {
    document.getElementById("MembersAddSearch").style.visibility = "visible";
}
var MembersDataTable;
function Group_UserSearch_Members() {
    $("#MiddleMess").html("");
    $("#message").html("");
    $("#Memberstablespace").html("<h4 style=\"color:orange\">Searching....Please wait</h4>");

    $.ajax({
        type: "post",
        url: "/GroupsManagement/Group_UserSearch_Members",
        data: "Mdomain=" + $("#Mdomain").val() + "&Msearchfield=" + $("#Msearchfield").val() + " &Msearchcriteria=" + $("#Msearchcriteria").val() + "&Msearchkeyword=" + $("#Msearchkeyword").val(),
        dataType: "json",
        success: function (data) {

            $("#Memberstablespace").html(data.Message);
            MembersDataTable = $('#MembersData').DataTable({
                "bAutoWidth": false,                   //是否启用自动适应列宽
                "aoColumns": [                          //设定各列宽度   
                                       { "sWidth": "50px" },
                                       { "sWidth": "*" },
               
                ]
            });
            $('#MembersData tbody').on('click', 'tr', function () {
                var d = MembersDataTable.row(this).data();
                console.log(d);
                UserModifySubmited = true;
                document.getElementById("modifybtn").disabled = false;
                var rownum = document.getElementById("Mem_info").innerHTML.split(" ")[5];
               
                var arr = new Array()
                for (var i = 0; i < rownum; i++) {
                    arr[i] = MembersTable.row(i).data()[1];
                }
               
                if (arr.indexOf(d[1]) != -1) {
                    alert("You selected group name already exists!");
                    console.log("存在");
                }
                else {
                    document.getElementById("canclebtn").disabled = false;
                    document.getElementById("modifybtn").disabled = false;
                    MembersTable.row.add([
                         rownum,
                         d[1],
                    ]).draw().nodes().to$().addClass('modified');
                    MembersTable.row(1).$('tr').addClass('ModifyState');
                    MembersDataTable.row(this).remove().draw(false);
                    UserModifySubmited = true;
                    console.log("不存在");
                }

            });
           
        }
        ,
        error: function () {
            $("#MiddleMess").html("Network interrupt, networking timeout");
        }
    });
}

var cache = "";
var  ExchangeEnabled=function(obj) {
  //var groupname = $(obj).children('td').next().html();
    var groupname = $(obj).parent().parent().children('td').next().html();
    cache = groupname;
    console.log(groupname);
    var domain = $("#domain").val();
    console.log(domain);
    $.ajax({
        type: "post",
        url: "/GroupsManagement/ExchangeEnabled",
        data: "groupname=" + groupname + "&domain=" + domain,
        dataType: "json",
        success: function (data) {
            if (data.Message == "1") {
                window.open("ModifyExchange?keyword=" + groupname + "&domain=" + domain);
                window.targrt = "_blank";
            }
            else {
                if (confirm("Are you sure you want to Enable the Exchange?")) {
                    EnableGroupExchange();
                }
                else {
                }
            }

        }
      ,
        error: function () {
            $("#message").html("Network interrupt, networking timeout");
        }
    });
}
function EnableGroupExchange() {
    var groupname = cache;
    cache = "";
    console.log(groupname);
    var domain = $("#domain").val();
    console.log(domain);
    $.ajax({
        type: "post",
        url: "/GroupsManagement/EnableGroupExchange",
        data: "groupname=" + groupname + "&domain=" + domain,
        dataType: "json",
        success: function (data) {
            if (data.Message == "1") {
                window.open("ModifyExchange?keyword=" + groupname + "&domain=" + domain);
                window.targrt = "_blank";
            } else {
                alert("Enable Exchange Failed!")
            }

        }
      ,
        error: function () {

            $("#message").html("Network interrupt, networking timeout");
        }

    });
}
function Group_Exchange_Modify() {

    var userid = GetQueryString("keyword");
    var domain = GetQueryString("domain");
    var DisplayName = document.getElementById("DisplayName").value;
    var DLShortname = document.getElementById("DLShortname").value;
    var GroupName = document.getElementById("GroupName").value;
    var Emailname = document.getElementById("Emailname").value;
    var RequireSenderAuthenticationEnabled = document.getElementById("RequireSenderAuthenticationEnabled").checked ? true : false;
  
    var IndudeinGalsync = document.getElementById("IndudeinGalsync").checked ? true : false;
    var Description = document.getElementById("Description").value;
   var HideFromOAB = document.getElementById("HideFromAB").checked ? true : false;
    
    $.ajax({
        type: "post",
        url: "/GroupsManagement/ExchangModfiy",
        data: "userid=" + userid + "&domain=" + domain + "&DisplayName=" + DisplayName + "&DLShortname=" + DLShortname + "&GroupName="
            + GroupName + "&Emailname=" + Emailname + "&RequireSenderAuthenticationEnabled=" + RequireSenderAuthenticationEnabled + "&IndudeinGalsync=" + IndudeinGalsync + "&Description=" + Description
         + "&HideFromOAB=" + HideFromOAB,
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
function SaveChange() {
    var userid = "789456";
    var domain = "fcachina.ccdroot.cn";
    

    $.ajax({
        type: "post",
        url: "/GroupsManagement/Test",
        data: "keyword=" + userid + "&domain=" + domain,
        dataType: "json",
        success: function (data) {
            

        }
       ,
        error: function () {

            $("#message").html("Network interrupt, networking timeout");
        }

    });
}
function GetQueryString(name) {    //获取浏览器地址栏字符串
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
var times = 0;
$("#domain").on("change", function () {
    $("#MiddleMess").html("");
    times += 1;
    if (times == 1) {
        var chosed = $("#domain").val();
        console.log(chosed);
        if (document.getElementById("combotree") != null) {
            $('#combotree').combotree({
                url: '/UserManagement/Outree?domain=' + chosed,
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