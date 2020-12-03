extern crate intcode;
use std::sync::mpsc::*;
use std::thread;
extern crate termion;
use std::io;
use std::io::Write;

use termion::raw::IntoRawMode;

mod ai;
fn main() {

    let mut memory: Vec<i64> = std::fs::read_to_string("./input.txt")
        .unwrap()
        .split(",")
        .map(|x| x.parse::<i64>().unwrap())
        .collect();
    memory[0] = 2;
    let (input_sender,input_receiver) = channel::<i64>();
    let (output_sender,output_receiver) = channel::<i64>();
    println!("starting game");

    let (ai_in, ai_out, ai_thread) = ai::create();
    let game_thread = intcode::run_async(memory.clone(), input_receiver, output_sender);

    println!("game started.");
    let mut stdout = io::stdout().into_raw_mode().unwrap();
    let mut score = 0;
    clear_screen();

    let wait_time = std::time::Duration::from_millis(1);
    println!("start game loop");
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

        ai_in.send(x).unwrap();
        ai_in.send(y).unwrap();
        ai_in.send(id).unwrap();

        // score overrides id.
        if x == -1 && y == 0 {
            score = id;
            write!(
                stdout,
                "{}score: {}",
                termion::cursor::Goto(0,24),
                score.to_string()
            ).unwrap();
            stdout.lock().flush().unwrap();
            continue;
        }

        if id == 4 {
            let ai_response = ai_out.recv().unwrap();
            input_sender.send(ai_response).unwrap();
        }
        
        let sign = match id {
            0 => " ",
            1 => "◻️",
            2 => "◼️",
            3 => "➖",
            4 => "💍",
            _ => continue,
        };
        write!(
            stdout,
            "{}{}",
            termion::cursor::Goto((x+1) as u16,(y+1) as u16),
            sign
        ).unwrap();
        stdout.lock().flush().unwrap();
        thread::sleep(wait_time);
    }
    // tell AI to halt.
    ai_in.send(99).unwrap();

    game_thread.join().unwrap();
    ai_thread.join().unwrap();


    println!("{}", score);

}
fn clear_screen() {
    print!("{}[2J", 27 as char);
}



/*

AI
#START
INPUT TO A
INPUT TO B
IF A > B 
  JMP OUTPUT 1
IF A < B
  JMP OUTPUT -1
IF A == B
  JMP OUTPUT 0
OUTPUT 1
JMP #START
OUTPUT -1
JMP #START
OUTPUT 0
JMP #START
*/


