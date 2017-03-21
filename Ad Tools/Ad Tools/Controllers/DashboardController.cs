using Ad_Tools.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
namespace Ad_Tools.Controllers
{
    public class DashboardController : BaseController
    {
        // GET: Dashboard
        public ActionResult ActivityandLog()
        {

            List<SelectListItem> filelist = new List<SelectListItem>();      //获取log文件的集合

            string filepath1 = "~/log/";
            string file1 = Server.MapPath(filepath1);
            DirectoryInfo folder = new DirectoryInfo(file1);
            string date = "AT_"+DateTime.Today.ToString("yyyyMMdd");
            filelist.Add(new SelectListItem { Text = date, Value = date });
            foreach (FileInfo logfile in folder.GetFiles("*.log"))
            {
                string filename = logfile.Name.Split('.')[0];
                if (filename != date)
                {
                    filelist.Add(new SelectListItem { Text = filename, Value = filename });

                }
                
            }
            string Operator = "";
            if (Session["username"].ToString() != null)
            {
                Operator = Session["username"].ToString();
            }                                                                       //Log日志要记录的用户名    
           // LogHelper.WriteLog(typeof(UserManagementController), Operator, "View%log", true);
            LogListModel list = new LogListModel();
            list.loglist = new List<LogModel>();
            string data = DateTime.Today.ToString("yyyyMMdd");
            string filepath = "~/log/AT_" + data + ".log";
            string file = Server.MapPath(filepath);
            using (FileStream fsRead = new FileStream(file, FileMode.Open))
            {
                StreamReader sr = new StreamReader(fsRead);
                // Read and display lines from the file until the end of 
                // the file is reached.
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(' ');
                    LogModel lm = new LogModel();
                    lm.Date = line[0];
                    lm.Time = line[1].Split(',')[0];
                    string[] msg = line[6].Split('*');
                    lm.Operator = msg[0];
                    lm.Description = msg[1].Replace("%", " ");
                    lm.Status = msg[2];
                    list.loglist.Add(lm);
                }
                LogListModel LM = new LogListModel()
                {
                    loglist = list.loglist,
                    filelist = filelist,
                };

                return View(LM);
            }
        }
        public ActionResult SystemHealthy()
        {
            return View();
        }
        public JsonResult ClearLog()
        {
            string logname = Request.Form["logname"].Trim().ToString();
            string date= DateTime.Today.ToString("yyyyMMdd");
            string filename = "~/log/" + logname + ".log";
            string path = Server.MapPath(filename);
            string verify = "AT_" + date;
            if (logname == verify)
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Write);
                fs.SetLength(0);
                fs.Close();
            }
            else
            {
                if (System.IO.File.Exists(path))
                {
                    //如果存在则删除
                    System.IO.File.Delete(path);
                }
            }
            string filepath1 = "~/log/";
            string file1 = Server.MapPath(filepath1);
            DirectoryInfo folder = new DirectoryInfo(file1);
            string currentdate = "AT_" + DateTime.Today.ToString("yyyyMMdd");
            string dropdown="<option value =\"" + currentdate + "\">" + currentdate + "</option>";
            foreach (FileInfo logfile in folder.GetFiles("*.log"))
            {
                string listfile = logfile.Name.Split('.')[0];
                if (listfile != currentdate)
                {
                  dropdown+= "<option value =\"" + listfile + "\">" + listfile + "</option>";

                }

            }
            string con = "<table id = \"example\" class=\"display\" cellspacing=\"0\" width=\"100%\"><thead><tr><th> Owner</th><th>Date</th><th>Target</th><th>Request Type</th><th>Gpt Object</th><th>Time</th><th>Status</th><th>Status Code</th><th>Description</th></tr></thead><tbody></tbody></table>";
            return Json(new JsonData(con+"@"+dropdown));
        }
        public JsonResult GetLog()
        {
            string logname = Request.Form["logname"].Trim().ToString();
            LogListModel list = new LogListModel();

            list.loglist = new List<LogModel>();
            string filepath = "~/log/"+logname + ".log";
            string file = Server.MapPath(filepath);
            using (FileStream fsRead = new FileStream(file, FileMode.Open))
            { 
                         StreamReader sr = new StreamReader(fsRead);
                
                while (!sr.EndOfStream)
                {
                    
                    string[] line = sr.ReadLine().Split(' ');
                    LogModel lm = new LogModel();
                    lm.Date = line[0];
                    lm.Time = line[1].Split(',')[0];
                    string[] msg = line[6].Split('*');
                    lm.Operator = msg[0];
                    lm.Description = msg[1].Replace("%", " ");
                    lm.Status = msg[2];
                    list.loglist.Add(lm);
                   
                }
                string con = "<table id = \"example\" class=\"display\" cellspacing=\"0\" width=\"100%\"><thead><tr><th> Owner</th><th>Date</th><th>Target</th><th>Request Type</th><th>Gpt Object</th><th>Time</th><th>Status</th><th>Status Code</th><th>Description</th></tr></thead><tbody>";
                for (int i = 0; i < list.loglist.Count; i++)
                {
                    LogModel lm = list.loglist[i];
                    con += "<tr><td>" + lm.Operator + "</td><td>" + lm.Date + "</td><td>" + lm.Date + "</td><td>" + lm.Date + "</td><td>" + lm.Date + "</td><td>" + lm.Time + "</td><td>" + lm.Status+ "</td><td>" + lm.Date + "</td><td>" + lm.Description+ "</td></tr>";//数据行，字段对应数据库查询字段
                }
                con += " </tbody></table>";



                return Json(new JsonData(con),JsonRequestBehavior.AllowGet);
            }
        }
    }
}