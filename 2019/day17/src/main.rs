extern crate intcode;
use std::collections::HashMap;
fn main() {
    let input: Vec<i64> = std::fs::read_to_string("./input.txt")
        .unwrap()
        .split(",")
        .map(|x| x.parse::<i64>().unwrap())
        .collect();

    
    part1(&input);
    //     println!("Hello, world!");
}

fn part1(input: &Vec<i64>) {
    let (_, input_rx) = std::sync::mpsc::channel::<i64>();
    let (output_tx,output_rx) = std::sync::mpsc::channel::<i64>(); 
    let thread = intcode::run_async(input.clone(), input_rx, output_tx);

    // let mut map = std::collections::HashMap<(i64,i64), i64> = std::collections::HashMap::new();
    let mut map: HashMap<(i64,i64),i64> = HashMap::new(); 
    let mut scaffolds: Vec<(i64,i64)> = vec![];
    let mut x = 0i64;
    let mut y = 0i64;
    loop {
        let response = output_rx.recv();
        if response.is_err() {
            break;
        }
        let output = response.unwrap();
        if is_newline(output) {
            y += 1;
            x = 0;
        } 
        map.insert((x,y), output);
        if is_scaffold(output) {
            scaffolds.push((x,y));
        }

        let output_as_char = std::char::from_u32(output as u32).unwrap();
        print!("{}", output_as_char);
        if !is_newline(output) {
            x += 1;
        }
    }

    let adjacent_map = [(1,0),(0,1),(-1,0),(0,-1)];
    let alignment = scaffolds.iter().fold(0, |acc, (x,y)| {
        let adjacent = adjacent_map.iter().fold(0, |adj_acc, (adj_x, adj_y)| {
            let adj_val = map.entry((x+adj_x, y+adj_y)).or_insert(-1);
            if is_scaffold(*adj_val) {
                return adj_acc + 1;
            } else {
                return adj_acc;
            }
        });
        if adjacent > 3 {
            println!("{:?} intersect with {} adjacent scaffolds", (x,y), adjacent);
            return acc + (x*y);
        }
        return acc;
    });
    println!("alignment: {}", alignment);

    thread.join().unwrap();
}

fn is_newline(i: i64) -> bool {
    return i == 10;
}
fn is_scaffold(i: i64) -> bool {
    return i == 35;
}
fn is_vacuum(i: i64) -> bool {
    return i == 46;
}