using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace AppDemoAndroid
{
    class GitHub
    {
        public async Task<List<string>> GetAsync(string user)
        {
            var repositories = new List<string>();
            string content ="sem resposta";
            try
            {
                string url = string.Format("https://api.github.com/users/{0}/repos", user);

                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "Other");

                var response = await client.GetAsync(url);
                content = await response.Content.ReadAsStringAsync();

                var json = JArray.Parse(content);

                
                foreach (var item in json)
                {
                    var repository = item.Value<string>("full_name");
                    repositories.Add(repository);
                }

                return repositories;
            }
            catch (Exception e)
            {

                //Console.WriteLine(e.Message);

                return new List<string> { e.Message, content,
                                "isto", "não", "está", "bem!" };
            }
            
            
        }
    }
}