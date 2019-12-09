#![allow(dead_code)]

pub mod utils;
pub mod operation;
pub mod parametermode;
pub mod memory;

use operation::Operation;
use parametermode::ParameterMode;



pub fn run_program<F: FnMut() -> i64+'static, F2: FnMut(i64) + 'static>(in_mem: Vec<i64>, mut input_cb: F, mut output_cb: F2) -> (i64, Vec<i64>, Vec<i64>) {
  let mut i = 0;
  let mut mem = memory::new(in_mem);
  loop {
    let code = mem.get(i, ParameterMode::IMMEDIATE);
    let instruction = utils::get_operation(code);
    // println!("{}\t:: command: {:?}({})", i, instruction, code);
    
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
        let result = input_cb();
        println!("----------------- INPUT: value {} ----------------------", result);
        mem.set(result, i+1, p1m);
        i += 2;
      },
      Operation::Output(p1m) => {
        let value = mem.get(i+1, p1m);
        println!("----------------- OUTPUT: value {} ----------------------", value);
        mem.output(value);
        output_cb(value);
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
      Operation::RelativeBaseOffset(p1m) => {
        mem.offset_relative_base(mem.get(i+1, p1m));
        i += 2;
      },
      Operation::Halt => {
        println!("------------------- !!!! HALT !!!! --------------------"); 
        return (mem.get(0, ParameterMode::IMMEDIATE), mem.get_output_clone(), mem.get_ram_clone());
      },
    }
  }
}

  #[cfg(test)]
mod tests {
  use super::*;
  
  #[test]
  fn test_run_program() {
    let mem = vec![1002,4,3,4,33];
    let expected = vec![1002,4,3,4,99];
    let (res, _, mem2) = run_program(mem, || 1, |_| {});
    assert_eq!(res, 1002);
    assert_eq!(mem2, expected);
  }


}
