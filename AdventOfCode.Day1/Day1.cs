using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AdventOfCode.Shared.Contracts;
using AdventOfCode.Shared.Helpers;

namespace AdventOfCode.Day1
{
    public class Day1 : IChallenge
    {
        public void Solve()
        {
            var input = FileHelper.ReadFileLines(Assembly.GetExecutingAssembly())
                .Select(x => Convert.ToInt32(x))
                .ToArray();

            Console.WriteLine($"Part 1: {Part1(input)}");
            Console.WriteLine($"Part 2: {Part2(input)}");
        }

        private static int Part1(IEnumerable<int> input)
        {
            var count = 0;
            var lastValue = int.MaxValue;

            foreach (var value in input)
            {
                if (value > lastValue)
                {
                    count++;
                }

                lastValue = value;
            }

            return count;
        }

        private static int Part2(IReadOnlyCollection<int> input) =>
            Part1(Enumerable.Range(0, input.Count - 2)
                .Select(i => input.Skip(i).Take(3))
                .Select(chunk => chunk.Sum()));
    }
}