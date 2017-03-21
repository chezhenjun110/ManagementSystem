using ADTOOLS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ad_Tools.Models
{
    public class JsonData
    {

    
        public bool logined { get; set; }
        public string Message { get; set; }
        //public string Message2 { get; set; }
        //public string Message3 { get; set; }
        public JsonData(string message)
        {
            this.Message = message;
        }
        public JsonData(string message,bool logined)
        {

            this.Message = message;
            this.logined = logined;
         
        }


    }
}