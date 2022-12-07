#pragma once

#include <string>
#include <vector>
#include <algorithm>
#include <iostream>
#include <numeric>
#include "../filehelper.cpp"
#include "../stringutils.cpp"

namespace adventofcode {
	namespace day07 {
		struct file {
			std::string name;
			int size;

			file(std::string name, int size) : name(name), size(size) {}
		};
		struct folder {
			std::string name;
			std::vector<folder*> folders;
			std::vector<file*> files;
			folder* parent = nullptr;

			folder(std::string name, folder* parent) : name(name), parent(parent) {
				folders = std::vector<folder*>();
				files = std::vector<file*>();
			}

			~folder()
			{
				for (auto it = folders.begin(); it != folders.end(); it++) {
					delete (*it);
				}
				for (auto it = files.begin(); it != files.end(); it++) {
					delete (*it);
				}
			}

			int calculateSize() {
				int total = 0;
				for (int i = 0; i < files.size(); i++) {
					total += files[i]->size;
				}				
				for (int i = 0; i < folders.size(); i++) {
					total += folders[i]->calculateSize();
				}
				return total;
			}
			folder* getFolder(std::string name) {
				for (int i = 0; i < folders.size(); i++) {
					if (folders[i]->name == name) {
						return folders[i];
					}
				}
			}
		}; 
		
		static bool isCommand(std::string row) {
			return row.starts_with('$');
		}

		static std::vector<folder*> buildTree(std::vector<std::string> input) {
			std::vector<folder*> folders;
			folder* root = new folder("/", nullptr);
			folder* currentFolder = root;
			folders.push_back(root);

			for (size_t i = 0; i < input.size(); i++) {
				std::string row = input[i];

				if (isCommand(row)) {
					auto args = split(row, " ");
					std::string command = args[1];
					if (command == "cd") {
						std::string folder = args[2];
						if (folder == "/") {
							currentFolder = root;
						}
						else if (folder == "..") {
							currentFolder = currentFolder->parent;
						}
						else {
							currentFolder = currentFolder->getFolder(folder);
						}
					}
					else if (command == "ls") {

						do
						{
							i++;
							row = input[i];
							auto args = split(row, " ");
							if (args[0] == "dir") {
								folder* f = new folder(args[1], currentFolder);
								folders.push_back(f);
								currentFolder->folders.push_back(f);
							}
							else {
								int size = std::stoi(args[0]);
								std::string name = args[1];
								currentFolder->files.push_back(new file(name, size));
							}
						} while (i + 1 < input.size() && !isCommand(input[i + 1]));

					}
				}

			}
			return folders;
		}

		static inline int part01(std::vector<std::string> input)
		{
			auto folders = buildTree(input);
			int limit = 100000;
			int result = 0;
			for (int i = 0; i < folders.size(); i++) {
				int size = folders[i]->calculateSize();
				if (size < limit) {
					result += size;
				}
			}
			return result;
		}

		static inline int part02(std::vector<std::string> input)
		{
			auto folders = buildTree(input);
			int totalSize = 70000000;
			int usedSize = folders[0]->calculateSize();
			int freeSize = totalSize - usedSize;
			int requiredSize = 30000000;
			int minimum = std::numeric_limits<int>::max();
			
			for (int i = 0; i < folders.size(); i++) {
				int size = folders[i]->calculateSize();
				if (size > minimum) continue;
				int tempFree = freeSize + size;
				if (tempFree >= requiredSize) {
					minimum = size;
				}

			}
			return minimum;
		}

		static inline void run() {
			auto lines = readlines("./input/day07.txt");
			int part01 = day07::part01(lines);
			int part02 = day07::part02(lines);
			std::cout << "day07: " << part01 << ", " << part02 << std::endl;
		}
	}
}