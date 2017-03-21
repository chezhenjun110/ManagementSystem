
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ad_Tools.Models
{
    public class LogListModel
    {
        public List<SelectListItem> filelist { get; set; }

        public List<LogModel> loglist { get; set; }
    }
}