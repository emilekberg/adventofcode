using System;
using System.IO;
using System.Threading.Tasks;

namespace AdventOfCode.Common
{
	public abstract class BaseDay<TInput, TResult>
	{
		public string DataFilePath => $"./Data/{GetType().Name}.txt";

		public async Task<TInput> LoadData(string filePath)
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
			TResult resultPart1 = Part1(input);
			TResult resultPart2 = Part2(input);
			Console.WriteLine($"Results Part1: {resultPart1}, Part2: {resultPart2}");
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
