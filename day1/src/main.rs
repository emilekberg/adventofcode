use std::fs;
use std::cmp;

fn main() {
  let filename = "./numbers.txt";
  println!("reading: {}", filename);
  let content = fs::read_to_string(filename)
    .expect("something went wrong when reading file: {}");

  // println!("with text: {}", content);


  let arr = content.lines();
  let mut sum: f32 = 0.;
  for value in arr {
    let mass: f32 = value.parse().unwrap();
    sum += get_required_fuel_recursive(mass);
  }

  println!("result: {}", sum);
}

fn get_required_fuel(mass: f32) -> f32 {
  return (mass / 3.).floor() - 2.;
}

fn get_required_fuel_recursive(mass: f32) -> f32 {
  let mut sum: f32 = 0.;
  let mut mass_to_calc = mass;
  loop {
    let fuel = get_required_fuel(mass_to_calc);
    mass_to_calc = fuel;
    println!("fuel {}, sum {}", fuel, sum);
    sum += fuel;
    if fuel <= 0. {
      sum -= fuel;
      return sum;
    }
  }

}