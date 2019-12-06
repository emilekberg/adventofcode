use std::fs;
mod body;
mod orbit_map;

fn main() {
  let content = fs::read_to_string("./input.txt")
    .expect("error while loading file.");
  // get_num_orbits(content);  
  let mut map = orbit_map::new();
  let rows: Vec<&str> = content.lines().collect();
  map.setup(rows);
  // part1(map);
  part2(map);
}

fn part1(map: orbit_map::OrbitMap) {
  let result = map.get_total_num_of_orbits();
  println!("total orbits: {}", result);
}

fn part2(map: orbit_map::OrbitMap) {
  let result = map.get_num_transfers_between(String::from("YOU"), String::from("SAN"));
  println!("moves between YOU and SAN: {}", result);
}