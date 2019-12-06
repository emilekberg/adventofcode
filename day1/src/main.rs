use std::fs;

fn main() {
  let filename = "./input.txt";
  println!("reading: {}", filename);
  let content = fs::read_to_string(filename)
    .expect("something went wrong when reading file: {}");

  println!("part1: {}", part1(content.clone()));
  println!("part2: {}", part2(content.clone()));
}

fn part1(content: String) -> i32 {
  let arr = content.lines();
  let mut sum = 0;
  for value in arr {
    let mass: i32 = value.parse().unwrap();
    sum += get_required_fuel(mass);
  }
  return sum;
}

fn part2(content: String) -> i32 {
  let arr = content.lines();
  let mut sum = 0;
  for value in arr {
    let mass: i32 = value.parse().unwrap();
    sum += get_required_fuel_recursive(mass);
  }
  return sum;
}

fn get_required_fuel(mass: i32) -> i32 {
  // will do a "floor" since we only use integers.
  return (mass / 3) - 2;
}

fn get_required_fuel_recursive(mass: i32) -> i32 {
  let mut sum = 0;
  let mut mass_to_calc = mass;
  loop {
    let fuel = get_required_fuel(mass_to_calc);
    mass_to_calc = fuel;
    sum += fuel;
    if fuel <= 0 {
      sum -= fuel;
      return sum;
    }
  }

}

#[cfg(test)]
mod tests {
  use super::*;
  #[test]
  fn get_required_fuel_12() {
    let result = get_required_fuel(12);
    let expected = 2;
    assert_eq!(result, expected);
  }
  #[test]
  fn get_required_fuel_14() {
    let result = get_required_fuel(12);
    let expected = 2;
    assert_eq!(result, expected);
  }
  #[test]
  fn get_required_fuel_1969() {
    let result = get_required_fuel(1969);
    let expected = 654;
    assert_eq!(result, expected);
  }
  #[test]
  fn get_required_fuel_100756() {
    let result = get_required_fuel(100756);
    let expected = 33583;
    assert_eq!(result, expected);
  }
}