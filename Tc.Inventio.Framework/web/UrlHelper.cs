using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tc.Inventio.Framework.Web
{
    public class UrlHelper
    {
        #region SLUG
        
        public static string Slug(string url)
        {
            return Slug(url, null);
        }

        public static string Slug(string url, Nullable<int> max)
        {
            string ret = string.Empty;

            if (!string.IsNullOrWhiteSpace(url))
            {
                ret = url.ToLower();
                // tildes
                ret = ReplaceDiacritics(ret);
                // invalid chars, make into spaces
                ret = Regex.Replace(ret, @"[^a-z0-9\s-]", "");
                // convert multiple spaces/hyphens into one space       
                ret = Regex.Replace(ret, @"[\s-]+", " ").Trim();
                // cut and trim it
                if (max.HasValue)
                {
                    ret = ret.Substring(0, ret.Length <= max.Value ? ret.Length : max.Value).Trim();
                }
                // hyphens
                ret = Regex.Replace(ret, @"\s", "-");
            }

            return ret;
        }

        private static string ReplaceDiacritics(string str)
        {
            string ret = string.Empty;

            if (!string.IsNullOrWhiteSpace(str))
            {
                ret = str.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u").Replace("ñ", "n").Replace("ü", "u"); // TODO
            }

            return ret;
        }

        #endregion

    }
}
