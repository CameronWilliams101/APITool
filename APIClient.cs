using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace APITool
{
    public class APIClient
    {
        public string url;
        public List<Hello> list;

        public APIClient()
        {
            url = "http://192.168.1.109:54949/hello";
            list = new List<Hello>();
        }

        public async Task GetAsync()
        {
            var httpClient = new HttpClient();
            var APIString = await httpClient.GetStringAsync(url);

            if (APIString == "[]")
            {
                Console.WriteLine("Empty");
                return;
            }

            //Parse
            APIString = APIString.Substring(1, APIString.Length - 2);
            var tokens = APIString.Split("},");

            //Append }
            for (int i = 0; i < tokens.Length - 1; i++)
            {
                tokens[i] += "}";
            }

            //Deserializing tokens into Hello objs
            foreach (var token in tokens)
            {
                list.Add(JsonConvert.DeserializeObject<Hello>(token));
            }
        }

        public void Post()
        {

        }

        public void Delete()
        {

        }
    }
}
