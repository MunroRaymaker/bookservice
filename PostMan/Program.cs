using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Net;
using System.Text;

namespace PostMan
{
    class Program
    {
        static void Main(string[] args)
        {
            var userInfo = string.Join(";",
                Environment.MachineName,
                Environment.UserName);
            
            var wc = new WebClient();
            var api = "http://localhost:44339/api/passwords/generate/" + userInfo;
            try
            {
                var response = wc.DownloadString(api);
                Console.WriteLine("\nResponse received was :\n{0}", response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
