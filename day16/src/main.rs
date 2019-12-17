fn main() {

    let input: String = std::fs::read_to_string("./input.txt")
        .unwrap();
    

    part1(input.clone()); 
    part2(input.clone());
}

fn str_to_int_array(s: String) -> Vec<i64> {
    return s
        .chars()
        .map(|c| c.to_digit(10).unwrap() as i64)
        .collect()
}

fn part1(s: String) -> i64 {
    let input = str_to_int_array(s);
    let base_pattern = vec![0,1,0,-1];
    let result = fft_passes(&input, &base_pattern, 100);
    let first_eight = result[0..8].to_vec();
    let answer = array_to_digit(first_eight);
    println!("part1: First eight {:?}", answer);
    return answer;
}

fn part2(s: String) -> i64 {
    let input = str_to_int_array(s);
    let base_pattern = vec![0,1,0,-1];
    let extended_input = extend_array(&input, 10000);
    println!("extended input from {} to {}", input.len(), extended_input.len());
    let result = fft_passes(&extended_input, &base_pattern, 100);
    let message_offset = array_to_digit(result[0..7].to_vec()) as usize;
    println!("message offset {:?}, {}", message_offset, result.len());
    let answer = array_to_digit(result[message_offset..message_offset+8].to_vec());
    println!("part2: {}", answer);
    return answer;
}

fn fft_passes(input: &Vec<i64>, base_pattern: &Vec<i64>, passes: i32) -> Vec<i64> {
    let mut result = input.clone();
    for i in 0..passes {
        result = fft(&result, &base_pattern);
        println!("{} passed done", i);
    }
    return result;
}

#[inline(always)]
fn fft(input: &Vec<i64>, pattern: &Vec<i64>) -> Vec<i64> {
    let mut to_return = vec![0;input.len()];
    for i in 0..input.len() {
        let result = multiply(&input, &pattern, i);
        to_return[i] = drop_digits(result);
    }
    return to_return;
}



#[inline(always)]
fn multiply(input: &Vec<i64>, pattern: &Vec<i64>, pattern_offset: usize) -> i64 {
    let mut result = 0;
    return input.iter().enumerate().skip(pattern_offset).fold(0, move |acc, (i,_)| {
        let index = ((1+i)/(pattern_offset+1))%pattern.len();
        let multiply_by = pattern[index];
        if multiply_by == 0 {
            return acc;
        }
        return acc + input[i] * multiply_by;
    });
    /*
    for i in pattern_offset..input.len() {
        let index = ((1+i)/(pattern_offset+1))%pattern.len();
        let multiply_by = pattern[index];
        if multiply_by == 0 {
            continue;
        }
        result += input[i] * multiply_by;
    }
    */
    return result;
}

#[inline(always)]
fn duplicate_pattern(pattern: &Vec<i64>, times: usize) -> Vec<i64> {
    let mut result = vec![];
    for p in pattern.iter() {
        for _ in 0..times {
            result.push(p.clone());
        }
    }
    return result;
}

#[inline(always)]
fn drop_digits(num: i64) -> i64 {
    return (num % 10).abs();
}

fn get_num_digits_in_integer(digit: i64) -> i64 {
    if digit > 0 {
        return ((digit as f32).log(10.) + 1.) as i64;
    }
    return 1;
}

#[inline(always)]
pub fn get_nth(num: i64, n: u32) -> i64 {
    let div = (10u32).pow(n) as i64;
    let result = (num / div) % 10;
    return result;
}

#[inline(always)]
pub fn digit_to_array(digit: i64) -> Vec<i64> {
    let n = get_num_digits_in_integer(digit);
    let mut result = vec![];
    for i in 0..n {
        result.push(get_nth(digit, i as u32));
    }
    result.reverse();
    return result;
}

#[inline(always)]
fn array_to_digit(array: Vec<i64>) -> i64 {
    let mut result = 0;
    let mut mul = 1;
    for (_,val) in array.iter().rev().enumerate() {
        result += val * mul;
        mul *= 10;
    }
    return result;
}

#[inline(always)]
fn extend_array(array: &Vec<i64>, num: i32) -> Vec<i64> {
    let mut result = vec![];
    for _ in 0..num {
        result.extend(array);
    }
    return result;
}

#[cfg(test)]
mod tests {
    use super::*;
    // #[test]
    fn test_multiply() {
        let input = vec![9,8,7,6,5];
        let pattern = vec![1,2,3];
        let result = multiply(&input, &pattern, 0);
        assert_eq!(result, 9*1 + 8*2 + 7*3 + 6*1 + 5*2);
    }
    #[test]
    fn test_drop_digits() {
        assert_eq!(drop_digits(38), 8);
        assert_eq!(drop_digits(-17), 7);
    }
    #[test]
    fn test_duplicate_pattern() {
        let pattern = vec![0,1,2,3];
        assert_eq!(duplicate_pattern(&pattern, 1), vec![0,1,2,3]);
        assert_eq!(duplicate_pattern(&pattern, 2), vec![0,0,1,1,2,2,3,3]);
        assert_eq!(duplicate_pattern(&pattern, 3), vec![0,0,0,1,1,1,2,2,2,3,3,3]);
    }

    #[test]
    fn test_get_num_digits_in_integer() {
        assert_eq!(get_num_digits_in_integer(10), 2);
        assert_eq!(get_num_digits_in_integer(0), 1);
        assert_eq!(get_num_digits_in_integer(1000), 4);
    }

    #[test]
    fn test_digit_to_array() {
        assert_eq!(digit_to_array(123456), vec![1,2,3,4,5,6])
    }

    #[test]
    fn test_array_to_digits() {
        assert_eq!(array_to_digit(vec![1,2,3,4,5,6]), 123456);
    }

    #[test]
    fn test_extend_array() {
        assert_eq!(extend_array(&vec![1,2,3], 2), vec![1,2,3,1,2,3]);
    }

    #[test]
    fn test_part1() {
        assert_eq!(part1(String::from("80871224585914546619083218645595")), 24176176);
        assert_eq!(part1(String::from("19617804207202209144916044189917")), 73745418);
        assert_eq!(part1(String::from("69317163492948606335995924319873")), 52432133);
    }
    #[test]
    fn test_part2() {
        assert_eq!(part2(String::from("03036732577212944063491565474664")), 84462026);
        // assert_eq!(part2(String::from("02935109699940807407585447034323")), 78725270);
        // assert_eq!(part2(String::from("03081770884921959731165446850517")), 53553731);
    }
}
