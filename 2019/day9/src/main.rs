extern crate intcode;

fn main() {
    let memory: Vec<i64> = std::fs::read_to_string("./input.txt")
        .unwrap()
        .split(",")
        .map(|x| x.parse().unwrap())
        .collect();

    part1(&memory);
    part2(&memory);
}
fn part1(memory: &Vec<i64>) {
    let (_,output,_) = intcode::run_program(memory.clone(), || 1, |_| {});
    let boost_keycode = *output.last().unwrap();
    println!("part1: {}", boost_keycode);
}

fn part2(memory: &Vec<i64>) {
    let now = std::time::Instant::now();
    let (_,output,_) = intcode::run_program(memory.clone(), || 2, |_| {});
    let boost_keycode = *output.last().unwrap();
    println!("part2: {}, {}", boost_keycode, now.elapsed().as_secs_f64());
}
#[cfg(test)]
mod tests {
    use super::*;
    #[test]
    fn test_copy() {
        let input = vec![109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99];
        let (_,output,_) = intcode::run_program(input.clone(), || 1, |_| {});
        assert_eq!(output, input);
    }

    #[test]
    fn test_16_number_digit() {
        let input = vec![1102,34915192,34915192,7,4,7,99,0];
        let (_,output,_) = intcode::run_program(input.clone(), || 1, |_| {});
        let result = output.last().unwrap();
        assert_eq!(result.to_string().len(), 16);
    }

    #[test]
    fn test_output_large_number() {
        let input = vec![104,1125899906842624,99];
        let (_,output,_) = intcode::run_program(input.clone(), || 1, |_| {});
        assert_eq!(output, vec![1125899906842624]);
    }
}