use super::operation::Operation;
use super::parametermode::ParameterMode;

pub fn get_ten_thousands(num: i32) -> i32 {
  let result: i32 = num / 10000;
  return result;
}

pub fn get_thousands(num: i32) -> i32 {
  let result: i32 = (num / 1000) % 10;
  return result;
}

pub fn get_hundreds(num: i32) -> i32 {
  let result: i32 = (num / 100) % 10;
  return result;
}

pub fn get_tenth(num: i32) -> i32 {
  let to_subtract: i32 = (num / 100) * 100;
  let result: i32 = num - to_subtract;
  return result;
}

pub fn get_parameter_modes(value: i32) -> [ParameterMode; 3] {
  return [
    ParameterMode::from_i32(get_ten_thousands(value)),
    ParameterMode::from_i32(get_thousands(value)),
    ParameterMode::from_i32(get_hundreds(value)),
  ]
}

pub fn get_opcode(value: i32) -> Operation {
  return Operation::from_i32(get_tenth(value));
}

#[cfg(test)]
mod utils {
  use super::*;
  #[test]
  fn test_get_ten_thousand() {
    let result = get_ten_thousands(12345);
    assert_eq!(result, 1);
  }

  #[test]
  fn test_get_thousand() {
    let result = get_thousands(12345);
    assert_eq!(result, 2);
  }


  #[test]
  fn test_get_hundreds() {
    let result = get_hundreds(12345);
    assert_eq!(result, 3);
  }

  #[test]
  fn test_get_tenth() {
    assert_eq!(get_tenth(12345), 45);
    assert_eq!(get_tenth(12301), 1);
    assert_eq!(get_tenth(00002), 2);
  }
  
  #[test]
  fn test_get_parameter_modes() {
    let [a,b,c] = get_parameter_modes(1002);
    assert_eq!(a, ParameterMode::POSITION);
    assert_eq!(b, ParameterMode::IMMEDIATE);
    assert_eq!(c, ParameterMode::POSITION);
  }

  #[test]
  fn test_get_parameter_modes_2() {
    let [a,b,c] = get_parameter_modes(11042);
    assert_eq!(a, ParameterMode::IMMEDIATE);
    assert_eq!(b, ParameterMode::IMMEDIATE);
    assert_eq!(c, ParameterMode::POSITION);
  }

  #[test]
  fn test_get_parameter_modes_3() {
    let [a,b,c] = get_parameter_modes(11100);
    assert_eq!(a, ParameterMode::IMMEDIATE);
    assert_eq!(b, ParameterMode::IMMEDIATE);
    assert_eq!(c, ParameterMode::IMMEDIATE);
  }

  #[test]
  fn test_get_parameter_modes_4() {
    let [a,b,c] = get_parameter_modes(11);
    assert_eq!(a, ParameterMode::POSITION);
    assert_eq!(b, ParameterMode::POSITION);
    assert_eq!(c, ParameterMode::POSITION);
  }
 
}