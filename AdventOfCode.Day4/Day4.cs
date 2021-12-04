using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AdventOfCode.Shared.Contracts;
using AdventOfCode.Shared.Helpers;

namespace AdventOfCode.Day4
{
    public class Day4 : IChallenge
    {
        public void Solve()
        {
            var (calledNumbers, bingoBoards) = ParseInput(
                FileHelper.ReadFileLines(Assembly.GetExecutingAssembly())
                    .ToList());

            Console.WriteLine($"Part 1: {Part1(calledNumbers, bingoBoards)}");

            // Reset bingo boards from part 1
            foreach (var bingoBoard in bingoBoards)
            {
                bingoBoard.Reset();
            }

            Console.WriteLine($"Part 2: {Part2(calledNumbers, bingoBoards)}");
        }

        private static int Part1(IEnumerable<int> calledNumbers, BingoBoard[] bingoBoards)
        {
            foreach (var calledNumber in calledNumbers)
            {
                var winningBoard = bingoBoards.FirstOrDefault(board => board.MarkNumberAndCheckForWin(calledNumber));

                if (winningBoard != null)
                {
                    return winningBoard.Numbers.SelectMany(x => x)
                        .Where(x => !x.Called)
                        .Sum(x => x.Value) * calledNumber;
                }
            }

            throw new Exception("Shouldn't have reached here");
        }

        private static int Part2(IEnumerable<int> calledNumbers, BingoBoard[] bingoBoards)
        {
            foreach (var calledNumber in calledNumbers)
            {
                foreach (var bingoBoard in bingoBoards)
                {
                    var winner = bingoBoard.MarkNumberAndCheckForWin(calledNumber);

                    // If all boards have won, we're the last board..
                    if (winner && bingoBoards.All(y => y.Won))
                    {
                        return bingoBoard.Numbers.SelectMany(x => x)
                            .Where(x => !x.Called)
                            .Sum(x => x.Value) * calledNumber;
                    }
                }
            }

            throw new Exception("Shouldn't have reached here");
        }

        // This is pretty ghetto but it works alright.
        private static (int[] calledNumbers, BingoBoard[] bingoBoards) ParseInput(IReadOnlyList<string> fileLines)
        {
            var calledNumbers = fileLines[0].Split(',').Select(x => Convert.ToInt32(x)).ToArray();

            List<BingoBoard> bingoBoards = new();
            var numberLines = new List<int[]>();
            foreach (var line in fileLines.Skip(2))
            {
                if (line.Trim().Length == 0)
                {
                    bingoBoards.Add(new BingoBoard(numberLines));
                    numberLines.Clear();
                    continue;
                }

                numberLines.Add(
                    line.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => Convert.ToInt32(x))
                        .ToArray());
            }

            bingoBoards.Add(new BingoBoard(numberLines));

            return (calledNumbers, bingoBoards.ToArray());
        }

        private class BingoBoard
        {
            public BingoNumber[][] Numbers { get; }
            public bool Won { get; private set; }

            public BingoBoard(IEnumerable<int[]> numbers)
            {
                Numbers = numbers
                    .Select(x => x.Select(y => new BingoNumber(y)).ToArray())
                    .ToArray();
            }

            public bool MarkNumberAndCheckForWin(int number)
            {
                var markedNumber = Numbers
                    .SelectMany(x => x)
                    .Any(x => x.Call(number));

                if (!markedNumber)
                {
                    return false;
                }

                // Horizontals
                if (Numbers.Any(x => x.All(y => y.Called)))
                {
                    Won = true;
                    return true;
                }

                // Verticals
                for (var i = 0; i < Numbers.First().Length; i++)
                {
                    if (Numbers.All(x => x[i].Called))
                    {
                        Won = true;
                        return true;
                    }
                }

                return false;
            }

            public void Reset()
            {
                Won = false;
                foreach (var number in Numbers.SelectMany(x => x))
                {
                    number.Reset();
                }
            }

            public class BingoNumber
            {
                public int Value { get; }
                public bool Called { get; private set; }

                public BingoNumber(int value)
                {
                    Value = value;
                }

                public bool Call(int number)
                {
                    if (Value != number) return false;
                    Called = true;
                    return true;
                }

                public void Reset()
                {
                    Called = false;
                }
            }
        }
    }
}