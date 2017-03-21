﻿using ADTOOLS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ad_Tools.Models
{
    public class ComputerCreateModels
    {
        public List<SelectListItem> domains { get; set; }
    }
    public class ComputerSearchViewModels
    {
        public List<SelectListItem> domains { get; set; }
        public List<SelectListItem> searchcriteria { get; set; }
        public string ComputerName { get; set; }
        public IEnumerable<ComputerDTO> ComputerLst {get;set;}
        public List<SelectListItem> searchfield { get; set; }
    }
    public class ComputerReport
    {
        public string ComputerNumber { get; set; }
    }
}