using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace app.api
{
    /// <summary>
    /// Entry point class for the application
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point for the application
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
              .UseKestrel()
              .UseContentRoot(Directory.GetCurrentDirectory())
              .UseIISIntegration()
              .UseStartup<Startup>()
              .CaptureStartupErrors(true)
              .Build();

            host.Run();
        }
    }
}
