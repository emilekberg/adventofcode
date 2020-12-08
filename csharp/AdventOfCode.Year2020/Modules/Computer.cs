using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Modules
{
	public record Command
	{
		public string Instruction { init; get; }
		public int Argument { init; get; }
	}

	public class Computer
	{
		public int Accumulator { get; protected set; } = 0;
		public void ResetAccumulator()
		{
			Accumulator = 0;
		}
		public bool HasInfiniteLoop(List<Command> commands)
		{
			var ranInstructions = new HashSet<int>(commands.Count());
			var i = 0;
			do
			{
				var command = commands[i];
				ranInstructions.Add(i);

				switch (command.Instruction)
				{
					case "acc":
						Accumulator += command.Argument;
						i++;
						break;
					case "jmp":
						i += command.Argument;
						break;
					case "nop":
						i++;
						break;
				}
				if (ranInstructions.Contains(i))
				{
					return true;
				}
				if (i == commands.Count())
				{
					return false;
				}
			}
			while (true);
		}
	}
}
