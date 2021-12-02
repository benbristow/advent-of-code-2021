using System;
using AdventOfCode.Shared.Contracts;

namespace AdventOfCode.Main
{
    public static class Program
    {
        // TODO: Probably make this a bit nicer
        private static readonly IChallenge[] Challenges =
        {
            new Day1.Day1(),
            new Day2.Day2()
        };

        public static void Main()
        {
            foreach (var challenge in Challenges)
            {
                Console.WriteLine($"Solving challenge for {challenge.GetType().Name}");
                challenge.Solve();
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}