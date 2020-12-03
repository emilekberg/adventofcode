#[derive(Debug,Clone,Copy,Hash,Eq,PartialEq)]
pub struct Vector3 {
  pub x: i32,
  pub y: i32,
  pub z: i32,
}

pub fn new(x: i32, y: i32, z: i32) -> Vector3 {
  Vector3 {
    x,
    y,
    z,
  }
}
pub fn zero() -> Vector3 {
  Vector3 {
    x: 0,
    y: 0,
    z: 0,
  }
}
pub fn unit_x() -> Vector3 {
  Vector3 {
    x: 1,
    y: 0,
    z: 0,
  }
}
pub fn unit_y() -> Vector3 {
  Vector3 {
    x: 0,
    y: 1,
    z: 0,
  }
}
pub fn unit_z() -> Vector3 {
  Vector3 {
    x: 0,
    y: 0,
    z: 1,
  }
}

impl Vector3 {
  pub fn eq(self, rhs: Vector3) -> bool {
    rhs.x == self.x && rhs.y == self.y && rhs.z == self.z
  }
}

impl std::ops::Add<Vector3> for Vector3 {
  type Output = Vector3;
  
  fn add(self, rhs: Self) -> Self {
    new(self.x + rhs.x, self.y + rhs.y, self.z + rhs.z)
  }
}
impl std::ops::Sub<Vector3> for Vector3 {
  type Output = Vector3;

  fn sub(self, rhs: Self) -> Self {
    new(self.x -rhs.x, self.y-rhs.y, self.z-rhs.z)
  }
}
impl std::ops::AddAssign<Vector3> for Vector3 {
  fn add_assign(&mut self, rhs: Self)  {
    *self = new(self.x+rhs.x,self.y+rhs.y,self.z+rhs.z);
  }
}
impl std::ops::SubAssign<Vector3> for Vector3 {
  fn sub_assign(&mut self, rhs: Self)  {
    *self = new(self.x-rhs.x,self.y-rhs.y,self.z-rhs.z);
  }
}
#[cfg(test)]
mod tests {
  use super::*;
  #[test]
  fn test_add() {
    let a = new(1,2,3,);
    let b = new(4,5,6,);
    let result = a+b;
    let expected = new(5,7,9,);
    assert_eq!(result.x,expected.x);
    assert_eq!(result.y,expected.y);
    assert_eq!(result.z,expected.z);
  }
}
