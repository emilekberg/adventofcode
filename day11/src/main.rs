extern crate intcode;
use std::collections::HashMap;
#[derive(Hash,Debug,Eq,PartialEq,Clone,Copy)]
struct Point {
    x: i64,
    y: i64,
}

impl Point {
    fn new(x: i64, y: i64) -> Self {
        Point {
            x,y
        }
    }
}

use std::io;
use std::io::prelude::*;

fn pause() {
    let mut stdin = io::stdin();
    let mut stdout = io::stdout();

    // We want the cursor to stay at the end of the line, so we print without a newline and flush manually.
    write!(stdout, "Press any key to continue...").unwrap();
    stdout.flush().unwrap();

    // Read a single byte and discard
    let _ = stdin.read(&mut [0u8]).unwrap();
}

fn main() {
    let data: Vec<i64> = std::fs::read_to_string("./input.txt")
        .unwrap()
        .split(",")
        .map(|x| x.parse::<i64>().unwrap())
        .collect();

    part1(&data, 1);
    // part1(&data, 1);
}

fn part1(data: &Vec<i64>, start_color: i64) {
    let mut colors: HashMap<Point, i64> = HashMap::new();
    let mut lookat = Point::new(0,1);
    let mut position = Point::new(0,0);
    let mut first = true;
    // sends from output to input callback, makes sure i can process it before returning input.
    let (tx, rx) = std::sync::mpsc::channel::<i64>();
    // sends the calculate result from the input data to an external place.
    let (tx_color, rx_color) = std::sync::mpsc::channel::<(i64,i64,i64)>();

    intcode::run_program(data.clone(), move || {
        if first {
            first = false;
            return start_color;
        }
        let color_to_paint = rx.recv().unwrap_or_default();
        let direction_to_turn = rx.recv().unwrap_or_default();
        // println!("input: {},{}", color_to_paint, direction_to_turn);
        colors.insert(position.clone(), color_to_paint);
        tx_color.send((position.x, position.y, color_to_paint)).unwrap();
        lookat = get_lookat(lookat, direction_to_turn);
        position.x += lookat.x;
        position.y -= lookat.y; // flip fgrom y positive being up to y positive being down
        let on_color = colors.entry(position).or_insert(0).clone();
        // println!("pos: {:?}, look: {:?}, color_to_paint: {}, on_color: {}, len: {}", position, lookat, color_to_paint, on_color, colors.len());
        return on_color;
        // return.
        // 0 = standing on black square
        // 1 = standing on white square
    }, move |out| {
        // println!("output: {}", out);
        // 1. color to paint.
        // 2. direction to turn
        tx.send(out).unwrap_or_default();
    });
    let mut colors: HashMap<Point, i64> = HashMap::new();
    let mut min_x = 0;
    let mut min_y = 0;
    let mut max_x = 0;
    let mut max_y = 0;
    loop {
        
        let res = rx_color.recv_timeout(std::time::Duration::from_millis(500)).ok();
        match res {
            Some((x,y,c)) => {
                if x < min_x {
                    min_x = x;
                }
                if x > max_x {
                    max_x = x;
                }
                if y < min_y {
                    min_y = y;
                }
                if y > max_y {
                    max_y = y;
                }
                colors.insert(Point::new(x,y), c);
            },
            None => {
                println!("{}", colors.len());
                break;
            }
        }
    }
    
    for y in min_y-2..max_y+2 {

        for x in min_x-2..max_x+2 {
            let color = colors.entry(Point::new(x,y)).or_insert(0);
            let sign = match color {
                0 => " ",
                1 => "#",
                _ => panic!(""),
            };
            print!("{}", sign);
        }
        print!("\n");
    }
    println!("x: {}-{}, y: {}-{}", min_x, max_x, min_y, max_y);

}

fn get_lookat(lookat: Point, rotation: i64) -> Point {
    let rotation_radian = match rotation {
        0 => std::f64::consts::FRAC_PI_2, // HALF PI
        1 => (3./2.) * std::f64::consts::PI,
        _ => panic!(""),
    };

    let sin: f64 = rotation_radian.sin();
    let cos: f64 = rotation_radian.cos();

    let tx = lookat.x as f64;
    let ty = lookat.y as f64;

    let rx = (cos * tx) - (sin * ty);
    let ry = (sin * tx) + (cos * ty);

    return Point::new(rx as i64, ry as i64);
}

#[cfg(test)]
mod tests {
    use super::*;
    #[test]
    fn get_lookat_test_up_left() {
        let lookat = Point::new(0,1);
        let result = get_lookat(lookat, 0);
        assert_eq!(result, Point::new(-1,0));
    }
    #[test]
    fn get_lookat_test_up_right() {
        let lookat = Point::new(0,1);
        let result = get_lookat(lookat, 1);
        assert_eq!(result, Point::new(1,0));
    }
    #[test]
    fn get_lookat_test_down_left() {
        let lookat = Point::new(0,-1);
        let result = get_lookat(lookat, 0);
        assert_eq!(result, Point::new(1,0));
    }
    #[test]
    fn get_lookat_test_down_right() {
        let lookat = Point::new(0,-1);
        let result = get_lookat(lookat, 1);
        assert_eq!(result, Point::new(-1,0));
    }
    #[test]
    fn get_lookat_test_left_up() {
        let lookat = Point::new(-1, 0);
        let result = get_lookat(lookat, 0);
        assert_eq!(result, Point::new(0,-1));
    }
    #[test]
    fn get_lookat_test_left_down() {
        let lookat = Point::new(-1,0);
        let result = get_lookat(lookat, 1);
        assert_eq!(result, Point::new(0,1));
    }
    #[test]
    fn get_lookat_test_right_up() {
        let lookat = Point::new(1, 0);
        let result = get_lookat(lookat, 0);
        assert_eq!(result, Point::new(0,1));
    }
    #[test]
    fn get_lookat_test_right_down() {
        let lookat = Point::new(1,0);
        let result = get_lookat(lookat, 1);
        assert_eq!(result, Point::new(0,-1));
    }
}