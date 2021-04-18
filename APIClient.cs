using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APITool
{
    public class APIClient
    {
        public string url;
        public List<Hello> list;
        public HttpClient httpClient;

        public APIClient()
        {
            url = "http://192.168.1.109:54949/hello";
            list = new List<Hello>();
            httpClient = new HttpClient();
        }

        public async Task GetAsync()
        {
            list.Clear();
            var APIString = await httpClient.GetStringAsync(url);

            if (APIString == "[]")
            {
                var orgColour = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("WebAPI list is Empty");
                Console.ForegroundColor = orgColour;
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

        public async Task<string> PostAsync(string name, int id)
        {
            var json = "{\"Name\":\"" + name + "\",\"Id\":" + id.ToString() + "}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> DeleteAsync(string name, int id)
        {
            var json = "{\"Name\":\"" + name + "\",\"Id\":" + id.ToString() + "}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url + "/delete", content);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> ListenAsync()
        {
            //Should probs make like a parralel task
            var initial = await httpClient.GetStringAsync(url);
            var current = initial;

            while (initial.Equals(current))
            {
                current = await httpClient.GetStringAsync(url);
            }

            return "API Updated";
        }
    }
}
