using AdventOfCode.Common;

namespace AdventOfCode.Year2021;
/// <Summary>
/// https://adventofcode.com/2021/day/4
/// </Summary> 
public class Day04 : BaseDay<string[], int>, IDay
{
	public override int Part1(string[] input)
	{
		var (numbersToDraw, boards) = ParseInput(input);
		var result = PlayBingoGame(numbersToDraw, boards);
		var (number, board) = result.First();
		return board.GetSumOfUnmarkedNumbers() * number;
	}

	public override int Part2(string[] input)
	{
		var (numbersToDraw, boards) = ParseInput(input);
		var result = PlayBingoGame(numbersToDraw, boards);
		var (number, board) = result.Last();
		return board.GetSumOfUnmarkedNumbers() * number;
	}
	public static (int[] numbersToDrawList, List<BingoBoard> boards) ParseInput(string[] input)
	{
		var queue = new Queue<string>(input);

		var numbersToDraw = input
			.Take(1)
			.Single()
			.Split(',')
			.Select(int.Parse)
			.ToArray();

		// converts input into a list of 2d arrays (bingo boards).
		var boardData = input
			.Where(x => !string.IsNullOrWhiteSpace(x))  // remove spacers and empty lines
			.Skip(1)                // skip the numbers to draw
			.Select(x => x			// convert numbers to string
				.Split(' ', StringSplitOptions.RemoveEmptyEntries)
				.Select(int.Parse)
				.ToArray()
			).Chunk(5)              // 5x5 in each bingo board
			.ToList();

		int i = 0;
		var boards = boardData.Select(board =>
		{
			var bingoBoard = new BingoBoard(++i);
			foreach (var row in board)
			{
				bingoBoard.AddRow(row);
			}
			return bingoBoard;
		}).ToList();
		return (numbersToDraw, boards);
	}
	public static List<(int number, BingoBoard board)> PlayBingoGame(int[] numbersToDraw, List<BingoBoard> boards)
	{
		var winningBoardDraws = new List<(int number, BingoBoard board)>();
		foreach (var number in numbersToDraw)
		{
			var boardsToRemove = new List<BingoBoard>();
			foreach (var board in boards)
			{
				if (boardsToRemove.Contains(board)) continue;
				var id = board.AddDrawnNumber(number);
				if (!id.HasValue) continue;
				var (row, col) = id.Value;
				if (board.HasBingoOnRowOrColumn(row, col))
				{
					winningBoardDraws.Add((number, board));
					boardsToRemove.Add(board);
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
		return winningBoardDraws;
	}
}


public class BingoBoard
{
	public int Id { get; set; }
	public Dictionary<int, (int row, int col)> NumberPositions = new();
	public Dictionary<(int row, int col), bool> NumbersMarked = new();
	private int NumberOfRows = 0;
	private int NumberOfColumns = 0;
	public BingoBoard(int id)
	{
		Id = id;
	}
	public void AddRow(int[] numbers)
	{
		int row = NumberOfRows++;
		NumberOfColumns = numbers.Length;
		for (int col = 0; col < numbers.Length; col++)
		{
			var id = (row, col);
			var number = numbers[col];
			NumberPositions.Add(number, id);
			NumbersMarked.Add(id, false);
		}
	}
	public (int row, int col)? AddDrawnNumber(int number)
	{
		if (!NumberPositions.TryGetValue(number, out var id))
		{
			return null;
		}
		NumbersMarked[id] = true;
		return id;
	}
	public bool HasBingoOnRowOrColumn(int hintRow, int hintCol)
	{
		var markedColumns = 0;
		var markedRows = 0;
		for (int i = 0; i < NumberOfRows; i++)
		{
			markedRows += PositionHadNumberDrawn(i, hintCol) ? 1 : 0;
			markedColumns += PositionHadNumberDrawn(hintRow, i) ? 1 : 0;
		}
		return (markedRows == NumberOfRows || markedColumns == NumberOfColumns);
	}

	public bool PositionHadNumberDrawn(int row, int col) => NumbersMarked[(row, col)];
	public int GetSumOfUnmarkedNumbers()
	{
		var result = NumberPositions.Sum(kvp =>
		{
			var id = kvp.Value;
			var number = kvp.Key;
			var isMarked = NumbersMarked[id];
			if (isMarked) return 0;
			return number;
		});

		return result;
	}

}