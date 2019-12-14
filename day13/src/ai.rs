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
    01105,
      1,
      6,
    0,          // mem_in ball x
    0,          // mem_in paddle x
    0,          // temp bool for holding result of query
    10003, 
        3,       // input ball x
    1008,         // check for halt
        3,
        99,
        5,
    1005,         // goto halt
        5,
        53,
    10003, 
        4,        // input paddle x
    7,        // LT 
        3, 
        4, 
        5, // if ball x > paddle x, store 1 in bool
    1005, 
        5,
        38,
    7,
        4,
        3,
        5,
    1005,
        5,
        43,
    8,
        4,
        3,
        5,
    1005,
        5,
        48,
    11104,  // output 1
      -1,
    11105,  // jump to start 
      1, 
      0,
    11104,  // output -1
      1,
    11105,  // jump to start
      1,
      0,
    11104,  // output 0
      0,
    11105,  // jump to start
      1,
      0,
    99,
  ]
}

#[cfg(test)]
mod tests {
  use super::*;
  #[test]
  fn test_paddle_left() {
    let (i,o,thread) = create();
    i.send(1).unwrap();
    i.send(2).unwrap();
    let res = o.recv().unwrap();
    
    assert_eq!(res, -1);
    
    i.send(99).unwrap();
    thread.join().unwrap();
  }
  #[test]
  fn test_paddle_right() {
    let (i,o,thread) = create();
    i.send(3).unwrap();
    i.send(2).unwrap();
    let res = o.recv().unwrap();
    
    assert_eq!(res, 1);
    
    i.send(99).unwrap();
    thread.join().unwrap();
  }
  #[test]
  fn test_paddle_middle() {
    let (i,o,thread) = create();
    i.send(2).unwrap();
    i.send(2).unwrap();
    let res = o.recv().unwrap();
    
    assert_eq!(res, 0);
    
    i.send(99).unwrap();
    thread.join().unwrap();
  }
  #[test]
  fn test_paddle_loops() {
    let (i,o,thread) = create();
    let mut result = 0;
    for _ in 0..10 {
      i.send(3).unwrap();
      i.send(2).unwrap();
      let res = o.recv().unwrap();
      result += res;
    }
    
    assert_eq!(result, 10);
    
    i.send(99).unwrap();
    thread.join().unwrap();
  }
}