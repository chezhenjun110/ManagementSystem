var UserId;
var UserPrincipalName;
var FirstName;
var LastName;
var DisplayName;
var Description;
var Office;
var Telephone;
var mail;
var UserModifySubmited = false;
var Address;
var PostOfficeBox;
var City;
var PostalCode;
var Province;
var profilePath;
var LoginScript;
var LocalPath;
var adminDescription;
var comment;
var Title;
var Department;
var Company;
var Pager;
var MobilePhone;
var admindisplayname;
var UserPrincipalDomain;
var MemberOfTable;
var groupchoseTable = "";
var keyupnname = "";
function User_entersearch() {                    //回车键搜索
    //alert(dd);
    var event = window.event || arguments.callee.caller.arguments[0];
    if (event.keyCode == 13) {
        Post();
    }
}
var table;
function Post() {              //搜索用户
    $("#MiddleMess").html("");
    $("#message").html("");
    $("#tablespace").html("<h4 style=\"color:orange\">Searching....Please wait</h4>");
    TabDateClear();
    $("#Grouptablespace").html("");
    $.ajax({
        type: "post",
        url: "/UserManagement/SearchModify",
        data: "domain=" + $("#domain").val() + "&searchfield=" + $("#searchfield").val() + " &searchcriteria=" + $("#searchcriteria").val() + "&searchtype=" + $("#searchtype").val() + "&searchkeyword=" + $("#searchkeyword").val(),
        dataType: "json",
        success: function (data) {

            $("#tablespace").html(data.Message);
            table = $('#example').DataTable();
           
            $('#example tbody').on('click', 'tr', function () {
               
            });
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
function User_ChangePass() {
    $("#MiddleMess").html("");
    $("#message").html("");
    var username = document.getElementById("key").innerHTML.split("@")[0];
    var domain = document.getElementById("key").innerHTML.split("@")[1];
    
    var newpass = $("#password").val();
    var datas = {
        "username": username, "domain": domain, "newpass": newpass

    };
    $.ajax({
        
        type: "post",
        url: "/UserManagement/ChangePass",
        data: datas,
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
function DisableLync_confirm() {
    if (confirm("Sure to submit the current user's modification?")) {
        DisableLync();
    }
    else {
        return;
    }
}
function DisableExchange_confirm() {
    if (confirm("Sure to submit the current user's modification?")) {
        DisableExchange();
    }
    else {
        return;
    }
}
function User_Update_Confirm() {
    if (confirm("Sure to submit the current user's modification?")) {
        User_update();
    }
    else {
        return;
    }
}
function User_update() {                                           //更新用户的信息
   
        $("#message").html("");
        $("#MiddleMess").html("");
        var action;
        if(document.getElementById("EnableAccount").checked){
            action = "enable";
        } else if (document.getElementById("DisableAccount").checked) {
            action = "disable";
        }
        var username = document.getElementById("key").innerHTML.split("@")[0];
        var upnname = $("#UserPrincipalName").val() + "@" + document.getElementById("upnname").value;
        var oupath;
        var ou1 = document.getElementById("ChoosedOu").innerHTML;
       
        if (ou1 != "") {
            var temp = document.getElementById("ChoosedOu").innerHTML.split('LDAP://')[1];
            oupath = temp.split('/')[1];
        } else {
            oupath = "";
        }
       
        var accountExpireDate = "";
        var v = $('#datebox').datebox('getValue');
        var d = v.split("/");
        if (document.getElementById("AccountExpiresNever").checked) {
            accountExpireDate = "Never";
        } else {
            accountExpireDate = d[2]+"/"+d[0]+"/"+d[1]+" 16:00:00";
        }
        var profilePath = document.getElementById("profilePath").value;
        var scriptPath = document.getElementById("LoginScript").value;
        var homeDirectory;
        var homeDrive;
        if (document.getElementById("LocalPathRadio").checked) {
            homeDrive = "";
            homeDirectory = document.getElementById("LocalPath").value;
        } else {
            homeDirectory = document.getElementById("Connect").value;
            homeDrive = document.getElementById("Profileconnect").value;
        }
        var domain = $("#domain").val();
        var userid = $("#UserId").val();
        var firstname = $("#FirstName").val();
        var lastname = $("#LastName").val();
        var displayname = $("#DisplayName").val();
        var description = $("#Description").val();
        var office = $("#Office").val();
        var telephone = $("#Telephone").val();
        var mail = $("#mail").val();
        var Address= $("#Address").val();
        var PostOfficeBox = $("#PostOfficeBox").val();
        var City = $("#City").val();
        var PostalCode = $("#PostalCode").val();
        var Province = $("#Province").val();
        var LoginScript = $("#LoginScript").val();
        var LocalPath = $("#LocalPath").val();
        var Pager = $("#Pager").val();
        var MobilePhone = $("#MobilePhone").val();
        var Title = $("#Title").val();
        var Department = $("#Department").val();
        var Company = $("#Company").val();
        var adminDescription = $("#adminDescription").val();
        var admindisplayname = $("#admindisplayname").val();
        var comment = $("#comment").val();
        var numberof="";
        var rownum = document.getElementById("Memberof_info").innerHTML.split(" ")[5];
        for (var i = 0; i < rownum; i++) {
       
            numberof += Memberoftable.row(i).data()[1]+",";
        }

   
        $.ajax({
            type: "post",
            url: "/UserManagement/Modify",
            data: "username=" + username + "&userid=" + userid + "&domain=" +
                domain + "&firstname=" + firstname + "&lastname=" + lastname + "&displayname=" + displayname + "&description=" +
                description + "&office=" + office + "&telephone=" + telephone + "&mail=" + mail + "&accountExpireDate=" + accountExpireDate
            + "&Address=" + Address+ "&PostOfficeBox=" + PostOfficeBox+ "&City=" + City+ "&PostalCode=" + PostalCode+ "&Province=" + Province
            + "&LoginScript=" + LoginScript+ "&LocalPath=" + LocalPath+ "&Pager=" + Pager+ "&MobilePhone=" + MobilePhone+ "&Title=" + Title+ "&Department=" + Department
            + "&Company=" + Company + "&adminDescription=" + adminDescription + "&admindisplayname=" + admindisplayname + "&comment=" +
            comment + "&numberof=" + numberof + "&upnname=" + upnname + "&profilePath=" + profilePath + "&homeDrive=" + homeDrive + "&scriptPath=" +
            scriptPath + "&homeDirectory=" + homeDirectory + "&action=" + action + "&oupath=" + oupath,
            dataType: "json",
            success: function (data) {
                $("#MiddleMess").html(data.Message);
                if (data.logined) {
                    keyupnname = "";
                    UserModifySubmited = false;
                    console.log(UserModifySubmited);
                    TabColorBlack();
                    document.getElementById("canclebtn").disabled = true;
                    var pageindex = table.page();
                
                    var d = table.row(targrt).data();
                    d[1] = userid;
                    d[2] = firstname;
                    d[3] = lastname;
                    d[4] = upnname;
                  
                    d[5] = displayname;
                    d[6] = Company;
                    d[7] = mail;
                    table.row(targrt).data(d).draw();
                    table.page(pageindex).draw(false);
                    table.$('tr.ModifyState').addClass('modified');
                    table.$('tr.ModifyState').removeClass('ModifyState');
                    table.$('tr.selected').addClass('ModifyState');
                    document.getElementById("modifybtn").disabled = true;
                    if ( action = "enable") {      //账户不可用
                        document.getElementById("DisableAccount").disabled = false;
                        document.getElementById("EnableAccount").disabled = true;
                      
                        document.getElementById("AccountEn").style.color = "green";

                    } else if(action = "disable") {                              //账户可用
                  
                        document.getElementById("EnableAccount").disabled = false;
                        document.getElementById("DisableAccount").disabled = true;
                        document.getElementById("AccountDis").style.color = "green";
                    }
                }
           
            }
            ,
            error: function () {
                $("#message").html("Network interrupt, networking timeout");
            }
        });
    }
    function User_Clear() {
        $("#MiddleMess").html("");
        document.getElementById("searchkeyword").value = "";
        document.getElementById("searchclear").disabled = true;
    }
    function User_Cancle() {
    
        $("#message").html("");
        document.getElementById("userlogonname").value = "";
        document.getElementById("lastname").value = "";
        document.getElementById("firstname").value = "";
        document.getElementById("fullname").value = "";
        document.getElementById("password").value = "";
        document.getElementById("confirmpassword").value = "";
        document.getElementById("umcpanl").checked = false;
        document.getElementById("uccp").checked = false;
        document.getElementById("pne").checked = false;
        document.getElementById("aid").checked = false;
        document.getElementById("Create_Cancle").disabled = true;
    }
    function User_Create() {                       //创建用户
        var membersof= document.getElementById("hidekey").innerHTML;
        
        var domain = document.getElementById("Userdomain").value;
        var userlogonname = $("#userlogonname").val();
        if (userlogonname == "") {
            alert("User name cannot be empty！！");
            return;
        }

       
        var temp = document.getElementById("ChoosedOu").innerHTML.split('LDAP://')[1];
        var OuPath = temp.split('/')[1];
        var firstname = $("#firstname").val();
        console.log(firstname);
        var lastname = $("#lastname").val(); console.log(lastname);
        if (document.getElementById("firstname").value== "" || document.getElementById("lastname").value == "") {
            alert("FirstName Or LastName cannot be empty！！");
            return;
        }
        var fullname = $("#fullname").val();
        var password = $("#password").val();
        if (document.getElementById("password").value == "") {
            alert("Password cannot be empty！！");
            return;
        }
        var umcpanl = document.getElementById("umcpanl").checked ? true : false;
        var uccp = document.getElementById("uccp").checked ? true : false;
        var pne = document.getElementById("pne").checked ? true : false;
        var aid = document.getElementById("aid").checked ? true : false;
        var AccountLockouttime = $("#AccountLockouttime").val();
        var officephone = $("#officephone").val();
        var Department = $("#Department").val();
        var Manager = $("#ManageBy").val();
        var Company = $("#Company").val();
        var EmployeeID = $("#EmployeeID").val();
        var Office = $("#Office").val();
        var Country = $("#Country").val();
        var City = $("#City").val();
        $("#message").html("User being created, please wait. . .");
        var datas = {
            "OuPath" : OuPath ,"domain" : domain, "userlogonname":userlogonname , "email":email ,
                   "firstname": firstname, "lastname": lastname , "fullname" : fullname ,"password":
                   password ,"umcpanl" : umcpanl , "uccp": uccp, "pne": pne, "aid": aid
              ,"AccountLockouttime": AccountLockouttime
               ,"officephone" : officephone
               ,"Department": Department
              , "Manager" : Manager
               ,"Company" : Company
               ,"EmployeeID": EmployeeID
               , "Office":Office,
              "Country":Country
               ,"City" : City , "membersof" : membersof

        };
            $.ajax({
                type: "post",
                url: "/UserManagement/CreateUser",
                data: datas

           //         "OuPath=" + OuPath + "&domain=" + domain + "&userlogonname=" + userlogonname + "&email=" + email +
           //         "&firstname=" + firstname + "&lastname=" + lastname + "&fullname=" + fullname + "&password=" +
           //         password + "&umcpanl=" + umcpanl + "&uccp=" + uccp + "&pne=" + pne + "&aid=" + aid
           //     + "&AccountLockouttime=" + AccountLockouttime
           //     + "&officephone=" + officephone
           //     + "&Department=" + Department
           //     + "&Manager=" + Manager
           //     + "&Company=" + Company
           //     + "&EmployeeID=" + EmployeeID

           //     + "&Office=" + Office
           //     + "&Country=" + Country
           //     + "&City=" + City + "&membersof=" + membersof
           ,
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
    function Delete_firm() {
        if (confirm("Are you sure you want to delete the user?")) {
            User_Delete();
        }
        else {
            //alert("你按了取消，那就是返回false");
        }
    }
    function User_Delete() {                                  //点击删除某个用户
        $("#message").html("");
        $("#MiddleMess").html("");
        table.row('.selected').remove().draw(false);
        var username = document.getElementById("key").innerHTML.split("@")[0];
        var domain = document.getElementById("key").innerHTML.split("@")[1];
        $.ajax({
            type: "post",
            url: "/UserManagement/Delete",
            data: "userid=" + username + "&domain=" + domain,
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
    function User_Detail_Cancle() {
        $("#MiddleMess").html("");
        $("#message").html("");
        document.getElementById("modifybtn").disabled = true;
        document.getElementById("canclebtn").disabled = true;
        TabColorBlack();
        document.getElementById("UserId").value = UserId;  //tab页下general 
        document.getElementById("UserPrincipalName").value = UserPrincipalName;
        document.getElementById("FirstName").value = FirstName;
        document.getElementById("LastName").value = LastName;
        document.getElementById("DisplayName").value = DisplayName;
        document.getElementById("Description").value = Description;
        document.getElementById("Office").value = Office;
        document.getElementById("Telephone").value = Telephone;
        document.getElementById("mail").value = mail;
        document.getElementById("Address").value = Address;
        document.getElementById("PostOfficeBox").value=PostOfficeBox;
        document.getElementById("City").value = City;
        document.getElementById("PostalCode").value = PostalCode;
        document.getElementById("Province").value = Province;
        document.getElementById("profilePath").value= profilePath;  //tab页profile下
        document.getElementById("LoginScript").value = LoginScript;
        document.getElementById("LocalPath").value = LocalPath;
        document.getElementById("adminDescription").value = adminDescription;     //tab页下others
        document.getElementById("comment").value = comment;
        document.getElementById("Title").value =Title;//tab页下organization
        document.getElementById("Department").value = Department;
        document.getElementById("Company").value = Company;
        document.getElementById("Pager").value = Pager;  //tab页下telephones
        document.getElementById("MobilePhone").value =MobilePhone;
        document.getElementById("admindisplayname").value = admindisplayname;
        document.getElementById("upnname").value = UserPrincipalDomain;
        $("#membertable").html(MemberOfTable);

        Memberoftable = $('#Memberof').DataTable({
            "bAutoWidth": false,                   //是否启用自动适应列宽
            "aoColumns": [                          //设定各列宽度   
                                   { "sWidth": "50px" },
                                   { "sWidth": "*" }]
        });
        $('#Memberof tbody').on('click', 'tr', function () {
            var rownum = document.getElementById("GroupChose_info").innerHTML.split(" ")[5];
            var d = Memberoftable.row(this).data();
            console.log("d1:" + d[1]);
            GroupChooser.row.add([

                     rownum,
                     d[1],
            ]).draw().nodes().to$().addClass('modified');
    
        
 
            Memberoftable.row(this).remove().draw(false);
            document.getElementById("canclebtn").disabled = false;
            document.getElementById("modifybtn1").disabled = false;
            UserModifySubmited = true;
            console.log(UserModifySubmited);
        });
        if (groupchoseTable != "") {

        
        $("#Grouptablespace").html(groupchoseTable);
        GroupChooser = $("#GroupChose").DataTable({
            "bAutoWidth": false,                   //是否启用自动适应列宽
            "aoColumns": [                          //设定各列宽度   
                                   { "sWidth": "50px" },
                                   { "sWidth": "*" }]
        });
        $('#GroupChose tbody').on('click', 'tr', function () {

            var rownum = document.getElementById("Memberof_info").innerHTML.split(" ")[5];
            var d = GroupChooser.row(this).data();
            console.log("d1:" + d[1]);
            var arr = new Array()
            for (var i = 0; i < rownum; i++) {
                arr[i] = Memberoftable.row(i).data()[1];
                console.log(Memberoftable.row(i).data()[1]);
            }
            if (arr.indexOf(d[1]) != -1) {
                alert("You selected group name already exists!");

            }
            else {
                document.getElementById("canclebtn").disabled = false;
                document.getElementById("modifybtn1").disabled = false;
                Memberoftable.row.add([

                     rownum,
                     d[1],
                ]).draw().nodes().to$().addClass('modified');
                GroupChooser.row(this).remove().draw(false);
                UserModifySubmited = true;
                console.log(UserModifySubmited);

            }
        });
    }

    }
  
    var targrt;
    var Memberoftable;
    var User_detail = function (obj) {   //获取点击用户的详细信息
        $("#message").html("");
        $("#MiddleMess").html("");
        document.getElementById("TabContent").style.visibility = "visible";
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
     
   
        if (UserModifySubmited) {
            if (confirm("Whether to submit the current user's modification?")) {
                User_update();
           
            }
            else {
                table.$('tr.ModifyState').removeClass('ModifyState');
                UserModifySubmited = false;       //提示修改的时候取消丢弃已作出的更改
                console.log(UserModifySubmited);
            }
        }
        TabColorBlack();
        TabDateClear();
        $.ajax({
            type: "post",
            url: "/UserManagement/Details",
            data: "userid=" + userid + "&domainname=" + $("#domain").val(),
            dataType: "json",
            success: function (data) {
                groupchoseTable = "";

              
                $('#combotree').combotree('setText',  data.DistinguishedName);
                document.getElementById("deletebtn").disabled = false;
                table.$('tr.ModifyState').removeClass('ModifyState');
                targrt = obj;
                table.$('tr.selected').addClass('ModifyState');
                document.getElementById("TabContent").style.visibility = "visible";
                $("#key").html(data.UserID+"@"+data.UserPrincipalName.split("@")[1]);
                document.getElementById("Address").value = data.Address;    //tab页address下
                Address = data.Address;
                document.getElementById("PostOfficeBox").value = data.PostOfficeBox;
                PostOfficeBox = data.PostOfficeBox;
                document.getElementById("City").value = data.City;
                City = data.City;
                document.getElementById("PostalCode").value = data.PostalCode;
                PostalCode = data.PostalCode;
                document.getElementById("Province").value = data.Province;
                Province = data.Province;
                document.getElementById("UserId").value = data.UserID;  //tab页下general 
                document.getElementById("UserId").style.color = "gray";
                UserId = data.UserID;
                document.getElementById("UserPrincipalName").value = data.UserPrincipalName.split("@")[0];
                UserPrincipalName = data.UserPrincipalName.split("@")[0];
                document.getElementById("FirstName").value = data.FirstName;
                FirstName = data.FirstName;
                document.getElementById("LastName").value = data.LastName;
                LastName = data.LastName;
                document.getElementById("DisplayName").value = data.DisplayName;
                DisplayName = data.DisplayName;
                document.getElementById("Description").value = data.Description;
                Description = data.Description;
                document.getElementById("Office").value = data.Office;
                Office = data.Office;
                document.getElementById("Telephone").value = data.Telephone;
                Telephone = data.Telephone;
                document.getElementById("mail").value = data.mail;
                mail = data.mail;
                document.getElementById("profilePath").value = data.profilePath;  //tab页profile下
                profilePath = data.profilePath;
                document.getElementById("LoginScript").value = data.scriptPath;
                LoginScript = data.scriptPath;
                if (data.isRemoteHomeFolder) {
                    document.getElementById("ConnectRadio").checked = true;
                    document.getElementById("Profileconnect").value = data.homeDrive;
                    document.getElementById("Connect").value = data.homeDirectory;
                } else {
                    document.getElementById("LocalPathRadio").checked = true;
                    document.getElementById("LocalPath").value = data.homeDirectory;
                }
           
                LocalPath = data.homeDirectory;
                document.getElementById("adminDescription").value = data.adminDescription;     //tab页下others
                adminDescription = data.adminDescription;
                if (data.admindisplayname) {
                    document.getElementById("admindisplayname").value = checked;
                }
                if (data.AccountLocked) {     //账户被锁
                    document.getElementById("UnlockAccount").disabled = false;
                    document.getElementById("AccountState").style.color = "green";
               
                } else {                  //账户没有被锁
                    document.getElementById("AccountState").style.color = "black";
                    document.getElementById("UnlockAccount").disabled = true;
                }
                document.getElementById("comment").value = data.comment;
                comment = data.comment;
                document.getElementById("Title").value = data.Title;//tab页下organization
                Title = data.Title;
                document.getElementById("Department").value = data.Department;
                Department = data.Department;
                document.getElementById("Company").value = data.Company;
                Company = data.Company;
                $("#membertable").html(data.memebertable);
                MemberOfTable = data.memebertable;
                Memberoftable = $('#Memberof').DataTable({
                    "bAutoWidth": false,                   //是否启用自动适应列宽
                    "aoColumns": [                          //设定各列宽度   
                                           { "sWidth": "50px" },
                                           { "sWidth": "*" }]
                });
                $('#Memberof tbody').on('click', 'tr', function () {
                    
                    var rownum = document.getElementById("GroupChose_info").innerHTML.split(" ")[5];
                   var d = Memberoftable.row(this).data();
                        console.log("d1:" + d[1]);
                        GroupChooser.row.add([

                                 rownum,
                                 d[1],
                        ]).draw().nodes().to$().addClass('modified');
                  Memberoftable.row(this).remove().draw(false);
                    document.getElementById("canclebtn").disabled = false;
                    document.getElementById("modifybtn1").disabled = false;
                    UserModifySubmited = true;
                    console.log(UserModifySubmited);
                });
                var timefirst = "0001/1/1";
                var aed = data.AccountExpDate.split(" ")[0];
                if (aed != timefirst) {
                    document.getElementById("AccountExpiresEndOf").checked = true;
                    var date = aed.split("/");

                    $('#datebox').datebox('setValue', date[1]+"/"+date[2]+"/"+date[0]);
                    $("#datebox").datebox("enable");
               
                } else {
                    document.getElementById("AccountExpiresNever").checked = true;
                    $("#datebox").datebox("disable");
                }
           
                document.getElementById("Pager").value = data.Pager;  //tab页下telephones
                Pager = data.Pager;
                document.getElementById("MobilePhone").value = data.MobilePhone;
                MobilePhone = data.MobilePhone;
                document.getElementById("admindisplayname").value = data.DisplayName;
                admindisplayname = data.DisplayName;

                var upnname = "<option value=\""+data.UserPrincipalName.split("@")[1] + "\">" + "@"+data.UserPrincipalName.split("@")[1] + "</option>";
                if (data.uPNSuffixes.length > 0) {
                    for (var i = 0; i < data.uPNSuffixes.length; i++) {
                        var mess = data.uPNSuffixes[i];
                        if (mess != data.UserPrincipalName.split("@")[1]) {
                            upnname += "<option value=\"" + mess + "\">" + "@" + mess + "</option>";
                        }

                    }
                }
                UserPrincipalDomain = data.UserPrincipalName.split("@")[1];
                document.getElementById("upnname").innerHTML = upnname;
                keyupnname = data.UserPrincipalName.split("@")[1];
            
            
                if (data.AccountDisabled) {      //账户不可用
                
                    document.getElementById("EnableAccount").checked = false;
                    document.getElementById("DisableAccount").checked = true;
                    document.getElementById("DisableAccount").disabled = true;
            
                } else {                              //账户可用
                    document.getElementById("EnableAccount").checked = true;
                    document.getElementById("EnableAccount").disabled = true;
                    document.getElementById("DisableAccount").checked = false;
             
                }
                if (data.SkypeForBusinessEnabled) {
                    document.getElementById("EnableLync").checked = true;
                    document.getElementById("EnableLync").disabled = true;
                    document.getElementById("DisableLync").checked = false;
               
                
                } else {
                    document.getElementById("EnableLync").checked = false;
                    document.getElementById("DisableLync").checked = true;
                    document.getElementById("DisableLync").disabled = true;
             
                
                }
                if (data.ExchangeEnabled) {
             
                    document.getElementById("EnableExchange").disabled = true;
                    document.getElementById("EnableExchange").checked = true;
                    document.getElementById("DisableExchange").checked = false;
                } else {
            
                    document.getElementById("DisableExchange").disabled = true;
                    document.getElementById("EnableExchange").checked = false;
                    document.getElementById("DisableExchange").checked = true;
              
                }
            }
               ,
            error: function () {
            
                $("#message").html("Network interrupt, networking timeout");
            }

        });
    };
 
    function RandomPass() {
        document.getElementById("passmess").innerHTML = "";
        document.getElementById("passwordremind").innerHTML = "";
        //获取随即密码
        $.ajax({
            type: "post",
            url: "/UserManagement/RandomPass",
            dataType: "json",
            success: function (data) {
                document.getElementById("password").value = data.Message;
                //document.getElementById("confirmpassword").value = data.Message;
            }
          ,
            error: function () {
                $("#message").html("Network interrupt, networking timeout");
            }
        });
    }
    function verify() {
        var newpass = $("#password").val();
        var confirmpass = $("#confirmpassword").val();
        if(newpass==confirmpass){
            document.getElementById("changePassbtn").disabled = false;
        }
    }
    function EnableLync() {         //Enable/Disable Lync
      
       
        var username = document.getElementById("key").innerHTML.split("@")[0];
    
        $("#MiddleMess").html("Waiting...");
        var action = "enable";
        $.ajax({
            type: "post",
            url: "/UserManagement/EnableLync",
            data: "username=" + username + "&domain=" + $("#domain").val() + "&action=" + action,
            dataType: "json",
            success: function (data) {
                $("#MiddleMess").html(data.Message);
                $("#UcMess").html("");
                document.getElementById("EnableLync").disabled = true;
                document.getElementById("DisableLync").disabled = false;
                document.getElementById("LyncEn").style.color = "green";
                document.getElementById("LyncDis").style.color = "black";
            }
            ,
            error: function () {
                $("#MiddleMess").html("Network interrupt, networking timeout");
            }
        });
    }
    function DisLyncConfirm() {
        if (confirm("Are you sure you want to Disable the Lync?")) {
            DisableLync();
        }
        else {
            document.getElementById("EnableLync").checked = true;
            document.getElementById("DisableLync").checked = false;
            //alert("你按了取消，那就是返回false");
        }
    }
    function DisableLync() {         //Enable/Disable Lync
       
      
        $("#MiddleMess").html("Waiting...");
        var username = document.getElementById("key").innerHTML.split("@")[0];
        var action = "disable";
        $.ajax({
            type: "post",
            url: "/UserManagement/EnableLync",
            data: "username=" + username + "&domain=" + $("#domain").val() + "&action=" + action,
            dataType: "json",
            success: function (data) {
                $("#MiddleMess").html(data.Message);
                $("#UcMess").html("");
                document.getElementById("EnableLync").disabled = false;
                document.getElementById("DisableLync").disabled = true;
                document.getElementById("LyncEn").style.color = "black";
                document.getElementById("LyncDis").style.color = "green";
            }
            ,
            error: function () {
                $("#MiddleMess").html("Network interrupt, networking timeout");
            }
        });
    }
    function EnableConfirm() {
        document.getElementById("exchangeType").style.visibility = "visible";
    }
    function EnableExchange() {                 //Enable/Disable Exchange
        var exchangeType;

        if (document.getElementById("Person").checked) {
            exchangeType = "UserMailbox";
        } else if (document.getElementById("Room").checked) {
            exchangeType = "RoomMailbox";
        } else if (document.getElementById("Share").checked) {
            exchangeType = "SharedMailbox";
        }else{
            exchangeType = "EquipmentMailbox";
}

        document.getElementById("key").innerHTML.split("@")[0]

        $("#MiddleMess").html("Waiting...");
        var username = document.getElementById("key").innerHTML.split("@")[0];
     
        
        var action = "enable";
        $.ajax({
            type: "post",
            url: "/UserManagement/EnableExchange",
            data: "username=" + username + "&domain=" + $("#domain").val() + "&action=" + action + "&exchangeType=" + exchangeType,
            dataType: "json",
            success: function (data) {
                $("#MiddleMess").html(data.Message);
            
                document.getElementById("ExchangeEn").style.color = "green";
                document.getElementById("EnableExchange").disabled = true;
                document.getElementById("ExchangeDis").style.color = "black";
                document.getElementById("DisableExchange").disabled = false;
                var d = table.row(targrt).data();
                d[9] = "<a type=\"button\" href=\"ModifyExchange?userid=" + d[1] + "&amp;domain=" + $("#domain").val() + "\" target=\"_blank\" class=\"btn btn - block\">Modify</a>";
                table.row(targrt).data(d).draw();
                table.$('tr.selected').addClass('modified');
            }
              ,
            error: function () {
                $("#MiddleMess").html("Network interrupt, networking timeout");
            }
        });
    }
    function DisExchangeConfirm() {
        if (confirm("Are you sure you want to Disable the Exchange?")) {
            DisableExchange();
        }
        else {
        
            document.getElementById("EnableExchange").checked = true;
            document.getElementById("DisableExchange").checked = false;
        }
    }
    function DisableExchange() {                 //Enable/Disable Exchange
      
        $("#MiddleMess").html("Waiting...");
        var username = document.getElementById("key").innerHTML.split("@")[0];
        var action = "disable";
        $.ajax({
            type: "post",
            url: "/UserManagement/EnableExchange",
            data: "username=" + username + "&domain=" + $("#domain").val() + "&action=" + action,
            dataType: "json",
            success: function (data) {
                $("#MiddleMess").html(data.Message);
                $("#MiddleMess").html("");
                document.getElementById("ExchangeEn").style.color = "black";
                document.getElementById("EnableExchange").disabled = false;
                document.getElementById("ExchangeDis").style.color = "green";
                document.getElementById("DisableExchange").disbled = true;
                var d = table.row(targrt).data();
                d[9] = " ";
                table.row(targrt).data(d).draw();
                table.$('tr.selected').addClass('modified');
        
            }
              ,
            error: function () {
                $("#MiddleMess").html("Network interrupt, networking timeout");
            }
        });
    }
    function UnlockAccount() {

        $("#MiddleMess").html("Waiting...");
        var username = document.getElementById("key").innerHTML.split("@")[0];
        $.ajax({
            type: "post",
            url: "/UserManagement/AccountUnlock",
            data: "username=" + username + "&domain=" + $("#domain").val() ,
            dataType: "json",
            success: function (data) {
                document.getElementById("AccountState").style.color = "black";
                document.getElementById("UnlockAccount").disabled = true;
                $("#MiddleMess").html(data.Message);
            }
            ,
            error: function () {

                $("#MiddleMess").html("Network interrupt, networking timeout");
            }
        });
    }
    var Userchange = function (obj) {                       //获取点击用户的详细信息

        var reg = /^[\s　]|[ ]$/gi;
        var girlfirend = $(obj).context;
        var text = girlfirend.value;
       
        if (text == Cache) {
            girlfirend.style.color = "black";
            return;
        }
        if (!reg.test(text)) {
            girlfirend.style.color = "green";
            UserModifySubmited = true;
            console.log(UserModifySubmited);
           document.getElementById("canclebtn").disabled = false;
            document.getElementById("modifybtn1").disabled = false;
       
        } else {
        
            return;
        }
    };
    var Userchange_firstname = function (obj) {                       //获取点击用户的详细信息

        var reg = /^[\s　]|[ ]$/gi;
        var girlfirend = $(obj).context;
        var text = girlfirend.value;
        document.getElementById("DisplayName").value = text + " " + document.getElementById("LastName").value;
        if (text == Cache) {
            girlfirend.style.color = "black";
            return;
        }
        if (!reg.test(text)) {
            girlfirend.style.color = "green";
            UserModifySubmited = true;
            console.log(UserModifySubmited);
            document.getElementById("canclebtn").disabled = false;
            document.getElementById("modifybtn1").disabled = false;

        } else {

            return;
        }
    };
    
    var Userchange_lastname = function (obj) {                       //获取点击用户的详细信息

        var reg = /^[\s　]|[ ]$/gi;
        var girlfirend = $(obj).context;
        var text = girlfirend.value;
        document.getElementById("DisplayName").value = document.getElementById("FirstName").value + " " + text;
        if (text == Cache) {
            girlfirend.style.color = "black";
            return;
        }
        if (!reg.test(text)) {
            girlfirend.style.color = "green";
            UserModifySubmited = true;
            console.log(UserModifySubmited);
            document.getElementById("canclebtn").disabled = false;
            document.getElementById("modifybtn1").disabled = false;

        } else {

            return;
        }
    };
    function TabColorBlack() {

        document.getElementById("AccountDis").style.color = "black";
        document.getElementById("AccountEn").style.color = "black";
        document.getElementById("UserId").style.color = "black";
        document.getElementById("UserPrincipalName").style.color = "black";
        document.getElementById("FirstName").style.color = "black";
        document.getElementById("LastName").style.color = "black";
        document.getElementById("DisplayName").style.color = "black";
        document.getElementById("Description").style.color = "black";
        document.getElementById("Office").style.color = "black";
        document.getElementById("Telephone").style.color = "black";
        document.getElementById("mail").style.color = "black";
        document.getElementById("Address").style.color = "black";
        document.getElementById("PostOfficeBox").style.color = "black";
        document.getElementById("City").style.color = "black";
        document.getElementById("PostalCode").style.color = "black";
        document.getElementById("Province").style.color = "black";
        document.getElementById("profilePath").style.color = "black";
        document.getElementById("LoginScript").style.color = "black";
        document.getElementById("LocalPath").style.color = "black";
        document.getElementById("Pager").style.color = "black";
        document.getElementById("MobilePhone").style.color = "black";
        document.getElementById("Title").style.color = "black";
        document.getElementById("Department").style.color = "black";
        document.getElementById("Company").style.color = "black";
        document.getElementById("adminDescription").style.color = "black";
        document.getElementById("admindisplayname").style.color = "black";
    
        document.getElementById("comment").style.color = "black";
        document.getElementById("LyncEn").style.color = "black";
        document.getElementById("LyncDis").style.color = "black";
        document.getElementById("ExchangeEn").style.color = "black";
        document.getElementById("ExchangeDis").style.color = "black";
        document.getElementById("AccountDis").style.color = "black";
        document.getElementById("AccountEn").style.color = "black";
    }
    function TabDateClear() {
        $("#AccountMess").html("");
        $('#combotree').combotree('setText', "");
        document.getElementById("Connect").value = "";
        document.getElementById("Profileconnect").value = "D:";
        document.getElementById("UnlockAccount").checked = false;
        document.getElementById("UnlockAccount").disabled = false;
        document.getElementById("EnableAccount").checked = false;
        document.getElementById("EnableAccount").disabled = false;
        document.getElementById("DisableAccount").checked = false;
        document.getElementById("DisableAccount").disabled = false;
        document.getElementById("AccountExpiresEndOf").checked = false;
        document.getElementById("AccountExpiresNever").disabled = false;
        document.getElementById("AccountExpiresEndOf").disabled = false;
        document.getElementById("AccountExpiresNever").checked = false;
        document.getElementById("DisableLync").checked = false;
        document.getElementById("EnableLync").checked = false;
        document.getElementById("EnableExchange").checked = false;
        document.getElementById("DisableExchange").checked = false;
        document.getElementById("DisableLync").disabled = false;
        document.getElementById("EnableLync").disabled = false;
        document.getElementById("EnableExchange").disabled = false;
        document.getElementById("DisableExchange").disabled = false;
        document.getElementById("Address").value = "";
        document.getElementById("PostOfficeBox").value = "";
        document.getElementById("upnname").value = "";
        document.getElementById("City").value = "";
        document.getElementById("PostalCode").value = "";
        document.getElementById("Province").value = "";
        document.getElementById("UserId").value = "";
        document.getElementById("UserPrincipalName").value = "";
        document.getElementById("FirstName").value = "";
        document.getElementById("LastName").value = "";
        document.getElementById("DisplayName").value = "";
        document.getElementById("Description").value = "";
        document.getElementById("Office").value = "";
        document.getElementById("Telephone").value = "";
        document.getElementById("mail").value = "";
        document.getElementById("profilePath").value = "";
        document.getElementById("LoginScript").value = "";
        document.getElementById("LocalPath").value = "";
        document.getElementById("adminDescription").value = "";
        document.getElementById("comment").value = "";
        document.getElementById("Title").value = "";
        document.getElementById("Department").value = "";
        document.getElementById("Company").value = "";
        document.getElementById("Pager").value = "";
        document.getElementById("MobilePhone").value = "";
    }
    var GroupChooser;
    function User_Group_Search() {
        $("#MiddleMess").html("");
        $("#Grouptablespace").html("<h4 style=\"color:orange\">Searching....Please wait</h4>");
        var rownum = document.getElementById("Memberof_info").innerHTML.split(" ")[5];
        for (var i = 0; i < rownum; i++) {
         
        }
        var userid= document.getElementById("key").innerHTML.split("@")[0];
        var userdomain= document.getElementById("key").innerHTML.split("@")[1];
        $.ajax({
            type: "post",
            url: "/UserManagement/GroupSearch",
            data: "groupdomain=" + $("#groupdomain").val() + "&searchkeyword=" + $("#groupkeyword").val() + "&userid=" + userid + "&domainname=" +userdomain,
            dataType: "json",
            success: function (data) {
                groupchoseTable = data.Message;
                console.log(groupchoseTable);
                $("#Grouptablespace").html(data.Message);
                 GroupChooser = $("#GroupChose").DataTable({
                    "bAutoWidth": false,                   //是否启用自动适应列宽
                    "aoColumns": [                          //设定各列宽度   
                                           { "sWidth": "50px" },
                                           { "sWidth": "*" }]
                });
                $('#GroupChose tbody').on('click', 'tr', function () {
                   
                    var rownum = document.getElementById("Memberof_info").innerHTML.split(" ")[5];
                    var d = GroupChooser.row(this).data();
                    console.log("d1:"+d[1]);
                    var arr = new Array()
                    for (var i = 0; i < rownum; i++) {
                        arr[i] = Memberoftable.row(i).data()[1];
                        console.log(Memberoftable.row(i).data()[1]);
                    }
                    if(arr.indexOf(d[1])!=-1){
                        alert("You selected group name already exists!");
                       
                    }
                    else {
                        document.getElementById("canclebtn").disabled = false;
                        document.getElementById("modifybtn1").disabled = false;
                        Memberoftable.row.add([
                             rownum,
                             d[1],
                        ]).draw().nodes().to$().addClass('modified');
                  GroupChooser.row(this).remove().draw(false);
                        UserModifySubmited = true;
                        console.log(UserModifySubmited);
                      
                    }
                });

            }
           ,
            error: function () {

                $("#message").html("Network interrupt, networking timeout");
            }

        });
    }
    
    var times = 0;
    $("#Userdomain").on("change", function () {
        $("#MiddleMess").html("");
        times += 1;
        if (times == 1) {
            var chosed = $("#Userdomain").val();
            console.log(chosed);
            if (document.getElementById("combotree") != null) {
                $('#combotree').combotree({
                    url:'/UserManagement/Outree?domain='+chosed,
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
    $("#upnname").on("change", function () {
        $("#MiddleMess").html("");
        var domain = $("#upnname").val();
        var username = document.getElementById("key").innerHTML.split("@")[0];
        if (keyupnname != domain) {
            $.ajax({
                type: "post",
                url: "/UserManagement/UPNName",
                data: "username=" + username + "&domain=" + domain,
                dataType: "json",
                success: function (data) {

                    if (data.Message == "1") {
                        alert("The Specified user Logon name already exists in the enterprise!")
                        document.getElementById("upnname").value = UserPrincipalDomain;
                    } else {
                        document.getElementById("canclebtn").disabled = false;
                        document.getElementById("modifybtn1").disabled = false;
                        UserModifySubmited = true;
                        console.log(UserModifySubmited);
                    }

                }
       ,
                error: function () {

                    $("#message").html("Network interrupt, networking timeout");
                }

            });
        }
    
    
    });

    function User_Group_Search_begin() {
        var event = window.event || arguments.callee.caller.arguments[0];
        if (event.keyCode == 13) {
            User_Group_Search();
        }
    }
    var verifyExchange = function (obj) {
        $("#MiddleMess").html("");
        var userid = $(obj).parent().parent().children('td').next().html();
        var domain = $("#domain").val();
        $.ajax({
            type: "post",
            url: "/UserManagement/verifyExchange",
            data: "userid=" + userid + "&domain=" + $("#domain").val(),
            dataType: "json",
            success: function (data) {

                if (data.logined) {
                
                    window.open("ModifyExchange?userid=" + data.Message.split("@")[0] + "&domain=" + data.Message.split("@")[1]);
                    window.targrt = "_blank";
                } else {
                    alert(data.Message);
                }
            }
              ,
            error: function () {
                $("#message").html("Network interrupt, networking timeout");
            }
        });
        return;
    }
    function AccountNever() {
        $("#datebox").datebox("disable");
    }
    function AccountEndof() {
        $("#datebox").datebox("enable");
    }
    var Cache;
    var Gettextlen = function (obj){
        Cache = $(obj).context.value;
        
    }
    function User_Exchange_Modify() {
       
        var userid = document.getElementById("key").innerHTML.split("@")[0];
        var domain = $("#domain").val();
        var DisplayName = document.getElementById("DisplayName").value;
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
                + "&BlackBerryEnabled=" + BlackBerryEnabled + "&RestrictedUsage=" + RestrictedUsage + "&HideFromOAB=" + HideFromOAB + "&IMAPEnabled=" + IMAPEnabled + "&POP3Enabled=" + POP3Enabled
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
    function EnableAccount() {
        $("#message").html("");
        $("#AccountMess").html("Waiting...");
        var username = document.getElementById("key").innerHTML.split("@")[0];
        var action = "enable";
        $.ajax({
            type: "post",
            url: "/UserManagement/EnableAccount",
            data: "username=" + username + "&domain=" + $("#domain").val() + "&action=" + action,
            dataType: "json",
            success: function (data) {

                $("#AccountMess").html(data.Message);

                document.getElementById("EnableAccount").disabled = true;

                document.getElementById("DisableAccount").disabled = false;
                document.getElementById("AccountEn").style.color = "green";
                document.getElementById("AccountDis").style.color = "black";
                table.$('tr.selected').addClass('modified');
            }
              ,
            error: function () {
                $("#message").html("Network interrupt, networking timeout");
            }
        });
    }
    function DisAccountConfirm() {
        if (confirm("Are you sure you want to Disable the Account?")) {
            DisableAccount();
        }
        else {
           
        }
    }
    function DisableAccount() {
        $("#message").html("");
        $("#AccountMess").html("Waiting...");
        var username = document.getElementById("key").innerHTML.split("@")[0];
        var action = "disable";
        $.ajax({
            type: "post",
            url: "/UserManagement/EnableAccount",
            data: "username=" + username + "&domain=" + $("#domain").val() + "&action=" + action,
            dataType: "json",
            success: function (data) {

                $("#AccountMess").html(data.Message);

                document.getElementById("DisableAccount").disabled = true;
                document.getElementById("AccountDis").style.color = "green";
                document.getElementById("EnableAccount").disabled = false;
                document.getElementById("AccountEn").style.color = "black";
                table.$('tr.selected').addClass('modified');

            }
              ,
            error: function () {
                $("#message").html("Network interrupt, networking timeout");
            }
        });
    }
    function User_create_manager() {
        var domain = document.getElementById("Userdomain").value;
        var manager = document.getElementById("ManageBy").value;
        $.ajax({
            type: "post",
            url: "/UserManagement/Testmanager",
            data: "domain=" + domain + "&manager=" + manager ,
            dataType: "json",
            success: function (data) {
                if (data.num > 0) {
                    if (data.Company != "") {
                        document.getElementById("Company").value = data.Company;
                    }
                    if (data.Department != "") {
                        document.getElementById("Department").value = data.Department;
                    }
                    if (data.Office != "") {
                        document.getElementById("Office").value = data.Office;
                    }
                    if (data.City != "") {
                        document.getElementById("City").value = data.City;
                    }
                    if (data.Telephone != "") {

                        document.getElementById("officephone").value = data.Telephone;
                    }
                    if (data.memebertable != "") {

                        $("#hidekey").html(data.memebertable);
                    }
                    
                } else {
                    if (confirm("The User You Typed Is Not Existed！")) {
                        document.getElementById("ManageBy").value = "";
                        document.getElementById("ManageBy").style.color = "";
                    }
                    else {
                        document.getElementById("ManageBy").value = "";
                        document.getElementById("ManageBy").style.color = "";
                    }
                }
            }
        ,
            error: function () {
                $("#MiddleMess").html("Network interrupt, networking timeout");
            }
        });
    }