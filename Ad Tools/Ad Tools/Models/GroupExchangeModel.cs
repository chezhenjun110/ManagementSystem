using System.Collections.Generic;
using System.Web.Mvc;
namespace Ad_Tools.Models
{
    public class GroupExchangeModel
    {
        
        public string GroupName { get; set; }
        public List<SelectListItem> accountType { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Emailname { get; set; }
        public bool HideFromAB { get; set; }
        public bool RequireSenderAuthenticationEnabled { get; set; }
        public string DLShortname { get; set; }
        public List<SelectListItem> EmailHost { set; get; }
        public List<SelectListItem> GroupContainer { get; set; }
        public bool IndudeinGalsync { get; set; }
        public List<SelectListItem> managedByList { get; set; }
        public List<SelectListItem> SendersAllowedList { get; set; }
        
        public string ValidFrom { get; set; }
        public string ValidUntil { get; set; }
       
    }
}