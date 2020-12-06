using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2020.Modules
{
    public class PassportValidator
    {
        public bool HasRequiredFields(Passport passport)
        {
            bool isValid;

            isValid = passport.BirthYear.HasValue &&
            // passport.CountryId.HasValue &&
            passport.ExpirationYear.HasValue &&
            !string.IsNullOrEmpty(passport.EyeColor) &&
            !string.IsNullOrEmpty(passport.HairColor) &&
            !string.IsNullOrEmpty(passport.Height) &&
            passport.IssueYear.HasValue &&
            !string.IsNullOrEmpty(passport.PassportId);
            return isValid;
        }

        public bool ValidateFields(Passport passportToValidate)
        {
            var validatorFunctions = new List<Func<Passport, bool>>
            {
                (passport) => HasRequiredFields(passport),
                (passport) => ValidateBirthYear(passport),
                (passport) => ValidateIssueYear(passport),
                (passport) => ValidateExpirationYear(passport),
                (passport) => ValidateHeight(passport),
                (passport) => ValidateHairColor(passport),
                (passport) => ValidateEyeColor(passport),
                (passport) => ValidatePassportId(passport),
                (passport) => ValidateCountryId(passport)
            };
            var isValid = validatorFunctions.All(validator => validator(passportToValidate));
            return isValid;
        }

        public bool ValidateBirthYear(Passport passport)
        {
            var isValid = passport.BirthYear is (>= 1920 and <= 2002);
            return isValid;
        }
        public bool ValidateIssueYear(Passport passport)
        {
            var isValid = passport.IssueYear is (>= 2010 and <= 2020);
            return isValid;
        }
        public bool ValidateExpirationYear(Passport passport)
        {
            var isValid = passport.ExpirationYear is (>= 2020 and <= 2030);
            return isValid;
        }

        public bool ValidateHeight(Passport passport)
        {
            bool isValid;
            if(passport.Height.Contains("cm"))
			{
                isValid = int.TryParse(passport.Height.Replace("cm", ""), out int heightInCm);
                if(!isValid)
				{
                    return false;
				}
                isValid = heightInCm is (>= 150 and <= 193);
                return isValid;
            }
            else if (passport.Height.Contains("in"))
			{
                isValid = int.TryParse(passport.Height.Replace("in", ""), out int heightInInches);
                if (!isValid)
                {
                    return false;
                }
                isValid = heightInInches is (>= 59 and <= 76);
                return isValid;
            }
            return false;
        }

        public bool ValidateHairColor(Passport passport)
		{
            var regex = new Regex(@"\#[a-f,0-9]{6}");
            var isValid = regex.IsMatch(passport.HairColor);
            return isValid;
        }
        public bool ValidateEyeColor(Passport passport)
		{
            var validEyeColors = new HashSet<string>
            {
                "amb", "blu", "brn", "gry", "grn", "hzl", "oth"
            };

            var isValid = validEyeColors.Contains(passport.EyeColor);
            return isValid;
        }

        public bool ValidatePassportId(Passport passport)
		{
            var regex = new Regex(@"\d{9}");
            var isValid = 
                passport.PassportId.Length == 9 &&
                regex.IsMatch(passport.PassportId);
            return isValid;
        }

        public bool ValidateCountryId(Passport passport)
		{
            return true;
		}
    }
}