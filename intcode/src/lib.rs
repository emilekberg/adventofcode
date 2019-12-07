#![allow(dead_code)]

use std::io;

mod utils;
mod operation;
mod parametermode;
mod memory;

use operation::Operation;
use parametermode::ParameterMode;

pub fn run_program(in_mem: Vec<i32>) -> (i32, Vec<i32>, Vec<i32>) {
  let mut i = 0;
  let mut mem = memory::new(in_mem);
  loop {
    let code = mem.get(i, ParameterMode::IMMEDIATE);
    let instruction = utils::get_operation(code);
    println!("command: {:?}({})", instruction, code);
    
    match instruction {
      Operation::Add(p1m, p2m, p3m) => {
        let result = mem.get(i + 1, p1m) + mem.get(i + 2, p2m);
        mem.set(result, i+3, p3m);
        i += 4;
      },
      Operation::Mul(p1m, p2m, p3m) => {
        let result = mem.get(i+1, p1m) * mem.get(i + 2, p2m);
        mem.set(result, i+3, p3m);
        i += 4;
      },
      Operation::Input(p1m) => {
        let result = input();
        mem.set(result, i+1, p1m);
        i += 2;
      },
      Operation::Output(p1m) => {
        let value = mem.get(i+1, p1m);
        println!("----------------- OUTPUT: pos {}, value {} ----------------------", i+1, value);
        mem.output(value);
        i += 2;
      },
      Operation::JumpIfTrue(p1m, p2m) => {
        if mem.get(i+1, p1m) != 0 {
          i = mem.get(i+2, p2m) as usize;
        } else {
          i += 3;
        }
      },
      Operation::JumpIfFalse(p1m, p2m) => {
        if mem.get(i+1, p1m) == 0 {
          i = mem.get(i+2, p2m) as usize;
        } else {
          i += 3;
        }
      },
      Operation::LessThan(p1m, p2m, p3m) => {
        let result = match mem.get(i+1, p1m) < mem.get(i+2, p2m) {
          true => 1,
          false => 0,
        };
        mem.set(result, i+3, p3m);
        i += 4;
      },
      Operation::Equals(p1m, p2m, p3m) => {
        let result = match mem.get(i+1, p1m) == mem.get(i+2, p2m) {
          true => 1,
          false => 0,
        };
        mem.set(result, i+3, p3m);
        i += 4;
      },
      Operation::Halt => {
        return (mem.get(0, ParameterMode::IMMEDIATE), mem.get_output_clone(), mem.get_ram_clone());
      },
    }
  }}
  fn input() -> i32 {
    let mut buffer = String::new();
    let result: i32 = match io::stdin().read_line(&mut buffer) {
      Ok(_) => {
        buffer.trim().parse()
          .expect("error while parsing number")
      },
      Err(error) => panic!(error),
    };
    return result;
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
