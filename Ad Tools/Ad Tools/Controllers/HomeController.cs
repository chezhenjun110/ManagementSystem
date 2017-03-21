using Ad_Tools.Log4net;
using Ad_Tools.Models;
using ADTOOLS.AD;
using ADTOOLS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Xml;

namespace Ad_Tools.Controllers
{
    public class HomeController : Controller
    {

        //GET

        public ActionResult Login(string a)
        {

            List<string> DomainLst = HttpContext.Application["domains"] as List<string>;
           
            if (DomainLst!=null&& DomainLst.Count > 0)
            {

                List<SelectListItem> DomainItem = new List<SelectListItem>();
                foreach (var Item in DomainLst)
                {
                    DomainItem.Add(new SelectListItem { Text = Item.ToString(), Value = Item.ToString() });
                }

                UserLoginModel USVM = new UserLoginModel()
                {
                    domains = DomainItem,
      
                };
                return View(USVM);
            }
            else
            {
                System.Exception exp = HttpContext.Application["startup_exception"] as System.Exception;
                if (exp != null)
                {
                    ModelState.AddModelError("Error", exp.Message);
                    return  new HttpStatusCodeResult(500, exp.Message);
                }
                else
                {
                    UserLoginModel USVM = new UserLoginModel()
                    {
                        domains = new List<SelectListItem>(),

                    };
                    return View(USVM);//返回结果集合
                }

               

            }
                
        }
        [HttpPost]
        public ActionResult Login()
        {

            string member = "";
            List<string> memberof = null;
            bool logined = false;
            string username = Request.Form["username"].Trim().ToString();
            string domian = Request.Form["domain"].Trim().ToString();
            string password = Request.Form["password"].Trim().ToString();
            string LoginFailedInfo = "";
            try
            {
                Users ad_user = HttpContext.Application["ad_user"] as Users;
                List<UserDTO> col = ad_user.SearchAllUserDTO(username, domian, (int)UserSearchKey.sAMAccountName, (int)SearchPattern.Equals, 0);
                UserDTO ud = col.ElementAt(0);
                memberof = ud.MemberOf;
               
            }
            catch (Exception e)
            {
                return Json(new JsonData(e.ToString()));

            }
           if(ADHelper.IsAuthenticated(username, password, ref LoginFailedInfo).Equals(LoginResult.LOGIN_USER_OK))
            {
                logined = true;
               
                Session["memberof"] = memberof;//存入session
                Session["username"] = username;
                Session["domain"] = domian;
                if (memberof.Contains("DeletePermission"))
                {
                    Session["DeletePermission"] = true;
                }
                else
                {
                    Session["DeletePermission"] = false;

                };
                LogHelper.WriteLog(typeof(HomeController), username, "Login",true);


               
                XmlDocument doc = new XmlDocument();
               
                doc.Load(Server.MapPath("~/UserDetail.xml"));
                XmlElement root = null;
                root = doc.DocumentElement;
                //从session得到用户
               
                //XmlNodeList listNodes = root.SelectNodes(permission);
                XmlNode node = doc.SelectSingleNode("ADUserDetail");
                     // 得到根节点的所有子节点
                XmlNodeList xnl = node.ChildNodes;
                
                foreach (XmlNode xn1 in xnl)
                {
                    
                    // 将节点转换为元素，便于得到节点的属性值
                    XmlElement xe = (XmlElement)xn1;
                    // 得到Type和ISBN两个属性的属性值
                    
                    if(xe.GetAttribute("Name").ToString()== username){
                        XmlNodeList xnl0 = xe.ChildNodes;
                     xnl0.Item(2).InnerXml=DateTime.Today.ToString("yyyyMMdd");
                    
                    }

                    
                }
                
                doc.Save(Server.MapPath("~/UserDetail.xml"));


            }
            else
            {
                member = LoginFailedInfo;
                LogHelper.WriteLog(typeof(HomeController), username, "Login",false);
            }

            return Json(new JsonData(member,logined));
        }
   
       
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            LogHelper.WriteLog(typeof(HomeController), Session["username"].ToString(), "LogOff", true);
            Session["username"] = null;
           
       

            return RedirectToAction("Login", "Home");
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}