﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ad_Tools.Models
{
    public class UserDetail
    {
        public int id { get; set; }
        public string Createby { get; set; }
        public string name { get; set; }
        public string CreateTime { get; set; }
        public string LatestLogin { get; set; }
    }
}