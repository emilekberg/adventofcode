extern crate intcode;
use std::sync::mpsc::*;
use std::io;
use std::io::prelude::*;
use std::collections::{HashMap, VecDeque};

extern crate termion;
use std::io::Write;

mod constants;
mod utils;
mod point;

use termion::raw::IntoRawMode;
fn main() {
    let content: Vec<i64> = std::fs::read_to_string("./input.txt")
        .unwrap()
        .split(',')
        .map(|x| x.parse::<i64>().unwrap())
        .collect();

    part1(&content);
}


fn part1(input: &Vec<i64>) {
    let (input_tx, input_rx) = channel::<i64>();
    let (output_tx, output_rx) = channel::<i64>();
    intcode::run_async(input.clone(), input_rx, output_tx);

    let mut direction = constants::NORTH;

    let mut map: std::collections::HashMap<point::Point, i64> = std::collections::HashMap::new();
    let mut pos = point::new(0,0);
    let initial_position = pos.clone();

    // let mut stdout = io::stdout().into_raw_mode().unwrap();
    let mut stdout = io::stdout();
    // clear_screen();
    let mut print = move |x: i64,y: i64,c: &str| {
        write!(
            stdout,
            "{}{}",
            termion::cursor::Goto((x + 5) as u16, (y + 5) as u16),
            c,
        ).unwrap();
        stdout.lock().flush().unwrap();
    };
    
    let wait_time = std::time::Duration::from_millis(0);
    let print_map = false;
    let mut steps = 0;
    let mut oxygen_position = point::new(0,0);
    loop {
        if steps > 0 {
            if pos == initial_position {
                utils::clear_screen();
                print(1,6, &format!("DONE!!!! {}", steps));
                print(1,7, &format!("oxygen at {:?}", oxygen_position));
                break;
            }
        }

        // pause();
        print(1,5, &format!("d:{}, pos:{},{}", utils::get_direction_to_string(direction), pos.x, pos.y));
        let move_direction = utils::get_left(direction);
        let mut target_position = utils::get_position_from_direction(pos, move_direction);
   
        input_tx.send(move_direction).unwrap();

 
        let mut response = output_rx.recv().unwrap();
        map.insert(target_position, response);
        std::thread::sleep(wait_time);
        if response == 2 {
            oxygen_position = target_position.clone();
        }
        // hits no wall
        if response > 0 {
            if print_map {
                print(pos.x, pos.y, ".");
            }
            steps += 1;
            direction = move_direction;
            pos = target_position;
            continue;
        }
        
        input_tx.send(direction).unwrap();
        target_position = utils::get_position_from_direction(pos, direction);
        response = output_rx.recv().unwrap();
        map.insert(target_position, response);

        if response == 2 {
            oxygen_position = target_position.clone();
        }

        // hits no wall
        if response > 0 {
            if print_map {
                print(pos.x, pos.y, ".");
            }
            pos = target_position;
            steps += 1;
        } else {
            direction = utils::get_left(move_direction);
        }
        std::thread::sleep(wait_time);
    }

    let res = bfs(&map, pos, oxygen_position);
    println!("result {}", res);

    let res2 = bfs_2(&map, oxygen_position);
    println!("result 2 {}", res2);

}


fn bfs(map: &HashMap<(point::Point), i64>, source: point::Point, destination: point::Point) -> i64 {
    if !map.contains_key(&source) || !map.contains_key(&destination) {
        panic!("incorrect map, source or destination");
    }
    let mut visited = HashMap::new();

    visited.insert(source, true);
    let mut queue = VecDeque::new();

    queue.push_front((source, 0));

    let rows = [-1,0,0,1];
    let cols = [0,-1,1,0];

    while !queue.is_empty() {
        let (pos, steps) = queue.front().unwrap();

        if pos.x == destination.x && pos.y == destination.y {
            return *steps;
        }
        
        let (pos, steps) = queue.pop_front().unwrap();

        for i in 0..4 {
            let row = pos.x + rows[i];
            let col = pos.y + cols[i];
            let test_pos = point::new(row, col);
            if map.contains_key(&test_pos) && !visited.contains_key(&test_pos) && *map.get(&test_pos).unwrap() != 0i64 {
                visited.insert(test_pos, true);
                let node = (test_pos.clone(), steps +1 );
                queue.push_back(node);
            }
        }
    }
    return -1;
}
fn bfs_2(map: &HashMap<(point::Point), i64>, source: point::Point) -> i64 {
    if !map.contains_key(&source) {
        panic!("incorrect map, source or destination");
    }
    let mut visited = HashMap::new();

    visited.insert(source, true);
    let mut queue = VecDeque::new();

    queue.push_front((source, 0));

    let rows = [-1,0,0,1];
    let cols = [0,-1,1,0];
    let mut num_steps = vec![];
    num_steps.push(0i64);
    while !queue.is_empty() {
        let (pos, steps) = queue.pop_front().unwrap();

        for i in 0..4 {
            let row = pos.x + rows[i];
            let col = pos.y + cols[i];
            let test_pos = point::new(row, col);
            if map.contains_key(&test_pos) && !visited.contains_key(&test_pos) && *map.get(&test_pos).unwrap() != 0i64 {
                visited.insert(test_pos, true);
                let node = (test_pos.clone(), steps +1 );
                num_steps.push(steps+1);
                queue.push_back(node);
            }
        }
    }
    let max_steps = num_steps.iter().max().unwrap();
    println!("{:?}", num_steps);
    return *max_steps;
}
fn part2() {

}



#[cfg(test)]
mod tests {
    use super::*;
    #[test]
    fn test_get_direction() {
        let mut result: Vec<i64> = vec![];
        let mut next = constants::NORTH;
        for _ in 0..5{
            result.push(next);
            next = utils::get_left(next);
        }
        assert_eq!(result, [constants::NORTH, constants::WEST, constants::SOUTH, constants::EAST, constants::NORTH]);
    }
    #[test]
    fn test_get_target_direction_north() {
        let result = utils::get_position_from_direction(point::new(0,0), constants::NORTH);
        assert_eq!(result, point::new(0,-1));
    }

    #[test]
    fn test_get_target_direction_south() {
        let result = utils::get_position_from_direction(point::new(0,0), constants::SOUTH);
        assert_eq!(result, point::new(0,1));
    }
    #[test]
    fn test_get_target_direction_west() {
        let result = utils::get_position_from_direction(point::new(0,0), constants::WEST);
        assert_eq!(result, point::new(-1,0));
    }
    #[test]
    fn test_get_target_direction_east() {
        let result = utils::get_position_from_direction(point::new(0,0), constants::EAST);
        assert_eq!(result, point::new(1,0));
    }
}