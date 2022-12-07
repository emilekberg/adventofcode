#pragma once

#include <string>
#include <vector>
#include <algorithm>
#include <iostream>
#include <numeric>
#include "../filehelper.cpp"
#include <unordered_set>
#include <stack>
#include <unordered_map>
#include <queue>
#include <regex>

namespace adventofcode {
	namespace day06 {

		static inline int solve(std::string input, size_t numInSequence) {
			std::unordered_set<char> map;
			for (size_t i = numInSequence; i < input.size(); i++) {
				map.clear();

				for (int j = 0; j < numInSequence; j++) {
					char c = input[i - j];
					map.insert(c);
				}
				if (map.size() == numInSequence) {
					return (int)i + 1;
				}


			}
			return 0;
		}

		static inline int part01(std::vector<std::string> input)
		{
			return solve(input[0], 4);
		}

		static inline int part02(std::vector<std::string> input)
		{
			return solve(input[0], 14);
		}

		static inline void run() {
			auto lines = readlines("./input/day06.txt");
			int part01 = day06::part01(lines);
			int part02 = day06::part02(lines);
			std::cout << "day06: " << part01 << ", " << part02 << std::endl;
		}
	}
}