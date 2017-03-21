using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ad_Tools.Models
{
    public class GroupSearchViewModels
    {
        public List<SelectListItem> domains { get; set; }
        public List<SelectListItem> searchcriteria { get; set; }
        public List<SelectListItem> searchfield { get; set; }
    }

    public class GroupCreateViewModels
    {
        public List<SelectListItem> domains { get; set; }

    }
    public class GroupReport
    {

        public string groupnumber { get; set; }
    }
}