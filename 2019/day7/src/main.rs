extern crate intcode;
use std::thread;
use std::sync::mpsc::*;
fn main() {
    let memory: Vec<i64> = std::fs::read_to_string("./input.txt")
        .unwrap()
        .split(",")
        .map(|x| x.parse().unwrap())
        .collect();

    let part1_result = part1(memory.clone());
    let part2_result = part2(memory.clone());
    println!("part 1 {}", part1_result);
    println!("part 2: {}", part2_result);

}

fn part1(memory: Vec<i64>) -> i64 {
    let mut max = 0;
    let posibilities = generate_all_posibilities(vec![0,1,2,3,4]);
    for setting in posibilities {
        let throttle = get_throttle(&mut setting.clone(), memory.clone());
        if throttle > max {
            max = throttle;
        }
    }
    return max;
}

pub fn get_throttle(init_settings: &mut Vec<i64>, memory: Vec<i64>) -> i64 {
    let mut prev_input = 0;
    for _ in 0..5 {
        let mut data = vec![init_settings.remove(0), prev_input];
        let (_,output,_) = intcode::run_program(memory.clone(), move || data.remove(0), |_| {});
        prev_input = *output.last().unwrap();
    }
    return prev_input;
}

fn part2(memory: Vec<i64>) -> i64 {
    let mut max = 0;
    let posibilities = generate_all_posibilities(vec![5,6,7,8,9]);
    for setting in posibilities {
        let throttle = get_throttle_feedback(&mut setting.clone(), memory.clone());
        if throttle > max {
            max = throttle;
        }
    }
    return max;
}

fn get_throttle_feedback(init_settings: &mut Vec<i64>, memory: Vec<i64>) -> i64 {
    let mut threads = vec![];
    let (tx_5, rx_1) = std::sync::mpsc::channel::<i64>();
    let (tx_1, rx_2) = std::sync::mpsc::channel::<i64>();
    let (tx_2, rx_3) = std::sync::mpsc::channel::<i64>();
    let (tx_3, rx_4) = std::sync::mpsc::channel::<i64>();
    let (tx_4, rx_5) = std::sync::mpsc::channel::<i64>();
    tx_5.send(init_settings[0]).unwrap();
    tx_1.send(init_settings[1]).unwrap();
    tx_2.send(init_settings[2]).unwrap();
    tx_3.send(init_settings[3]).unwrap();
    tx_4.send(init_settings[4]).unwrap();
    
    tx_5.send(0).unwrap();
    threads.push(spawn_thread(1,memory.clone(), rx_1, tx_1));
    threads.push(spawn_thread(2,memory.clone(), rx_2, tx_2));
    threads.push(spawn_thread(3,memory.clone(), rx_3, tx_3));
    threads.push(spawn_thread(4,memory.clone(), rx_4, tx_4));
    threads.push(spawn_thread(5,memory.clone(), rx_5, tx_5));

    let result: Vec<i64> = threads.into_iter().map(|thread| thread.join().unwrap()).collect();
    return *result.last().unwrap();
}

fn spawn_thread(_id: i32, memory: Vec<i64>, rx: Receiver<i64>, tx: Sender<i64>) -> std::thread::JoinHandle<(i64)> {
    return thread::spawn(move || {
        let (_,output,_) = intcode::run_program(memory.clone(), move || {
            let val = rx.recv().unwrap();
            return val;
        }, move |x| {
            // this will fail when trying to output a message to a processor which has already been halted. thus we can fail silently.
            tx.send(x).unwrap_or_default();
        });
        if output.len() == 0 {
            return 0;
        }
        return output.last().unwrap().clone();
    });
} 

fn generate_all_posibilities(allowed_numbers: Vec<i64>) -> Vec<Vec<i64>> {
    let mut result = Vec::new();
    gen_phases(&mut result, &mut Vec::new(), &allowed_numbers);
    return result;
}

fn gen_phases(result: &mut Vec<Vec<i64>>, target: &mut Vec<i64>, phases: &Vec<i64>) {
    if phases.len() == 0 {
        result.push(target.clone());
    }
    for i in 0..phases.len() {
        let mut new_target = target.clone();
        let mut new_phase = phases.clone();
        let phase = new_phase.remove(i);
        new_target.push(phase);
        gen_phases(result, &mut new_target, &new_phase);
    }
}


#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_generate() {
        let res = generate_all_posibilities(vec![0,1,2,]);
        assert_eq!(res, vec![
            vec![0,1,2],
            vec![0,2,1],
            vec![1,0,2],
            vec![1,2,0],
            vec![2,0,1],
            vec![2,1,0]
        ]);
    }

    #[test]
    fn test_part1_43210() {
        let max = get_throttle(&mut vec![4,3,2,1,0], vec![3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0]);
        assert_eq!(max, 43210);
    }
    
    #[test]
    fn test_part1_54321() {
        let max = get_throttle(&mut vec![0,1,2,3,4], vec![3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0]);
        assert_eq!(max, 54321);
    }

    #[test]
    fn test_part1_65210() {
        let max = get_throttle(&mut vec![1,0,4,3,2], vec![3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0]);
        assert_eq!(max, 65210);
    }


    #[test]
    fn test_part2() {
        let max = get_throttle_feedback(&mut vec![9,8,7,6,5], vec![3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5]);
        assert_eq!(max, 139629729);
    }
    
    #[test]
    fn test_part2_18216() {
        let max = get_throttle_feedback(&mut vec![9,7,8,5,6], vec![3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10]);
        assert_eq!(max, 18216);
    }
}