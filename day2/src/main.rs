extern crate intcode;
fn main() {  
  let memory: Vec<i32> = std::fs::read_to_string("./input.txt")
    .unwrap()
    .split(",")
    .map(|x| x.parse().unwrap())
    .collect();
  
  let part1_result = part1(&mut memory.clone());
  let part2_result = part2(&mut memory.clone());
  println!("part 1: {}", part1_result);
  println!("part 2: {}", part2_result);
}

fn set_verb_noun(memory: &mut Vec<i32>, verb: i32, noun: i32) -> &mut Vec<i32> {
  memory[1] = verb;
  memory[2] = noun;
  return memory;
}

fn part1(memory: &mut Vec<i32>) -> i32 {
  let (result, _, _) = intcode::run_program(set_verb_noun(memory, 12, 2));
  return result;
}

fn part2(memory: &mut Vec<i32>) -> i32{
let search_for = 19690720;
  let mut noun = 0;
  let mut verb = 0;
  loop {

    let (result, _, _) = intcode::run_program(set_verb_noun(&mut memory.clone(), noun, verb));
    if result == search_for {
      return 100*noun+verb;
    }
    noun += 1;
    if noun == 100 {
     noun = 0;
     verb += 1; 
    }
  }
}