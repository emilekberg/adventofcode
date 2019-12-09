use std::fmt;

#[derive(Copy, Clone, Eq, PartialEq)]
pub enum ParameterMode {
  POSITION = 0,
  IMMEDIATE = 1,
  RELATIVE = 2,
}
impl ParameterMode {
  pub fn from_i64(value: i64) -> ParameterMode {
    match value {
      0 => ParameterMode::POSITION,
      1 => ParameterMode::IMMEDIATE,
      2 => ParameterMode::RELATIVE,
      _ => panic!(),
    }
  }
}

impl fmt::Debug for ParameterMode {
  fn fmt(&self, f: &mut fmt::Formatter) -> fmt::Result {
    match *self {
      ParameterMode::POSITION => write!(f, "POSITION"),
      ParameterMode::IMMEDIATE => write!(f, "IMMEDIATE"),
      ParameterMode::RELATIVE => write!(f, "RELATIVE"),
    }
  }
}