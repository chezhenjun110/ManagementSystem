using ADTOOLS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ad_Tools.Models
{
    public class UserLoginModel
    {
        public string CurrentUser { get; set; }
        public List<SelectListItem> domains { get; set; }
    }
    public class UserCreateModel
    {
       
        public List<SelectListItem> domains { get; set; }
        public List<SelectListItem> accounttype { get; set; }
        public string logonname { set; get; }
        public string usertype { set; get; }
        public string firstname { set; get; }
        public string lastname { set; get; }
        public string fullname { set; get; }
        public string password { set; get; }
        public string confirmpassword { set; get; }
        public bool Umcpanl { set; get; }  //user must change password at next logon
        public bool uccp { set; get; }  //user cannot cahnge password
        public bool pne { get; set; }   //password never expires
        public bool aid { get; set; }  //account is disabled
    }
    public class UserSearchModifyViewModel
    {
        
              public List<SelectListItem> Profile_connect { get; set; }
        public List<SelectListItem> groupdomain { get; set; }
        public List<SelectListItem> domains { get; set; }
        public List<SelectListItem> searchfield { get; set; }
        public List<SelectListItem> searchcriteria { get; set; }
        public List<SelectListItem> searchtype { get; set; }
        public string UserName { get; set; }
        public IEnumerable<UserDTO> UserLst { get; set; }
        public List<userdto> dto { get; set; }
    }
    
}