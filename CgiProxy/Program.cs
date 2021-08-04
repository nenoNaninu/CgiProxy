using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CgiProxy
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Content-type: application/json" + Environment.NewLine + Environment.NewLine);

            var query = Environment.GetEnvironmentVariable("QUERY_STRING");

            if (query is null)
            {
                Console.WriteLine("query is empty");
                return;
            }

            var collection = HttpUtility.ParseQueryString(query);
            var url = collection["dst"];

            if (url is null)
            {
                Console.WriteLine("dst query is empty");
                return;
            }

            var postData = Console.ReadLine();

            if (postData is null)
            {
                Console.WriteLine("postData is empty");
                return;
            }

            var httpClient = new HttpClient();

            var content = new ByteArrayContent(Encoding.UTF8.GetBytes(postData));
            var response = await httpClient.PostAsync(url, content);

            var responseJson = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseJson);
        }
    }
}
