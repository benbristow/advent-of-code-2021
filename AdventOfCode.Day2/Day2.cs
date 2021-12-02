using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AdventOfCode.Shared.Contracts;
using AdventOfCode.Shared.Helpers;

namespace AdventOfCode.Day2
{
    public class Day2 : IChallenge
    {
        public void Solve()
        {
            var instructions = FileHelper.ReadFileLines(Assembly.GetExecutingAssembly())
                .Select(x => new Instruction(x))
                .ToArray();

            Console.WriteLine($"Part 1: {Part1(instructions)}");
            Console.WriteLine($"Part 2: {Part2(instructions)}");
        }

        private static int Part1(IEnumerable<Instruction> instructions)
        {
            var horizontal = 0;
            var depth = 0;

            foreach (var instruction in instructions)
            {
                switch (instruction.Direction)
                {
                    case Direction.Down:
                        depth += instruction.Units;
                        break;
                    case Direction.Forward:
                        horizontal += instruction.Units;
                        break;
                    case Direction.Up:
                        depth -= instruction.Units;
                        break;
                }
            }

            return horizontal * depth;
        }

        private static int Part2(IEnumerable<Instruction> instructions)
        {
            var horizontal = 0;
            var depth = 0;
            var aim = 0;

            foreach (var instruction in instructions)
            {
                switch (instruction.Direction)
                {
                    case Direction.Down:
                        aim += instruction.Units;
                        break;
                    case Direction.Forward:
                        horizontal += instruction.Units;
                        depth += (aim * instruction.Units);
                        break;
                    case Direction.Up:
                        aim -= instruction.Units;
                        break;
                }
            }

            return horizontal * depth;
        }

        private class Instruction
        {
            public Instruction(string inputLine)
            {
                var inputLineSplit = inputLine.Split(' ');
                Direction = Enum.Parse<Direction>(inputLineSplit[0].ToLower(), ignoreCase: true);
                Units = Convert.ToInt32(inputLineSplit[1]);
            }

            public Direction Direction { get; }
            public int Units { get; }
        }

        private enum Direction
        {
            Down,
            Forward,
            Up
        }
    }
}