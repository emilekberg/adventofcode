extern crate intcode;

fn main() {
  let memory: Vec<i64> = std::fs::read_to_string("./input.txt")
    .unwrap()
    .split(",")
    .map(|x| x.parse().unwrap())
    .collect();
  let (res, output, _) = intcode::run_program(memory.clone(), || 1, |_| {});
  println!("first position: {}", res);
  println!("output last: {}", output.last().unwrap());
  
  let (res, output, _) = intcode::run_program(memory.clone(), || 5, |_| {});
  println!("first position: {}", res);
  println!("output last: {}", output.last().unwrap());

}