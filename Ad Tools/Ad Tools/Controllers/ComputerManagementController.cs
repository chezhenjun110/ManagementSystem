using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ADTOOLS.AD;
using ADTOOLS.DTO;
using Ad_Tools.Models;
using Ad_Tools.Log4net;
using System.Xml;

namespace Ad_Tools.Controllers
{
    public class ComputerManagementController : BaseController
    {


      
        // GET: ComputerManagement
        public ActionResult SearchModify(string abc)
        {
            string currentdomain = (string)Session["domain"];

            List<string> DomainLst = HttpContext.Application["domains"] as List<string>;  
            if (DomainLst.Count>0)
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
                ComputerSearchViewModels CSVM = new ComputerSearchViewModels()
                {
                    domains = DomainItem,
                    searchcriteria = searchcriteria,
                    searchfield=searchfield,
                   
                };
               return View(CSVM);    
           }
            else
                return View();//返回结果集合
        }
        [HttpPost]
        public JsonResult SearchModify()
        {
                                                                            //Log日志要记录的用户名       
            string domain = Request.Form["domain"].Trim().ToString();
            string searchcriteria = Request.Form["searchcriteria"].Trim().ToString();
            string searchkeyword = Request.Form["searchkeyword"].Trim().ToString();
            Computers ad_computer = HttpContext.Application["ad_computer"] as Computers;
            List<ComputerDTO> col;
            if (searchkeyword.Trim() != "")
            {
                col = ad_computer.SearchAllComputersDTO(searchkeyword, domain, Int32.Parse(searchcriteria));

            }
            else
            {
                col = ad_computer.SearchAllComputersDTO(searchkeyword, domain, 0);

            }

            string con = "<table id = \"example\" class=\"display\" cellspacing=\"0\" width=\"100%\"><thead><tr><th>ID</th><th>Name</th><th>OUName</th><th>OperatingSystem</th></tr></thead><tbody>";

            for (int i = 0; i < col.Count; i++)
            {
                ComputerDTO cd = col.ElementAt(i);
                con += "<tr" + " " + "onclick=\"computer_detail(this)\"><td>" + i + "</td><td>" + cd.Name + "</td><td>" + cd.OUName + "</td><td>" + cd.operatingSystem + "</td></tr>";//数据行，字段对应数据库查询字段
            }
            con += " </tbody></table>";
         
            return Json(new JsonData(con));
        }
        [HttpPost]
        public JsonResult Details()
        {
                                                                               //Log日志要记录的用户名      
            string computername = Request.Form["computername"].Trim().ToString();
            string domainname = Request.Form["domainname"].Trim().ToString();
            Computers ad_computer = HttpContext.Application["ad_computer"] as Computers;
            List<ComputerDTO> col = ad_computer.SearchAllComputersDTO(computername, domainname, (int)SearchPattern.Equals);
            ComputerDTO cd = col.ElementAt(0);
            computerdto cdt = new computerdto(cd);
            return Json(cdt,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Update_Computer()
        {
            string Operator = "";
            if (Session["username"].ToString() != null)
            {
                Operator = Session["username"].ToString();
            }                                                                       //Log日志要记录的用户名       
            string name = Request.Form["name"].ToString();
            string computername = Request.Form["computername"].Trim().ToString();
            string site = Request.Form["site"].Trim().ToString();
            string oupath = Request.Form["oupath"].ToString();
            string description = Request.Form["Description"].Trim().ToString();
            string domain = Request.Form["domain"].Trim().ToString();
            string manageby = Request.Form["manageby"].ToString();
            Computers ad_computer = HttpContext.Application["ad_computer"] as Computers;

            List<ComputerDTO> col = ad_computer.SearchAllComputersDTO(name, domain, (int)SearchPattern.Equals);

            ComputerDTO cd = col.ElementAt(0);
            cd.site = site;
            cd.Name = computername;
            cd.managedBy = manageby;
           cd.Description = description;
            string m;
            if (oupath != "")
            {
                if (ad_computer.UpdateComputerDTO(cd) && ad_computer.MoveComputerIntoNewOU(cd, oupath))
                {
                    m = "Successful modification!";
                    LogHelper.WriteLog(typeof(ComputerManagementController), Operator, "Modify%the%information%of%" + computername.Replace(" ", ""), true);
                    return Json(new JsonData(m, true));
                }
                else
                {
                    m = "Modify failed!";
                    LogHelper.WriteLog(typeof(ComputerManagementController), Operator, "Modify%the%information%of%" + computername.Replace(" ", ""), false);
                    return Json(new JsonData(m, false));
                }
            }else
            {
                if (ad_computer.UpdateComputerDTO(cd))
                {
                    m = "Successful modification!";
                    LogHelper.WriteLog(typeof(ComputerManagementController), Operator, "Modify%the%information%of%" + computername.Replace(" ", ""), true);
                    return Json(new JsonData(m, true));
                }
                else
                {
                    m = "Modify failed!";
                    LogHelper.WriteLog(typeof(ComputerManagementController), Operator, "Modify%the%information%of%" + computername.Replace(" ", ""), false);
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
            string computername = Request.Form["computername"].Trim().ToString();
            string domain = Request.Form["domain"].Trim().ToString();
            Computers ad_computer = HttpContext.Application["ad_computer"] as Computers;
            List<ComputerDTO> col = ad_computer.SearchAllComputersDTO(computername, domain, (int)SearchPattern.Equals);
            ComputerDTO cd = col.ElementAt(0);
            string m;
            if (ad_computer.DeleteComputerDTO(cd))
            {
                m = "Delete success";
                LogHelper.WriteLog(typeof(ComputerManagementController), Operator, "Delete%a%Computer%named%" + computername, true);
            }
            else
            {
                m = "Delete failed";
                LogHelper.WriteLog(typeof(ComputerManagementController), Operator, "Delete%a%Computer%named%" + computername, false);
            }
            return Json(new JsonData(m));
        }
        public ActionResult Report()
        {
            
            List<ComputerDetail> data = new List<ComputerDetail>();
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("~/ComputerDetail.xml"));
            XmlNode node = doc.SelectSingleNode("ADComputerDetail");



            // 得到根节点的所有子节点
            XmlNodeList xnl = node.ChildNodes;
            int i = 0;
            foreach (XmlNode xn1 in xnl)
            {
                ComputerDetail computer = new ComputerDetail();
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                // 得到Type和ISBN两个属性的属性值
                computer.id = i++;
                computer.name = xe.GetAttribute("Name").ToString();

                XmlNodeList xnl0 = xe.ChildNodes;
                computer.Createby = xnl0.Item(0).InnerText;
                computer.CreateTime = xnl0.Item(1).InnerText;
               
                data.Add(computer);
            }
            ComputerDetailModel CM = new ComputerDetailModel()
            {
                computerdetail =data,
            };

            return View(CM);
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
                
                ComputerCreateModels UCM = new ComputerCreateModels()
                {
                    domains = DomainItem,
                   

                };
                return View(UCM);
            }
            else
                return View();//返回结果集合
        }
        [HttpPost]
        public ActionResult Create()
        {
            string Operator = "";
            if (Session["username"].ToString() != null)
            {
                Operator = Session["username"].ToString();
            }                                                                       //Log日志要记录的用户名      
            string message = "";

            string domain = Request.Form["domain"].Trim().ToString();
            string computername = Request.Form["computername"].Trim().ToString();
            string ou = Request.Form["ou"].Trim().ToString();
            string description = Request.Form["description"].Trim().ToString();

            Computers ad_computer = HttpContext.Application["ad_computer"] as Computers;
            ComputerDTO cdt = new ComputerDTO();
            cdt.Description = description;
            cdt.Name = computername;
            cdt.dNSHostName = computername + "@" + domain;
            int errLevel = 0;
            if (ad_computer.CreateComputerDTO(cdt, ou,ref errLevel))
            {
                message = "The name <span style =\"color:green\">" + computername + "</span> is created for the success of the computer";
                LogHelper.WriteLog(typeof(ComputerManagementController), Operator, "Create%Computer%named%" + computername, true);
                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("~/ComputerDetail.xml"));
                XmlNode rootnode = doc.SelectSingleNode("ADComputerDetail");
                XmlElement xe1 = doc.CreateElement("computer");
                xe1.SetAttribute("Name", computername);
                XmlElement xesub1 = doc.CreateElement("CreateBy");
                xesub1.InnerText = Operator;
                xe1.AppendChild(xesub1);
                XmlElement xesub2 = doc.CreateElement("CreateTime");
                xesub2.InnerText = DateTime.Today.ToString("yyyyMMdd");
                xe1.AppendChild(xesub2);
                rootnode.AppendChild(xe1);
                doc.Save(Server.MapPath("~/ComputerDetail.xml"));
            }
            else
            {

                message = "Failed to create a computer, may be the name<span style =\"color:green\">" + computername + "</span> already exists";
                LogHelper.WriteLog(typeof(ComputerManagementController), Operator, "Create%Computer%named%" + computername, false);
            }
            return Json(new JsonData(message));
        }
        public JsonResult Computer_UserSearch()
        { 
         string domain = Request.Form["domain"].Trim().ToString();
        string searchfield = Request.Form["searchfield"].Trim().ToString();
        string searchcriteria = Request.Form["searchcriteria"].Trim().ToString();

        string searchkeyword = Request.Form["searchkeyword"].Trim().ToString();
     
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
string con = "<table id = \"example2\" class=\"display\" cellspacing=\"0\" width=\"100%\"><thead><tr><th>ID</th><th>UserID</th><th>OU</th></tr></thead><tbody>";
            for (int i = 0; i<col.Count; i++)
            {
                UserDTO ud = col.ElementAt(i);

                con += "<tr><td id=\"ID" + i + "\">" + i + "</td><td>" + ud.UserID + "</td><td>" + ud.DistinguishedName + "</td></tr>";//数据行，字段对应数据库查询字段
              }
            con += " </tbody></table>";
          
            return Json(new JsonData(con));
         

        }
        
    }
}