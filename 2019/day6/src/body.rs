#[derive(Debug,Hash)]
pub struct Body {
  pub name: String,
  pub parent: String,
  pub children: Vec<String>,
}
pub fn new(name: String) -> Body {
  return Body {
    name,
    parent: String::from(""),
    children: Vec::new(),
  };
}