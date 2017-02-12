using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ebs.Tools
{
    public abstract class Security
    {
        //encode
        public static string EnCode(string source)
        {
            return Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(source)).Replace("+", "%2B");
        }

        //decode
        public static string DeCode(string source)
        {
            return System.Text.Encoding.Default.GetString(Convert.FromBase64String(source.Replace("%2B", "+")));
        }
    }
}