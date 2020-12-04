using Xunit;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Year2020.Modules;

namespace AdventOfCode.Year2020.Tests.Modules
{
  public class PasswordValidatorTests
  {
    [Theory]
    [InlineData(1,3,'a', "abcde", true)]
    [InlineData(1,3,'b', "cdefg", false)]
    [InlineData(2,9,'c', "ccccccccc", true)]
    public void ValidatePassword_Counts(int min, int max, char character, string password, bool expectedResult)
    {
      var validator = new PasswordValidator();
      var policy = new PasswordPolicy
      {
         Min = min,
         Max = max,
         Character = character
      };

      Assert.Equal(expectedResult, validator.ValidatePassword(password, policy));
    }

    [Theory]
    [InlineData(1,3,'a', "abcde", true)]
    [InlineData(1,3,'b', "cdefg", false)]
    [InlineData(2,9,'c', "ccccccccc", false)]
    public void ValidatePasswordPositions(int min, int max, char character, string password, bool expectedResult)
    {
      var validator = new PasswordValidator();
      var policy = new PasswordPolicy
      {
         Min = min,
         Max = max,
         Character = character
      };

      Assert.Equal(expectedResult, validator.ValidatePasswordPositions(password, policy));
    }
  }

  public class PasswordPolicyTests
  {
    [Theory]
    [InlineData("1-3 a: abcde", 1, 3, 'a')]
    [InlineData("1-3 b: cdefg", 1, 3, 'b')]
    [InlineData("2-9 c: ccccccccc", 2, 9, 'c')]
    public void FromString(string input, int min, int max, char c)
    {
      var actual = PasswordPolicy.FromString(input);

      Assert.Equal(min, actual.Min);
      Assert.Equal(max, actual.Max);
      Assert.Equal(c, actual.Character);
    }
  }
}