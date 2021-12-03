using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AdventOfCode.Shared.Contracts;
using AdventOfCode.Shared.Helpers;

namespace AdventOfCode.Day3
{
    public class Day3 : IChallenge
    {
        public void Solve()
        {
            var values = FileHelper.ReadFileLines(Assembly.GetExecutingAssembly())
                .Select(x => x.Select(y => (int)char.GetNumericValue(y)).ToArray())
                .ToArray();

            Console.WriteLine($"Part 1: {Part1(values)}");
            Console.WriteLine($"Part 2: {Part2(values)}");
        }

        private static int Part1(IReadOnlyList<int[]> values)
        {
            var gammaBinaryString = string.Empty;
            var epsilonBinaryString = string.Empty;

            for (var position = 0; position < values[0].Length; position++)
            {
                var zeroCount = values.Count(x => x[position] == 0);
                var oneCount = values.Count - zeroCount;

                gammaBinaryString += zeroCount > oneCount ? "0" : "1";
                epsilonBinaryString += zeroCount < oneCount ? "0" : "1";
            }

            return BinaryHelper.BinaryStringToNumber(gammaBinaryString) *
                   BinaryHelper.BinaryStringToNumber(epsilonBinaryString);
        }

        private static int Part2(IReadOnlyList<int[]> values)
        {
            var carbonDioxideScrubberRating = FindRatingValue(RatingType.CarbonDioxideScrubber, values);
            var oxygenGeneratorRating = FindRatingValue(RatingType.OxygenGenerator, values);

            return carbonDioxideScrubberRating * oxygenGeneratorRating;
        }

        private static int FindRatingValue(RatingType ratingType, IEnumerable<int[]> values)
        {
            var valueMemory = values.ToList();
            var position = 0;

            while (valueMemory.Count != 1)
            {
                var zeroCount = valueMemory.Count(x => x[position] == 0);
                var oneCount = valueMemory.Count - zeroCount;

                valueMemory.RemoveAll(x => ratingType switch
                {
                    RatingType.CarbonDioxideScrubber => x[position] == (zeroCount <= oneCount ? 1 : 0),
                    RatingType.OxygenGenerator => x[position] == (zeroCount <= oneCount ? 0 : 1),
                    _ => throw new ArgumentOutOfRangeException(nameof(ratingType), ratingType, null)
                });

                position++;
            }

            return BinaryHelper.BinaryStringToNumber(string.Join(string.Empty, valueMemory.Single()));
        }

        private enum RatingType
        {
            CarbonDioxideScrubber,
            OxygenGenerator,
        }
    }
}