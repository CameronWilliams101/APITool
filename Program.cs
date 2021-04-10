using System;
using System.Threading.Tasks;

namespace APITool
{
    public class Program
    {
        public static async Task Main(string[] args)
        {          
            APIClient apiClient = new APIClient();
            await apiClient.GetAsync();

            //Printing
            foreach(var hello in apiClient.list)
            {
                Console.WriteLine(hello.name);
            }
        }
    }
}
