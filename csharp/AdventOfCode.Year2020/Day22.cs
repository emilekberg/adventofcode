using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Common;

namespace AdventOfCode.Year2020
{
	public class Day22 : BaseDay<string, int>, IDay
	{
		public List<int> GetDeck(string input)
		{
			return input
				.Split("\n", StringSplitOptions.RemoveEmptyEntries)
				.Skip(1)
				.Select(int.Parse)
				.ToList();
		}
		public override int Part1(string input)
		{
			var split = input.Replace("\r", string.Empty).Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
			var deckPlayer1 = new Queue<int>(GetDeck(split[0]));
			var deckPlayer2 = new Queue<int>(GetDeck(split[1]));

			do
			{
				var playedP1 = deckPlayer1.Dequeue();
				var playedP2 = deckPlayer2.Dequeue();
				if(playedP1 > playedP2)
				{
					deckPlayer1.Enqueue(playedP1);
					deckPlayer1.Enqueue(playedP2);
				}
				else
				{
					deckPlayer2.Enqueue(playedP2);
					deckPlayer2.Enqueue(playedP1);
				}
				

			}
			while (deckPlayer1.Count > 0 && deckPlayer2.Count > 0);

			var toCount = deckPlayer1.Count > 0 ? deckPlayer1 : deckPlayer2;
			var amount = toCount
				.Reverse()
				.Select((card, index) => (card, index))
				.Aggregate(0, (acc, tuple) => acc + (tuple.card * (tuple.index + 1)));

			return amount;
		}

		public override int Part2(string input)
		{
			var split = input.Replace("\r", string.Empty).Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
			var deckPlayer1 = new Queue<int>(GetDeck(split[0]));
			var deckPlayer2 = new Queue<int>(GetDeck(split[1]));

			var winner = RecursiveCombat(deckPlayer1, deckPlayer2);
			var amount = winner
				.Reverse()
				.Select((card, index) => (card, index))
				.Aggregate(0, (acc, tuple) => acc + (tuple.card * (tuple.index + 1)));
			return amount;
		}

		public Queue<int> RecursiveCombat(Queue<int> deckPlayer1, Queue<int> deckPlayer2)
		{
			var previousRounds = new HashSet<string>();
			var round = 0;
			do
			{
				round++;
				// check if this round configuration has been played before, if so, the player wins.
				var roundHash = $"{string.Join(",", deckPlayer1)}#{string.Join(",", deckPlayer2)}";
				if(previousRounds.Contains(roundHash))
				{
					return deckPlayer1;
				}
				previousRounds.Add(roundHash);

				// gets the next cards to play.
				var playedP1 = deckPlayer1.Dequeue();
				var playedP2 = deckPlayer2.Dequeue();
				Queue<int> winner = null;
				// if both players have enough cards to play another recursive game.
				if(deckPlayer1.Count >= playedP1 && deckPlayer2.Count >= playedP2)
				{
					// copy the list for the new game, and take the amount of cards equal to the value played.
					var p1Copy = new Queue<int>(deckPlayer1.Take(playedP1));
					var p2Copy = new Queue<int>(deckPlayer2.Take(playedP2));
					var subWinner = RecursiveCombat(p1Copy, p2Copy);

					winner = subWinner == p1Copy ? deckPlayer1 : deckPlayer2;
				}
				else
				{
					// otherwise check score as normally.
					winner = playedP1 > playedP2 ? deckPlayer1 : deckPlayer2;
				}
				if (winner == deckPlayer1)
				{
					deckPlayer1.Enqueue(playedP1);
					deckPlayer1.Enqueue(playedP2);
				}
				else
				{
					deckPlayer2.Enqueue(playedP2);
					deckPlayer2.Enqueue(playedP1);
				}

				
			}
			while (deckPlayer1.Count > 0 && deckPlayer2.Count > 0);

			return deckPlayer1.Count > 0 ? deckPlayer1 : deckPlayer2;
		}
	}
}
