use super::operation::Operation;
use super::parametermode::ParameterMode;

pub fn get_tenth(num: i32) -> i32 {
  let to_subtract: i32 = (num / 100) * 100;
  let result: i32 = num - to_subtract;
  return result;
}

/// returns the nth number in within a number.
/// example: 12345, 3 is the 2nd number (index from the right);
pub fn get_nth(num: i32, n: u32) -> i32 {
  let div = (10u32).pow(n) as i32;
  let result = (num / div) % 10;
  return result;
}

pub fn get_first_param(num: i32) -> ParameterMode {
  return ParameterMode::from_i32(get_nth(num, 2));
}

pub fn get_second_param(num: i32) -> ParameterMode {
  return ParameterMode::from_i32(get_nth(num, 3));
}

pub fn get_third_param(num: i32) -> ParameterMode {
  return ParameterMode::from_i32(get_nth(num, 4));
}

pub fn get_opcode(num: i32) -> i32 {
  return get_tenth(num);
}

pub fn get_operation(value: i32) -> Operation {
  return match get_opcode(value) {
    1 => Operation::Add(get_first_param(value), get_second_param(value), get_third_param(value)),
    2 => Operation::Mul(get_first_param(value), get_second_param(value), get_third_param(value)),
    3 => Operation::Input(get_first_param(value)),
    4 => Operation::Output(get_first_param(value)),
    5 => Operation::JumpIfTrue(get_first_param(value), get_second_param(value)),
    6 => Operation::JumpIfFalse(get_first_param(value), get_second_param(value)),
    7 => Operation::LessThan(get_first_param(value), get_second_param(value), get_third_param(value)),
    8 => Operation::Equals(get_first_param(value), get_second_param(value), get_third_param(value)),
    99 => Operation::Halt,
    _ => panic!(),
  };
}

#[cfg(test)]
mod utils {
  use super::*;

  #[test]
  fn test_get_nth() {
    assert_eq!(get_nth(12345, 2), 3);
    assert_eq!(get_nth(12345, 3), 2);
    assert_eq!(get_nth(12345, 4), 1);
    assert_eq!(get_nth(12345, 5), 0);
  }


  #[test]
  fn test_get_tenth() {
    assert_eq!(get_tenth(12345), 45);
    assert_eq!(get_tenth(12301), 1);
    assert_eq!(get_tenth(00002), 2);
  }
}