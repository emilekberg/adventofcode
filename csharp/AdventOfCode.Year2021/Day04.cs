using AdventOfCode.Common;

namespace AdventOfCode.Year2021;
/// <Summary>
/// https://adventofcode.com/2021/day/3
/// </Summary> 
public class Day04 : BaseDay<string[], int>, IDay
{
	public (int[] numbersToDrawList, List<BingoBoard> boards) ParseInput(string[] input)
	{
		var queue = new Queue<string>(input);
		var numbersToDraw = queue.Dequeue().Split(',').Select(int.Parse).ToArray();

		var boards = new List<BingoBoard>();
		BingoBoard currentBoard = null;
		while (queue.TryDequeue(out string result))
		{
			if (string.IsNullOrWhiteSpace(result))
			{
				currentBoard = new BingoBoard(boards.Count + 1);
				boards.Add(currentBoard);
				continue;
			}

			var row = result.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
			currentBoard.AddRow(row);
		}
		return (numbersToDraw, boards);
	}
	public override int Part1(string[] input)
	{
		var (numbersToDraw, boards) = ParseInput(input);
		bool winFound = false;
		BingoBoard winningBoard = null;
		int winningNumber = 0;
		foreach(var number in numbersToDraw)
		{
			foreach(var board in boards)
			{
				var id = board.AddDrawnNumber(number);
				if (!id.HasValue) continue;
				var row = id.Value.row;
				var col = id.Value.col;
				winFound = board.CheckForWinWithHints(row, col);
				if(winFound)
				{
					winningBoard = board;
					winningNumber = number;
					break;
				}
			}
			if (winFound) break;
		}
		var score = winningBoard.GetSumOfUnmarkedNumbers();
		return score * winningNumber;
	}

	public override int Part2(string[] input)
	{
		var (numbersToDraw, boards) = ParseInput(input);
		bool winFound = false;
		BingoBoard winningBoard = null;
		int winningNumber = 0;
		List<(int number, BingoBoard board)> winningBoardDraws = new List<(int number, BingoBoard board)>();
		foreach (var number in numbersToDraw)
		{
			var boardsToRemove = new List<BingoBoard>();
			foreach (var board in boards)
			{
				if (boardsToRemove.Contains(board)) continue;
				var id = board.AddDrawnNumber(number);
				if (!id.HasValue) continue;
				var row = id.Value.row;
				var col = id.Value.col;
				winFound = board.CheckForWinWithHints(row, col);
				if (winFound) 
				{
					winningBoardDraws.Add((number, board));
					winningBoard = board;
					winningNumber = number;
					boardsToRemove.Add(winningBoard);
				}
			}
			if (boardsToRemove.Count > 0)
			{
				boards.RemoveAll(board => boardsToRemove.Contains(board));
				if (boards.Count is 0)
				{
					break;
				}
			}
		}
		var score = winningBoard.GetSumOfUnmarkedNumbers();
		return score * winningNumber;
	}
}


public class BingoBoard
{
	public int Id { get; set; }
	public Dictionary<int, (int row, int col)> NumberPositions = new();
	public Dictionary<(int row, int col), bool> NumberStatus = new();
	private int NumberOfRows = 0;
	public BingoBoard(int id)
	{
		Id = id;
	}
	public void AddRow(int[] numbers)
	{
		int row = NumberOfRows++;
		for(int col = 0; col < numbers.Length; col++)
		{
			var id = (row, col);
			var number = numbers[col];
			NumberPositions.Add(number, id);
			NumberStatus.Add(id, false);
		}
	}
	public (int row, int col)? AddDrawnNumber(int number)
	{
		if(!NumberPositions.TryGetValue(number, out var id))
		{
			return null;
		}
		NumberStatus[id] = true;
		return id;
	}
	public bool CheckForWinWithHints(int hintRow, int hintCol)
	{
		var result = true;
		for(int row = 0; row < NumberOfRows; row++)
		{
			result = result && PositionHadNumberDrawn(row, hintCol);
		}
		if(result)
		{
			return true;
		}
		result = true;
		for (int col = 0; col < NumberOfRows; col++)
		{
			result = result && PositionHadNumberDrawn(hintRow, col);
		}
		return result;
	}

	public bool PositionHadNumberDrawn(int row, int col)
	{
		var id = (row, col);
		var hadNumberDrawn = NumberStatus[id];
		return hadNumberDrawn;
	}
	public int GetSumOfUnmarkedNumbers()
	{
		var result = NumberPositions.Select(kvp =>
		{
			var id = kvp.Value;
			var number = kvp.Key;
			var isMarked = NumberStatus[id];
			if (isMarked) return 0;
			return number;
		}).Sum();

		return result;
	}

}