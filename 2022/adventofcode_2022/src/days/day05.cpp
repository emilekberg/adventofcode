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
	namespace day05 {

		static inline std::string solve(std::vector<std::string> input, bool keepOrderOnMove) {
			std::unordered_map<size_t, std::stack<char>> stacks;
			auto it = std::find(input.begin(), input.end(), "");
			size_t setupEndRow = it - input.begin();

			for (int row = setupEndRow; row >= 0; row--) {
				for (size_t col = 0; col < input[row].size(); col += 4) {
					size_t stack = col / 4 + 1;
					if (!stacks.contains(stack)) {
						stacks.insert_or_assign(stack, std::stack<char>());
					}
					char c = input[row][col + 1];
					if (c >= 'A' && c <= 'Z') {
						stacks[stack].push(c);
					}
				}
			}

			std::regex re("move (\\d+) from (\\d+) to (\\d+)");
			for (size_t line = setupEndRow + 1; line < input.size(); line++) {
				std::smatch matches;
				if (!std::regex_search(input[line], matches, re)) {
					continue;
				}
				int numToMove = std::stoi(matches[1].str());
				int fromStack = std::stoi(matches[2].str());
				int toStack = std::stoi(matches[3].str());

				std::deque<char> tempQueue;
				for (int i = 0; i < numToMove; i++) {
					char c = stacks[fromStack].top();
					stacks[fromStack].pop();
					tempQueue.push_back(c);
				}
				if (keepOrderOnMove) {
					std::reverse(tempQueue.begin(), tempQueue.end());
				}
				
				do {
					stacks[toStack].push(tempQueue.back());
					tempQueue.pop_back();
				} while (tempQueue.size() > 0);
			}
			std::string result;
			for (auto it = stacks.begin(); it != stacks.end(); it++) {
				result += (*it).second.top();
			}
			return result;
		}

		static inline std::string part01(std::vector<std::string> input)
		{
			return solve(input, true);
		}

		static inline std::string part02(std::vector<std::string> input)
		{
			return solve(input, false);
		}

		static inline void run() {
			auto lines = readlines("./input/day05.txt");
			std::string part01 = adventofcode::day05::part01(lines);
			std::string part02 = adventofcode::day05::part02(lines);
			std::cout << "day05: " << part01 << ", " << part02 << std::endl;
		}
	}
}