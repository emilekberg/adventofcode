use std::vec;
use std::io;

enum ParameterMode {
  POSITION = 0,
  IMMEDIATE = 1,
}
impl ParameterMode {
  fn from_i32(value: i32) -> ParameterMode {
    match value {
      1 => ParameterMode::POSITION,
      2 => ParameterMode::IMMEDIATE,
      _ => panic!(),
    }
  }
}

enum Operation {
  ADD = 1,
  MUL = 2,
  INPUT = 3,
  OUTPUT = 4,
  HALT = 99,
}
impl Operation {
  fn from_i32(value: i32) -> Operation {
      match value {
          1 => Operation::ADD,
          2 => Operation::MUL,
          3 => Operation::INPUT,
          4 => Operation::OUTPUT,
          99 => Operation::HALT,
          _ => panic!("Unknown value: {}", value),
      }
  }
}

fn main() {
  let res = run_program();
  println!("{}", res);
}

fn get_memory_copy() -> Vec<i32> {
  return vec![3,225,1,225,6,6,1100,1,238,225,104,0,1002,188,27,224,1001,224,-2241,224,4,224,102,8,223,223,1001,224,6,224,1,223,224,223,101,65,153,224,101,-108,224,224,4,224,1002,223,8,223,1001,224,1,224,1,224,223,223,1,158,191,224,101,-113,224,224,4,224,102,8,223,223,1001,224,7,224,1,223,224,223,1001,195,14,224,1001,224,-81,224,4,224,1002,223,8,223,101,3,224,224,1,224,223,223,1102,47,76,225,1102,35,69,224,101,-2415,224,224,4,224,102,8,223,223,101,2,224,224,1,224,223,223,1101,32,38,224,101,-70,224,224,4,224,102,8,223,223,101,3,224,224,1,224,223,223,1102,66,13,225,1102,43,84,225,1101,12,62,225,1102,30,35,225,2,149,101,224,101,-3102,224,224,4,224,102,8,223,223,101,4,224,224,1,223,224,223,1101,76,83,225,1102,51,51,225,1102,67,75,225,102,42,162,224,101,-1470,224,224,4,224,102,8,223,223,101,1,224,224,1,223,224,223,4,223,99,0,0,0,677,0,0,0,0,0,0,0,0,0,0,0,1105,0,99999,1105,227,247,1105,1,99999,1005,227,99999,1005,0,256,1105,1,99999,1106,227,99999,1106,0,265,1105,1,99999,1006,0,99999,1006,227,274,1105,1,99999,1105,1,280,1105,1,99999,1,225,225,225,1101,294,0,0,105,1,0,1105,1,99999,1106,0,300,1105,1,99999,1,225,225,225,1101,314,0,0,106,0,0,1105,1,99999,1108,226,677,224,1002,223,2,223,1005,224,329,101,1,223,223,108,226,226,224,1002,223,2,223,1005,224,344,1001,223,1,223,1107,677,226,224,1002,223,2,223,1006,224,359,101,1,223,223,1008,226,226,224,1002,223,2,223,1005,224,374,101,1,223,223,8,226,677,224,102,2,223,223,1006,224,389,101,1,223,223,7,226,677,224,1002,223,2,223,1005,224,404,1001,223,1,223,7,226,226,224,1002,223,2,223,1005,224,419,101,1,223,223,107,226,677,224,1002,223,2,223,1005,224,434,101,1,223,223,107,226,226,224,1002,223,2,223,1005,224,449,1001,223,1,223,1107,226,677,224,102,2,223,223,1006,224,464,1001,223,1,223,1007,677,226,224,1002,223,2,223,1006,224,479,1001,223,1,223,1107,677,677,224,1002,223,2,223,1005,224,494,101,1,223,223,1108,677,226,224,102,2,223,223,1006,224,509,101,1,223,223,7,677,226,224,1002,223,2,223,1005,224,524,1001,223,1,223,1008,677,226,224,102,2,223,223,1005,224,539,1001,223,1,223,1108,226,226,224,102,2,223,223,1005,224,554,101,1,223,223,107,677,677,224,102,2,223,223,1006,224,569,1001,223,1,223,1007,226,226,224,102,2,223,223,1006,224,584,101,1,223,223,8,677,677,224,102,2,223,223,1005,224,599,1001,223,1,223,108,677,677,224,1002,223,2,223,1005,224,614,101,1,223,223,108,226,677,224,102,2,223,223,1005,224,629,101,1,223,223,8,677,226,224,102,2,223,223,1006,224,644,1001,223,1,223,1007,677,677,224,1002,223,2,223,1006,224,659,1001,223,1,223,1008,677,677,224,1002,223,2,223,1005,224,674,101,1,223,223,4,223,99,226];
}

fn run_program() -> i32 {
  let mut memory = get_memory_copy();
  let mut i = 0;
  loop {
    if i >= memory.len() {
      panic!();
    }
    let param_modes = get_parameter_modes(memory[i]);
    let instruction = get_opcode(memory[i]);
    
    
    match instruction {
      Operation::ADD => {
        let result_pos = memory[i+3].clone() as usize;
        memory[result_pos] = add(&memory, i);
        i += 4;
      },
      Operation::MUL => {
        let result_pos = memory[i+3].clone() as usize;
        memory[result_pos] = mul(&memory, i);
        i += 4;
      },
      Operation::INPUT => {
        let store_pos = memory[i+1].clone() as usize;
        memory[store_pos] = input();
        i += 2;
      },
      Operation::OUTPUT => {
        let output_pos = memory[i+1].clone() as usize;
        output(&memory, output_pos);
        i += 2;
      },
      Operation::HALT => {
        println!("halt!");
        // print(&memory);
        return memory[0];
      },
    }
  }
}
fn add(memory: &vec::Vec<i32>, pos: usize) -> i32 {
  let first_pos = memory[pos + 1].clone() as usize;
  let second_pos = memory[pos + 2].clone() as usize;
  // println!("add");
  let a = memory[first_pos];
  let b = memory[second_pos];
  return a+b;
}

fn mul(memory: &vec::Vec<i32>, pos: usize) -> i32 {
  let first_pos = memory[pos + 1].clone() as usize;
  let second_pos = memory[pos + 2].clone() as usize;
  // println!("add");
  let a = memory[first_pos];
  let b = memory[second_pos];
  return a*b;
}

fn input() -> i32 {
  let mut buffer = String::new();
  match io::stdin().read_line(&mut buffer) {
    Ok(_) => {
      return buffer.parse().unwrap();
    },
    Err(error) => panic!(error),
  }
}

fn output(memory: &Vec<i32>, pos: usize) {
  println!("{}", memory[pos]);
}

fn get_parameter_modes(value: i32) -> [ParameterMode; 3] {
  return [
    ParameterMode::from_i32(get_ten_thousands(value)),
    ParameterMode::from_i32(get_thousands(value)),
    ParameterMode::from_i32(get_hundreds(value)),
  ]
}

fn get_opcode(value: i32) -> Operation {
  return Operation::from_i32((get_hundreds(value)));
}

fn get_ten_thousands(num: i32) -> i32 {
  let result: i32 = num / 10000;
  return result;
}

fn get_thousands(num: i32) -> i32 {
  let result: i32 = (num / 1000) % 10;
  return result;
}


fn get_hundreds(num: i32) -> i32 {
  let result: i32 = (num / 100) % 10;
  return result;
}

fn get_tenth(num: i32) -> i32 {
  let to_subtract: i32 = (num / 100) * 100;
  let result: i32 = num - to_subtract;
  return result;
}

#[cfg(test)]
mod tests {
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
}