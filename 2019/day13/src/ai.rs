extern crate intcode;
use std::sync::mpsc::*;
use std::thread::JoinHandle;

pub fn create() -> (Sender<i64>, Receiver<i64>, JoinHandle<()>)
{
  let (input_sender,input_receiver) = std::sync::mpsc::channel::<i64>();
  let (output_sender,output_receiver) = std::sync::mpsc::channel::<i64>();
  let handle = intcode::run_async(get_ai_mem(), input_receiver, output_sender);

  return (input_sender, output_receiver, handle);
}


fn get_ai_mem() -> Vec<i64> {
  vec![
    10003,
    74,
1008,
    74,
    99,
    77,
1005,
    77,
    73,// HALT POSITION
10003,
    75,
10003,
    76,
1008,   // IF PADDLE
    76,
    3,
    77, 
1005,   // JUMP TO PADDLE STORE
    77,
    30, 
1008,   // IF BALL
    76,
    4,
    77, 
1005,   // JUMP TO BALL POSITION PROCESS
    77,
    37, 
11005,  // JUMP TO START IF IT IS SOMETHING ELSE
    1,
    0,
01001,  // STORE PADDLE IN PADDLE X POS
    74,
    0,
    78,
11005,  // JUMP TO START
    1,
    0,
7,      // BALL < PADDLE
    74,
    78,
    77,
1005,
    77,
    58, // JMP TO OUTPUT  
7,      // BALL > PADDLE
    78,
    74,
    77,
1005,
    77,
    63,  // IF PADDLE < BALL
8,
    74,
    78,
    77,
1005,
    77,
    68, // IF PADDLE == BALL
11104,  // RETURN -1
    -1,
11105,
  1,
  0,
11104,  // RETURN 1
  1,
11105,
  1,
  0,
11104,  // RETURN 0
  0,
11105,
  1,
  0,
99,     // HALT
0,  // pos x
0,  // pos y 
0,  // id
0,  // bool
0,  // paddle x
  ]
}

#[cfg(test)]
mod tests {
  use super::*;
  #[test]
  fn test_paddle_right() {
    let (i,o,thread) = create();
    i.send(142).unwrap();
    i.send(0).unwrap();
    i.send(3).unwrap();
    i.send(141).unwrap();
    i.send(0).unwrap();
    i.send(4).unwrap();
    let res = o.recv().unwrap();
    
    assert_eq!(res, -1);
    
    i.send(99).unwrap();
    thread.join().unwrap();
  }
  #[test]
  fn test_paddle_left() {
    let (i,o,thread) = create();
    i.send(120).unwrap();
    i.send(0).unwrap();
    i.send(3).unwrap();
    i.send(121).unwrap();
    i.send(0).unwrap();
    i.send(4).unwrap();
    let res = o.recv().unwrap();
    
    assert_eq!(res, 1);
    
    i.send(99).unwrap();
    thread.join().unwrap();
  }
  #[test]
  fn test_paddle_middle() {
    let (i,o,thread) = create();
    i.send(1).unwrap();
    i.send(0).unwrap();
    i.send(3).unwrap();
    i.send(1).unwrap();
    i.send(0).unwrap();
    i.send(4).unwrap();
    let res = o.recv().unwrap();
    
    assert_eq!(res, 0);
    
    i.send(99).unwrap();
    thread.join().unwrap();
  }
  #[test]
  fn test_paddle_does_not_wait_for_input_for_other_than_3() {
    let (i,_,thread) = create();
    let mut result = 0;
    for _ in 0..10 {
      i.send(0).unwrap();
      i.send(0).unwrap();
      i.send(0).unwrap();
      result += 1;
    }
    assert_eq!(result, 10);
    i.send(99).unwrap();
    thread.join().unwrap();
  }
}