using Ad_Tools.Models;
using ADTOOLS.AD;
using ADTOOLS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Ad_Tools.Controllers
{
    public class ExchangeManagementController : BaseController
    {
        // GET: ExchangeManagement
        [HttpGet]
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
                UserSearchKey.company.ToString();
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
                UserSearchModifyViewModel USVM = new UserSearchModifyViewModel()
                {
                    domains = DomainItem,
                    searchcriteria = searchcriteria,
                    searchtype = searchtype,
                    searchfield = searchfield,
                };
                return View(USVM);
            }
            else
                return View();//返回结果集合
        }
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
            if (searchkeyword.Trim() == "")
            {
                col = ad_user.SearchAllUserDTO(searchkeyword, domain, Int32.Parse(searchfield), 0, Int32.Parse(searchtype));
            }
            else
            {
                col = ad_user.SearchAllUserDTO(searchkeyword, domain, Int32.Parse(searchfield), Int32.Parse(searchcriteria), Int32.Parse(searchtype));
            }
            string con = "<table id = \"example\" class=\"display\" cellspacing=\"0\" width=\"100%\"><thead><tr><th>ID</th><th>UserID</th><th>First Name</th><th>Last Name</th><th>UserPrincipal Name</th><th>Full Name</th><th>Company</th><th>Mail</th><th>ID Manager</th></tr></thead><tbody>";
            for (int i = 0; i < col.Count; i++)
            {
                UserDTO ud = col.ElementAt(i);
                con += "<tr" + " " + "onclick=\"Exchange_detail(this)\"><td>" + i + "</td><td>" + ud.UserID + "</td><td>" + ud.FirstName + "</td><td>" + ud.LastName + "</td><td>" + ud.UserPrincipalName + "</td><td>" + ud.FirstName + "" + ud.LastName + "</td><td>" + ud.Company + "</td><td>" + ud.mail + "</td><td>" + ud.managedBy + "</td></tr>";//数据行，字段对应数据库查询字段
            }
            con += " </tbody></table>";
            // LogHelper.WriteLog(typeof(UserManagementController),Operator,"Search%Users",true);
            return Json(new JsonData(con));
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
            ExchangeMailboxDTO EMD = new ExchangeMailboxDTO(ud);
            return Json(EMD, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]


        public JsonResult ExchangModfiy()
        {
            string userid = Request.Form["userid"].Trim().ToString();
            string domain = Request.Form["domain"].Trim().ToString();
             string Database = Request.Form["Database"].Trim().ToString();
            string emailname = Request.Form["emailname"].Trim().ToString();
           string DisplayName = Request.Form["DisplayName"].Trim().ToString();
            string FMADelegates = Request.Form["FMADelegates"].Trim().ToString();
            string BlackBerryEnabled = Request.Form["BlackBerryEnabled"].Trim().ToString();
            string RestrictedUsage = Request.Form["RestrictedUsage"].Trim().ToString();
            string HideFromOAB = Request.Form["HideFromOAB"].Trim().ToString();
            string IMAPEnabled = Request.Form["IMAPEnabled"].Trim().ToString();
            string POP3Enabled = Request.Form["POP3Enabled"].Trim().ToString();
            string EmailDomian = Request.Form["EmailDomian"].Trim().ToString();
            string ActiveSyncMailboxPolicys = Request.Form["ActiveSyncMailboxPolicys"].Trim().ToString();
            string Emailboxquotas = Request.Form["Emailboxquotas"].ToString();
            Users ad_user = HttpContext.Application["ad_user"] as Users;
            List<UserDTO> col = ad_user.SearchAllUserDTO(userid, domain, (int)UserSearchKey.sAMAccountName, (int)SearchPattern.Equals, 0);
            UserDTO ud = col.ElementAt(0);
            ExchangeMailboxDTO EMD = new ExchangeMailboxDTO(ud);
             EMD.homeMDB = Database;
            EMD.PrimaryEmailAddress = emailname + "@" + EmailDomian;
            EMD.DisplayName = DisplayName;

            if (RestrictedUsage == "true")
            {
                EMD.RestrictedUsage = true;
            }else
            {
                EMD.RestrictedUsage = false;
            }
            if (POP3Enabled == "true")
            {
                EMD.POP3Enabled = true;
            }
            else
            {
                EMD.POP3Enabled = false;
            }
            if (IMAPEnabled == "true")
            {
                EMD.IMAPEnabled = true;
            }
            else
            {
                EMD.IMAPEnabled = false;
            }
            if (HideFromOAB == "true")
            {
                EMD.HideFromOAB = true;
            }
            else
            {
                EMD.HideFromOAB = false;
            }
            if (BlackBerryEnabled == "true")
            {
                EMD.BlackBerryEnabled = true;
            }
            else
            {
                EMD.BlackBerryEnabled = false;
            }
            EMD.ActiveSyncMailboxPolicy = ActiveSyncMailboxPolicys;
        
            EMD.MailboxQuota =EMD.GetQuotaPlan(Emailboxquotas);
            string estr = "";
            EMD.Update(ref estr);
            if (estr == "")
            {
                return Json(new JsonData("Successful modification!"), JsonRequestBehavior.AllowGet);
            }else
            {
                return Json(new JsonData(estr), JsonRequestBehavior.AllowGet);
            }
           
        }
        
    }
}