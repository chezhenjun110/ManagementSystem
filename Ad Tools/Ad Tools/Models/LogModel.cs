using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ad_Tools.Models
{
    public class LogModel
    {
        public string Date { get; set; }
        public string Operator { get; set; }
        public string Description { get; set; }
        public string Time { get; set; }

        public string Status { get; set; }

    }
}