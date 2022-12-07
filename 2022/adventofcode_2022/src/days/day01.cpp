#pragma once

#include <string>
#include <vector>
#include <algorithm>
#include <iostream>
#include "../filehelper.cpp"

namespace adventofcode {
	namespace day01 {
		static inline int part01(std::vector<std::string> input)
		{
			int result = 0;
			int max = 0;
			for (int i = 0; i < input.size(); i++) {
				if (input[i] == "") {
					if (result > max) {
						max = result;
					}
					result = 0;
					continue;
				}
				int toAdd = std::stoi(input[i]);
				result += toAdd;
			}
			return max;
		}

		static inline int part02(std::vector<std::string> input)
		{
			std::vector<int> result;
			int total = 0;
			int max = 0;
			for (int i = 0; i < input.size(); i++) {
				if (input[i] == "") {
					result.push_back(total);
					total = 0;
					continue;
				}
				int toAdd = std::stoi(input[i]);
				total += toAdd;
			}

			std::sort(result.begin(), result.end(), std::greater<int>());

			int sum = result[0] + result[1] + result[2];

			return sum;
		}

		static inline void run() {
			auto lines = readlines("./input/day01.txt");
			int part01 = day01::part01(lines);
			int part02 = day01::part02(lines);
			std::cout << "day01: " << part01 << ", " << part02 << std::endl;
		}
	}
}