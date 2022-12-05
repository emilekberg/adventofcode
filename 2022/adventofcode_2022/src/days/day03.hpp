#pragma once

#include <string>
#include <vector>
#include <algorithm>
#include <iostream>
#include <numeric>
#include "../filehelper.hpp"
#include <unordered_set>

namespace adventofcode {
	namespace day03 {

		constexpr int charToPriority(char c) {
			if (c >= 'a' && c <= 'z') {
				return (c - 'a' + 1);
			}
			return (c - 'A' + 27);
		}

		static inline int part01(std::vector<std::string> input)
		{
			int total = 0;
			for (size_t i = 0; i < input.size(); i++) {
				size_t midpoint = input[i].length() / 2;

				std::string lhs = input[i].substr(0, midpoint);
				std::string rhs = input[i].substr(midpoint, midpoint);

				std::unordered_set<int> existsInBoth;

				for (size_t y = 0; y < lhs.length(); y++) {
					char c = lhs[y];
					if (rhs.find(c) != std::string::npos) {
						existsInBoth.insert(charToPriority(c));
					}
				}

				int result = std::accumulate(existsInBoth.begin(), existsInBoth.end(), 0);
				total += result;
			}
			return total;
		}

		static inline int part02(std::vector<std::string> input)
		{
			int total = 0;
			for (size_t i = 0; i < input.size(); i += 3) {
				for (size_t y = 0; y < input[i].length(); y++) {
					char c = input[i][y];
					if (input[i+1].find(c) != std::string::npos && input[i+2].find(c) != std::string::npos) {
						total += charToPriority(c);
						break;
					}
				}
			}
			return total;
		}

		static inline void run() {
			auto lines = readlines("./input/day03.txt");
			int part01 = adventofcode::day03::part01(lines);
			int part02 = adventofcode::day03::part02(lines);
			std::cout << "day03: " << part01 << ", " << part02 << std::endl;
		}
	}
}