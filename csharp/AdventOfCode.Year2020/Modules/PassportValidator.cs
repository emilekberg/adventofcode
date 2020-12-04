using System;
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
        passport.PassportId.HasValue;
      return isValid;
    }

    public bool ValidateFields(Passport passport)
    {
      bool isValid;
    }

    public bool ValidateBirthYear(Passport passport)
    {
      return passport.BirthYear >= 1920 && passport.BirthYear <= 2002;
    }
    public bool ValidateIssueYear(Passport passport)
    {
      return passport.IssueYear >= 1920 && passport.IssueYear <= 2002;
    }
  }
}