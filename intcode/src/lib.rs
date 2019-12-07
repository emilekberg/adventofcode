#![allow(dead_code)]

use std::io;

mod utils;
mod operation;
mod parametermode;
mod memory;

use operation::Operation;
use parametermode::ParameterMode;
use memory::Memory;

pub fn run_program(in_mem: Vec<i32>) -> (i32, Vec<i32>, Vec<i32>) {
  let mut i = 0;
  let mut output_list: Vec<i32> = Vec::new();
  let mut mem = memory::new(in_mem);
  loop {
    let code = mem.get(i, ParameterMode::IMMEDIATE);    
    let param_modes = utils::get_parameter_modes(code);
    let instruction = utils::get_opcode(code);
    println!("command: {:?}({}), ParameterModes: {:?},{:?},{:?}", instruction, code, param_modes[0], param_modes[1], param_modes[2]);
    
    match instruction {
      Operation::Add => {
        add(&mut mem, i, param_modes);
        i += 4;
      },
      Operation::Mul => {
        mul(&mut mem, i, param_modes);
        i += 4;
      },
      Operation::Input => {
        input(&mut mem, i, param_modes);
        i += 2;
      },
      Operation::Output => {
        let result = output(&mut mem, i, param_modes);
        output_list.push(result);
        i += 2;
      },
      Operation::JumpIfTrue => {
        let result = jump_if_true(&mut mem, i, param_modes);
        if result != 0 {
          i = result as usize;
        } else {
          i += 3;
        }
      },
      Operation::JumpIfFalse => {
        let result = jump_if_false(&mut mem, i, param_modes);
        if result != 0 {
          i = result as usize;
        } else {
          i += 3;
        }
      },
      Operation::LessThan => {
        less_then(&mut mem, i, param_modes);
        i += 4;
      },
      Operation::Equals => {
        equals(&mut mem, i, param_modes);
        i += 4;
      },

      Operation::Halt => {
        println!("halt!");
        // print(&memory);
        return (mem.get(0, ParameterMode::IMMEDIATE), output_list, mem.get_ram());
      },
    }
  }}

fn add(mem: &mut Memory, offset: usize, modes: [ParameterMode; 3]) {
  let result = mem.get(offset + 1, modes[2]) + mem.get(offset + 2, modes[1]);
  mem.set(result, offset+3, modes[0]);
}

fn mul(mem: &mut Memory, offset: usize, modes: [ParameterMode; 3]) {
  let result = mem.get(offset + 1, modes[2]) * mem.get(offset + 2, modes[1]);
  mem.set(result, offset+3, modes[0]);
}

fn input(mem: &mut Memory, offset: usize, modes: [ParameterMode; 3]) {
  let mut buffer = String::new();
  let result: i32 = match io::stdin().read_line(&mut buffer) {
    Ok(_) => {
      buffer.trim().parse()
        .expect("error while parsing number")
    },
    Err(error) => panic!(error),
  };
  mem.set(result, offset+1, modes[2]);
}

fn output(mem: &mut Memory, offset: usize, modes: [ParameterMode; 3]) -> i32 {
  let value = mem.get(offset+1, modes[2]);
  println!("----------------- OUTPUT: pos {}, value {} ----------------------", offset+1, value);
  return value;
}

fn jump_if_true(mem: &mut Memory, offset: usize, modes: [ParameterMode; 3]) -> i32 {
  if mem.get(offset+1, modes[2]) > 0 {
    return mem.get(offset+2, modes[1]);
  }
  return 0;
}

fn jump_if_false(mem: &mut Memory, offset: usize, modes: [ParameterMode; 3]) -> i32 {
  if mem.get(offset+1, modes[2]) == 0 {
    return mem.get(offset+2, modes[1]);
  }
  return 0;
}

fn less_then(mem: &mut Memory, offset: usize, modes: [ParameterMode; 3]) {
  let result = match mem.get(offset+1, modes[2]) < mem.get(offset+2, modes[1]) {
    true => 1,
    false => 0,
  };
  mem.set(result, offset+3, modes[0]);
}

fn equals(mem: &mut Memory, offset: usize, modes: [ParameterMode; 3]) {
  let result = match mem.get(offset+1, modes[2]) == mem.get(offset+2, modes[1]) {
    true => 1,
    false => 0,
  };
  mem.set(result, offset+3, modes[0]);
}
#[cfg(test)]
mod tests {
  use super::*;
  
  #[test]
  fn test_run_program() {
    let mut mem = vec![1002,4,3,4,33];
    let expected = vec![1002,4,3,4,99];
    let (res, _, mem2) = run_program(mem);
    assert_eq!(res, 1002);
    assert_eq!(mem2, expected);
  }
}
