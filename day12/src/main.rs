extern crate utils;
extern crate regex;
extern crate num; 

fn main() {
    let content = std::fs::read_to_string("./input.txt").unwrap();
    let reg = regex::Regex::new(r"<x=(-?\d*),? y=(-?\d*),? z=(-?\d*)>").unwrap();

    let mut moons: [([i32;3],[i32;3]);4] = [([0,0,0],[0,0,0]);4];
    let mut i = 0;
    for cap in reg.captures_iter(&content) {
        let x = &cap[1].parse::<i32>().unwrap();
        let y = &cap[2].parse::<i32>().unwrap();
        let z = &cap[3].parse::<i32>().unwrap();
       
        moons[i] = ([*x,*y,*z],[0,0,0]);
        i += 1;
    }

     part1(moons.clone());
    part2(moons.clone());




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
        }
    }
    loop {
        if loops == 1000 {
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

fn part2(moons: [([i32;3],[i32;3]);4]) {
    do_logic(moons);
}

fn do_logic(mut moons: [([i32;3],[i32;3]);4]) {
    let mut loops = 0i64; 
    let mut moons_combinations = vec![];
    let initial = moons.clone();
    let mut periods = [0i64;3];
    for i in 0..moons.len() {
        for j in i..moons.len() {
            if i == j {
                continue;
            }
            moons_combinations.push((i,j));
        }
    }
    loop {
        let mut all_has_values = true;
        for j in 0..3 {
            all_has_values = all_has_values && periods[j] != 0;
            if periods[j] != 0 || loops == 0 {
                continue;
            }
            let mut all_moons_equal_axis = true;
            for i in 0..4 {
                if !(moons[i].0[j] == initial[i].0[j] && moons[i].1[j] == initial[i].1[j])  {
                    all_moons_equal_axis = false;
                }
            }
            if all_moons_equal_axis {
                periods[j] = loops;
            }

        }
        if all_has_values {
            println!("has all steps after {}", loops);
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
    for (i,p) in periods.iter().enumerate() {
        println!("{},{}", i,p);
    }

    let sum: i64 = periods.iter().fold(1i64, |acc, period| num::integer::lcm(acc, *period));
    println!("sum: {}", sum);
}