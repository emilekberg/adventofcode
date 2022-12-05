#pragma once
#include <vector>
#include <string>
namespace adventofcode {
	std::vector<std::string> split(std::string input, std::string delimiter) {
		auto result = std::vector<std::string>();
		size_t len = 0;
		size_t startPos = 0;
		for (size_t i = 0; i < input.length()-1; i++) {
			std::string part = input.substr(i, delimiter.length());
			if (part != delimiter) {
				continue;
			}
			std::string sub = input.substr(startPos, i);
			result.push_back(sub);
			startPos = i;
		}


		return result;
	}
}