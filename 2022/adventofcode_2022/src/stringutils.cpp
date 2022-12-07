#pragma once
#include <vector>
#include <string>

namespace adventofcode {
	std::vector<std::string> split(std::string toSplit, std::string delimiter) {
		size_t index;
		size_t start = 0;
		std::vector<std::string> result;
		while ((index = toSplit.find(delimiter, start)) != std::string::npos) {
			index = toSplit.find(delimiter, start);
			std::string splitResult = toSplit.substr(start, index - start);
			result.push_back(splitResult);
			start = index+1;
		};
		result.push_back(toSplit.substr(start));
		return result;
	}
}