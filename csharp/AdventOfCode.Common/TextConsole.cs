using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Common
{
	public class TextConsole : IConsole
	{
		public Task<int> Menu(List<string> options)
		{
			return Task.Run(() =>
			{
				var validInput = false;
				do
				{
					Console.Clear();

					int key = 0;
					var builder = new StringBuilder();

					options.ForEach(option =>
					{
						builder.Append(++key);
						builder.Append(") ");
						builder.AppendLine(option);
					});

					builder.AppendLine("Select a option: ");
					WriteLine(builder.ToString());

					var input = ReadLine();
					if (!int.TryParse(input, out var selection))
					{
						continue;
					}
					if (selection < 1 || selection > options.Count)
					{
						continue;
					}
					selection--;
					return Task.FromResult(selection);
				}
				while (!validInput);
				return Task.FromResult(-1);
			});
		}

		public void Write(string output)
		{
			Console.Write(output);
		}

		public void WriteLine(string line)
		{
			Console.WriteLine(line);
		}

		protected virtual string ReadLine()
		{
			return Console.ReadLine();
		}
	}
}
