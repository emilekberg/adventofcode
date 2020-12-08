using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2020.Modules
{
	/// 
	public class PasswordPolicy
	{
		public static PasswordPolicy FromString(string input)
		{
			Regex regex = new Regex(@"(\d+)-(\d+)\s(\w+)");
			var match = regex.Match(input);
			if (!match.Success)
			{
				throw new ArgumentException($"input was not correct format. {input}");
			}
			if (
			  !int.TryParse(match.Groups[1].Value, out var min) ||
			  !int.TryParse(match.Groups[2].Value, out var max) ||
			  !char.TryParse(match.Groups[3].Value, out var c))
			{
				throw new ArgumentException($"could not parse values. check format. {input}");
			}
			return new PasswordPolicy
			{
				Min = min,
				Max = max,
				Character = c
			};
		}
		public char Character { get; set; }
		public int Min { get; set; } = 0;
		public int Max { get; set; } = 0;
	}
	public class PasswordValidator
	{
		public bool ValidatePassword(string password, PasswordPolicy policy)
		{
			var result = password
			  .ToCharArray()
			  .ToList()
			  .GroupBy(c => c)
			  .Select(group => group.ToList())
			  .Select(group => new { Char = group.FirstOrDefault(), Count = group.Count })
			  .Where(g => g.Char == policy.Character)
			  .FirstOrDefault();

			if (result == null)
			{
				return false;
			}
			return result.Count >= policy.Min && result.Count <= policy.Max;
		}

		public bool ValidatePasswordPositions(string password, PasswordPolicy policy)
		{
			var result = password
			  .ToCharArray()
			  .ToList()
			  .Where((x, i) => i + 1 == policy.Min || i + 1 == policy.Max)
			  .Aggregate(0, (acc, curr) =>
			  {
				  var toAdd = 0;
				  if (curr == policy.Character)
				  {
					  toAdd = 1;
				  }
				  return acc + toAdd;
			  });

			return result == 1;
		}
	}
}