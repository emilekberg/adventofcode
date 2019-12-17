fn main() {

    // let input: String = std::fs::read_to_string("./input.txt").unwrap();
    
    let input = String::from("03036732577212944063491565474664");
    part1(input.clone()); 
    // part2(input.clone());
}

fn str_to_int_array(s: String) -> Vec<i32> {
    return s
        .chars()
        .map(|c| c.to_digit(10).unwrap() as i32)
        .collect()
}

fn part1(s: String) -> i32 {
    let input = str_to_int_array(s);
    let base_pattern = vec![0,1,0,-1];
    let result = fft_passes(&input, &base_pattern, 100);
    let first_eight = result[0..8].to_vec();
    let answer = array_to_digit(first_eight);
    println!("part1: First eight {:?}", answer);
    return answer;
}

fn part2(s: String) -> i32 {
    let input = str_to_int_array(s);
    let base_pattern = vec![0,1,0,-1];
    let extended_input = extend_array(&input, 10000);
    println!("extended input from {} to {}", input.len(), extended_input.len());
    let result = fft_passes(&extended_input, &base_pattern, 100);
    let message_offset = array_to_digit(input[0..7].to_vec()) as usize;
    println!("message offset {:?}, {}", message_offset, result.len());
    let answer = array_to_digit(result[message_offset..message_offset+8].to_vec());
    println!("part2: {}", answer);
    return answer;
}

fn fft_passes(input: &Vec<i32>, base_pattern: &Vec<i32>, passes: i32) -> Vec<i32> {
    let mut result = input.clone();
    for i in 0..passes {
        result = fft(&result, &base_pattern);
        println!("{} passed done", i);
    }
    return result;
}

#[inline(always)]
fn fft(input: &Vec<i32>, pattern: &Vec<i32>) -> Vec<i32> {
    return input.iter().enumerate().map(|(i,_)| {
        println!("");
        return drop_digits(multiply(&input, &pattern, i));
    }).collect();
}

fn fft_2(input: &Vec<i32>, pattern: &Vec<i32>) -> Vec<i32> {
    
    
    let mut ret = vec![0i32;input.len()];
    ret[input.len()-1] = input[input.len()-1];

    let len = input.len()-2;
    for i in 0..len {
        ret[len-i] = (input[len-i] + ret[len-i+1]) % 10;
    }
    return ret;
    /* 
    for i in 0..input.len() {
        let mut result = 0;
        for j in i..input.len()/2 {
            let pattern_index = ((1+j)/(i+1))%pattern.len();
            let multiply_by = pattern[pattern_index];
            result += input[j] * multiply_by;
        }
        
        ret[i] = result % 10;
    }
    */
    return ret;
}

#[inline(always)]
fn multiply(input: &Vec<i32>, pattern: &Vec<i32>, pattern_offset: usize) -> i32 {
    return input.iter().enumerate().skip(pattern_offset).fold(0, move |acc, (i,val)| {
        let index = ((1+i)/(pattern_offset+1))%pattern.len();
        let multiply_by = pattern[index];
        print!("{},", multiply_by);
        if multiply_by == 0 {
            return acc;
        }
        return acc + (val * multiply_by);
    });
}

#[inline(always)]
fn drop_digits(num: i32) -> i32 {
    return (num % 10).abs();
}

fn get_num_digits_in_integer(digit: i32) -> i32 {
    if digit > 0 {
        return ((digit as f32).log(10.) + 1.) as i32;
    }
    return 1;
}

#[inline(always)]
pub fn get_nth(num: i32, n: u32) -> i32 {
    let div = (10u32).pow(n) as i32;
    let result = (num / div) % 10;
    return result;
}

#[inline(always)]
pub fn digit_to_array(digit: i32) -> Vec<i32> {
    let n = get_num_digits_in_integer(digit);
    let mut result = vec![];
    for i in 0..n {
        result.push(get_nth(digit, i as u32));
    }
    result.reverse();
    return result;
}

#[inline(always)]
fn array_to_digit(array: Vec<i32>) -> i32 {
    let mut result = 0;
    let mut mul = 1;
    for (_,val) in array.iter().rev().enumerate() {
        result += val * mul;
        mul *= 10;
    }
    return result;
}

#[inline(always)]
fn extend_array(array: &Vec<i32>, num: i32) -> Vec<i32> {
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
