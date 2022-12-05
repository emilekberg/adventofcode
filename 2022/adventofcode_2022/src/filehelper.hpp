#pragma once
#include <fstream>
#include <string>
#include <vector>

namespace adventofcode {
	static inline std::vector<std::string> readlines(std::string filepath) {
		auto result = std::vector<std::string>();
		std::fstream filestream;
		filestream.open(filepath, std::ios::in);

		if (!filestream.is_open()) {
			throw "could not open file";
		}
		std::string line;
		while (getline(filestream, line)) {
			result.push_back(line);
		}
		filestream.close();
		return result;
	}
}