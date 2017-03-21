using Ad_Tools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Xml;

namespace Ad_Tools.Controllers
{
    public class _LayoutController : Controller
    {
        // GET: _Layout
      public JsonResult DeleteButton()
        {
           bool t=(Boolean) Session["DeletePermission"];
            if (t)
            {
                return Json(new JsonData("true"),JsonRequestBehavior.AllowGet);
            }else
            {
                return Json(new JsonData("false"),JsonRequestBehavior.AllowGet);
            }
          
        }
        public ActionResult UserPermission()
        {
    
        string permission = "";
            List<string> al = new List<string>();
            String p = null;
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("~/per.xml"));
            XmlElement root = null;
            root = doc.DocumentElement;
            XmlNode rootnode;
            //从session得到用户

            if (Session["memberof"] != null)
            {

                List<string> abc = (List<string>)Session["memberof"];
               string domain= (string)Session["domain"];
                for (int i = 0; i < abc.Count; i++)
                {
                    permission = "/Authority/"+domain +abc[i].Replace(" ", "") + "/Permission";
                    rootnode = root.SelectSingleNode(permission);
                    if (rootnode != null)
                    {
                        p = rootnode.InnerText;
                        String[] str = new String[] { };
                        str = p.Split(',');
                        for (int j = 0; j < str.Length; j++)
                        {
                            if (str[j] != "") { al.Add(str[j]); }

                        }
                    }
                      
                }
            }
            if (al.Count == 0)
            {
                al.Add("0");
            }
            //根据用户从配置文件中得到权限
            List<int> cache = al.Select(x => int.Parse(x)).ToList();
            List<int> perList = cache.Distinct().ToList();
            perList.Sort((x, y) => x.CompareTo(y));
            return Json(perList, JsonRequestBehavior.AllowGet);
        }
    }
}