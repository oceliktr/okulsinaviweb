using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErzurumOdmMvc.Common.Library
{
    public class ReCaptcha
    {
        public bool Success { get; set; }

        /// <summary>
        /// g-recaptcha-response isimli formdan gelen değer
        /// </summary>
        /// <param name="encodedResponse"></param>
        /// <returns></returns>
        public static bool Validate(string encodedResponse)
        {
            if (string.IsNullOrEmpty(encodedResponse)) return false;

            var client = new System.Net.WebClient();
             const string secret = "6LfYzMkUAAAAAOCjwVyDECbSMzukREJJRKikZcdT";

            if (string.IsNullOrEmpty(secret)) return false;

            var googleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, encodedResponse));

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            ReCaptcha reCaptcha = serializer.Deserialize<ReCaptcha>(googleReply);

            return reCaptcha.Success;
        }
    }
}
