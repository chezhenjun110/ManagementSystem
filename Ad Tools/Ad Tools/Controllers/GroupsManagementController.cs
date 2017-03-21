using Ad_Tools.Log4net;
using Ad_Tools.Models;
using ADTOOLS.AD;
using ADTOOLS.DTO;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web.Mvc;
using System.Xml;

namespace Ad_Tools.Controllers
{
    public class GroupsManagementController : BaseController
    {

        // GET: GroupsManagement
        public ActionResult SearchModify()
        {
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
                List<SelectListItem> searchcriteria = new List<SelectListItem>();
                searchcriteria.Add(new SelectListItem { Text = "Equals", Value = "1" });
                searchcriteria.Add(new SelectListItem { Text = "Contains", Value = "2" });
                searchcriteria.Add(new SelectListItem { Text = "Stars with", Value = "3" });
                searchcriteria.Add(new SelectListItem { Text = "Ends with", Value = "4" });
                List<SelectListItem> searchfield = new List<SelectListItem>();            //为searchfield添加下拉选项
                searchfield.Add(new SelectListItem { Text = "UserID", Value = "0" });
                searchfield.Add(new SelectListItem { Text = "First Name", Value = "1" });
                searchfield.Add(new SelectListItem { Text = "Last Name", Value = "2" });
                searchfield.Add(new SelectListItem { Text = "Full Name", Value = "3" });
                searchfield.Add(new SelectListItem { Text = "Company", Value = "4" });
                searchfield.Add(new SelectListItem { Text = "Mail", Value = "5" });
                searchfield.Add(new SelectListItem { Text = "ID Manager", Value = "6" });
                GroupSearchViewModels CSVM = new GroupSearchViewModels()
                {
                    domains = DomainItem,
                    searchfield = searchfield,
                    searchcriteria = searchcriteria,
                };
                return View(CSVM);
            }
            else
                return View();//返回结果集合
        }
        [HttpPost]
        public JsonResult Search()
        {
            //Log日志要记录的用户名      
            string domain = Request.Form["domain"].Trim().ToString();

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


            string con = "<table id = \"example\" class=\"display\" cellspacing=\"0\" width=\"100%\"><thead><tr><th>ID</th><th>Name</th><th>BelongsOUPath</th><th>Description</th><th>Exchange Modify</th></tr></thead><tbody>";

            for (int i = 0; i < col.Count; i++)
            {
                GroupsDTO gd = col.ElementAt(i);
                con += "<tr" + " " + "onclick=\"Group_detail(this)\"><td>" + i + "</td><td>" + gd.Name + "</td><td>" + gd.BelongsOUPath + "</td><td>" + gd.Description + "</td><td ><a type=\"button\" onclick=\"ExchangeEnabled(this)\" target=\"_blank\" class=\"btn btn - block\">Modify</a></td></tr>";//数据行，字段对应数据库查询字段
            }
            con += " </tbody></table>";

            return Json(new JsonData(con));
        }
        public ActionResult Report()
        {
            List<GroupDetail> data = new List<GroupDetail>();
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("~/GroupDetail.xml"));
            XmlNode node = doc.SelectSingleNode("ADGroupDetail");



            // 得到根节点的所有子节点
            XmlNodeList xnl = node.ChildNodes;
            int i = 0;
            foreach (XmlNode xn1 in xnl)
            {
                GroupDetail group = new GroupDetail();
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                // 得到Type和ISBN两个属性的属性值
                group.id = i++;
                group.name = xe.GetAttribute("Name").ToString();

                XmlNodeList xnl0 = xe.ChildNodes;
                group.Createby = xnl0.Item(0).InnerText;
                group.CreateTime = xnl0.Item(1).InnerText;

                data.Add(group);
            }
            GroupDetailModel GM = new GroupDetailModel()
            {
                groupdetail = data,
            };

