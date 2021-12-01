using AdventOfCode.Common;
using AdventOfCode.Common.Extensions;
using AdventOfCode.ServiceHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

DayHelper.RegisterAssembly(typeof(AdventOfCode.Year2018.Day01).Assembly);
DayHelper.RegisterAssembly(typeof(AdventOfCode.Year2020.Day01).Assembly);
DayHelper.RegisterAssembly(typeof(AdventOfCode.Year2021.Day01).Assembly);
var host = Host
    .CreateDefaultBuilder()
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
await host.RunAsync();
