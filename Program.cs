
using System.Diagnostics;

namespace KnockKnock
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            string filePath = "./request.json";
            if(args.Length>0 && !string.IsNullOrWhiteSpace(args[0])){
                filePath = args[0];
            }
            var parser = new ConfigParser();
            var config = await parser.ParseConfig(filePath);

            var tester = new ApiTester(config);
            
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start(); 
            await tester.Execute();
            stopwatch.Stop();

            Console.WriteLine($"Elapsed Time is {stopwatch.ElapsedMilliseconds}ms");
        }
    }
}