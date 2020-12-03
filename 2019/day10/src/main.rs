mod vector2;

use vector2::Vector2;

fn load_map(path: &str) -> Vec<Vector2> {
    let data: Vec<char> = std::fs::read_to_string(path)
        .unwrap()
        .chars()
        .collect();
    let mut x: i32 = 0;
    let mut y: i32 = 0;
    let mut asteroids = vec![];
    for c in &data {
        print!("{}", c);
    }
    println!("");
    for c in data {
        match c {
            '\n' => {
                x = 0;
                y += 1;
                continue;
            },
            '#' => {
                let v = vector2::new(x as f32, y as f32);
                asteroids.push(v);
            },
            _ => {},
        }
        x += 1;
    }
    return asteroids;
}

fn main() {
    let map = load_map("./input.txt");
    let (v, see) = part1(map.clone());
    println!("part1: {:?} sees {}", v, see);

    let asteroid = part2(&v, map.clone());

    println!("part2: {}", asteroid[199].x * 100. + asteroid[199].y);
}

fn part1(asteroids: Vec<Vector2>) -> (Vector2, i32) {
    let mut can_see_map: Vec<Vec<usize>> = vec![];
    for (i, a) in asteroids.iter().enumerate() {
        let mut tmp: Vec<f32> = vec![];
        can_see_map.push(vec![]);
        for (j, b) in asteroids.iter().enumerate() {
            if i == j {
                continue;
            }
            let angle = Vector2::angle(a,b);
            if !tmp.contains(&angle) {
                tmp.push(angle);
                can_see_map[i].push(j);
            }
        }
    }
    let mut max_visible = 0;
    let mut max_index = 0;
    for (i,see) in can_see_map.iter().enumerate() {
        if see.len() > max_visible {
            max_index = i;
            max_visible = see.len();
        }
    }
    let best = asteroids[max_index].clone(); 
    println!("{:?} can see {}", best, max_visible);
    return (best, max_visible as i32);
}

fn part2(base_position: &Vector2, asteroids: Vec<Vector2>) -> Vec<Vector2> {
    let mut angles_asteroids: Vec<(f32, Vec<Vector2>)> = vec![];
    for a in asteroids.iter() {
        let angle = Vector2::angle(base_position,a);
        if !angles_asteroids.iter().any(|(x,_)| x == &angle) {
            angles_asteroids.push((angle, vec![]));
        }
        let index = angles_asteroids.iter().position(|(x,_)| x == &angle).unwrap();
        &angles_asteroids[index].1.push(a.clone());
    }

    angles_asteroids.sort_by(|(a,_),(b, _)| a.partial_cmp(b).unwrap());

    // makes 0 be up instead of right.
    let offset_by_half_pi = -0.5 * std::f32::consts::PI;
    let mut i = angles_asteroids.iter().position(|(x,_)| *x == offset_by_half_pi).unwrap();
    
    let mut asteroids_removed = vec![];
    let mut angles_cleared = 0;
    loop {
        if i >= angles_asteroids.len() {
            i = 0;
        }
        if angles_cleared == angles_asteroids.len() {
            println!("all asteroids destroyed");
            break;
        }

        let res = angles_asteroids[i].1.pop();
        match res {
            Some(x) => {
                println!("{} removed {:?} at angle {}", asteroids_removed.len(), x, angles_asteroids[i].0);
                asteroids_removed.push(x);
            },
            None => angles_cleared += 1,
        }
        i += 1;
    }
    return asteroids_removed.clone();
}

#[cfg(test)]
mod tests {
    use super::*;
    #[test]
    fn test_0() {
        let map = load_map("./test_0.txt");
        let (pos,sees) = part1(map);
        assert_eq!(pos.x, 3.);
        assert_eq!(pos.y, 4.);
        assert_eq!(sees, 8);
    }
    #[test]
    fn test_1() {
        let map = load_map("./test_1.txt");
        let (pos,sees) = part1(map);
        assert_eq!(pos.x, 5.);
        assert_eq!(pos.y, 8.);
        assert_eq!(sees, 33);
    }
    #[test]
    fn test_2() {
        let map = load_map("./test_2.txt");
        let (pos,sees) = part1(map);
        assert_eq!(pos.x, 1.);
        assert_eq!(pos.y, 2.);
        assert_eq!(sees, 35);
    }
    #[test]
    fn test_3() {
        let map = load_map("./test_3.txt");
        let (pos,sees) = part1(map);
        assert_eq!(pos.x, 6.);
        assert_eq!(pos.y, 3.);
        assert_eq!(sees, 41);
    }
    #[test]
    fn test_4() {
        let map = load_map("./test_4.txt");
        let (pos,sees) = part1(map);
        assert_eq!(pos.x, 11.);
        assert_eq!(pos.y, 13.);
        assert_eq!(sees, 210);
    }

    #[test]
    fn test_vaporize() {
        let map = load_map("./test_4.txt");
        let best = vector2::new(11., 13.);
        let asteroids = part2(&best, map);

        assert_eq!(asteroids[199].x, 8.);
        assert_eq!(asteroids[199].y, 2.);
    }

    // #[test]
    fn test_render() {
        let data: Vec<char> = std::fs::read_to_string("./test_5.txt")
            .unwrap()
            .chars()
            .collect();
        let mut x: i32 = 0;
        let mut y: i32 = 0;        
        let best = vector2::new(8., 3.);
        let asteroids = part2(&best, load_map("./test_5.txt"));
        let temp: Vec<&Vector2> = asteroids.iter().take(5).collect();
        for c in &data {
            let mut print = *c;
            if *c == '\n' {
                y += 1;
                x = 0;
            }
            for (a,b) in temp.iter().enumerate() {
                if b.x == x as f32 && b.y == y as f32 {
                    let cha = std::char::from_digit(a as u32, 10).unwrap();
                    print = cha;
                }
            }
            if x as f32 == best.x && y as f32 == best.y {
                print = 'x';
            }
            print!("{}", print);
        }
        assert_eq!(false, true);
    }
}