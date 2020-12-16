using System;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AdventOfCode.Common
{
	public abstract class BaseDay<TInput, TResult> 
	{
		public string DataFilePath => $"./Data/{GetType().FullName.Replace("AdventOfCode.", "")}.txt";

		public virtual async Task<TInput> LoadData(string filePath)
		{
			var type = typeof(TInput);
			object file = null;
			if (type == typeof(string[]))
			{
				file = await File.ReadAllLinesAsync(filePath);
			}
			else if (type == typeof(string))
			{
				file = await File.ReadAllTextAsync(filePath);
			}
			if(file == null)
			{
				throw new ArgumentException($"could not convert {typeof(TInput)}");
			}
			return (TInput)Convert.ChangeType(file, typeof(TInput));
			
		}
		public async Task ExecuteAsync()
		{
			TInput input = await LoadData(DataFilePath);
			var day = GetType().Name;
			var stopwatch = new Stopwatch();
			stopwatch.Start();
			TResult resultPart1 = Part1(input);
			stopwatch.Stop();
			var elaspedPart1 = stopwatch.Elapsed;
			stopwatch.Restart();
			TResult resultPart2 = Part2(input);
			stopwatch.Stop();
			var elaspedPart2 = stopwatch.Elapsed;
			Console.WriteLine($"{day} Results Part1: {resultPart1} (took {elaspedPart1}), Part2: {resultPart2} (took {elaspedPart2})");
		}
		public virtual TResult Part1(TInput input)
		{
			return default;
		}

		public virtual TResult Part2(TInput input)
		{
			return default;
		}
	}
}
