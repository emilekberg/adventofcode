use std::collections::HashMap;
#[derive(Debug)]
pub struct Material {
  pub requires: Vec<(i64, String)>,
  pub produces: i64,
}
fn new_material(requires: Vec<(i64, String)>, produces: i64) -> Material {
  return Material {
    requires,
    produces,
  };
}

#[derive(Debug)]
pub struct NanoFactory {
  pub available_materials: HashMap<String, i64>,
  pub reactions: HashMap<String, (Material)>,
  pub required_ore: i64,
}
pub fn new(reactions_dictionary: HashMap<(i64, String), Vec<(i64, String)>>) -> NanoFactory {
  let mut factory = NanoFactory {
    reactions: HashMap::new(),
    available_materials: HashMap::new(),
    required_ore: 0,
  };
  for (key, value) in reactions_dictionary.iter() {
    let requires = value.iter().cloned().map(|(x,y)| (x, String::from(y))).collect();
    let produces = (key.0, key.1.clone());
    let mat = new_material(requires, produces.0);
    factory.reactions.insert(produces.1, mat);
  }
  factory.reactions.insert(String::from("ORE"), new_material(vec![], 1));
  // make materials
  return factory;
}

impl NanoFactory {
  fn require_ore(&mut self, amount: i64) {
    self.required_ore += amount;
    let ore = String::from("ORE");
    self.available_materials.entry(ore).or_insert(amount);
  }
  fn get_available_materials(&mut self, material: String) -> i64 {
    let entry = self.available_materials.entry(material).or_insert(0);
    if *entry > 0 {
      let ret = entry.clone();
      *entry = 0;
      return ret;
    }
    return 0;
  }
  pub fn produce(&mut self, material: String, amount: i64) {
    let to_produce = amount - self.get_available_materials(material.clone());
    if material == "ORE" {
      self.require_ore(to_produce);
      return;
    }
    let multiplier = (to_produce as f64 / self.reactions[&material].produces as f64).ceil() as i64;
    for i in 0..self.reactions[&material].requires.len() {
      let req = self.reactions[&material].requires[i].clone();
      self.produce(req.1, req.0 * multiplier);
    }
    let leftovers = to_produce - self.reactions[&material].produces * multiplier;
    if leftovers < 0 {
      *self.available_materials.entry(material).or_insert(0) += 0-leftovers;
    }
  }
  pub fn clear(&mut self) {
    self.required_ore =0;
    self.available_materials.clear();
  }
}