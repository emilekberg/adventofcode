use std::collections::HashMap;
use super::body;
pub struct OrbitMap {
  map: HashMap<String, body::Body>
}
pub fn new() -> OrbitMap {
  return OrbitMap {
    map: HashMap::new(),
  };
}
impl OrbitMap {
  pub fn setup(&mut self, bodies: Vec<&str> ) {
    for row in bodies {
      let result: Vec<&str> = row.split(")").collect();
      let parent_name = result[0].to_string();
      let child_name = result[1].to_string();
      
      let parent = self.map.entry(parent_name.clone()).or_insert(body::new(parent_name.clone()));
      parent.children.push(child_name.clone());
    
      let child = self.map.entry(child_name.clone()).or_insert(body::new(child_name.clone()));
      child.parent = parent_name.clone();
    }
  }

  pub fn get_orbits_for_body(&self, body_name: &String) -> i32 {
    let mut body = self.map.get(body_name).unwrap();
    let mut orbits = 0;
    loop {
      if body.parent.is_empty()  {
        return orbits;
      }
      body = self.map.get(&body.parent).unwrap();
      orbits += 1;
    }
  }

  pub fn get_total_num_of_orbits(self) -> i32 {
    let mut sum = 0;
    for (name, _) in &self.map {
      sum += OrbitMap::get_orbits_for_body(&self, name);
    }
    return sum as i32;
  }
  
  // gets the route to COM, returns a list of tuples with bodies and their respective distance from "name" body.
  fn get_route_to_com(&self, body: &body::Body) -> Vec<String> {
    let mut route_to_com: Vec<String> =  Vec::new();
    let parent_body = self.map.get(&body.parent).unwrap();
    let mut body: &body::Body = parent_body;
    loop {
      if body.parent.is_empty()  {
        break;
      }
      route_to_com.push(body.name.clone());
      body = self.map.get(&body.parent).unwrap();
    }
    return route_to_com;
  }

  pub fn get_num_transfers_between(self, origin: String, target: String) -> i32 {
    let you_body = self.map.get(&origin).unwrap();
    let santa_body = self.map.get(&target).unwrap();

    let you_steps = Self::get_route_to_com(&self, &you_body);
    let santa_steps = Self::get_route_to_com(&self, &santa_body);
    // search for first shared parent, that is the closest route.
    for (i, body) in you_steps.iter().enumerate() {
      for (j, body2) in santa_steps.iter().enumerate() {
        if body != body2 {
          continue;
        }
        return (i + j) as i32;
      }
    }
    return 0;
  }
}

#[cfg(test)]
mod tests {
  use super::*;
  #[test]
  fn returns_correct_orbits() {
    let data = String::from("COM)B
B)C
C)D
D)E
E)F
B)G
G)H
D)I
E)J
J)K
K)L");
    let mut map = new();
    let rows: Vec<&str> = data.lines().collect();
    map.setup(rows);
    let result = map.get_total_num_of_orbits();
    assert_eq!(result, 42);
  }

  #[test]
  fn returns_correct_transfers_between_targets() {
    let data = String::from("COM)B
B)C
C)D
D)E
E)F
B)G
G)H
D)I
E)J
J)K
K)L
K)YOU
I)SAN");
    let mut map = new();
    let rows: Vec<&str> = data.lines().collect();
    map.setup(rows);
    let result = map.get_num_transfers_between(String::from("YOU"), String::from("SAN"));
    assert_eq!(result, 4);
  }
}