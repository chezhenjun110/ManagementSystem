using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ad_Tools.Models
{
    public class OuTreeModel
    {public string path { get; set; }
        public string state { get; set; }
        public int id { get; set; }
        public string text { get; set; }
        public List<OuTreeModel> children { get; set; }
    }
}
