use std::collections::HashMap;
mod nano_factory;
extern crate regex;
use regex::Regex;
fn main() {
  let content: String = std::fs::read_to_string("./input.txt")
    .unwrap();

  let map = get_map_from_string(content);
  part1(&map);
  part2(&map);
  
}

fn part1(map: &HashMap<(i64, String), Vec<(i64, String)>>) -> i64 {
  let mut factory = nano_factory::new(map.clone());
  factory.produce(String::from("FUEL"), 1);
  println!("Part1: {}", factory.required_ore);
  println!("{}",  1000000000000f64 / factory.required_ore as f64);
  return factory.required_ore;
}

fn part2(map: &HashMap<(i64, String), Vec<(i64, String)>>) -> i64 {
  println!("part 2");
  let max = 1000000000000i64;
  let mut factory = nano_factory::new(map.clone());
  let mut high = max; 
  let mut low = 1;
  let mut i = 1;
  while low <= high {
    i = (low + high) / 2;
    factory.produce(String::from("FUEL"), i);
    if factory.required_ore > max {
      high = i-1;
    }
    else if factory.required_ore < max {
      low = i+1;
    }
    factory.clear();
  }


  println!("Done {}", i);
  return i;
}

fn get_map_from_string(s: String) -> HashMap<(i64, String), Vec<(i64, String)>> {
  let content: Vec<(Vec<String>)> = s
    .lines()
    .map(|x| x.to_string())
    .map(|x| x
      .split(" => ")
      .map(|y| y.to_string()).collect()
    )
    .collect();
  
  let mut map: HashMap<(i64, String), Vec<(i64, String)>> = HashMap::new();

  for to_parse in content.iter() {
    // ["10 a"],["20 b"] => map.insert("20b", "10a");
    
    // key
    let key = string_to_material_info(to_parse[1].clone()); 
    // value
    let mut value: Vec<(i64, String)> = vec![];
    
    to_parse[0].trim().split(",").for_each(|x| {
      let val = string_to_material_info(String::from(x));
      value.push(val);
    });

    map.insert(key, value);
  }
  return map;
}

fn string_to_material_info(s: String) -> (i64, String) {
  let key_re = Regex::new(r"(\d+) (\w+)").unwrap();
  let mut info: (i64, String) = (0, String::default());
  for cap in key_re.captures_iter(&s) {
    let amount: i64 = cap[1].parse().unwrap();
    let mat: String = String::from(&cap[2]);
    info = (amount, mat);
  }
  return info;
}

#[cfg(test)]
mod tests {
  use super::*;

  #[test]
  fn test_first() {
    let map = get_map_from_string(std::fs::read_to_string("./test_01.txt").unwrap());
    let res = part1(&map);
    assert_eq!(res, 31);
  }
  #[test]
  fn test_second() {
    let map = get_map_from_string(std::fs::read_to_string("./test_02.txt").unwrap());
    let res = part1(&map);
    assert_eq!(res, 165);
  }

  #[test]
  fn test_third() {
    let map = get_map_from_string(std::fs::read_to_string("./test_03.txt").unwrap());
    assert_eq!(part1(&map), 13312);
    assert_eq!(part2(&map), 82892753);
  }  
  #[test]
  fn test_fourth() {
    let map = get_map_from_string(std::fs::read_to_string("./test_04.txt").unwrap());
    assert_eq!(part1(&map), 180697);
    assert_eq!(part2(&map), 5586022);
  }
  #[test]
  fn test_fifth() {
    let map = get_map_from_string(std::fs::read_to_string("./test_05.txt").unwrap());
    assert_eq!(part1(&map), 2210736 );
    assert_eq!(part2(&map), 460664 );
  }  
}
