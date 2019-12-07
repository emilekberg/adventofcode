use std::fmt;
use super::ParameterMode;
#[derive(Copy, Clone, Eq, PartialEq)]
pub enum Operation {
  Add(ParameterMode, ParameterMode, ParameterMode),
  Mul(ParameterMode, ParameterMode, ParameterMode),
  Input(ParameterMode),
  Output(ParameterMode),
  JumpIfTrue(ParameterMode, ParameterMode),
  JumpIfFalse(ParameterMode, ParameterMode),
  LessThan(ParameterMode, ParameterMode, ParameterMode),
  Equals(ParameterMode, ParameterMode, ParameterMode),
  Halt,
}

impl fmt::Debug for Operation {
  fn fmt(&self, f: &mut fmt::Formatter) -> fmt::Result {
    match *self {
      Operation::Add(_,_,_)          => write!(f, "ADD"),
      Operation::Mul(_,_,_)          => write!(f, "MUL"),
      Operation::Input(_)            => write!(f, "INPUT"),
      Operation::Output(_)           => write!(f, "OUTPUT"),
      Operation::JumpIfTrue(_,_)     => write!(f, "JUMP_IF_TRUE"),
      Operation::JumpIfFalse(_,_)    => write!(f, "JUMP_IF_FALSE"),
      Operation::LessThan(_,_,_)     => write!(f, "LESS_THEN"),
      Operation::Equals(_,_,_)       => write!(f, "EQUALS"),
      Operation::Halt                => write!(f, "HALT"),
    }
  }
}
