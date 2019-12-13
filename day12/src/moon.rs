extern crate utils;
use utils::vector3::Vector3;
#[derive(Debug,Clone,Copy,Eq,PartialEq,Hash)]
pub struct Moon {
  pub pos: Vector3,
  pub vel: Vector3,
}

pub fn new(pos: Vector3) -> Moon {
  Moon {
    pos,
    vel: utils::vector3::zero(),
  }
}

impl Moon {
  pub fn tick(&mut self) {
    self.pos = self.pos + self.vel;
  }

  pub fn gravity(&mut self, rhs: &Self) {

  } 
}
