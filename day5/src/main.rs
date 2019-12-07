extern crate intcode;

fn main() {
  let memory = std::fs::read_to_string("./input.txt")
    .unwrap()
    .split(",")
    .map(|x| x.parse().unwrap())
    .collect(); 
  let (res, output, _) = intcode::run_program(memory);
  println!("first position: {}", res);
  println!("output last: {}", output.last().unwrap());
}