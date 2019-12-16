#[derive(Debug,Hash,Eq,PartialEq,Copy,Clone)]
pub struct Point {
    pub x: i64,
    pub y: i64,
}
pub fn new(x: i64, y: i64) -> Point {
    return Point {
        x,y
    };
}
