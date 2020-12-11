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
	/// <Summary>
	/// https://adventofcode.com/2020/day/4
	/// </Summary>
	public class Day04 : BaseDay<string, int>, IDay
	{
		public override int Part1(string input)
		{
			var factory = new PassportFactory();
			var validator = new PassportValidator();
			var passports = factory.Create(input);
			var validPassports = passports.Where(passport => validator.HasRequiredFields(passport));
			return validPassports.Count();
		}

		public override int Part2(string input)
		{
			var factory = new PassportFactory();
			var validator = new PassportValidator();
			var passports = factory.Create(input);
			var validPassports = passports
				.Where(passport => validator.ValidateFields(passport));
			return validPassports.Count();
		}
	}

}
