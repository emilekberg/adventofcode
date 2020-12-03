use super::*;
pub fn get_direction_to_string(direction: i64) -> &'static str {
  return match direction {
      constants::NORTH => "N",
      constants::SOUTH => "S",
      constants::WEST => "W",
      constants::EAST => "E",
      _ => panic!("unexpected direction"),
  };
}

pub fn get_left(direction: i64) -> i64 {
  return match direction {
      constants::NORTH => constants::WEST,
      constants::SOUTH => constants::EAST,
      constants::WEST => constants::SOUTH,
      constants::EAST => constants::NORTH,
      _ => panic!("unexpected direction"),
  };
}
pub fn get_position_from_direction(pos: point::Point, direction: i64) -> point::Point {
  return match direction {
      constants::NORTH => point::new(pos.x, pos.y-1),
      constants::SOUTH => point::new(pos.x, pos.y+1),
      constants::WEST => point::new(pos.x-1, pos.y),
      constants::EAST => point::new(pos.x+1, pos.y),
      _ => panic!("unexpected direction"),
  }
}
pub fn clear_screen() {
  print!("{}[2J", 27 as char);
}

pub fn pause() {
  let mut stdin = std::io::stdin();
  let mut stdout = std::io::stdout();

  // Read a single byte and discard
  let _ = stdin.read(&mut [0u8]).unwrap();
}

pub fn get_render_symbol(response: i64) -> &'static str {
  match response {
      0 => "#",
      1 => ".",
      2 => "G",
      _ => panic!("unexpected symbol")
  }
}

