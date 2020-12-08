using System;
using System.Threading.Tasks;
using AdventOfCode.Common;
using System.Linq;
using System.Collections.Generic;
using AdventOfCode.Year2020.Modules;
using System.Text.RegularExpressions;
using System.IO;

namespace AdventOfCode.Year2020
{
	public class PasswordData
	{
		public PasswordPolicy Policy { get; set; }
		public string Password { get; set; }
	}
	/// <Summary>
	/// https://adventofcode.com/2020/day/2
	/// </Summary>
	public class Day02 : BaseDay<string[], int>, IDay
	{
		public List<PasswordData> FormatInput(string[] input)
		{
			var regex = new Regex(@"\d+-\d+\s\w:\s(\w+)");
			return input
			  .Select(x =>
			  {
				  var match = regex.Match(x);
				  var password = match.Groups[1].Value;
				  return new PasswordData()
				  {
					  Policy = PasswordPolicy.FromString(x),
					  Password = password
				  };
			  })
			  .ToList();
		}

		public override int Part1(string[] input)
		{
			var data = FormatInput(input);
			var passwordValidator = new PasswordValidator();
			var result = data.Aggregate(0, (acc, passwordData) =>
			{
				var toAdd = 0;
				if (passwordValidator.ValidatePassword(passwordData.Password, passwordData.Policy))
				{
					toAdd = 1;
				}
				return acc + toAdd;
			});
			return result;
		}

		public override int Part2(string[] input)
		{
			var data = FormatInput(input);
			var passwordValidator = new PasswordValidator();
			var result = data.Aggregate(0, (acc, passwordData) =>
			{
				var toAdd = 0;
				if (passwordValidator.ValidatePasswordPositions(passwordData.Password, passwordData.Policy))
				{
					toAdd = 1;
				}
				return acc + toAdd;
			});
			return result;
		}
	}

}
