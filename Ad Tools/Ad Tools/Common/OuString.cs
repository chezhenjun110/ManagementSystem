using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ad_Tools.Common
{
    public class OuString
    {
        public static string OuStringFormat(string OuPath)
        {

            List<String> list = new List<string>();
            string OuFormated="";
           string[] cache= OuPath.Split(',');
            string domain="1";
            string host = "";
            for(int i = 0; i < cache.Length; i++)
            {
                if (cache[i].Split('=')[0]== "DC")

                {
                    string a = "." + cache[i].Split('=')[1];
                    domain = domain+a;

                }
                else
                {
                    list.Add(cache[i].Split('=')[1]);

                }
            }

            for(int i = list.Count-1; i >= 0; i--)
            { 
                host += list[i] + "/";

            }
            OuFormated = domain.Split(new string[] { "1."},StringSplitOptions.None)[1]+"/"+host;




            return OuFormated;

        }
    }
}