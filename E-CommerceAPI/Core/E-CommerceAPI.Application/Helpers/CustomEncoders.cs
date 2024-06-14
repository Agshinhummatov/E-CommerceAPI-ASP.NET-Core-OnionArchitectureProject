using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Helpers
{
    static public class CustomEncoders
    {
        public static string UrlEnocode(this string value)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value); // biz burda encoding edib byte cevirik bu datamizi  

            return WebEncoders.Base64UrlEncode(bytes); // daha sonra burda ise biz icinde +/ bu kimi yazilar olduquna gore https prtokulu bunu qebul elemir burda sifreleyirik
        }

        public static string UrlDecode(this string value)
        {
            byte[] bytes = WebEncoders.Base64UrlDecode(value); // bu gelen resetTokeni Base64UrlDecode edir oz haline cevirib byte cevirik
            return Encoding.UTF8.GetString(bytes);  // burda ise biz gelen reset tokenin sifresini aciriq  cunki bunu gonderde sifrelemisdik biz 
        }

    }
}
