use std::vec;
fn main() {
  let res = run_program(12,2);
  println!("{}", res);
  let search_for = 19690720;
  let mut noun = 0;
  let mut verb = 0;
  loop {

    let result = run_program(noun, verb);
    if result == search_for {
      println!("{}", 100*noun+verb);
      return;
    }
    noun += 1;
    if noun == 100 {
     noun = 0;
     verb += 1; 
    }
  }
}

fn get_memory_copy() -> Vec<i32> {
  return vec![1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,1,10,19,1,19,5,23,2,23,9,27,1,5,27,31,1,9,31,35,1,35,10,39,2,13,39,43,1,43,9,47,1,47,9,51,1,6,51,55,1,13,55,59,1,59,13,63,1,13,63,67,1,6,67,71,1,71,13,75,2,10,75,79,1,13,79,83,1,83,10,87,2,9,87,91,1,6,91,95,1,9,95,99,2,99,10,103,1,103,5,107,2,6,107,111,1,111,6,115,1,9,115,119,1,9,119,123,2,10,123,127,1,127,5,131,2,6,131,135,1,135,5,139,1,9,139,143,2,143,13,147,1,9,147,151,1,151,2,155,1,9,155,0,99,2,0,14,0];
}

fn run_program(noun: i32, verb: i32) -> i32 {
  let mut memory = get_memory_copy();
  memory[1] = noun;
  memory[2] = verb;
  let mut i = 0;
  let len = memory.len();
  loop {
    if i >= len {
      return -1;
    }
    let instruction = memory[i];
    match instruction {
      1 => {
        let result_pos = memory[i+3].clone() as usize;
        let result = add(&memory, i);
        i += 4;
        memory[result_pos] = result;
      },
      2 => {
        let result_pos = memory[i+3].clone() as usize;
        let result = mul(&memory, i);
        i += 4;
        memory[result_pos] = result;
      },
      99 => {
        println!("halt!");
        // print(&memory);
        return memory[0];
      },
      _ => continue,
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

fn print(memory: &vec::Vec<i32>) {
  println!("result: ");
  for i in 0..memory.len() {
    print!("{},", memory[i]);
  }
}