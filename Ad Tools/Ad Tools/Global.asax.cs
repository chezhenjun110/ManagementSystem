using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Configuration;
using ADTOOLS.AD;
using log4net.Config;
using System.Web.UI.WebControls;
using System.Web;
using Ad_Tools.Common;
using ADTOOLS.DTO;
using System.Xml;
using System.Linq;
namespace Ad_Tools
{
    public class MvcApplication : System.Web.HttpApplication
    {
        string admin;
        string password;
        string domain;
        Directory ad_directory;
        Directory ad_directory_Outree;
        List<String> domains;
        System.Exception startup_exception;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            try
            {
                XmlConfigurator.Configure();   //程序启动时启动log4net来生成日志
                admin = ConfigurationManager.AppSettings["ServiceAccount"];
                password = ADTOOLS.Common.DotNetEncrypt.DESEncrypt.Decrypt(ConfigurationManager.AppSettings["ServiceAccountPWD"]);
                domain = ConfigurationManager.AppSettings["ServiceAccountDomain"];
                ad_directory = new Directory(admin, password);
                if (!ad_directory.connected) throw new System.Exception("Not able connect to Active Directory, Please check up network settings!");
                domains = ad_directory.GetAllDomainsNamesInForest();
                Computers ad_computer = new Computers(admin, password, domain);
                Users ad_user = new Users(admin, password, domain);
                Groups ad_group = new Groups(admin, password, domain);
                ADHelper ad_helper = new ADHelper(admin, password, domain);
                Application["ad_directory"] = ad_directory;
                Application["domains"] = domains;
                Application["ad_computer"] = ad_computer;
                Application["ad_user"] = ad_user;
                Application["ad_group"] = ad_group;
                Application["ad_helper"] = ad_helper;
                for (int i = 0; i < domains.Count; i++)                               //程序启动时缓存domain对应的树状菜单
                {
                    ad_directory_Outree = new Directory(admin, password, domains[i]);
                    TreeNodeCollection tnc = ad_directory_Outree.TreeView.Nodes;
                    OuTreeCache.SetCache(domains[i], tnc);
                    //程序启动时获取所有domain中的组名并写入xml文件
                }
            }
            catch (Exception e)
            {
                Application["startup_exception"] = e;
            }
            finally
            {

            }

        }

        private void RedirectToAction(object p)
        {
            throw new NotImplementedException();
        }
    }
}
