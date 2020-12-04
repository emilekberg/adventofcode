using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode.Year2020.Modules
{
  public class PassportFactory
  {
    public List<Passport> Create(string input)
    {
      var passports = new List<Passport>();
      var passport = new Passport();
      input.Replace(Environment.NewLine, "\n")
        .Split('\n')
        .ToList()
        .ForEach(row => 
        {
          // blank line is the end of a passport.
          if(row.Count() == 0)
          {
            passports.Add(passport);
            passport = new Passport();
            return;
          }

          FillPassportData(passport, row);
        });
      // add the last one to the list.
      passports.Add(passport);
      return passports;
    }

    public Passport FillPassportData(Passport passport, string data)
    {
      var split = data.Split(' ');
      split.ToList()
        .ForEach(x => 
        {
          var keyValue = x.Split(':');
          var key = keyValue[0];
          var value = keyValue[1];
          try
          {
            switch(key)
            {
              case "byr":
                passport.BirthYear = int.Parse(value);
                break;
              case "iyr":
                passport.IssueYear = int.Parse(value);
                break;
              case "eyr":
                passport.ExpirationYear = int.Parse(value);
                break;
              case "hgt":
                passport.Height = value;
                break;
              case "hcl":
                passport.HairColor = value;
                break;
              case "ecl":
                passport.EyeColor = value;
                break;
              case "pid":
                passport.PassportId = value;
                break;
              case "cid":
                passport.CountryId = int.Parse(value);
                break;
            }
          }
          catch(Exception ex)
          {
            Console.WriteLine($"{key}:{value}");
            throw;
          }
          
        });
        return passport;
    }
  }
}