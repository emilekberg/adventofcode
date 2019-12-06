#![allow(dead_code)]

use std::io;

mod utils;
mod operation;
mod parametermode;

use operation::Operation;
use parametermode::ParameterMode;

pub fn run_program(memory: &mut Vec<i32>) -> (i32, Vec<i32>, &mut Vec<i32>) {
  let mut i = 0;
  let mut output_list: Vec<i32> = Vec::new();
  loop {
    if i >= memory.len() {
      panic!();
    }
    let param_modes = utils::get_parameter_modes(memory[i]);
    let instruction = utils::get_opcode(memory[i]);
    println!("command: {:?}({}), ParameterModes: {:?},{:?},{:?}", instruction, memory[i], param_modes[0], param_modes[1], param_modes[2]);
    
    match instruction {
      Operation::Add => {
        add(memory, i, param_modes);
        i += 4;
      },
      Operation::Mul => {
        mul(memory, i, param_modes);
        i += 4;
      },
      Operation::Input => {
        input(memory, i, param_modes);
        i += 2;
      },
      Operation::Output => {
        let result = output(memory, i, param_modes);
        output_list.push(result);
        i += 2;
      },
      Operation::JumpIfTrue => {
        let result = jump_if_true(memory, i, param_modes);
        if result != 0 {
          i = result as usize;
        } else {
          i += 3;
        }
      },
      Operation::JumpIfFalse => {
        let result = jump_if_false(memory, i, param_modes);
        if result != 0 {
          i = result as usize;
        } else {
          i += 3;
        }
      },
      Operation::LessThan => {
        less_then(memory, i, param_modes);
        i += 4;
      },
      Operation::Equals => {
        equals(memory, i, param_modes);
        i += 4;
      },

      Operation::Halt => {
        println!("halt!");
        // print(&memory);
        return (memory[0], output_list, memory);
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

fn get_pos(memory: &mut Vec<i32>, param_mode: ParameterMode, memory_offset: usize) -> usize {
  return match param_mode {
    ParameterMode::POSITION => memory[memory_offset] as usize,
    ParameterMode::IMMEDIATE => memory_offset,
  };
}
fn add(memory: &mut Vec<i32>, pos: usize, param_modes: [ParameterMode; 3]) {
  let a = get_value(memory, param_modes[2], pos + 1);
  let b = get_value(memory, param_modes[1], pos + 2);
  let result_pos = get_pos(memory, param_modes[0], pos + 3);
  let result = a+b;
  memory[result_pos as usize] = result;
}

fn mul(memory: &mut Vec<i32>, pos: usize, param_modes: [ParameterMode; 3]) {
  let a = get_value(memory, param_modes[2], pos + 1);
  let b = get_value(memory, param_modes[1], pos + 2);
  let result_pos = get_pos(memory, param_modes[0], pos + 3);
  let result = a*b;
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

fn output(memory: &mut Vec<i32>, pos: usize, param_modes: [ParameterMode; 3]) -> i32 {
  let value = get_value(memory, param_modes[2], pos + 1);
  println!("----------------- OUTPUT: pos {}, value {} ----------------------", pos+1, value);
  return value;
}

fn jump_if_true(memory: &mut Vec<i32>, pos: usize, param_modes: [ParameterMode; 3]) -> i32 {
  let check_if_true = get_value(memory, param_modes[2], pos + 1);
  let jump_to = get_value(memory, param_modes[1], pos + 2);
  if check_if_true > 0 {
    return jump_to;
  }
  return 0;
}

fn jump_if_false(memory: &mut Vec<i32>, pos: usize, param_modes: [ParameterMode; 3]) -> i32 {
  let check_if_false = get_value(memory, param_modes[2], pos + 1);
  let jump_to = get_value(memory, param_modes[1], pos + 2);
  if check_if_false == 0 {
    return jump_to;
  }
  return 0;
}

fn less_then(memory: &mut Vec<i32>, pos: usize, param_modes: [ParameterMode; 3]) {
  let a = get_value(memory, param_modes[2], pos + 1);
  let b = get_value(memory, param_modes[1], pos + 2);
  let result_pos = get_pos(memory, param_modes[0], pos + 3);
  let result: i32 = match a < b {
    true => 1,
    false => 0,
  };
  memory[result_pos as usize] = result;
}

fn equals(memory: &mut Vec<i32>, pos: usize, param_modes: [ParameterMode; 3]) {
  let a = get_value(memory, param_modes[2], pos + 1);
  let b = get_value(memory, param_modes[1], pos + 2);
  let result_pos = get_pos(memory, param_modes[0], pos + 3);
  let result: i32 = match a == b {
    true => 1,
    false => 0,
  };
  memory[result_pos as usize] = result;
}
#[cfg(test)]
mod tests {
  use super::*;
  
  #[test]
  fn test_run_program() {
    let mut mem = vec![1002,4,3,4,33];
    let expected = &mut vec![1002,4,3,4,99];
    let (res, _, mem2) = run_program(&mut mem);
    assert_eq!(res, 1002);
    assert_eq!(mem2, expected);
  }
}
