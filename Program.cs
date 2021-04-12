using System;
using System.Threading.Tasks;

namespace APITool
{
    public class Program
    {
        static APIClient apiClient = new APIClient();
        static string action;
        static ConsoleColor orgColour = Console.ForegroundColor;

        public static async Task Main(string[] args)
        {                      
            do
            {
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("0) Quit");
                Console.WriteLine("1) Get from API, to fill the list");
                Console.WriteLine("2) Post");
                Console.WriteLine("3) Delete");
                Console.WriteLine("4) Print");
                Console.Write("Enter action:");
                action = Console.ReadLine();

                switch(action)
                {
                    case "0":
                        return;
                    case "1":
                        await GetAsync();
                        break;
                    case "2":
                        await PostAsync();
                        break;
                    case "3":
                        await DeleteAsync();
                        break;
                    case "4":
                        Print();
                        break;
                }

            } while (action != "0");
        }

        public static async Task GetAsync()
        {
            //GET, to fill the list
            await apiClient.GetAsync();
            Print();
        }

        public static async Task PostAsync()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(await apiClient.PostAsync("Cam", 1));
            Console.ForegroundColor = orgColour;
        }

        public static async Task DeleteAsync()
        {
            var response = await apiClient.DeleteAsync("Cam", 1);
            if (response.Equals("Deleted Successfully"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(response);
                Console.ForegroundColor = orgColour;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(response);
                Console.ForegroundColor = orgColour;
            }
        }

        public static void Print()
        {            
            if(apiClient.list.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Local list Empty, may need to Get update");
                Console.ForegroundColor = orgColour;
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Local List, may need to Get update");
            //Printing
            foreach (var hello in apiClient.list)
            {
                Console.WriteLine(hello);
            }
            Console.ForegroundColor = orgColour;
        }
    }
}
