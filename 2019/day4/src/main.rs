fn main() {
    let start = 130254;
    let end = 678275;
    let mut i = start;
    let mut num = 0;
    loop {
      if i == end {
        break;
      }
      let password = str_to_vecu8(i.to_string());
      if is_password_valid(password) {
        num += 1;
      }

      i += 1;
    }
    println!("{}", num);
}

fn is_password_valid(to_validate: Vec<u8>) -> bool {
  let mut prev = 0;
  let mut i: usize = 0;
  let len = to_validate.len();
  let mut has_double = false;
  let mut same_count = 0;
  loop {
    if i == len {
      if !has_double && same_count == 1 {
        return true;
      }
      return has_double;
    }
    let curr = to_validate[i];

    if i != 0 {
      if curr < prev {
        return false;
      } else if curr == prev {
        println!("inc, {},{}", prev, curr);
        same_count += 1;
      } else if curr > prev && !has_double {
        println!("{}", same_count);
        has_double = same_count == 1;
        same_count = 0;
      }

    }
    i += 1;
    prev = curr;
  }
}

fn str_to_vecu8(s: String) -> Vec<u8> {
  // let a: Vec<String> = s.chars().map(|x| x.to_string()).collect();
  // println!("{}", a[0]);
  // return s.split("").map(|x| x.parse().unwrap()).collect();
  return s.chars().map(|x| x.to_string()).map(|x| x.parse().unwrap()).collect();
}

#[cfg(test)]
mod tests {
  use super::*;
  #[test]
  fn is_password_valid_test_should_be_false_no_decrease() {
    assert_eq!(is_password_valid(vec![2,2,3,4,5,0,]), false);
  }
  #[test]
  fn is_password_valid_test_should_be_false_no_double() {
    assert_eq!(is_password_valid(vec![1,2,3,7,8,9,]), false);
  }

  #[test]
  fn is_password_valid_test_has_doubles() {
    assert_eq!(is_password_valid(vec![1,1,1,1,1,1,]), false);
  }

  #[test]
  fn is_password_valid_test_has_doubles_not_decrease() {
    assert_eq!(is_password_valid(vec![1,1,2,2,3,3,]), true);
  }

  #[test]
  fn is_password_valid_test_doubles_not_in_larger_group() {
    assert_eq!(is_password_valid(vec![1,2,3,4,4,4,]), false);
  }

  #[test]
  fn is_password_valid_test_doubles_has_exactly_two() {
    assert_eq!(is_password_valid(vec![1,1,1,1,2,2,]), true);
  }


  #[test]
  fn str_to_u8_arr_test() {
    let arr = str_to_vecu8(String::from("123456"));
    assert_eq!(arr, vec![1,2,3,4,5,6,]);
  }
  #[test]
  fn str_to_u8_arr_test_2() {
    let arr = str_to_vecu8(String::from("572351"));
    assert_eq!(arr, vec![5,7,2,3,5,1,]);
  }
}
