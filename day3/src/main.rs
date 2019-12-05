use std::fs;
use std::fmt;
use std::collections::HashMap;
#[derive(PartialEq)]
pub enum Direction {
  Up,
  Down,
  Left,
  Right,
  Unknown,
}

#[derive(Hash, Eq, PartialEq, Debug)]
pub struct Point {
  x: i32,
  y: i32,
}


impl fmt::Debug for Direction {
  fn fmt(&self, f: &mut fmt::Formatter) -> fmt::Result {
      match *self {
          Direction::Up => write!(f, "Up"),
          Direction::Down => write!(f, "Down"),
          Direction::Left => write!(f, "Left"),
          Direction::Right => write!(f, "Right"),
          Direction::Unknown => write!(f, "Unknown"),
      }
  }
}

pub fn main() {
  let content = fs::read_to_string("./input.txt")
    .expect("something went wrong when reading fle");
  let [len,steps] = calculate_manhattan_length(content, false);
  println!("len: {}, steps: {}", len, steps);
}

pub fn calculate_manhattan_length(s: String, use_steps: bool) -> [i32;2] {
  let mut map = HashMap::new();
  let mut map2 = HashMap::new();
  println!("{}", s);
  let [a,b] = get_vectors(s);

  process_list(&mut map, a);
  process_list(&mut map2, b);

  let origo = Point{x: 0, y: 0};
  let mut prev_length = 100000000;
  let mut _lowest_steps = 1000000000;
  for (point, steps_a) in map {
    if map2.contains_key(&point) {
      let steps_b = map2.get(&point).unwrap();
      let len = get_manhattan_distance(&origo, &point);
      let steps = steps_a + steps_b;

      if steps < _lowest_steps {
        _lowest_steps = steps;
        println!("{}", steps);
      }

      if len < prev_length {
        prev_length = len;
        println!("{}", len);
      }

    }
  }
  return [prev_length, _lowest_steps];
}

pub fn process_list(map: &mut HashMap<Point, i32>, v: Vec<String>) {
  let mut x = 0;
  let mut y = 0;
  let mut steps = 0;
  for input in v {
    let dir = get_direction(&input);
    let len = get_length(&input);
    for _i in 1..len+1 {
      match dir {
        Direction::Up => y += 1,
        Direction::Down => y -= 1,
        Direction::Left => x -= 1,
        Direction::Right => x += 1,
        _ => panic!(),
      }
      steps += 1;
      let p = Point{x,y,};
      *map.entry(p).or_insert(0) = steps;
    }
  }
}

pub fn get_manhattan_distance(p1: &Point, p2: &Point) -> i32 {
  return (p1.x-p2.x).abs() + (p1.y-p2.y).abs();
}

pub fn get_vectors(s: String) -> [Vec<String>; 2] {
  let mut inputs = s.lines();
  let a = inputs.next().unwrap();
  let b = inputs.next().unwrap();

  let c = line_to_vec(a);
  let d = line_to_vec(b);

  return [
    c,
    d,
  ];
}

pub fn line_to_vec(s: &str) -> Vec<String> {
  return s.split(",").map(|s| s.to_string()).collect();
}

pub fn get_direction(s: &String) -> Direction {
  let (first, _) = s.split_at(1);
  match first {
    "U" => Direction::Up,
    "D" => Direction::Down,
    "L" => Direction::Left,
    "R" => Direction::Right,
    _ => Direction::Unknown,
  }
}

pub fn get_length(s: &String) -> i32 {
  let (_, second) = s.split_at(1);
  return second.parse::<i32>().unwrap();
}
#[cfg(test)]
mod tests {
  use super::*;
  #[test]
  fn test_get_length() {
    let len = get_length(&String::from("R123"));
    assert_eq!(len, 123);
  }

  #[test]
  fn test_get_direction() {
    let dir = get_direction(&String::from("U1423"));
    assert_ne!(dir, Direction::Unknown);
    assert_eq!(dir, Direction::Up);
  }

  #[test]
  fn test_simple() {
    let input = "R8,U5,L5,D3
U7,R6,D4,L4";
    let [len,_] = calculate_manhattan_length(String::from(input), false);
    assert_eq!(len, 6);
  }


  #[test]
  fn test_first() {
    let input = "R75,D30,R83,U83,L12,D49,R71,U7,L72
U62,R66,U55,R34,D71,R55,D58,R83";
    let [len,_] = calculate_manhattan_length(String::from(input), false);
    assert_eq!(len, 159);
  }

  #[test]
  fn test_second() {
    let input = "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51
U98,R91,D20,R16,D67,R40,U7,R15,U6,R7";
    let [len,_] = calculate_manhattan_length(String::from(input), false);
    assert_eq!(len, 135);
  }

  #[test]
  fn test_simple_steps() {
    let input = "R8,U5,L5,D3
U7,R6,D4,L4";
    let [_,steps] = calculate_manhattan_length(String::from(input), true);
    assert_eq!(steps, 30);
  }
  #[test]
  fn test_first_steps() {
    let input = "R75,D30,R83,U83,L12,D49,R71,U7,L72
U62,R66,U55,R34,D71,R55,D58,R83";
    let [_,steps] = calculate_manhattan_length(String::from(input), true);
    assert_eq!(steps, 610);
  }

  #[test]
  fn test_second_steps() {
    let input = "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51
U98,R91,D20,R16,D67,R40,U7,R15,U6,R7";
    let [_,steps] = calculate_manhattan_length(String::from(input), true);
    assert_eq!(steps, 410);
  }
}