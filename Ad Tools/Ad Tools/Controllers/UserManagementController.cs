using ADTOOLS.AD;
using ADTOOLS.DTO;
using Ad_Tools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ad_Tools.Log4net;
using System.Web.UI.WebControls;
using System.Xml;
using System.Configuration;
using Ad_Tools.Common;

namespace Ad_Tools.Controllers
{
    public class UserManagementController : BaseController
    {
        [HttpGet]
        // GET: UserManagement
        public ActionResult SearchModify(string abc)
        {

            List<string> domainlist = new List<string>();
            string currentdomain = (string)Session["domain"];
            List<string> DomainLst = HttpContext.Application["domains"] as List<string>;
            if (DomainLst.Count > 0)
            {

                List<SelectListItem> DomainItem = new List<SelectListItem>();
                if (currentdomain != null)
                {
                    DomainItem.Add(new SelectListItem { Text = currentdomain, Value = currentdomain });
                }
                foreach (var Item in DomainLst)
                {
                    if (Item.ToString() != currentdomain) { DomainItem.Add(new SelectListItem { Text = Item.ToString(), Value = Item.ToString() }); }
                }
                List<SelectListItem> searchfield = new List<SelectListItem>();            //为searchfield添加下拉选项
                searchfield.Add(new SelectListItem { Text = "UserID", Value = "0" });
                searchfield.Add(new SelectListItem { Text = "First Name", Value = "1" });
                searchfield.Add(new SelectListItem { Text = "Last Name", Value = "2" });
                searchfield.Add(new SelectListItem { Text = "Full Name", Value = "3" });
                searchfield.Add(new SelectListItem { Text = "Company", Value = "4" });
                searchfield.Add(new SelectListItem { Text = "Mail", Value = "5" });
                searchfield.Add(new SelectListItem { Text = "ID Manager", Value = "6" });
               
                
                List<SelectListItem> searchcriteria = new List<SelectListItem>();            //为searchcriteria添加下拉选项
                searchcriteria.Add(new SelectListItem { Text = "Equals", Value = "1" });
                searchcriteria.Add(new SelectListItem { Text = "Contains", Value = "2" });
                searchcriteria.Add(new SelectListItem { Text = "Stars with", Value = "3" });
                searchcriteria.Add(new SelectListItem { Text = "Ends with", Value = "4" });
                List<SelectListItem> searchtype = new List<SelectListItem>();            //为searchtype添加下拉选项
                searchtype.Add(new SelectListItem { Text = "All", Value = "0" });
                searchtype.Add(new SelectListItem { Text = "User Employee", Value = "1" });
                searchtype.Add(new SelectListItem { Text = "User Consultant", Value = "2" });
                searchtype.Add(new SelectListItem { Text = "User Extranet", Value = "3" });
                searchtype.Add(new SelectListItem { Text = "Service Account", Value = "4" });
                List<SelectListItem> Profile_connect = new List<SelectListItem>();
                Profile_connect.Add(new SelectListItem { Text = "C:", Value = "C:" });
                Profile_connect.Add(new SelectListItem { Text = "D:", Value = "D:" });
                Profile_connect.Add(new SelectListItem { Text = "E:", Value = "E:" });
                Profile_connect.Add(new SelectListItem { Text = "F:", Value = "F:" });
                Profile_connect.Add(new SelectListItem { Text = "G:", Value = "G:" });
                Profile_connect.Add(new SelectListItem { Text = "H:", Value = "H:" });
                Profile_connect.Add(new SelectListItem { Text = "I:", Value = "I:" });
                Profile_connect.Add(new SelectListItem { Text = "J:", Value = "J:" });
                Profile_connect.Add(new SelectListItem { Text = "K:", Value = "K:" });
                Profile_connect.Add(new SelectListItem { Text = "L:", Value = "L:" });
                Profile_connect.Add(new SelectListItem { Text = "M:", Value = "M:" });
                Profile_connect.Add(new SelectListItem { Text = "N:", Value = "N:" });
                Profile_connect.Add(new SelectListItem { Text = "O:", Value = "O:" });
                Profile_connect.Add(new SelectListItem { Text = "P:", Value = "P:" });
                Profile_connect.Add(new SelectListItem { Text = "Q:", Value = "Q:" });
                Profile_connect.Add(new SelectListItem { Text = "R:", Value = "R:" });
                Profile_connect.Add(new SelectListItem { Text = "S:", Value = "S:" });
                Profile_connect.Add(new SelectListItem { Text = "T:", Value = "T:" });
                Profile_connect.Add(new SelectListItem { Text = "U:", Value = "U:" });
                Profile_connect.Add(new SelectListItem { Text = "V:", Value = "V:" });
                Profile_connect.Add(new SelectListItem { Text = "W:", Value = "W:" });
                Profile_connect.Add(new SelectListItem { Text = "X:", Value = "X:" });
                Profile_connect.Add(new SelectListItem { Text = "Y:", Value = "Y:" });
                Profile_connect.Add(new SelectListItem { Text = "Z:", Value = "Z:" });

                UserSearchModifyViewModel USVM = new UserSearchModifyViewModel()
                {
                    domains = DomainItem,
                    searchcriteria = searchcriteria,
                    searchtype = searchtype,
                    searchfield = searchfield,
                    groupdomain = DomainItem,
                    Profile_connect = Profile_connect,
                };
                return View(USVM);
            }
            else
                return View();//返回结果集合
        }
        [HttpPost]
        public JsonResult SearchModify()
        {
            string Operator = "";
            if (Session["username"].ToString() != null)
            {
                Operator = Session["username"].ToString();
            }                                                                       //Log日志要记录的用户名                  
            string domain = Request.Form["domain"].Trim().ToString();
            string searchfield = Request.Form["searchfield"].Trim().ToString();
            string searchcriteria = Request.Form["searchcriteria"].Trim().ToString();
            string searchtype = Request.Form["searchtype"].Trim().ToString();
            string searchkeyword = Request.Form["searchkeyword"].Trim().ToString();
            string message1 = "domain:" + domain + ";searchcriteria:" + searchcriteria + ";searchfield:" + searchfield + ";searchtype:" + searchtype;
            Users ad_user = HttpContext.Application["ad_user"] as Users;
            List<UserDTO> col;
            DateTime dt = new DateTime();
            if (searchkeyword.Trim() == "")
            {
                col = ad_user.SearchAllUserDTO(searchkeyword, domain, Int32.Parse(searchfield), 0, Int32.Parse(searchtype));
            }
            else
            {
                col = ad_user.SearchAllUserDTO(searchkeyword, domain, Int32.Parse(searchfield), Int32.Parse(searchcriteria), Int32.Parse(searchtype));
            }
            string con = "<table id = \"example\" class=\"display\" cellspacing=\"0\" width=\"100%\"><thead><tr><th>ID</th><th>UserID</th><th>First Name</th><th>Last Name</th><th>UserPrincipal Name</th><th>Full Name</th><th>Company</th><th>Mail</th><th>ID Manager</th><th>Exchange Modify</th></tr></thead><tbody>";
            for (int i = 0; i < col.Count; i++)
            {
                UserDTO ud = col.ElementAt(i);
               dt= ud.AccountExpDate;
                con += "<tr onclick=\"User_detail(this)\"><td id=\"ID" + i + "\">" + i + "</td><td id=\"UserID" + i + "\">" + ud.UserID + "</td><td id=\"FirstName" + i + "\">" + ud.FirstName + "</td><td id=\"LastName" + i + "\">" + ud.LastName + "</td><td id=\"UserPrincipalName" + i + "\">" + simplify(ud.UserPrincipalName) + "</td><td id=\"FirstName" + i + "\">" + simplify(ud.DisplayName) + "</td><td id=\"Company" + i + "\">" + ud.Company + "</td><td id=\"mail" + i + "\">" + simplify(ud.mail) +
                    "</td><td id=\"managedBy" + i + "\">" + ud.managedBy + "</td><td ><a type=\"button\" onclick=\"verifyExchange(this)\" target=\"_blank\" class=\"btn btn - block\">Modify</a></td></tr>";//数据行，字段对应数据库查询字段
              }
            con += " </tbody></table>";
            // LogHelper.WriteLog(typeof(UserManagementController),Operator,"Search%Users",true);
            return Json(new JsonData(con));
         
        }
        public string simplify(string text)                          //表格部分文本太长只显示部分
        {
            if (text.Length>40)
            {
                string str = text.Substring(0, 35);
                return str;
            }
           else
            {
                return text;
            }
        }
        public JsonResult Details()
        {
            string Operator = "";
            if (Session["username"].ToString() != null)
            {
                Operator = Session["username"].ToString();
            }                                                                       //Log日志要记录的用户名    
            string userid = Request.Form["userid"].Trim().ToString();
            string domainname = Request.Form["domainname"].Trim().ToString();
            Users ad_user = HttpContext.Application["ad_user"] as Users;
            List<UserDTO> col = ad_user.SearchAllUserDTO(userid, domainname, (int)UserSearchKey.sAMAccountName, (int)SearchPattern.Equals, 0);
            UserDTO ud = col.ElementAt(0);
            Directory ad_directory = HttpContext.Application["ad_directory"] as Directory;
            List<string> upnname = ad_directory.uPNSuffixes;
            string con = "<table id = \"Memberof\" class=\"display\" cellspacing=\"0\" width=\"100%\"><thead><tr><th>ID</th><th>GroupName</th></tr></thead><tbody>";
            for (int i = 0; i < ud.MemberOf.Count; i++)
            {
                string memberof = ud.MemberOf.ElementAt(i);
               
                    con += "<tr><td>" + i + "</td><td>" + memberof + "</td></tr>";//数据行，字段对应数据库查询字段
            }
            con += " </tbody></table>";
           userdto userdto = new userdto(ud, con,upnname);
             return Json(userdto, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Modify()
        {
            string Operator = "";
            if (Session["username"].ToString() != null)
            {
                Operator = Session["username"].ToString();
            }
            //Log日志要记录的用户名     
            string action = Request.Form["action"].Trim().ToString();
            string upnname = Request.Form["upnname"].Trim().ToString();
            string username = Request.Form["username"].Trim().ToString();
            string userid = Request.Form["userid"].Trim().ToString();
            string domain = Request.Form["domain"].Trim().ToString();
            string oupath = Request.Form["oupath"].ToString();
            string firstname = Request.Form["firstname"].Trim().ToString();
            string lastname = Request.Form["lastname"].Trim().ToString();
            string displayname = Request.Form["displayname"].Trim().ToString();
            string description = Request.Form["description"].Trim().ToString();
            string office = Request.Form["office"].Trim().ToString();
            string telephone = Request.Form["telephone"].Trim().ToString();
            string mail = Request.Form["mail"].Trim().ToString();
            string Address = Request.Form["Address"].Trim().ToString();
            string PostOfficeBox = Request.Form["PostOfficeBox"].Trim().ToString();
            string City = Request.Form["City"].Trim().ToString();
             string admindisplayname = Request.Form["admindisplayname"].Trim().ToString();
            string comment = Request.Form["comment"].Trim().ToString();
            string adminDescription = Request.Form["adminDescription"].Trim().ToString();
            string Company = Request.Form["Company"].Trim().ToString();
            string PostalCode = Request.Form["PostalCode"].Trim().ToString();
            string LoginScript = Request.Form["LoginScript"].Trim().ToString();
            string LocalPath = Request.Form["LocalPath"].Trim().ToString();
            string Pager = Request.Form["Pager"].Trim().ToString();
            string Province = Request.Form["Province"].Trim().ToString();
            string MobilePhone = Request.Form["MobilePhone"].Trim().ToString();
            string Title = Request.Form["Title"].Trim().ToString();
            string Department = Request.Form["Department"].Trim().ToString();
             string accountExpireDate = Request.Form["accountExpireDate"].ToString();
            string profilePath = Request.Form["profilePath"].Trim().ToString();
            string homeDrive = Request.Form["homeDrive"].Trim().ToString();
            string scriptPath = Request.Form["scriptPath"].Trim().ToString();
            string homeDirectory = Request.Form["homeDirectory"].Trim().ToString();
            string numberof = Request.Form["numberof"].ToString();
           int leng= numberof.Split(',').Length;
            List<string> MemberOf = new List<string>();
            for(int i = 0; i < leng-1; i++)
            {
                MemberOf.Add(numberof.Split(',')[i]);
            }
            Users ad_user = HttpContext.Application["ad_user"] as Users;
            List<UserDTO> col = ad_user.SearchAllUserDTO(username, domain, (int)UserSearchKey.sAMAccountName, (int)SearchPattern.Equals, 0);
            UserDTO ud = col.ElementAt(0);
            ud.FirstName = firstname;
            ud.scriptPath = scriptPath;
            ud.homeDrive = homeDrive;
            ud.homeDirectory = homeDirectory;
            ud.PostalCode = PostalCode;
            ud.UserPrincipalName = upnname;
            ud.Pager = Pager;
            ud.Province = Province;
            ud.profilePath = profilePath;
            ud.MobilePhone = MobilePhone;
            ud.Title = Title;
            ud.Department = Department;
            ud.UserID = userid;
            if (accountExpireDate != "Never")
            {
                DateTime dt = Convert.ToDateTime(accountExpireDate);
                ud.AccountExpDate = dt;
            }else
            {
                ud.AccountExpDate = DateTime.MinValue;
            }
           
            ud.Address = Address;
            ud.PostOfficeBox = PostOfficeBox;
            ud.City = City;
            ud.adminDescription = adminDescription;
            ud.adminDisplayName = admindisplayname;
            ud.comment = comment;
            ud.LastName = lastname;
            ud.DisplayName = displayname;
            ud.Description = description;
            ud.Office = office;
            ud.Telephone = telephone;
            ud.mail = mail;
            ud.Company = Company;
            ud.MemberOf = MemberOf;
           
            if (action == "enable")
            {
                ud.AccountDisabled = false;
            }
            else
            {
                ud.AccountDisabled = true;
                
            }
           
            string m;
            string a = "";
            if (oupath != "")
            {
                if (ad_user.UpdateUserDTO(ud,ref a) && ad_user.MoveUserIntoNewOU(ud, oupath))
                {
                    m = "Successful modification!";
                    LogHelper.WriteLog(typeof(UserManagementController), Operator, "Modify%the%information%of%" + upnname, true);
                    return Json(new JsonData(m, true));
                }
                else
                {
                    m = "Modify failed!";
                    LogHelper.WriteLog(typeof(UserManagementController), Operator, "Modify%the%information%of%" + upnname, false);
                    return Json(new JsonData(m, false));
                }
            }
            else
            {
                if (ad_user.UpdateUserDTO(ud,ref a))
                {
                    m = "Successful modification!";
                    LogHelper.WriteLog(typeof(UserManagementController), Operator, "Modify%the%information%of%" + upnname, true);
                    return Json(new JsonData(m, true));
                }
                else
                {
                    m = "Modify failed!";
                    LogHelper.WriteLog(typeof(UserManagementController), Operator, "Modify%the%information%of%" + upnname, false);
                    return Json(new JsonData(m, false));
                }
            }
           
            


        }
        public JsonResult Delete()
        {
            string Operator = "";
            if (Session["username"].ToString() != null)
            {
                Operator = Session["username"].ToString();
            }                                                                       //Log日志要记录的用户名    
            string userid = Request.Form["userid"].Trim().ToString();
            string domain = Request.Form["domain"].Trim().ToString();
            Users ad_user = HttpContext.Application["ad_user"] as Users;
            List<UserDTO> col = ad_user.SearchAllUserDTO(userid, domain, (int)UserSearchKey.sAMAccountName, (int)SearchPattern.Equals, 0);
            UserDTO ud = col.ElementAt(0);
            string m;
            if (ad_user.DeleteUserDTO(ud))
            {
                m = "Delete success";
                LogHelper.WriteLog(typeof(UserManagementController), Operator, "Delete%a%user%named%" + userid, true);
            }
            else
            {
                m = "Delete failed";
                LogHelper.WriteLog(typeof(UserManagementController), Operator, "Delete%a%user%named%" + userid, false);
            }

            return Json(new JsonData(m));
        }
        [HttpGet]
        public ActionResult Create(string abc)
        {
            string currentdomain = (string)Session["domain"];
            Users ad_user;
            List<string> DomainLst = HttpContext.Application["domains"] as List<string>;
            if (DomainLst.Count > 0)
            {
                ad_user = HttpContext.Application["ad_user"] as Users;
                List<SelectListItem> DomainItem = new List<SelectListItem>();
                if (currentdomain != null)
                {
                    DomainItem.Add(new SelectListItem { Text = currentdomain, Value = currentdomain });
                }
                foreach (var Item in DomainLst)
                {
                    if (Item.ToString() != currentdomain) { DomainItem.Add(new SelectListItem { Text = Item.ToString(), Value = Item.ToString() }); }
                }
                List<SelectListItem> accounttype = new List<SelectListItem>();            //为searchfield添加下拉选项
                accounttype.Add(new SelectListItem { Text = "User Employee ", Value = "1" });
                accounttype.Add(new SelectListItem { Text = "User Consultant", Value = "2" });
                accounttype.Add(new SelectListItem { Text = "User Extranet", Value = "3" });
                accounttype.Add(new SelectListItem { Text = "Service Account", Value = "4" });
                UserCreateModel UCM = new UserCreateModel()
                {
                    domains = DomainItem,
                    accounttype = accounttype,
                };
                return View(UCM);
            }
            else
                return View();//返回结果集合
        }
        [HttpPost]
        public JsonResult CreateUser()
        {
            string Operator = "";
            if (Session["username"].ToString() != null)
            {
                Operator = Session["username"].ToString();
            }                                                                       //Log日志要记录的用户名    
            string message = "";

            string memberof = Request.Form["membersof"].Trim().ToString();
            int leng = memberof.Split(',').Length;
            List<string> MemberOf = new List<string>();
            for (int i = 0; i < leng - 1; i++)
            {
                MemberOf.Add(memberof.Split(',')[i]);
            }
            string OuPath = Request.Form["OuPath"].Trim().ToString();
            string domain = Request.Form["domain"].Trim().ToString();
            string userlogonname = Request.Form["userlogonname"].Trim().ToString();
            string firstname = Request.Form["firstname"].Trim().ToString();
            string lastname = Request.Form["lastname"].Trim().ToString();
            string fullname = Request.Form["fullname"].Trim().ToString();
            string password = Request.Form["password"].Trim().ToString();
            string umcpanl = Request.Form["umcpanl"].Trim().ToString();
            string uccp = Request.Form["uccp"].Trim().ToString();
            string pne = Request.Form["pne"].Trim().ToString();
            string aid = Request.Form["aid"].Trim().ToString();
            string AccountLockouttime = Request.Form["AccountLockouttime"].Trim().ToString();
            string officephone = Request.Form["officephone"].Trim().ToString();
            string Department = Request.Form["Department"].Trim().ToString();

            string Manager = Request.Form["Manager"].Trim().ToString();
            string Company = Request.Form["Company"].Trim().ToString();
            string EmployeeID = Request.Form["EmployeeID"].Trim().ToString();
            string Office = Request.Form["Office"].Trim().ToString();

            string Country = Request.Form["Country"].Trim().ToString();
            string City = Request.Form["City"].Trim().ToString();
            Users ad_user = HttpContext.Application["ad_user"] as Users;
            UserDTO ud = new UserDTO();
            ud.City = City;
            ud.MemberOf = MemberOf;
            ud.Company = Company;
            ud.Office = Office;
            ud.managedBy = Manager;
            ud.Department = Department;
            ud.FirstName = firstname;
            ud.LastName = lastname;
            if (pne.Equals("true"))
            {
                ud.PasswordNeverExpires = true;
            }
            if (uccp.Equals("true"))
            {
                ud.PasswordCantChange = true;
            }
            if (umcpanl.Equals("true"))
            {
                ud.MustChangePassword = true;
            }
            if (aid.Equals("true"))
            {
                ud.AccountLocked = true;
            }
          
            ud.UserPrincipalName = userlogonname + "@" + domain;
            ud.DisplayName = firstname + " " + lastname;
            ud.UserID = userlogonname;
            string errLevel = "";
            if (ad_user.CreateUserDTO(ud, OuPath, password, ref errLevel))
            {
                message = "User: <span style=\"color:green\">" + userlogonname + "</span> create successful";
                LogHelper.WriteLog(typeof(UserManagementController), Operator, "Create%user%named%" + userlogonname, true);
                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("~/UserDetail.xml"));
                XmlNode rootnode = doc.SelectSingleNode("ADUserDetail");
                XmlElement xe1 = doc.CreateElement("user");
                xe1.SetAttribute("Name", userlogonname);
                XmlElement xesub1 = doc.CreateElement("CreateBy");
                xesub1.InnerText = Operator;
                xe1.AppendChild(xesub1);
                XmlElement xesub2 = doc.CreateElement("CreateTime");
                xesub2.InnerText = DateTime.Today.ToString("yyyyMMdd"); 
                xe1.AppendChild(xesub2);
                XmlElement xesub3 = doc.CreateElement("LatestLogin");
                xesub3.InnerText = "";
                xe1.AppendChild(xesub3);
                rootnode.AppendChild(xe1);
                doc.Save(Server.MapPath("~/UserDetail.xml"));
            }
            else
            {
                message = "User created failed, may be the user <span style=\"color:green\">" + userlogonname + "</span> already exists.";
           
                LogHelper.WriteLog(typeof(UserManagementController), Operator, "Create%user%named%" + userlogonname, false);
            }
            return Json(new JsonData(message));
        }
      
        [HttpPost]
        public JsonResult RandomPass()
        {
            string randompass = COMMON.GenerateRandomPassword();
            return Json(new JsonData(randompass), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ChangePass()
        {
            string Operator = "";
            if (Session["username"].ToString() != null)
            {
                Operator = Session["username"].ToString();
            }                                                                       //Log日志要记录的用户名    
            string username = Request.Form["username"].Trim().ToString();
            string domain = Request.Form["domain"].Trim().ToString();
            string newpass = Request.Form["newpass"].Trim().ToString();
            Users ad_user = HttpContext.Application["ad_user"] as Users;
            List<UserDTO> col = ad_user.SearchAllUserDTO(username, domain, (int)UserSearchKey.sAMAccountName, (int)SearchPattern.Equals, 0);
            UserDTO ud = col.ElementAt(0);
            string m;
            if (ad_user.ResetUserDTOPwd(ud, newpass))
            {
                m = "Password modification success";
                LogHelper.WriteLog(typeof(UserManagementController), Operator, "Modified%the%password%of" + username, true);
            }
            else
            {
                m = "Password modification failed";
                LogHelper.WriteLog(typeof(UserManagementController), Operator, "Modified%the%password%of" + username, false);
            }
            return Json(new JsonData(m));
        }
        public JsonResult Outree()
        {
            string currentdomain = (string)Session["domain"];
            string domain = Request.QueryString["domain"].ToString();
            TreeNodeCollection tnc;
            //string admin = ConfigurationManager.AppSettings["ServiceAccount"];
            //string password = ADTOOLS.Common.DotNetEncrypt.DESEncrypt.Decrypt(ConfigurationManager.AppSettings["ServiceAccountPWD"]);
            //Directory ad_directory = new Directory(admin,password,"ccdroot.cn");
            // Directory ad_directory = HttpContext.Application["ad_directory"] as Directory;
            if (OuTreeCache.GetCache(domain) != null)
            {
                 tnc = (TreeNodeCollection)OuTreeCache.GetCache(domain);
            }
            else
            {
                tnc = (TreeNodeCollection)OuTreeCache.GetCache(currentdomain);

            }
           //TreeNodeCollection tnc = ad_directory.TreeView.Nodes;
            OuTreeModel ou = getTree(tnc[0], 1);
            List<OuTreeModel> result = new List<OuTreeModel>();
            result.Add(ou);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult inite_Outree()
        {
            string currentdomain = (string)Session["domain"];
           
            
            //string admin = ConfigurationManager.AppSettings["ServiceAccount"];
            //string password = ADTOOLS.Common.DotNetEncrypt.DESEncrypt.Decrypt(ConfigurationManager.AppSettings["ServiceAccountPWD"]);
            //Directory ad_directory = new Directory(admin,password,"ccdroot.cn");
            // Directory ad_directory = HttpContext.Application["ad_directory"] as Directory;
           
                TreeNodeCollection tnc = (TreeNodeCollection)OuTreeCache.GetCache(currentdomain);

            
            //TreeNodeCollection tnc = ad_directory.TreeView.Nodes;
            OuTreeModel ou = getTree(tnc[0], 1);
            List<OuTreeModel> result = new List<OuTreeModel>();
            result.Add(ou);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public OuTreeModel getTree(TreeNode tnc, int m)
        {
            OuTreeModel model = new OuTreeModel();
            model.id = m++;
            model.text = tnc.Text;
            model.path = tnc.ToolTip;
            model.children = new List<OuTreeModel>();
            if (tnc.ChildNodes.Count > 0)
            {
                foreach (TreeNode node in tnc.ChildNodes)
                {
                    OuTreeModel childmodel = new OuTreeModel();
                    childmodel.children = new List<OuTreeModel>();
                    childmodel.text = node.Text;
                    m = 10 + m++;
                    childmodel.id = m;
                    childmodel.path = node.ToolTip;
                    if (node.ChildNodes.Count > 0)
                    {
                        for (int i = 0; i < node.ChildNodes.Count; i++)
                        {
                            childmodel.state = "closed";
                            childmodel.children.Add(getTree(node.ChildNodes[i], m * 10));
                        }
                    }
                    model.children.Add(childmodel);
                }
            }
            return model;
        }
        [HttpPost]
        public JsonResult EnableLync()
        {
            string Operator = "";
            if (Session["username"].ToString() != null)
            {
                Operator = Session["username"].ToString();
            }                                                                       //Log日志要记录的用户名     
            string username = Request.Form["username"].Trim().ToString();
            string domain = Request.Form["domain"].Trim().ToString();
            string action = Request.Form["action"].Trim().ToString();
            Users ad_user = HttpContext.Application["ad_user"] as Users;
            List<UserDTO> col = ad_user.SearchAllUserDTO(username, domain, (int)UserSearchKey.sAMAccountName, (int)SearchPattern.Equals, 0);
            UserDTO ud = col.ElementAt(0);
            SkypeForBusinessDTO sip = new SkypeForBusinessDTO(ud);
            string mess = "";
            string m;
            if (action == "enable")
            {
                if (sip.EnableSIP(ref mess))
                {
                    m = mess;
                    // LogHelper.WriteLog(typeof(UserManagementController), Operator, "Disable%the%Lync%of%" + username + "Succeed", true);
                }
                else
                {
                    m = mess;
                    // LogHelper.WriteLog(typeof(UserManagementController), Operator, "Disable%the%Lync%of%" + username + "Failed", false);
                }
            }
            else
            {
                if (sip.DisableSIP(ref mess))
                {
                    m = mess;
                    //LogHelper.WriteLog(typeof(UserManagementController), Operator, "Disable%the%Lync%of%" + username + "Succeed", true);
                }
                else
                {
                    m = mess;
                    // LogHelper.WriteLog(typeof(UserManagementController), Operator, "Disable%the%Lync%of%" + username + "Failed", false);
                }
            }
            return Json(new JsonData(m));
        }
        [HttpPost]
        public JsonResult EnableExchange()
        {
            string Operator = "";
            if (Session["username"].ToString() != null)
            {
                Operator = Session["username"].ToString();
            }                                                                       //Log日志要记录的用户名     
            string username = Request.Form["username"].Trim().ToString();
            string exchangeType = Request.Form["exchangeType"].Trim().ToString();
            string domain = Request.Form["domain"].Trim().ToString();
            string action = Request.Form["action"].Trim().ToString();
            Users ad_user = HttpContext.Application["ad_user"] as Users;
            List<UserDTO> col = ad_user.SearchAllUserDTO(username, domain, (int)UserSearchKey.sAMAccountName, (int)SearchPattern.Equals, 0);
            UserDTO ud = col.ElementAt(0);
            ExchangeMailboxDTO EMD = new ExchangeMailboxDTO(ud);
            string m;
            string mess = "";

            if (action == "enable")
            {

                EMD.AccountType = exchangeType;
              
                if (EMD.EnableMailbox(ref mess))
                {
                   
                    m = "Successful modification!";
                    LogHelper.WriteLog(typeof(UserManagementController), Operator, "Disable%the%Lync%of%" + username + "Succeed", true);
                }
                else
                {
                    m = mess;
                   LogHelper.WriteLog(typeof(UserManagementController), Operator, "Disable%the%Lync%of%" + username + "Failed", false);
                }
            }
            else
            {
                if (EMD.DisableMailbox(ref mess))
                {
                    m = "Successful modification!";
                     LogHelper.WriteLog(typeof(UserManagementController), Operator, "Disable%the%Lync%of%" + username + "Succeed", true);
                }
                else
                {
                    m = mess;
                    LogHelper.WriteLog(typeof(UserManagementController), Operator, "Disable%the%Lync%of%" + username + "Failed", false);
                }
            }
            return Json(new JsonData(m));
        }
        public JsonResult EnableAccount()
        {
            string Operator = "";
            if (Session["username"].ToString() != null)
            {
                Operator = Session["username"].ToString();
            }                                                                       //Log日志要记录的用户名     
            string username = Request.Form["username"].Trim().ToString();
            string domain = Request.Form["domain"].Trim().ToString();
            string action = Request.Form["action"].Trim().ToString();
            Users ad_user = HttpContext.Application["ad_user"] as Users;
            List<UserDTO> col = ad_user.SearchAllUserDTO(username, domain, (int)UserSearchKey.sAMAccountName, (int)SearchPattern.Equals, 0);
            UserDTO ud = col.ElementAt(0);
            string m;
            string a = "";
            if (action == "enable")
            {
                ud.AccountDisabled = false;
                if (ad_user.UpdateUserDTO(ud,ref a))
                {
                    m = "Enable account success";
                    // LogHelper.WriteLog(typeof(UserManagementController), Operator, "Disable%the%Lync%of%" + username + "Succeed", true);
                }
                else
                {
                    m = "Enable account failed";
                    // LogHelper.WriteLog(typeof(UserManagementController), Operator, "Disable%the%Lync%of%" + username + "Failed", false);
                }
            }
            else
            {
                ud.AccountDisabled = true;
                if (ad_user.UpdateUserDTO(ud,ref a))
                {
                    m = "Disable account success";
                    // LogHelper.WriteLog(typeof(UserManagementController), Operator, "Disable%the%Lync%of%" + username + "Succeed", true);
                }
                else
                {
                    m = "Disable account failed";
                    // LogHelper.WriteLog(typeof(UserManagementController), Operator, "Disable%the%Lync%of%" + username + "Failed", false);
                }
            }
            return Json(new JsonData(m));
        }
        [HttpPost]
        public JsonResult AccountUnlock()
        {
            string Operator = "";
            if (Session["username"].ToString() != null)
            {
                Operator = Session["username"].ToString();
            }                                                                       //Log日志要记录的用户名     
            string username = Request.Form["username"].Trim().ToString();
            string domain = Request.Form["domain"].Trim().ToString();
          

            Users ad_user = HttpContext.Application["ad_user"] as Users;
            List<UserDTO> col = ad_user.SearchAllUserDTO(username, domain, (int)UserSearchKey.sAMAccountName, (int)SearchPattern.Equals, 0);
            UserDTO ud = col.ElementAt(0);
            
                ud.AccountDisabled = false;
           
            string m;
            string a = "";
            if (ad_user.UpdateUserDTO(ud,ref a))
            {
                m = "Unlock account success";
                LogHelper.WriteLog(typeof(UserManagementController), Operator, "UnLock%the%Account%of%" + username, true);
            }
            else
            {
                m = "Unlock failed!";
                LogHelper.WriteLog(typeof(UserManagementController), Operator, "UnLock%the%Account%of%" + username, false);
            }
            return Json(new JsonData(m));
        }
        [HttpGet]
        public ActionResult ModifyExchange(string abc)
        {
            string userid = Request.QueryString["userid"].Trim().ToString();
            string domainname = Request.QueryString["domain"].Trim().ToString();
            Users ad_user = HttpContext.Application["ad_user"] as Users;
            List<UserDTO> col = ad_user.SearchAllUserDTO(userid, domainname, (int)UserSearchKey.sAMAccountName, (int)SearchPattern.Equals, 0);
            UserDTO ud = col.ElementAt(0);
            ExchangeMailboxDTO EMD = new ExchangeMailboxDTO(ud);
            List<SelectListItem> Database = new List<SelectListItem>();
            string currentdb = EMD.homeMDB;
            Database.Add(new SelectListItem { Text = currentdb, Value = currentdb });
            foreach (var Item in EMD.MDBs)
            {
                if (Item.Name != currentdb)
                {
                    Database.Add(new SelectListItem { Text = Item.Name, Value = Item.Name });
                }
            }
            List<SelectListItem> EAitem = new List<SelectListItem>();
            string currentaddress = EMD.PrimaryEmailAddress.Split('@')[1];
            EAitem.Add(new SelectListItem { Text = currentaddress, Value = currentaddress });

            foreach (var Item in EMD.AcceptedDomain)
            {
                if (Item.ToString() != currentaddress) { EAitem.Add(new SelectListItem { Text = Item.ToString(), Value = Item.ToString() }); }
            }
            List<SelectListItem> MailboxPolicys = new List<SelectListItem>();
            string Policys = EMD.ActiveSyncMailboxPolicy;
            MailboxPolicys.Add(new SelectListItem { Text = Policys, Value = Policys });

            foreach (var Item in EMD.ActiveSyncMailboxPolicys)
            {
                if (Item.ToString() != Policys) { MailboxPolicys.Add(new SelectListItem { Text = Item.ToString(), Value = Item.ToString() }); }
            }
            List<SelectListItem> QuotaPlan = new List<SelectListItem>();
            string Quotas = EMD.MailboxQuota.Name;
            QuotaPlan.Add(new SelectListItem { Text = Quotas, Value = Quotas });

            foreach (var Item in EMD.QuotaPlan)
            {
                if (Item.Name.ToString() != Quotas) { QuotaPlan.Add(new SelectListItem { Text = Item.Name.ToString(), Value = Item.Name.ToString() }); }
            }
            ExchangeModel Em = new ExchangeModel()
            {
                AccountType = EMD.AccountType,
                DataCenter = EMD.DataCenter,
                EmailAddress = EMD.PrimaryEmailAddress.Split('@')[0],
                LinkedAccountname = EMD.LinkedAccount[1],
                SendAsDelegates = EMD.SendAsDelegates,
                FMADelegates = EMD.FMADelegates,
                Database = Database,
                EAitem = EAitem,
                LinkedAccountdomain = EMD.LinkedAccount[0],
                 BlackBerryEnabled=EMD.BlackBerryEnabled,
                IMAPEnabled =EMD.IMAPEnabled,
                POP3Enabled =EMD.POP3Enabled,
                HideFromOAB =EMD.HideFromOAB,
                RestrictedUsage =EMD.RestrictedUsage,
                MailboxPolicys = MailboxPolicys,
                QuotaPlan=QuotaPlan,
                DisplayName=EMD.DisplayName,
            };
            return View(Em);
        }
        public JsonResult Group_GroupSearch()
        {
                                                                               //Log日志要记录的用户名      
            string domain = Request.Form["groupdomain"].Trim().ToString();
            string searchkeyword = Request.Form["searchkeyword"].ToString();
            Groups ad_group = HttpContext.Application["ad_group"] as Groups;
            List<GroupsDTO> col;
            if (searchkeyword.Trim() == "")
            {
                col = ad_group.SearchAllGroupsDTO(domain);
            }
            else
            {
                col = ad_group.SearchAllGroupsDTO(searchkeyword, domain);
            }


            string con = "<table id = \"GroupChose\" class=\"display\" cellspacing=\"0\" width=\"100%\"><thead><tr><th>ID</th><th>Name</th></tr></thead><tbody>";

            for (int i = 0; i < col.Count; i++)
            {
                GroupsDTO gd = col.ElementAt(i);

                con += "<tr><td>" + i + "</td><td>" + gd.Name + "</td></tr>";//数据行，字段对应数据库查询字段
            }
            con += " </tbody></table>";

            return Json(new JsonData(con));
        }
        public JsonResult GroupSearch()
        {
                                                                                 //Log日志要记录的用户名      
            string domain = Request.Form["groupdomain"].Trim().ToString();
            string searchkeyword = Request.Form["searchkeyword"].ToString();

            string userid = Request.Form["userid"].Trim().ToString();
            string domainname = Request.Form["domainname"].Trim().ToString();
            Groups ad_group = HttpContext.Application["ad_group"] as Groups;
            List<GroupsDTO> col;
            if (searchkeyword.Trim() == "")
            {
                col = ad_group.SearchAllGroupsDTO(domain);
            }
            else
            {
                col = ad_group.SearchAllGroupsDTO(searchkeyword, domain);
            }
 Users ad_user = HttpContext.Application["ad_user"] as Users;
            List<UserDTO> col1 = ad_user.SearchAllUserDTO(userid, domainname, (int)UserSearchKey.sAMAccountName, (int)SearchPattern.Equals, 0);
            UserDTO ud = col1.ElementAt(0);
           string con = "<table id = \"GroupChose\" class=\"display\" cellspacing=\"0\" width=\"100%\"><thead><tr><th>ID</th><th>Name</th></tr></thead><tbody>";
           
           
            for (int i = 0; i < col.Count; i++)
            {
                GroupsDTO gd = col.ElementAt(i);

                if (Array.IndexOf<string>(ud.MemberOf.ToArray(), gd.Name) == -1) { 
                 
                        con += "<tr><td>" + i + "</td><td>" + gd.Name + "</td></tr>";//数据行，字段对应数据库查询字段
                    }
 }
            con += " </tbody></table>";
          
            return Json(new JsonData(con));
        }
        public JsonResult GroupSearch_Group()
        {
            //Log日志要记录的用户名      
            string domain = Request.Form["groupdomain"].Trim().ToString();
            string searchkeyword = Request.Form["searchkeyword"].ToString();

           
            Groups ad_group = HttpContext.Application["ad_group"] as Groups;
            List<GroupsDTO> col;
            if (searchkeyword.Trim() == "")
            {
                col = ad_group.SearchAllGroupsDTO(domain);
            }
            else
            {
                col = ad_group.SearchAllGroupsDTO(searchkeyword, domain);
            }
           
            string con = "<table id = \"GroupChose\" class=\"display\" cellspacing=\"0\" width=\"100%\"><thead><tr><th>ID</th><th>Name</th></tr></thead><tbody>";


            for (int i = 0; i < col.Count; i++)
            {
                GroupsDTO gd = col.ElementAt(i);

                

                    con += "<tr><td>" + i + "</td><td>" + gd.Name + "</td></tr>";//数据行，字段对应数据库查询字段
               
            }
            con += " </tbody></table>";

            return Json(new JsonData(con));
        }
        public ActionResult Report()
        {
          List<UserDetail> data = new List<UserDetail>();
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("~/UserDetail.xml"));
           XmlNode node = doc.SelectSingleNode("ADUserDetail");
       
    XmlNodeList xnl = node.ChildNodes;
            int i = 0;
    foreach (XmlNode xn1 in xnl)
                {
                UserDetail user = new UserDetail();
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                // 得到Type和ISBN两个属性的属性值
                user.id = i++;
                user.name = xe.GetAttribute("Name").ToString();
                   
      XmlNodeList xnl0 = xe.ChildNodes;
                   user.Createby = xnl0.Item(0).InnerText;
     user.CreateTime = xnl0.Item(1).InnerText;
                     user.LatestLogin = xnl0.Item(2).InnerText;
                data.Add(user);
            }
            UserDetailModel UM = new UserDetailModel()
            {
                userdetail=data,
            };
           
             return View(UM);

        }
        public JsonResult verifyExchange()
        {
            string mess = "";
            string userid = Request.Form["userid"].Trim().ToString();
            string domain = Request.Form["domain"].Trim().ToString();
            Users ad_user = HttpContext.Application["ad_user"] as Users;
            List<UserDTO> col = ad_user.SearchAllUserDTO(userid, domain, (int)UserSearchKey.sAMAccountName, (int)SearchPattern.Equals, 0);
            UserDTO ud = col.ElementAt(0);
            if (ud.ExchangeEnabled)
            {
                mess = userid + "@" + domain;
                return Json(new JsonData(mess, true));
            }
            else
            {
                mess = "Exchange is not yet enable, please enable before use! ";
                return Json(new JsonData(mess,false));
            }

            
        }
        public JsonResult UPNName()
        {
            
            string username = Request.Form["username"].Trim().ToString();
            
            string domain = Request.Form["domain"].Trim().ToString();
            Users ad_user = HttpContext.Application["ad_user"] as Users;
            List<UserDTO> col = new List<UserDTO>();
            int counter;
            try
            {
               col = ad_user.SearchAllUserDTO(username, domain, 0, 1, 0);
                counter = col.Count;
            }catch(Exception e)
            {
                counter = 0;
                
            }
            return Json(new JsonData(counter.ToString()));
        }
        public JsonResult Testmanager()         //创建用户时检测当前domain是否存在该用户
        {
            
          
            string userid = Request.Form["manager"].Trim().ToString();
            string domainname = Request.Form["domain"].Trim().ToString();
            Users ad_user = HttpContext.Application["ad_user"] as Users;
            List<UserDTO> col = ad_user.SearchAllUserDTO(userid, domainname, (int)UserSearchKey.sAMAccountName, (int)SearchPattern.Equals, 0);
            if (col.Count > 0)
            {
                string memeberof = "";
                UserDTO  ud = col.ElementAt(0);
               
                for(int i = 0; i < ud.MemberOf.Count; i++)
                {
                    memeberof += ud.MemberOf[i] + ",";
                }
                userdto userdto = new userdto(ud, memeberof,1);
                return Json(userdto, JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Json(new JsonData("The User You Typed Is Not Existed!"));

            }
           
          
        }
    }
}