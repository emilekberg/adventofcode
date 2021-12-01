using AdventOfCode.Common;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode.ServiceHost;

public class AdventOfCodeService : BackgroundService
{
    private readonly ILogger _logger;
    private readonly IConsole _console;
    private readonly List<IDay> _days;
    private readonly List<string> _yearsStrings;
    private readonly List<string> _dayStrings;
    public AdventOfCodeService(ILogger<AdventOfCodeService> logger, IConsole console, IEnumerable<IDay> days)
    {
        _logger = logger;
        _console = console;
        _days = days.ToList();

        var yearRegex = new Regex(@"(Year\d{4})");
        _yearsStrings = days
            .Select(x => x.GetType().ToString())
            .Select(x => yearRegex.Match(x).Groups[1].Value)
            .Distinct()
            .ToList();

        _dayStrings = _days
            .Select(x => x.GetType().ToString())
            .ToList();

    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var dayRegex = new Regex(@"(Day\d{1,2})");
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(100);
            // prompts user to select a year
            var selectedYear = await _console.Menu(_yearsStrings);

            // creates a list of days for the selected year
            var daysInSelectedYear = _dayStrings
                .Where(x => x.Contains(selectedYear.Value))
                .Select(x => dayRegex.Match(x).Groups[1].Value)
                .ToList();

            // prompts user to select a day
            daysInSelectedYear.Add("All");
            var selectedDay = await _console.Menu(daysInSelectedYear);

            // finds the assembly for the selected day and year.
            var daysToRun = _days
                .Where(x =>
                {
                    if (selectedDay.Value == "All") return true;
                    var typeString = x.GetType().ToString();
                    return typeString.Contains(selectedDay.Value) && typeString.Contains(selectedYear.Value);
                });

            try
            {
                foreach (var day in daysToRun)
                {
                    await day.ExecuteAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while parsing");
                throw;
            }
            finally
            {
                _console.WriteLine("Done! Press any key to continue...");
                Console.ReadKey();
            }


        }
    }
}
