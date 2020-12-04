using AdventOfCode.Common;
using AdventOfCode.Common.Extensions;
using AdventOfCode.ServiceHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Threading.Tasks;

namespace AdventOfCode.ServiceHost
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            DayHelper.RegisterAssembly(typeof(Year2018.Day01).Assembly);
            DayHelper.RegisterAssembly(typeof(Year2020.Day01).Assembly);
            var host = BuildHost();
            await host.RunAsync();
            return 0;
        }

        static IHost BuildHost() =>
           Host.CreateDefaultBuilder()
            .ConfigureLogging(logging =>
            {
               logging.AddConsole();
            })
            .ConfigureServices(services =>
            {
                services.AddSingleton<IConsole, TextConsole>();
                services.AddHostedService<AdventOfCodeService>();
                services.AddAdventOfCodeDays();
            })
            .Build();
    }
}
