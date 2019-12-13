extern crate utils;
extern crate regex;

mod moon;
use std::collections::HashSet;

fn main() {
    let content = std::fs::read_to_string("./input.txt").unwrap();
    // let reg = regex::Regex::new(r"\d+x=(-?\d+), y=(-?\d+), z=(-?\d+)").unwrap();
    let reg = regex::Regex::new(r"<x=(-?\d*),? y=(-?\d*),? z=(-?\d*)>").unwrap();

    let mut moons: [([i32;3],[i32;3]);4] = [([0,0,0],[0,0,0]);4];
    let mut i = 0;
    for cap in reg.captures_iter(&content) {
        //println!("{}", cap.len());
        let x = &cap[1].parse::<i32>().unwrap();
        let y = &cap[2].parse::<i32>().unwrap();
        let z = &cap[3].parse::<i32>().unwrap();
       
        moons[i] = ([*x,*y,*z],[0,0,0]);
        i += 1;
    }

    part1(moons.clone());
    // part2(moons.clone());




}

fn part1(mut moons: [([i32;3],[i32;3]);4]) {
    let mut loops = 0; 
    let mut moons_combinations = vec![];
    for i in 0..moons.len() {
        for j in i..moons.len() {
            if i == j {
                continue;
            }
            moons_combinations.push((i,j));
            println!("{},{}", i,j);
        }
    }
    loop {
        if loops == 1000 {
            println!("after {} loops:", loops);
            break;
        }
        for (i,j) in &moons_combinations {
            for v in 0..3 {
                if moons[*i].0[v] < moons[*j].0[v] {
                    moons[*i].1[v] += 1;
                    moons[*j].1[v] -= 1;
                } else if moons[*i].0[v] > moons[*j].0[v] {
                    moons[*i].1[v] -= 1;
                    moons[*j].1[v] += 1
                }
            }
        }
        for moon in moons.iter_mut() {
            moon.0[0] += moon.1[0];
            moon.0[1] += moon.1[1];
            moon.0[2] += moon.1[2]
        }
        loops += 1;
    }

    let mut sum = 0;
    for moon in moons.iter() {
        let pot = moon.0[0].abs() + moon.0[1].abs() + moon.0[2].abs();
        let kin = moon.1[0].abs() + moon.1[1].abs() + moon.1[2].abs();
        let total = pot * kin;
        sum += total;
    }
    println!("total: {}", sum);
}
//  233000000 was max i reached
fn part2(moons: [([i32;3],[i32;3]);4]) {
    do_logic(moons);
}

fn do_logic(mut moons: [([i32;3],[i32;3]);4]) {
    let mut loops = 0i64; 
    let init_state = moons.clone();
    let mut moons_combinations = vec![];
    for i in 0..moons.len() {
        for j in i..moons.len() {
            if i == j {
                continue;
            }
            moons_combinations.push((i,j));
            println!("{},{}", i,j);
        }
    }

    // moon 0 loop is 462 

    loop {

        // println!("after {} steps", loops);
        for (i, moon) in moons.iter().enumerate() {
            if moon.1 == [0;3] {
                println!("moon {} velocity is 0 in step {}", i, loops);
                break;
            }
            // print_moon(moon);
        }

        for (i,j) in &moons_combinations {
            for v in 0..3 {
                if moons[*i].0[v] < moons[*j].0[v] {
                    moons[*i].1[v] += 1;
                    moons[*j].1[v] -= 1;
                } else if moons[*i].0[v] > moons[*j].0[v] {
                    moons[*i].1[v] -= 1;
                    moons[*j].1[v] += 1
                }
            }
        }
        for moon in moons.iter_mut() {
            moon.0[0] += moon.1[0];
            moon.0[1] += moon.1[1];
            moon.0[2] += moon.1[2]
        }
        loops += 1;
        if moons == init_state {
            println!("{}", loops);
            break;
        }
    }
}

fn print_moon(moon: &([i32;3],[i32;3])) {
    println!("pos: {},{},{} vel {},{},{}", 
        moon.0[0], moon.0[1], moon.0[2],
        moon.1[0], moon.1[1], moon.1[2]
    );
}