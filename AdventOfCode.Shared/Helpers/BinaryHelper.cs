using System;

namespace AdventOfCode.Shared.Helpers
{
    public static class BinaryHelper
    {
        public static int BinaryStringToNumber(string binaryString) => Convert.ToInt32(binaryString, 2);
    }
}