#pragma once

#include <string>
#include <vector>
#include <algorithm>
#include <iostream>
#include "../filehelper.cpp"

namespace adventofcode {
	namespace day02 {
		constexpr bool isRock(const int i) { return i == 0; }
		constexpr bool isPaper(const int i) { return i == 1; }
		constexpr bool isScissors(const int i) { return i == 2; }
		constexpr int getWinningHandFromHand(const int hand) { return (hand + 2) % 3; }

		static inline int getScore(const int opponent, const int player) {
			int score = player + 1;
			if (opponent == player) {
				score += 3;
			}
			else if (getWinningHandFromHand(player) == opponent) {
				score += 6;
			}
			return score;
		}

		

		static inline int part01(std::vector<std::string> input)
		{
			int total = 0;
			for (size_t i = 0; i < input.size(); i++) {
				int opponent = input[i][0] - 'A';
				int player = input[i][2] - 'X';
				total += getScore(opponent, player);
			}
			return total;
		}

		static inline int part02(std::vector<std::string> input)
		{
			const int LOSE = 'X';
			const int DRAW = 'Y';
			const int WIN = 'Z';
			int total = 0;
			for (size_t i = 0; i < input.size(); i++) {
				int opponentHand = input[i][0] - 'A';
				int playerHand = opponentHand;
				char result = input[i][2];

				switch (result) {
				case LOSE:
					playerHand = getWinningHandFromHand(opponentHand);
					total += (playerHand + 1);
					break;
				case DRAW:
					playerHand = opponentHand;
					total += (playerHand + 1 + 3);
					break;
				case WIN:
					playerHand = (opponentHand + 1) % 3;
					total += (playerHand + 1 + 6);
					break;
				}

			}
			return total;
		}

		static inline void run() {
			auto lines = readlines("./input/day02.txt");
			int part01 = day02::part01(lines);
			int part02 = day02::part02(lines);
			std::cout << "day02: " << part01 << ", " << part02 << std::endl;
		}
	}
}