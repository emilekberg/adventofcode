use std::fmt;
#[derive(PartialEq)]
pub enum Direction {
  Up,
  Down,
  Left,
  Right,
  Unknown,
}

impl fmt::Debug for Direction {
  fn fmt(&self, f: &mut fmt::Formatter) -> fmt::Result {
      match *self {
          Direction::Up => write!(f, "Up"),
          Direction::Down => write!(f, "Down"),
          Direction::Left => write!(f, "Left"),
          Direction::Right => write!(f, "Right"),
          Direction::Unknown => write!(f, "Unknown"),
      }
  }
}