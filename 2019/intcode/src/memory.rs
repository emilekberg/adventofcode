use super::parametermode::ParameterMode;
pub struct Memory {
  ram: Vec<i64>,
  out: Vec<i64>,
  relative_base: i64,
}
pub fn new(mut ram: Vec<i64>) -> Memory {
  ram.resize(10000, 0);
  return Memory {
    ram,
    out: Vec::new(),
    relative_base: 0,
  };
}
impl Memory {
  pub fn output(&mut self, value: i64) {
    self.out.push(value);
  }

  pub fn set(&mut self, value: i64, offset: usize, mode: ParameterMode) {
    let pos = self.get_pos(offset,mode);
    self.ram[pos] = value;
  }

  pub fn get(&self, offset: usize, mode: ParameterMode) -> i64 {
    let pos = self.get_pos(offset, mode);
    return self.ram[pos];
  }

  pub fn get_pos(&self, offset: usize, mode: ParameterMode) -> usize {
    return match mode {
      ParameterMode::POSITION => self.ram[offset] as usize,
      ParameterMode::IMMEDIATE => offset,
      ParameterMode::RELATIVE => (self.ram[offset] + self.relative_base) as usize,
    };
  }

  pub fn get_ram_clone(&self) -> Vec<i64> {
    return self.ram.clone();
  }

  pub fn get_output_clone(&self) -> Vec<i64> {
    return self.out.clone();
  }

  pub fn offset_relative_base(&mut self, relative_base: i64) {
    self.relative_base += relative_base;
  }
}

#[cfg(test)]
mod memory_tests {
  use super::*;
  #[test]
  pub fn test_get_pos_immediate() {
    let mem = new(vec![5,6,7,8,0,1,2,3,4]);
    assert_eq!(mem.get_pos(2, ParameterMode::IMMEDIATE), 2);
    assert_eq!(mem.get_pos(1, ParameterMode::IMMEDIATE), 1);
    assert_eq!(mem.get_pos(8, ParameterMode::IMMEDIATE), 8);
    assert_eq!(mem.get_pos(6, ParameterMode::IMMEDIATE), 6);
  }
  #[test]
  pub fn test_get_pos_position() {
    let mem = new(vec![5,6,7,8,0,1,2,3,4]);
    assert_eq!(mem.get_pos(2, ParameterMode::POSITION), 7);
    assert_eq!(mem.get_pos(1, ParameterMode::POSITION), 6);
    assert_eq!(mem.get_pos(8, ParameterMode::POSITION), 4);
    assert_eq!(mem.get_pos(6, ParameterMode::POSITION), 2);
  }
  #[test]
  fn test_set_immediate() {
    let mut mem = new(vec![0,0,0,0]);
    mem.set(10, 0, ParameterMode::IMMEDIATE);
    mem.set(20, 1, ParameterMode::IMMEDIATE);
    mem.set(30, 2, ParameterMode::IMMEDIATE);
    mem.set(40, 3, ParameterMode::IMMEDIATE);
    assert_eq!(mem.ram, vec![10,20,30,40]);
  }

  #[test]
  fn test_set_position() {
    let mut mem = new(vec![1,0,2,0]);
    mem.set(3, 0, ParameterMode::POSITION);
    mem.set(100, 1, ParameterMode::POSITION);
    assert_eq!(mem.ram, vec![1,3,2,100]);
  }
  #[test]
  fn test_get() {
    let mem = new(vec![1,50,3,120]);
    assert_eq!(mem.get(0, ParameterMode::POSITION), 50);
    assert_eq!(mem.get(2, ParameterMode::POSITION), 120);
  }
}