            return View(GM);

        }
        [HttpGet]
        public ActionResult Create(string abc)
        {

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

                GroupCreateViewModels GCVM = new GroupCreateViewModels()
                {
                    domains = DomainItem,
                };
                return View(GCVM);
            }
            else
                return View();//返回结果集合
        }
        [HttpPost]
        public JsonResult Create()
        {
            string msg = "";
            string Operator = "";
            if (Session["username"].ToString() != null)
            {
                Operator = Session["username"].ToString();
            }
            string OuPath = Request.Form["OuPath"].Trim().ToString();                                                                   //Log日志要记录的用户名       
            string groupname = Request.Form["groupname"].Trim().ToString();
            string domain = Request.Form["domain"].Trim().ToString();
            string grouptype = Request.Form["grouptype"].Trim().ToString();
            string groupscope = Request.Form["groupscope"].Trim().ToString();
            string EnableExchange = Request.Form["EnableExchange"].Trim().ToString();
            Groups ad_group = HttpContext.Application["ad_group"] as Groups;
            GroupsDTO gd = new GroupsDTO();

            gd.BelongsOUPath = OuPath;
            gd.SamAccountName = groupname;
            if (grouptype.Equals("Security"))
            {
                gd.isSecurityGroup = true;

            }
            if (groupscope.Equals("DomainLocale"))
            {

                gd.GroupScope = GroupScope.Local;

            }
            else if (groupscope.Equals("Gloable"))
            {
                gd.GroupScope = GroupScope.Global;
            }
            else
            {
                gd.GroupScope = GroupScope.Universal;
            }

            string message;
            if (ad_group.CreateUserGroup(gd))
            {
                if (EnableExchange.Equals("true"))
                {
                    List<GroupsDTO> col = ad_group.SearchAllGroupsDTO(groupname, domain);
                    GroupsDTO gd2 = col.ElementAt(0);
                    ExchangeDistributionGroupDTO EDG = new ExchangeDistributionGroupDTO(gd2);

                    if (EDG.EnableDistributionGroup(ref msg))
                    {
                        gd2.ExchangeEnabled = true;
                        EDG = new ExchangeDistributionGroupDTO(gd2);
                    }
                    else
                    {
                        msg = "error";
                    }
                }
                message = "Group:<span style =\"color:green\">" + groupname + "</span> create successful";
                LogHelper.WriteLog(typeof(GroupsManagementController), Operator, "Create%Group%named%" + groupname, true);
                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("~/GroupDetail.xml"));
                XmlNode rootnode = doc.SelectSingleNode("ADGroupDetail");
                XmlElement xe1 = doc.CreateElement("user");
                xe1.SetAttribute("Name", groupname);
                XmlElement xesub1 = doc.CreateElement("CreateBy");
                xesub1.InnerText = Operator;
                xe1.AppendChild(xesub1);
                XmlElement xesub2 = doc.CreateElement("CreateTime");
                xesub2.InnerText = DateTime.Today.ToString("yyyyMMdd");
                xe1.AppendChild(xesub2);
                rootnode.AppendChild(xe1);
                doc.Save(Server.MapPath("~/GroupDetail.xml"));
            }
            else
            {
                message = "Name for group <span style =\"color:green\">" + groupname + "</span>creation failed,It could be that the group already exists";
                LogHelper.WriteLog(typeof(GroupsManagementController), Operator, "Create%Group%named%" + groupname, false);
            }
            return Json(new JsonData(message));
        }
        [HttpPost]
        public JsonResult Details()
        {
            //Log日志要记录的用户名      
            string groupname = Request.Form["groupname"].Trim().ToString();
            string domain = Request.Form["domain"].Trim().ToString();
            Groups ad_group = HttpContext.Application["ad_group"] as Groups;
            List<GroupsDTO> col = ad_group.SearchAllGroupsDTO(groupname, domain);
            GroupsDTO gd = col.ElementAt(0);


            string con = "<table id = \"Memberof\" class=\"display\" cellspacing=\"0\" width=\"100%\"><thead><tr><th>ID</th><th>GroupName</th></tr></thead><tbody>";
            for (int i = 0; i < gd.MembersOf.Count; i++)
            {
                string memberof = gd.MembersOf.ElementAt(i);

                con += "<tr><td>" + i + "</td><td>" + memberof + "</td></tr>";//数据行，字段对应数据库查询字段
            }
            con += " </tbody></table>";
            string members = "<table id = \"Mem\" class=\"display\" cellspacing=\"0\" width=\"100%\"><thead><tr><th>ID</th><th>MembersName</th></tr></thead><tbody>";
            for (int i = 0; i < gd.Members.Count; i++)
            {
                string mem = gd.Members.ElementAt(i);

                members += "<tr><td>" + i + "</td><td>" + mem + "</td></tr>";//数据行，字段对应数据库查询字段
            }
            members += " </tbody></table>";
            groupdto gdt = new groupdto(gd, con, members);

            return Json(gdt, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Update()
        {
            string Operator = "";
            if (Session["username"].ToString() != null)
            {
                Operator = Session["username"].ToString();
            }                                                                       //Log日志要记录的用户名      
            string name = Request.Form["name"].ToString();
            string groupname = Request.Form["groupname"].Trim().ToString();
            string domain = Request.Form["domain"].Trim().ToString();
            string oupath = Request.Form["oupath"].ToString();
            string description = Request.Form["description"].Trim().ToString();
            string email = Request.Form["email"].Trim().ToString();
            string note = Request.Form["note"].Trim().ToString();
            string manageby = Request.Form["manageby"].ToString();
            string numberof = Request.Form["numberof"].ToString();
            string grouptype = Request.Form["grouptype"].Trim().ToString();
            string groupscope = Request.Form["groupscope"].ToString();
            int leng = numberof.Split(',').Length;
            List<string> MemberOf = new List<string>();
            for (int i = 0; i < leng - 1; i++)
            {
                MemberOf.Add(numberof.Split(',')[i]);
            }
            string members = Request.Form["Members"].ToString();
            int len = members.Split(',').Length;
            List<string> Members = new List<string>();
            for (int i = 0; i < len - 1; i++)
            {
                Members.Add(members.Split(',')[i]);
            }
            Groups ad_group = HttpContext.Application["ad_group"] as Groups;
            List<GroupsDTO> col = ad_group.SearchAllGroupsDTO(name, domain);
            GroupsDTO gd = col.ElementAt(0);
            if (groupscope.Equals("DomainLocale"))
            {
                gd.GroupScope = GroupScope.Local;
            }
            else if (groupscope.Equals("Gloable"))
            {
                gd.GroupScope = GroupScope.Global;
            }
            else
            {
                gd.GroupScope = GroupScope.Universal;
            }
            if (grouptype.Equals("Security"))
            {
                gd.isSecurityGroup = true;

            }
            gd.Description = description;
            gd.SamAccountName = groupname;
            gd.Email = email;
            gd.Note = note;
            gd.managedBy = manageby;
            gd.MembersOf = MemberOf;
            gd.Members = Members;
            string m;
            string msg = "";
            if (oupath != "")
            {
                if (ad_group.UpdateGroupsDTO(gd, ref msg) && ad_group.MoveGroupIntoNewOU(gd, oupath))
                {
                    m = "Successful modification!";
                    LogHelper.WriteLog(typeof(GroupsManagementController), Operator, "Modify%the%information%of%" + name.Replace(" ", ""), true);
                    return Json(new JsonData(m, true));
                }
                else
                {
                    m = "Modify failed! " + msg;
                    LogHelper.WriteLog(typeof(GroupsManagementController), Operator, "Modify%the%information%of%" + name.Replace(" ", ""), false);
                    return Json(new JsonData(m, false));
                }
            }
            else
            {
                if (ad_group.UpdateGroupsDTO(gd, ref msg))
                {
                    m = "Successful modification!";
                    LogHelper.WriteLog(typeof(GroupsManagementController), Operator, "Modify%the%information%of%" + name.Replace(" ", ""), true);
                    return Json(new JsonData(m, true));
                }
                else
                {
                    m = "Modify failed! " + msg;
                    LogHelper.WriteLog(typeof(GroupsManagementController), Operator, "Modify%the%information%of%" + name.Replace(" ", ""), false);
                    return Json(new JsonData(m, false));
                }
            }


        }
        [HttpPost]
        public JsonResult Delete()
        {
            string Operator = "";
            if (Session["username"].ToString() != null)
            {
                Operator = Session["username"].ToString();
            }                                                                       //Log日志要记录的用户名      
            string groupname = Request.Form["groupname"].Trim().ToString();
            string domain = Request.Form["domain"].Trim().ToString();

            Groups ad_group = HttpContext.Application["ad_group"] as Groups;
            List<GroupsDTO> col = ad_group.SearchAllGroupsDTO(groupname, domain);

            GroupsDTO gd = col.ElementAt(0);

            string m;
            if (ad_group.DeleteUserGroupsDTO(gd))
            {
                m = "Delete Success";
                LogHelper.WriteLog(typeof(GroupsManagementController), Operator, "Delete%a%Group%named%" + groupname, true);
            }
            else
            {
                m = "Delete failed";
                LogHelper.WriteLog(typeof(GroupsManagementController), Operator, "Delete%a%Group%named%" + groupname, false);
            }
            return Json(new JsonData(m));
        }
        public ActionResult AuthorityManagement(string abc)
        {
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
                List<SelectListItem> searchcriteria = new List<SelectListItem>();
                searchcriteria.Add(new SelectListItem { Text = "Equals", Value = "1" });
                searchcriteria.Add(new SelectListItem { Text = "Contains", Value = "2" });
                searchcriteria.Add(new SelectListItem { Text = "Stars with", Value = "3" });
                searchcriteria.Add(new SelectListItem { Text = "Ends with", Value = "4" });
                GroupSearchViewModels CSVM = new GroupSearchViewModels()
                {
                    domains = DomainItem,


                };
                return View(CSVM);
            }
            else
                return View();//返回结果集合
        }
        [HttpPost]
        public JsonResult AuthorityManagement()
        {
            string Operator = "";
            if (Session["username"].ToString() != null)
            {
                Operator = Session["username"].ToString();
            }                                                                       //Log日志要记录的用户名       
            string domain = Request.Form["domain"].Trim().ToString();
            string searchcriteria = Request.Form["searchcriteria"].Trim().ToString();
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


            string con = "<table id = \"example\" class=\"display\" cellspacing=\"0\" width=\"100%\"><thead><tr><th>ID</th><th>Name</th></tr></thead><tbody>";
            for (int i = 0; i < col.Count; i++)
            {
                GroupsDTO gd = col.ElementAt(i);
                con += "<tr" + " " + "onclick=\"GetAuthorityCode(this)\"><td>" + i + "</td><td>" + gd.Name + "</td></tr>";//数据行，字段对应数据库查询字段
            }
            con += " </tbody></table>";
            return Json(new JsonData(con));

        }
        public JsonResult GetAuthorityCode()                          //得到权限组的代码,读取per.xml文件
        {
            string Operator = "";
            if (Session["username"].ToString() != null)
            {
                Operator = Session["username"].ToString();
            }                                                                       //Log日志要记录的用户名      

            string groupname = Request.Form["groupname"].Trim().ToString();
            string domain = Request.Form["domain"].Trim().ToString();
         
            string permission = "";
            XmlDocument doc = new XmlDocument();
            List<String> al = new List<String>();
            doc.Load(Server.MapPath("~/per.xml"));
            XmlElement root = doc.DocumentElement;
            //从session得到用户
            permission = "/Authority/" + domain +groupname.Replace(" ", "") + "/Permission";
            XmlNode node=root.SelectSingleNode(permission);
            if (node != null)
            {
                String p = node.InnerText;
                String[] str = new String[] { };
                str = p.Split(',');
                for (int j = 0; j < str.Length; j++)
                {
                    al.Add(str[j]);
                }

            }else
            {
                XmlNode rootnode = doc.SelectSingleNode("Authority");
                XmlElement xe1 = doc.CreateElement(domain + groupname.Replace(" ", ""));
                XmlElement xe11 = doc.CreateElement("Permission");
                xe11.InnerText = "";
                xe1.AppendChild(xe11);
                rootnode.AppendChild(xe1);
                doc.Save(Server.MapPath("~/per.xml"));
                al.Add("");
            }
          

            //根据用户从配置文件中得到权限


            return Json(al, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SetAuthorityCode()                          //设置权限组的代码，存储到per.xml文件
        {
            string Operator = "";
            if (Session["username"].ToString() != null)
            {
                Operator = Session["username"].ToString();
            }                                                                       //Log日志要记录的用户名       
            string message = "";
            string groupname = Request.Form["groupname"].Trim().ToString();
            string domain = Request.Form["domain"].Trim().ToString();
            string codestr = Request.Form["code"].Trim().ToString();
            string realgroupname = groupname.Replace(" ", "");
            string permission = "";
            XmlDocument doc = new XmlDocument();
            List<String> al = new List<String>();
            doc.Load(Server.MapPath("~/per.xml"));
            XmlElement root = null;
            root = doc.DocumentElement;
            //从session得到用户
            permission = "/Authority/" + domain+ groupname.Replace(" ", "") + "/Permission";
            XmlNodeList listNodes = root.SelectNodes(permission);
            int num = listNodes.Count;
               message = "修改节点";
             listNodes[0].InnerXml = codestr;
             doc.Save(Server.MapPath("~/per.xml"));
            

            return Json(new JsonData(message));
        }
        public JsonResult Group_UserSearch()
        {
            string domain = Request.Form["Udomain"].Trim().ToString();
            string searchfield = Request.Form["Usearchfield"].Trim().ToString();
            string searchcriteria = Request.Form["Usearchcriteria"].Trim().ToString();

            string searchkeyword = Request.Form["Usearchkeyword"].Trim().ToString();

            Users ad_user = HttpContext.Application["ad_user"] as Users;
            List<UserDTO> col;

            if (searchkeyword.Trim() == "")
            {
                col = ad_user.SearchAllUserDTO(searchkeyword, domain, Int32.Parse(searchfield), 0, 0);
            }
            else
            {
                col = ad_user.SearchAllUserDTO(searchkeyword, domain, Int32.Parse(searchfield), Int32.Parse(searchcriteria), 0);
            }
            string con = "<table id = \"example1\" class=\"display\" cellspacing=\"0\" width=\"100%\"><thead><tr><th>ID</th><th>UserID</th><th>OU</th></tr></thead><tbody>";
            for (int i = 0; i < col.Count; i++)
            {
                UserDTO ud = col.ElementAt(i);

                con += "<tr><td id=\"ID" + i + "\">" + i + "</td><td>" + ud.UserID + "</td><td>" + ud.DistinguishedName + "</td></tr>";//数据行，字段对应数据库查询字段
            }
            con += " </tbody></table>";


            return Json(new JsonData(con));


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
                return Json(new JsonData(mess, false));
            }


        }
        public JsonResult Group_UserSearch_Members()
        {
            string domain = Request.Form["Mdomain"].Trim().ToString();
            string searchfield = Request.Form["Msearchfield"].Trim().ToString();
            string searchcriteria = Request.Form["Msearchcriteria"].Trim().ToString();
            string searchkeyword = Request.Form["Msearchkeyword"].Trim().ToString();

            Users ad_user = HttpContext.Application["ad_user"] as Users;
            List<UserDTO> col;

            if (searchkeyword.Trim() == "")
            {
                col = ad_user.SearchAllUserDTO(searchkeyword, domain, Int32.Parse(searchfield), 0, 0);
            }
            else
            {
                col = ad_user.SearchAllUserDTO(searchkeyword, domain, Int32.Parse(searchfield), Int32.Parse(searchcriteria), 0);
            }
            string con = "<table id = \"MembersData\" class=\"display\" cellspacing=\"0\" width=\"100%\"><thead><tr><th>ID</th><th>UserID</th></tr></thead><tbody>";
            for (int i = 0; i < col.Count; i++)
            {
                UserDTO ud = col.ElementAt(i);

                con += "<tr><td id=\"ID" + i + "\">" + i + "</td><td>" + ud.DisplayName + "</td></tr>";//数据行，字段对应数据库查询字段
            }
            con += " </tbody></table>";


            return Json(new JsonData(con));


        }
        public ActionResult ModifyExchange()
        {
            string keyword = Request.QueryString["keyword"].Trim().ToString();
            string domainname = Request.QueryString["domain"].Trim().ToString();
            // string keyword = Request.Form["keyword"].Trim().ToString();


            Groups ad_group = HttpContext.Application["ad_group"] as Groups;
            List<GroupsDTO> col = ad_group.SearchAllGroupsDTO(keyword, domainname);
            GroupsDTO gd = col.ElementAt(0);
            ExchangeDistributionGroupDTO EDG = new ExchangeDistributionGroupDTO(gd);

            List<SelectListItem> EmailHost = new List<SelectListItem>();
            string currentdb = EDG.PrimaryEmailAddress.Split('@')[1];
            EmailHost.Add(new SelectListItem { Text = "@" + currentdb, Value = currentdb });
            foreach (var Item in EDG.AcceptedDomain)
            {
                if (Item.ToString() != currentdb)
                {
                    EmailHost.Add(new SelectListItem { Text = "@" + Item.ToString(), Value = Item.ToString() });
                }
            }
            List<SelectListItem> Manager = new List<SelectListItem>();


            foreach (var Item in EDG.managedByList)
            {

                EmailHost.Add(new SelectListItem { Text = Item.ToString(), Value = Item.ToString() });

            }
            List<SelectListItem> SendersAllowedList = new List<SelectListItem>();


            foreach (var Item in EDG.SendersAllowedList)
            {

                EmailHost.Add(new SelectListItem { Text = Item.ToString(), Value = Item.ToString() });

            }


            GroupExchangeModel GEM = new GroupExchangeModel()
            {

                Description = EDG.Description,
                DisplayName = EDG.DisplayName,
                GroupName = EDG.DisplayName,
                managedByList = Manager,
                SendersAllowedList = SendersAllowedList,
                EmailHost = EmailHost,
                Emailname = EDG.PrimaryEmailAddress.Split('@')[0],
                HideFromAB = EDG.HideFromOAB,
                RequireSenderAuthenticationEnabled = EDG.RequireSenderAuthenticationEnabled,
                IndudeinGalsync = EDG.IncludedInGalsync,
            };
            return View(GEM);

        }   //显示数据
        public JsonResult ExchangModfiy()   //后台处理提交的数据并修改
        {

            string estr = "";
            string userid = Request.Form["userid"].Trim().ToString();
            string domain = Request.Form["domain"].Trim().ToString();
            string DLShortname = Request.Form["DLShortname"].Trim().ToString();
            string GroupName = Request.Form["GroupName"].Trim().ToString();
            string Emailname = Request.Form["Emailname"].Trim().ToString();
            string RequireSenderAuthenticationEnabled = Request.Form["RequireSenderAuthenticationEnabled"].Trim().ToString();
            string IndudeinGalsync = Request.Form["IndudeinGalsync"].Trim().ToString();
            string Description = Request.Form["Description"].Trim().ToString();
            string DisplayName = Request.Form["DisplayName"].Trim().ToString();
            string HideFromOAB = Request.Form["HideFromOAB"].Trim().ToString();
            Groups ad_group = HttpContext.Application["ad_group"] as Groups;
            List<GroupsDTO> col = ad_group.SearchAllGroupsDTO(userid, domain);
            GroupsDTO gd = col.ElementAt(0);
            ExchangeDistributionGroupDTO EDG = new ExchangeDistributionGroupDTO(gd);
            EDG.Description = Description;
            EDG.DisplayName = DisplayName;
            if (HideFromOAB == "true")
            {
                EDG.HideFromOAB = true;
            }
            else
            {
                EDG.HideFromOAB = false;
            }
            if (RequireSenderAuthenticationEnabled == "true")
            {
                EDG.RequireSenderAuthenticationEnabled = true;
            }
            else
            {
                EDG.RequireSenderAuthenticationEnabled = false;
            }
            bool result = EDG.Update(ref estr);
            if (result)
            {
                return Json(new JsonData("Successful modification!"), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new JsonData(estr), JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ExchangeEnabled()
        {
            string msg = "";                                               //Log日志要记录的用户名       
            string groupname = Request.Form["groupname"].Trim().ToString();
            string domain = Request.Form["domain"].Trim().ToString();
            Groups ad_group = HttpContext.Application["ad_group"] as Groups;
            List<GroupsDTO> col = ad_group.SearchAllGroupsDTO(groupname, domain);
            GroupsDTO gd = col.ElementAt(0);
            if (gd.ExchangeEnabled)
            {
                msg = "1";
            }
            else
            {
                msg = "0";

            }
            return Json(new JsonData(msg));

        }
        public JsonResult EnableGroupExchange()
        {
            string msg = "";                                               //Log日志要记录的用户名       
            string groupname = Request.Form["groupname"].Trim().ToString();
            string domain = Request.Form["domain"].Trim().ToString();

            Groups ad_group = HttpContext.Application["ad_group"] as Groups;

            List<GroupsDTO> col = ad_group.SearchAllGroupsDTO(groupname, domain);
            GroupsDTO gd = col.ElementAt(0);

            if (gd.GroupScope != GroupScope.Universal)
            {
                gd.GroupScope = GroupScope.Universal;

            }
            bool result = ad_group.UpdateGroupsDTO(gd, ref msg);
            if (result)
            {
                ExchangeDistributionGroupDTO EDG = new ExchangeDistributionGroupDTO(gd);

                if (EDG.EnableDistributionGroup(ref msg))
                {
                    gd.ExchangeEnabled = true;
                    EDG = new ExchangeDistributionGroupDTO(gd);
                    msg = "1";
                }
                else
                {
                    msg = "0";
                }
            }
            else
            {
                msg = "0";
            }

            return Json(new JsonData(msg));
        }



    }
}
