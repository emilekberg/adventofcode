using AdventOfCode.Common;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode.ServiceHost
{
	public class AdventOfCodeService : BackgroundService
	{
		private readonly ILogger _logger;
		private readonly IConsole _console;
		private readonly List<IDay> _days;
		public AdventOfCodeService(ILogger<AdventOfCodeService> logger, IConsole console, IEnumerable<IDay> days)
		{
			_logger = logger;
			_console = console;
			_days = days.ToList();

			
		}
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while(!stoppingToken.IsCancellationRequested)
			{
				await Task.Delay(500);
				var list = _days.Select(x => x.GetType().ToString()).ToList();

				var selection = await _console.Menu(list);
				try
				{
					await _days[selection].ExecuteAsync();
				}
				catch(Exception ex)
				{
					_logger.LogError(ex, "Error occured while parsing");
				}
				finally
				{
					_console.WriteLine("Done! Press any key to continue...");
					Console.ReadKey();
				}
				
				
			}
		}
	}
}
