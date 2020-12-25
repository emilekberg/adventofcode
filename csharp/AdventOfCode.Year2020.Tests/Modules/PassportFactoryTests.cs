
using Xunit;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Year2020.Modules;
using System;

namespace AdventOfCode.Year2020.Tests.Modules
{
	public class PassportFactoryTests
	{

		[Fact]
		public void FillPassportData()
		{
			var input = "ecl:gry pid:860033327 eyr:2020 hcl:#fffffd";
			var input2 = "byr:1937 iyr:2017 cid:147 hgt:183cm";

			var passport = new Passport();
			PassportFactory.FillPassportData(passport, input);
			PassportFactory.FillPassportData(passport, input2);

			Assert.Equal("gry", passport.EyeColor);
			Assert.Equal("860033327", passport.PassportId);
			Assert.Equal(2020, passport.ExpirationYear);
			Assert.Equal("#fffffd", passport.HairColor);
			Assert.Equal(1937, passport.BirthYear);
			Assert.Equal(2017, passport.IssueYear);
			Assert.Equal(147, passport.CountryId);
			Assert.Equal("183cm", passport.Height);
		}

		[Fact]
		public void Create()
		{
			var input = @"ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
byr:1937 iyr:2017 cid:147 hgt:183cm

iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
hcl:#cfa07d byr:1929

hcl:#ae17e1 iyr:2013
eyr:2024
ecl:brn pid:760753108 byr:1931
hgt:179cm

hcl:#cfa07d eyr:2025 pid:166559648
iyr:2011 ecl:brn hgt:59in";
			var passports = PassportFactory.Create(input);
			Assert.NotEmpty(passports);
			Assert.Equal(4, passports.Count);
		}
	}
}