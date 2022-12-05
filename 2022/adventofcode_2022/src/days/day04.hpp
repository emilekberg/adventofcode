#pragma once

#include <string>
#include <vector>
#include <algorithm>
#include <iostream>
#include <numeric>
#include "../filehelper.hpp"
#include <unordered_set>

namespace adventofcode {
	namespace day04 {
		enum mode {
			Contains = 1,
			Overlaps = 2
		};
		struct range {
			int min, max;

			range(int min, int max) : min(min), max(max) {}

			bool contains(range other) {
				return min <= other.min && max >= other.max;
			}

			bool overlaps(range other) {
				return (other.min >= min && other.min <= max) || (other.max >= min && other.max <= max);
			}
		};

		range getRange(std::string input) {
			size_t splitIndex = input.find('-');
			std::string lhs = input.substr(0, splitIndex);
			std::string rhs = input.substr(splitIndex + 1);

			return range(atoi(lhs.c_str()), atoi(rhs.c_str()));
		}

		static inline int solve(std::vector<std::string> input, mode mode) {
			int result = 0;
			for (size_t i = 0; i < input.size(); i++) {

				size_t splitIndex = input[i].find(',');
				auto first = getRange(input[i].substr(0, splitIndex));
				auto second = getRange(input[i].substr(splitIndex + 1));

				switch (mode) {
				case mode::Contains:
					if (first.contains(second) || second.contains(first)) {
						result++;
					}
					break;
				case mode::Overlaps:
					if (first.overlaps(second) || second.overlaps(first)) {
						result++;
					}
					break;
				}
			}
			return result;
		}

		static inline int part01(std::vector<std::string> input) {
			return solve(input, mode::Contains);
		}

		static inline int part02(std::vector<std::string> input) {
			return solve(input, mode::Overlaps);
		}

		static inline void run() {
			auto lines = readlines("./input/day04.txt");
			int part01 = adventofcode::day04::part01(lines);
			int part02 = adventofcode::day04::part02(lines);
			std::cout << "day04: " << part01 << ", " << part02 << std::endl;
		}
	}
}