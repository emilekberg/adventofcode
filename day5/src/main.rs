extern crate intcode;

fn main() {
  let mut memory: Vec<i32> = std::fs::read_to_string("./input.txt")
    .unwrap()
    .split(",")
    .map(|x| x.parse().unwrap())
    .collect();
  let (res, output, _) = intcode::run_program(&mut memory);
  println!("first position: {}", res);
  println!("output last: {}", output.last().unwrap());
}