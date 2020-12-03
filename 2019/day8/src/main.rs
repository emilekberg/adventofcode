#[derive(Debug)]
struct Image {
    width: usize,
    height: usize,
    pub layers: Vec<Vec<i32>>
}
impl Image {
    fn new(width: usize, height: usize) -> Image {
        return Image {
            width, 
            height,
            layers: vec![]
        };
    }
    fn read_data(&mut self, data: Vec<i32>) {
        let layer_size = self.width * self.height;
        for (i,n) in data.iter().enumerate() {
            let layer = i / layer_size;
            if self.layers.len() == layer {
                self.layers.push(vec![]);
            }
            self.layers[layer].push(*n);
        }
    }

}
fn main() {

    let data: Vec<i32> = std::fs::read_to_string("./input.txt")
        .unwrap()
        .chars()
        .map(|x| x.to_string().parse().unwrap())
        .collect();
    let mut image = Image::new(25,6);
    image.read_data(data);

    part1(&image);
    part2(&image);
}

fn part1(image: &Image) {

    // creates an vector with number of zeroes as value, same index as the layers.
    let num_zeroes: Vec<usize> = image.layers
        .iter()
        .enumerate()
        .map(|(_,x)| x.iter()
            .cloned()
            .filter(|x| x.eq(&0))
            .count()
        ).collect();
    // finds the layer value with minimum zeroes.
    let min_value = num_zeroes.iter().min().unwrap();
    // finds the index of the minimum zeroes.
    let min_index = num_zeroes.iter().position(|x| x == min_value).unwrap();

    let (ones,twos) = image.layers[min_index]
        .iter()
        .fold((0,0), |(ones,twos),n| {
            match n {
                1 => return (ones + 1, twos),
                2 => return (ones, twos + 1),
                _ => return (ones, twos),
            };
        });
    println!("part1: {}", ones*twos);
}

fn part2(image: &Image) {
    let mut result = vec!["";image.width*image.height];
    for layer in image.layers.iter().rev() {
        for (i, color) in layer.iter().enumerate() {
            match color {
                // black
                0 => {
                    result[i] = " ";
                },
                // white
                1 => {
                    result[i] = "@";
                },
                // transparent
                2 => continue,
                _ => continue,
            }
        }
    }
    println!("part2: ");
    for (i, color) in result.iter().enumerate() {
        if i % image.width == 0 {
            println!("");
        }
        print!("{}", color);
    }
    println!("");
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn read_data() {
        let mut image = Image::new(3,2);
        image.read_data(vec![1,2,3,4,5,6,7,8,9,0,1,2]);
        assert_eq!(image.layers[0], vec![1,2,3,4,5,6]);
        assert_eq!(image.layers[1], vec![7,8,9,0,1,2]);
    }
}