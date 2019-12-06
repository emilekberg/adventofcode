use std::fmt;

#[derive(Copy, Clone, Eq, PartialEq)]
pub enum ParameterMode {
  POSITION = 0,
  IMMEDIATE = 1,
}
impl ParameterMode {
  pub fn from_i32(value: i32) -> ParameterMode {
    match value {
      0 => ParameterMode::POSITION,
      1 => ParameterMode::IMMEDIATE,
      _ => panic!(),
    }
  }
}

impl fmt::Debug for ParameterMode {
  fn fmt(&self, f: &mut fmt::Formatter) -> fmt::Result {
    match *self {
      ParameterMode::POSITION => write!(f, "POSITION"),
      ParameterMode::IMMEDIATE => write!(f, "IMMEDIATE"),
    }
  }
}