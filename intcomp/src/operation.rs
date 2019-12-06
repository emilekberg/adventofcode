use std::fmt;
#[derive(Copy, Clone, Eq, PartialEq)]
pub enum Operation {
  Add = 1,
  Mul = 2,
  Input = 3,
  Output = 4,
  JumpIfTrue = 5,
  JumpIfFalse = 6,
  LessThan = 7,
  Equals = 8,
  Halt = 99,
}
impl Operation {
  pub fn from_i32(value: i32) -> Operation {
    match value {
        1 => Operation::Add,
        2 => Operation::Mul,
        3 => Operation::Input,
        4 => Operation::Output,
        5 => Operation::JumpIfTrue,
        6 => Operation::JumpIfFalse,
        7 => Operation::LessThan,
        8 => Operation::Equals,
        99 => Operation::Halt,
        _ => panic!("Unknown value: {}", value),
    }
  }
}
impl fmt::Debug for Operation {
  fn fmt(&self, f: &mut fmt::Formatter) -> fmt::Result {
    match *self {
      Operation::Add => write!(f, "ADD"),
      Operation::Mul => write!(f, "MUL"),
      Operation::Input => write!(f, "INPUT"),
      Operation::Output => write!(f, "OUTPUT"),
      Operation::JumpIfTrue => write!(f, "JUMP_IF_TRUE"),
      Operation::JumpIfFalse => write!(f, "JUMP_IF_FALSE"),
      Operation::LessThan => write!(f, "LESS_THEN"),
      Operation::Equals => write!(f, "EQUALS"),
      Operation::Halt => write!(f, "HALT"),
    }
  }
}
