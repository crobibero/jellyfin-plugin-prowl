using System.Collections.Generic;
using System.Web;

namespace Jellyfin.Plugin.Prowl.Utils
{
    public static class QueryString
    {
        public static string ToQueryString(this IDictionary<string, string> dict)
        {
            var list = new List<string>();
            foreach (var (key, value) in dict)
            {
                list.Add(key + "=" + HttpUtility.UrlEncode(value.Trim()));
            }

            return string.Join("&", list);
        }
    }
}