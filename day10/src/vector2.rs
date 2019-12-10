#[derive(Debug,PartialEq,Clone)]
pub struct Vector2 {
    pub x: f32,
    pub y: f32
}
pub fn new(x: f32, y: f32) -> Vector2 {
  Vector2 {
    x,
    y,
  }
}
pub fn zero() -> Vector2 {
  Vector2 {
    x: 0.,
    y: 0.,
  }
}

impl Vector2 {
  pub fn clone(&self) -> Self {
    new(self.x, self.y)
  }
  pub fn angle(lhs: &Self, rhs: &Self) -> f32 {
    let delta = Vector2::sub(rhs, lhs);
    let result = delta.y.atan2(delta.x);
    return result;
  }
  pub fn sub(lhs: &Self, rhs: &Self) -> Self {
    new(lhs.x-rhs.x, lhs.y-rhs.y)
  }

  pub fn div(lhs: &Self, div: f32) -> Self {
    new(lhs.x/div, lhs.y/div)
  }

  pub fn len(lhs: &Self) -> f32 {
    Vector2::len_squared(lhs).sqrt()
  }

  pub fn len_squared(lhs: &Self) -> f32 {
    (lhs.x * lhs.x) + (lhs.y * lhs.y)
  }

  pub fn normalize(lhs: &Self) -> Self {
    let len = Vector2::len(lhs);
    Vector2::div(lhs, len) 
  }
}

#[cfg(test)]
pub mod tests {
  use super::*;

  #[test]
  fn test_sub() {
    let a = new(2.,4.);
    let b = new(1.,1.);
    let result = Vector2::sub(&a,&b);

    assert_eq!(result.x, 1.);
    assert_eq!(result.y, 3.);
  }

  #[test]
  fn test_len() {
    let a = new(2.,0.,);
    let result = Vector2::len(&a); 
    assert_eq!(result, 2.);
  }

  #[test]
  fn test_len_squared() {
    let a = new (2.0,4.0);
    let result = Vector2::len_squared(&a);
    assert_eq!(result, 20.);
  }

  #[test]
  fn test_normalize() {
    let a = new (8.0, 0.);
    let result = Vector2::normalize(&a);
    assert_eq!(result.x, 1.);
    assert_eq!(result.y, 0.);
  }
}