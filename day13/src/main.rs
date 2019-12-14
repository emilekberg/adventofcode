extern crate intcode;
use std::sync::mpsc::*;
use std::thread;
extern crate termion;
use std::io;
use std::io::Write;

use termion::raw::IntoRawMode;
fn main() {

    let mut memory: Vec<i64> = std::fs::read_to_string("./input.txt")
        .unwrap()
        .split(",")
        .map(|x| x.parse::<i64>().unwrap())
        .collect();
    memory[0] = 2;
    let (input_sender,input_receiver) = std::sync::mpsc::channel::<i64>();
    let (output_sender,output_receiver) = std::sync::mpsc::channel::<i64>();
    println!("starting game");
    let mut stdout = io::stdout().into_raw_mode().unwrap();
    let game_thread = start_game_on_new_thread(memory.clone(), output_sender, input_receiver);
    println!("game started.");
    // let mut display = std::collections::HashMap::new();
    let mut score = 0;
    clear_screen();

    let wait_time = std::time::Duration::from_millis(2);
    let mut last_paddle_pos = (0,0);
    loop {
        let x_rec = output_receiver.recv();
        let y_rec = output_receiver.recv();
        let id_rec = output_receiver.recv();
        
        if x_rec.is_err() || y_rec.is_err() || id_rec.is_err() {
            break;
        }
        let x = x_rec.unwrap();
        let y = y_rec.unwrap();
        let id = id_rec.unwrap();

        // score overrides id.
        if x == -1 && y == 0 {
            score = id;
            write!(
                stdout,
                "{}score: {}",
                termion::cursor::Goto(0,24),
                score.to_string()
            ).unwrap();
            continue;
        }

        let sign = match id {
            0 => " ",
            1 => "#",
            2 => ".",
            3 => "=",
            4 => "@",
            _ => continue,
        };

        if id == 3 {
            last_paddle_pos.0 = x;
            last_paddle_pos.1 = y;
        }
        if id == 4 {
            if last_paddle_pos.0 < x {
                input_sender.send(1).unwrap_or_default();
            } else if last_paddle_pos.0 > x {
                input_sender.send(-1).unwrap_or_default();
            } else {
                input_sender.send(0).unwrap_or_default();
            }
        }


        write!(
            stdout,
            "{}{}",
            termion::cursor::Goto(x as u16,y as u16),
            sign
        ).unwrap();
        stdout.lock().flush().unwrap();
        thread::sleep(wait_time);
    }
    println!("{}", score);

}
fn clear_screen() {
    print!("{}[2J", 27 as char);
}
fn start_game_on_new_thread(memory: Vec<i64>, output: Sender<i64>, input: Receiver<i64>) -> std::thread::JoinHandle<()> {
    return thread::spawn(move || {
        intcode::run_program(memory.clone(), move || {
            let result = input.recv();
            if result.is_err() {
                return 0;
            }
            return result.unwrap();
            
        }, move |o| {
            output.send(o).unwrap_or_default();
        });
    });
}