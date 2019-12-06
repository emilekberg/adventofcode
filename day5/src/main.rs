use std::vec;
use std::fmt;
use std::io;

#[derive(Copy, Clone, Eq, PartialEq)]
enum ParameterMode {
  POSITION = 0,
  IMMEDIATE = 1,
}
impl ParameterMode {
  fn from_i32(value: i32) -> ParameterMode {
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


#[derive(Copy, Clone, Eq, PartialEq)]
enum Operation {
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
  fn from_i32(value: i32) -> Operation {
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
fn main() {
  let memory = get_memory_copy();
  let (res, _) = run_program(memory);
  println!("{}", res);
}

fn get_memory_copy() -> Vec<i32> {
  return vec![3,225,1,225,6,6,1100,1,238,225,104,0,1002,188,27,224,1001,224,-2241,224,4,224,102,8,223,223,1001,224,6,224,1,223,224,223,101,65,153,224,101,-108,224,224,4,224,1002,223,8,223,1001,224,1,224,1,224,223,223,1,158,191,224,101,-113,224,224,4,224,102,8,223,223,1001,224,7,224,1,223,224,223,1001,195,14,224,1001,224,-81,224,4,224,1002,223,8,223,101,3,224,224,1,224,223,223,1102,47,76,225,1102,35,69,224,101,-2415,224,224,4,224,102,8,223,223,101,2,224,224,1,224,223,223,1101,32,38,224,101,-70,224,224,4,224,102,8,223,223,101,3,224,224,1,224,223,223,1102,66,13,225,1102,43,84,225,1101,12,62,225,1102,30,35,225,2,149,101,224,101,-3102,224,224,4,224,102,8,223,223,101,4,224,224,1,223,224,223,1101,76,83,225,1102,51,51,225,1102,67,75,225,102,42,162,224,101,-1470,224,224,4,224,102,8,223,223,101,1,224,224,1,223,224,223,4,223,99,0,0,0,677,0,0,0,0,0,0,0,0,0,0,0,1105,0,99999,1105,227,247,1105,1,99999,1005,227,99999,1005,0,256,1105,1,99999,1106,227,99999,1106,0,265,1105,1,99999,1006,0,99999,1006,227,274,1105,1,99999,1105,1,280,1105,1,99999,1,225,225,225,1101,294,0,0,105,1,0,1105,1,99999,1106,0,300,1105,1,99999,1,225,225,225,1101,314,0,0,106,0,0,1105,1,99999,1108,226,677,224,1002,223,2,223,1005,224,329,101,1,223,223,108,226,226,224,1002,223,2,223,1005,224,344,1001,223,1,223,1107,677,226,224,1002,223,2,223,1006,224,359,101,1,223,223,1008,226,226,224,1002,223,2,223,1005,224,374,101,1,223,223,8,226,677,224,102,2,223,223,1006,224,389,101,1,223,223,7,226,677,224,1002,223,2,223,1005,224,404,1001,223,1,223,7,226,226,224,1002,223,2,223,1005,224,419,101,1,223,223,107,226,677,224,1002,223,2,223,1005,224,434,101,1,223,223,107,226,226,224,1002,223,2,223,1005,224,449,1001,223,1,223,1107,226,677,224,102,2,223,223,1006,224,464,1001,223,1,223,1007,677,226,224,1002,223,2,223,1006,224,479,1001,223,1,223,1107,677,677,224,1002,223,2,223,1005,224,494,101,1,223,223,1108,677,226,224,102,2,223,223,1006,224,509,101,1,223,223,7,677,226,224,1002,223,2,223,1005,224,524,1001,223,1,223,1008,677,226,224,102,2,223,223,1005,224,539,1001,223,1,223,1108,226,226,224,102,2,223,223,1005,224,554,101,1,223,223,107,677,677,224,102,2,223,223,1006,224,569,1001,223,1,223,1007,226,226,224,102,2,223,223,1006,224,584,101,1,223,223,8,677,677,224,102,2,223,223,1005,224,599,1001,223,1,223,108,677,677,224,1002,223,2,223,1005,224,614,101,1,223,223,108,226,677,224,102,2,223,223,1005,224,629,101,1,223,223,8,677,226,224,102,2,223,223,1006,224,644,1001,223,1,223,1007,677,677,224,1002,223,2,223,1006,224,659,1001,223,1,223,1008,677,677,224,1002,223,2,223,1005,224,674,101,1,223,223,4,223,99,226];
}

fn run_program(mut memory: Vec<i32>) -> (i32, Vec<i32>) {
  let mut i = 0;
  loop {
    if i >= memory.len() {
      panic!();
    }
    let param_modes = get_parameter_modes(memory[i]);
    let instruction = get_opcode(memory[i]);
    println!("command: {:?}({}), ParameterModes: {:?},{:?},{:?}", instruction, memory[i], param_modes[0], param_modes[1], param_modes[2]);
    
    match instruction {
      Operation::Add => {
        add(&mut memory, i, param_modes);
        i += 4;
      },
      Operation::Mul => {
        mul(&mut memory, i, param_modes);
        i += 4;
      },
      Operation::Input => {
        input(&mut memory, i, param_modes);
        i += 2;
      },
      Operation::Output => {
        output(&mut memory, i, param_modes);
        i += 2;
      },
      Operation::JumpIfTrue => {
        let result = jump_if_true(&mut memory, i, param_modes);
        if result != 0 {
          i = result as usize;
        } else {
          i += 3;
        }
      },
      Operation::JumpIfFalse => {
        let result = jump_if_false(&mut memory, i, param_modes);
        if result != 0 {
          i = result as usize;
        } else {
          i += 3;
        }
      },
      Operation::LessThan => {
        less_then(&mut memory, i, param_modes);
        i += 4;
      },
      Operation::Equals => {
        equals(&mut memory, i, param_modes);
        i += 4;
      },

      Operation::Halt => {
        println!("halt!");
        // print(&memory);
        return (memory[0], memory);
      },
    }
  }
}

fn get_value(memory: &mut Vec<i32>, param_mode: ParameterMode, memory_offset: usize) -> i32 {
  let result: i32 = match param_mode {
    ParameterMode::POSITION => {
      let pos = memory[memory_offset] as usize;
      memory[pos]
    },
    ParameterMode::IMMEDIATE => {
      memory[memory_offset]
    }
  };
  return result;
}

fn get_pos_to_store(memory: &mut Vec<i32>, param_mode: ParameterMode, memory_offset: usize) -> i32 {
  let result: i32 = match param_mode {
    ParameterMode::POSITION => {
      memory[memory_offset]
    },
    ParameterMode::IMMEDIATE => {
      memory_offset as i32
    }
  };
  return result;
}
fn add(memory: &mut Vec<i32>, pos: usize, param_modes: [ParameterMode; 3]) {
  println!("- {},{},{}", memory[pos+1], memory[pos+2], memory[pos+3]);
  let a = get_value(memory, param_modes[2], pos + 1);
  let b = get_value(memory, param_modes[1], pos + 2);
  let result_pos = get_pos_to_store(memory, param_modes[0], pos + 3);
  let result = a+b;
  println!("- {}+{}={} in {}", a,b,result,result_pos);
  memory[result_pos as usize] = result;
}

fn mul(memory: &mut Vec<i32>, pos: usize, param_modes: [ParameterMode; 3]) {
  println!("- {},{},{}", memory[pos+1], memory[pos+2], memory[pos+3]);
  let a = get_value(memory, param_modes[2], pos + 1);
  let b = get_value(memory, param_modes[1], pos + 2);
  let result_pos = get_pos_to_store(memory, param_modes[0], pos + 3);
  let result = a*b;
  println!("- {}x{}={} in {}", a,b,result,result_pos);
  memory[result_pos as usize] = result;
}

fn input(memory: &mut Vec<i32>, pos: usize, _param_modes: [ParameterMode; 3]) {
  let mut buffer = String::new();
  let result: i32 = match io::stdin().read_line(&mut buffer) {
    Ok(_) => {
      buffer.trim().parse()
        .expect("error while parsing number")
    },
    Err(error) => panic!(error),
  };
  let result_pos = memory[pos+1];
  memory[result_pos as usize] = result;
}

fn output(memory: &mut Vec<i32>, pos: usize, param_modes: [ParameterMode; 3]) {
  let value = get_value(memory, param_modes[2], pos + 1);
  println!("----------------- OUTPUT: pos {}, value {} ----------------------", pos+1, value);
}

fn jump_if_true(memory: &mut Vec<i32>, pos: usize, param_modes: [ParameterMode; 3]) -> i32 {
  println!("- {},{}", memory[pos+1], memory[pos+2]);
  let check_if_true = get_value(memory, param_modes[2], pos + 1);
  let jump_to = get_value(memory, param_modes[1], pos + 2);
  if check_if_true > 0 {
    return jump_to;
  }
  return 0;
}

fn jump_if_false(memory: &mut Vec<i32>, pos: usize, param_modes: [ParameterMode; 3]) -> i32 {
  println!("- {},{}", memory[pos+1], memory[pos+2]);
  let check_if_false = get_value(memory, param_modes[2], pos + 1);
  let jump_to = get_value(memory, param_modes[1], pos + 2);
  if check_if_false == 0 {
    return jump_to;
  }
  return 0;
}

fn less_then(memory: &mut Vec<i32>, pos: usize, param_modes: [ParameterMode; 3]) {
  println!("- {},{},{}", memory[pos+1], memory[pos+2], memory[pos+3]);
  let a = get_value(memory, param_modes[2], pos + 1);
  let b = get_value(memory, param_modes[1], pos + 2);
  let result_pos = get_pos_to_store(memory, param_modes[0], pos + 3);
  let result: i32 = match a < b {
    true => 1,
    false => 0,
  };
  println!("- {}<{}={} in {}", a,b,result,result_pos);
  memory[result_pos as usize] = result;
}

fn equals(memory: &mut Vec<i32>, pos: usize, param_modes: [ParameterMode; 3]) {
  println!("- {},{},{}", memory[pos+1], memory[pos+2], memory[pos+3]);
  let a = get_value(memory, param_modes[2], pos + 1);
  let b = get_value(memory, param_modes[1], pos + 2);
  let result_pos = get_pos_to_store(memory, param_modes[0], pos + 3);
  let result: i32 = match a == b {
    true => 1,
    false => 0,
  };
  println!("- {}=={}={} in {}", a,b,result,result_pos);
  memory[result_pos as usize] = result;
}

fn get_parameter_modes(value: i32) -> [ParameterMode; 3] {
  return [
    ParameterMode::from_i32(get_ten_thousands(value)),
    ParameterMode::from_i32(get_thousands(value)),
    ParameterMode::from_i32(get_hundreds(value)),
  ]
}

fn get_opcode(value: i32) -> Operation {
  return Operation::from_i32(get_tenth(value));
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

  #[test]
  fn test_get_parameter_modes() {
    let [a,b,c] = get_parameter_modes(1002);
    assert_eq!(a, ParameterMode::POSITION);
    assert_eq!(b, ParameterMode::IMMEDIATE);
    assert_eq!(c, ParameterMode::POSITION);
  }

  #[test]
  fn test_get_parameter_modes_2() {
    let [a,b,c] = get_parameter_modes(11042);
    assert_eq!(a, ParameterMode::IMMEDIATE);
    assert_eq!(b, ParameterMode::IMMEDIATE);
    assert_eq!(c, ParameterMode::POSITION);
  }

  #[test]
  fn test_get_parameter_modes_3() {
    let [a,b,c] = get_parameter_modes(11100);
    assert_eq!(a, ParameterMode::IMMEDIATE);
    assert_eq!(b, ParameterMode::IMMEDIATE);
    assert_eq!(c, ParameterMode::IMMEDIATE);
  }

  #[test]
  fn test_get_parameter_modes_4() {
    let [a,b,c] = get_parameter_modes(11);
    assert_eq!(a, ParameterMode::POSITION);
    assert_eq!(b, ParameterMode::POSITION);
    assert_eq!(c, ParameterMode::POSITION);
  }

  #[test]
  fn test_run_program() {
    let mem = vec![1002,4,3,4,33];
    let expected = vec![1002,4,3,4,99];
    let (res, mem2) = run_program(mem);
    assert_eq!(res, 1002);
    assert_eq!(mem2, expected);
  }
}